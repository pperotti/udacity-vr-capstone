# OVERVIEW
This project contains the material for the VR Udacity's Capstone. In this opportunity, the project is an Air Hockey game built from scratch so users of the game can play with their friends online. 

# INTRODUCTION
This AirHockey edition let 2 friends play together online which means that one of the players will HOST the game and the other one will join such instance of the game. 

The game offered is pretty simple, it is composed by 3 scenes: welcome, main  and credits. All the game action happen in the main scene where the player can choose to enter the IP of the device hosting a game or host a game waiting for someone to join.

The game is set so the first player scoring 7 goals wins the game. At this point the game is completed. 

One goal I wanted to achive with how the project is structured was supporting multiple platforms with the same code base and for this submittion I will be delivering the scenes for MAC and GearVR platforms. 

## HOW THE GAME WAS DEVELOPED TO SUPPORT SUCH GOAL?
The game was developed modeling all the game main assets as Prefabs so we have the following main prefabs used to achieve this game: 
- Table: Represents where the game will occur. 
- Player: Represents how the player will be shown in the screen. Here a player is just the paddle he or she uses to play the game. 
- Scores: This prefab displays the game scores over networking. 
- Puck: As its name describes, this prefab is the small disk used in the table to play the game.

It is important to mention prefabs fulfill small pieces of the whole game and it is up to the platform's scene to use them in the way it is most useful for that platform. 

## BEYOND THE PREFABS. IT'S ALL ABOUT THE LOGIC
The prefabs described in the previous section does not make the game works by just dropping them into a platform scene. The key elements in the game is its logic and that is modeled via all the scripts. The scripts that coordinates all the game is named GameLogic.cs and this is placed in a GameController object in both delivered scene (GearVRAirHockey & MacAirHockey).

# MATERIAL DELIVERED
The elements delivered are: 
- Code (shared in this repo)
- The following video that explains the project and presents how this works per platform:
https://youtu.be/WvZFeCoooSA

- This video explains about achievements: https://youtu.be/494Q_UBWHC8


# HARDWARE REQUIREMENTS

## For GearVR
Any GearVR compatible devices listed here. Galaxy S9, S9+, Note8, S8, S8+, S7, S7 edge, Note5, S6 edge+, S6, S6 edge, A8, A8+
GearVR Headset
GearVR Controller is required.

## For MAC.
Mac computer.

# CODE

## PROJECT URL
https://github.com/pperotti/udacity-vr-capstone

## HOW TO RUN THE PROJECT FOR GEARVR
In order to run the project in a Samsung device for the GearVR platform you have first to generate a signature for your device so you can run the app in a custom device. This is explained here: 

https://dashboard.oculus.com/tools/osig-generator/

NOTE: It is important mentioning that you need to login to Facebook to be able to create a new signature file for your device.  

After creating and downloading the signature you need to place it in <project>/Plugins/Android/Assets. Once this is completed you can now run the project in your device.

Now, as final step you need to make sure you are running the correct scenes. To do this go to File -> Build Settings and make sure you have the following scenes added in the shown order.
1) Scenes/GearVrWelcome
2) Scenes/GearVrAirHockey
3) Scenes/GearVrCredits

You can now build the project with your Samsung device connected. 

## HOW TO RUN THE PROJECT FOR MAC
To run the project for MAC you have to open the scene 'Scenes/MacAirHockey' and then setting it as enabled in File -> Build Settings.

# FUTURE FEATURES
Every project usually has room for improvement and this is not the exception. THe identified improvements I identified are the following: 
- Add support for more AR platforms.
- Add support for AI agents so the game can be play without other players.
- Improve networking so user does not have to know about IPs but only rooms. Each room should be created by those challenging other players.
- Integrate the game into any multi-player platform where each user can gain 'coins' or similar mechanism each time a game starts. 
- Add the feeling the game happens inside a stadium for the VR experience. When the AR experience is created, the table will only be displayed on top of the proyected area, there is no need here for supporting this immersive experience. 
- Allow people to connect to the game as "viewers". This person will be located at a random spot with the audience. 

# SCENES TO REVIEW
For the capstone, only 4 scenes are provided:
- Scenes/GearVRWelcome: 
- Scenes/GEarVRCredits:
- Scenes/GearVRAirHockey
- Scenes/MacAirHockey

# ACHIEVEMENTS
The following achievements were completed per category:
## Fundamentals
- Scale 
- Lightning 
- Locomotion
- Physics
- Empathy

## Completeness
- Gamification

## Challenges
- Multiplayer

# LICENSES
Sound effects obtained from https://www.zapsplat.com
Additional sound effects from https://www.zapsplat.com
Music from https://www.zapsplat.com

# ABOUT THE CONTENTS
All the contents was create with the idea of creating something that entertain the user and produce the idea of joy.

# AUTHOR
All the material (recording city tour, edited video and code) was created by Pablo Perotti (pablo.perotti@gmail.com).