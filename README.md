# Unity-DragonLib
A library of handy C# scripts to use in Unity.
Feel free to use, adapt and learn from these however you like.

## Overview

Here's a quick overview of the scripts.

### Add At Start Of Project

**TakeScreenshotAtStart.cs**: Add to a GameObject in a scene you'd like to get screenshots of. The script automatically creates a screenshot after x seconds and puts t in a "Screenshots" folder. Useful for looking back and seeing your progress and to have some screenshots ready to share with your peers.  
  
### Audio

**OneShotAudioWithPitch.cs**: Add to a prefab with an Audio Source to easily change the pitch of the played audio.  

## Camera  

**CameraBackgroundColorChanger.cs**: Transitions the camera background color between 2 chosen colors. 
**VisibleOrNot.cs**: Invoke an event/UnityEvent when the GameObject this is attached to is visible to the camera. Only triggers if the object is actually visible and not fully blocked by other objects (with colliders) by using linecasts.

## Editor  

**CreateFolders.cs**: This adds a menu to the editor under "Assets/Create Default Folders" that creates a bunch of asset folders luke "Scripts", "Materials", etc.  
**DeleteMyPlayerPrefs.cs**: Adds a menu to the editor under "Tools/DeleteMyPlayerPrefs" that deletes all player prefs for testing purposes.  
**MoveWithArrowKeys.cs**: Add to a GameObject in the scene to be able to move any selected GameObject using the arrow keys.

## Extensions  

**ExtensionMethods.cs**: A bunch of handy extension methods for strings, Vector3, Color, etc. Take a look at the script to see what's included.

## GameObject State  

## Gradient  

## Import SVG  

## LoadingAndSaving   

## MultiBuild  

## Physics  

## Textures  

## Transform  

## UGUI  

## Web  
