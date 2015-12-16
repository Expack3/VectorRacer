# VectorRacer
A simple 3D action game built with XNA built for my 3D Game Development class at Davenport University.
## Requirements
* The [XNA Redistribute](http://www.microsoft.com/en-us/download/details.aspx?id=20914)
* Visual Studio 2010
* NVIDIA GeForce GTX 580 (unoptimized code ahoy!)
* My [XNA Helper Library](https://github.com/Expack3/XNA_HelperLibrary) - you'll need to adjust the reference in order to compile the application.

## Controls for Gameplay
###Keyboard
Up/Down/Left/Right Arrows: Move Forward/Backward/Left/Right
Enter Key - destroys obstacle if stuck on one

###XBOX360 (or compatibles)
D-Pad Up/Down/Left/Right: Move Forward/Backward/Left/Right
Right Thumbstick Button - destroys obstacle if stuck on one

##Gameplay Objects
Shields (intangible) - destroys obstacle or enemy on contact without time penalty.

Spheres - shield powerup, adds shield to toal number of shields.
O's - Obstacle, freezes player unless Enter/Right Thumbstick Button pressed or shield possessed.
Triangle - Enemy, will subtract time every frame when in contact with player unless shield possessed.
