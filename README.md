# MetaQuest_GestureRecognition
 
## Overview

This project implements hand gesture recognition using a MetaQuest3 using unity. The primary objective was to explore the possibilities given in a timeframe of 14 weeks. The end result is the recognition of a closed fist, and of an opened palm with every finger touching each others, available for the right and the left hand. 

## File organization 

This project is organized into the following folders:

### Behaviour_Scripts 

All the scripts in this folder are examples of how game objects of a scene can subscribe and react to the recognition of hand gestures. By subscribing to precise events thrown form the scripts in "Recognition_Script" folder, the behaviour change of these objects is simple and can for example change color or move around.

### Recognition_Scripts 

All the scripts in this folder are scripts used to recognize the gestures. 

- PoseDetectorSingle.cs

This script will raise the following events : FistClosedRight, FistNotClosedRight, FistClosedLeft, FistNotClosedLeft, PalmOpenedRight, PalmNotOpenedRight, PalmOpenedLeft, PalmNotOpenedLeft. 

- PoseDetectorDouble.cs

This script will raise the following events : FistBump, FistNoBump, PalmJoin, PalmNoJoin.

- RotationDetectorSingle.cs 

This script will raise the following events : palmUp_Fist, palmDown_Fist, palmNormal_Fist, Action palmUp_Palm, palmDown_Palm, palmNormal_Palm.

These 3 scripts use the various scripts that can be found in the subfolder named "Necessary_toRecognition".

#### Necessary_toRecognition 

This folder holds four scripts named : FistBump.cs, FistClosed.cs, PalmJoin.cs, PalmStraight.cs.

These 4 scripts have the respective methods used to calculate hand bone position or distances used to recognize hand gestures.

## Usage 

To be able to recognize a gesture :

- Attach the appropriate script raising the corresponding event to the "OVRhand prefab" in your scene. 

Example : To recognize a closed fist of the right hand you need the "PoseDetectorSingle.cs" on the right OVRhand prefab, because this script raises "FistClosedRight" and "FistNotClosedRight". 

- Attach the corresponding "Necessary_toRecognition" script to the same OVRhand prefab in your scene. 

Example : To recognize a closed fist of the right hand you need the "FistClosed.cs" on the right OVRhand prefab, because this script holds the function processing the hand to detect when the fist is closed. 

At this point when the scene is run in the Metaquest3, the event "FistClosedRight" is raised when the right fist is detected as closed and "FistNotClosedRight" is raised when the right fist is not anymore detected as closed. 

- In the script you need to react when the right fist is closed. Subscribe your custom function to the "FistClosedRight" event, and it will be executed once.

Example : the script "fistColorChangeRight.cs", the "Start()" method retrieves the component poseDetector attached to the right hand, and subscribes the "OnFistClosedRight()" method that changes the color of the gameobject of which this script is attached to.
