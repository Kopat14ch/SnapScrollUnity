🌀 SnapScrollBehaviour for Unity
SnapScrollBehaviour is a Unity component that extends the functionality of a ScrollRect by adding smooth snapping and swipe-based navigation between child elements. Perfect for scrollable menus, carousels, or paginated UIs. 🎮

✨ Features
Smooth snapping: Ensures that the scroll view lands precisely on the closest child element.
Swipe navigation: Navigate between elements using swipe gestures with a customizable threshold.
Customizable behavior: Adjust snapping speed, sensitivity, and thresholds for optimal user experience.
Lightweight and easy to integrate with existing ScrollRect setups.
📜 Usage
Add the Component
Attach the SnapScrollBehaviour script to a GameObject that already has a ScrollRect component.

Customize Settings
In the Unity Inspector, configure the following parameters:

_snapThreshold: The tolerance for stopping the snapping motion (default: 0.05).
_snapSpeed: Speed of snapping (default: 10f).
_swipeThreshold: Minimum swipe velocity to trigger navigation (default: 1000f).
Structure Your ScrollRect

Ensure that the ScrollRect has a Content child containing the scrollable elements.
Each child should have a RectTransform for proper snapping.
Optional Tweaks

Adjust child spacing and anchoring to ensure smooth snapping behavior.
