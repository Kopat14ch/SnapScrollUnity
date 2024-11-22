using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Scroll.Behaviours
{
    [RequireComponent(typeof(ScrollRect))]
    public class SnapScrollBehaviour : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private float _snapThreshold = 0.05f;
        [SerializeField] public float _snapSpeed = 10f;
        [SerializeField] public float _swipeThreshold = 1000f;
        
        private ScrollRect _scrollRect;
        private Coroutine _snappingCoroutine;
        private int _currentIndex;

        void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_snappingCoroutine != null)
                StopCoroutine(_snappingCoroutine);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(_scrollRect.velocity.x) >= _swipeThreshold)
            {
                int tempIndex = _currentIndex;
                
                if (_scrollRect.velocity.x > 0)
                    tempIndex--;
                else
                    tempIndex++;
                
                tempIndex = Mathf.Clamp(tempIndex, 0, _scrollRect.content.childCount - 1);

                if (_currentIndex == tempIndex)
                    return;

                _currentIndex = tempIndex;
                
                Vector2 childPosition = _scrollRect.content.GetChild(_currentIndex).GetComponent<RectTransform>().anchoredPosition;
                
                _snappingCoroutine = StartCoroutine(Snapping(childPosition));
            }
            else
            {
                _snappingCoroutine = StartCoroutine(Snapping(GetClosestPosition()));
            }
        }

        private IEnumerator Snapping(Vector2 targetPosition)
        {
            targetPosition.y = _scrollRect.content.anchoredPosition.y;
            targetPosition.x = -Mathf.Abs(targetPosition.x);
            
            while (Mathf.Abs(_scrollRect.content.anchoredPosition.x - targetPosition.x) > _snapThreshold)
            {
                Vector2 newPosition = _scrollRect.content.anchoredPosition;
                newPosition.x = Mathf.Lerp(newPosition.x, targetPosition.x, Time.deltaTime * _snapSpeed);
                
                _scrollRect.content.anchoredPosition = newPosition;
                
                yield return null;
            }
            
            _scrollRect.content.anchoredPosition = targetPosition;
            _snappingCoroutine = null;
        }

        private Vector2 GetClosestPosition()
        {
            Vector2 targetPosition = Vector2.zero;

            float closetDistance = float.MaxValue;
            int tempIndex = 0;

            foreach (RectTransform contentRectTransform in _scrollRect.content)
            {
                float distance = Mathf.Abs(Mathf.Abs(_scrollRect.content.anchoredPosition.x) - contentRectTransform.anchoredPosition.x);
                
                if (distance < closetDistance)
                {
                    _currentIndex = tempIndex;
                    closetDistance = distance;
                    targetPosition = contentRectTransform.anchoredPosition;
                }
                
                tempIndex++;
            }

            return targetPosition;
        }
    }
}
