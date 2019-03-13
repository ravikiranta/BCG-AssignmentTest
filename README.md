# BCG-AssignmentTest

Here are the instructions:

1. Use a textured surface for detection to work well.
2. Once it is detected you will see a virtual surface being created on your screen. 
3. Now press the arrow at the bottom to open the augment object menu
4. Select an object from the menu and click on the virtual surface to place it. Undetected surfaces will not let you place the object on it.
5. Clicking on an already placed object will select it and change its material to transparent. Clicking again on it will unselect the object.
6. When object is selected, press on a different point on the surface to move the selected object.
7. Zoom action will scale the object up or down. (I implemented this instead of camera zoom as you cannot zoom camera when using ARCore)
8. Swipe action will rotate the object.

This App uses ARCore and will run only on supported Android devices.