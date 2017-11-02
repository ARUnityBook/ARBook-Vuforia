# Augmented Reality for Developers [book] Projects

This repository contains Unity project versions of the book for the following platforms and SDK:

* Vuforia 6.5
* Unity 2017.2

For other SDK implementations, check other Branches of this repository and visit https://github.com/ARUnityBook

## AR Projects

The following book's AR projects are included as separate Unity scenes:

* Chapter 4 - Augmented Business Cards: Drone
* Chapter 5 - AR Solar System
* Chapter 6 - How to Change a Flat Tire
* Chapter 7 - Augmenting the Instruction Manual
* Chapter 8 - Room Decoration with AR: Photo Frames
* Chapter 9 - Poke the Ball Game


## About the Book

*Augmented Reality for Developers*

Build exciting AR applications on mobile and wearable devices with Unity 3D, Vuforia, ARToolKit, Microsoft Mixed Reality HoloLens, Apple ARKit, and Google ARCore

by Jonathan Linowes, Krystian Babilinski

Available at:

* [PacktPub](https://www.packtpub.com/web-development/augmented-reality-developers)
* [Amazon](https://www.amazon.com/Augmented-Reality-Developers-Jonathan-Linowes/dp/1787286436)

## How This Implementation Differs from the Book

The implementation in this repository/branch is with Unity 2017.2 with integrated Vuforia 6.5. The primary difference between this and the instructions given in the book (Unity 2017.1 + Vuforia 6.2) is the prefabs are now replaced with integrated GameObjects.

### Chapter 2

*Pg 42-44: Installing Unity: Download and Install*

Install Unity 2017.2 or later, including the Vuforia components. Be sure to check the "Vuforia Augmented Reality Support" in Download Assistant.

*Pg 60: Using Camaeras in AR* 

The Vuforia "AR Camera" is not a prefab, it is now a standard GameObject that can be inserted into the scene via main menu **GameObject | Vuforia | AR Camera** (or Hierarchy Create menu).

*Pg 63-68: Getting and using Vuforia*

Vuforia is not a separate asset package. Install Vuforia via Download Assistant. Then, 

1. Go into **Player Settings** (Edit | Project Settings | Player)
2. In Inspector, in the XR Settings section, check the **Vuforia Augmented Reality Supported** checkbox
3. When you attempt to insert a Vuforia game object into a scene, for example **GameObject | Vuforia | AR Camera** you will be prompted to "Import Vuforia Assets". Click the **Import** button to add them to your project.

You will now have the following new folders and files in your Assets:

* Editor/Vuforia
* Resources/VuforiaConfiguration (file)
* StreamingAssets/Vuforia
* Vuforia/

*Pg 73: Adding AR Camera to the Scene* 

To replace the default Main Camera with a Vuforia AR Camera:

1. In the Hierarchy panel, delete the Main Camera
2. From main menu, select **GameObject | Vuforia | AR Camera**

There is no need to add the **Camera Settings** component.

*Pg 74: Adding a target image*

If the sample images are no longer available at the Vuforia.com link indicated in the chapter, try the Downloads Samples page (https://developer.vuforia.com/downloads/samples). You can also find them in the book's GitHub (https://github.com/ARUnityBook/ARBook-Vuforia/tree/master/Assets/Editor/Vuforia/ForPrint)

Alternatively you can go off the rails a little, download and play with the current Core Features samples from the Unity Asset Store (Mars assets)
https://assetstore.unity.com/packages/templates/packs/vuforia-core-samples-99026

To add ImageTarget to your scene, instead of a prefab, use **GameObject | Vuforia | Image**

### Chapter 3

No changes.

### Chapter 4

*Pg. 167: Setting up the project (Vuforia)*

To create a new project using Vuforia:

1. Open Unity 2017.2 (or later), create a new 3D project
2. In **Player Settings** (Edit | Projects Settings | Player) check the **Vuforia Augmented Reality Supported** checkbox
3. In your browser, go to Vuforia website Developer Portal's License Manager (https://developer.vuforia.com/targetmanager/licenseManager/licenseListing, login required), create or choose a license key to open it, and copy the key codes from the textbox on the screen
4. Back in Unity, from main menu choose Windows | Vuforia Configuration 
5. In the Inspector, paste your license key into the **App License Key** area
6. Save the scene (File | Save Scenes), and save the project (File | Save Project)

To add the AR camera to the scene,

1. Delete the Main Camera object from the Hierarchy
2. From main menu select **GameObject | Vuforia | AR Camera**
3. Save the scene

*Pg. 168: Adding the ImageTarget to the scene*

1. From main menu, select **GameObject | Vuforia | Image**

*Pg. 172: Enable extended tracking or not?*

The **Extended Tracking** option is now located under **Advanced** options in the Image Target Behavior component.



### Chapter 5

*Pg. 219: Creating our initial project*

See steps above (Chapter 4)

*Pg. 221: Using a marker target*

If the sample images are no longer available at the Vuforia.com link indicated in the chapter, try the Downloads Samples page (https://developer.vuforia.com/downloads/samples). You can also find them in the book's GitHub (https://github.com/ARUnityBook/ARBook-Vuforia/tree/master/Assets/Editor/Vuforia/ForPrint)

Find the configuration settings via **Window | Vuforia Configuration**



### Chapter 6

No changes.


### Chapter 7

*Pg. 331: Setting up the project for AR with Vuforia*

See steps above (Chapter 4)

*Pg. 338: Adding a user-defined target builder*

1. From main menu, select **GameObject | Vuforia | Camera Image | Camera Image Builder**

*Pg 339: Adding an image target*

1. From main menu, select **GameObject | Vuforia | Camera Image | Camera Image Target**



### Chapter 8

*Pg. 450: Set up project and scene for Vuforia"

See steps above (Chapter 4)

*Pg. 451: Set the image target*

1. From main menu. select **GameObject | Vuforia | Image**



### Chapter 9

SmartTerrain is not yet integrated into Unity 2017.2 built-in Vuforia support. Until it becomes available (Unity 2017.3 ?) we recommend you use Vuforia 6.2 version components.