# Game Engine/ Code Connoisseurs

> Welcome to the City Builder Game developed by team Code Connoisseurs!
> 
> If you are a player, please refer to this README for user instructions.  
> Developers continuing work on the project should check out the README and files in the [`Developer-Documentations`](Developer-Documentations) directory for more detailed development information.  

### Table of contents
#### [Partner Intro](#partner-intro)
#### [Description of the project](#description-of-the-project)
#### [Key Features](#key-features)
#### [Instructions](#instructions)
#### [Licenses](#licenses)


## Partner Intro
**James Rhule**  
- Email: jamesrhule@projecthumancity.com
- Role: Project: Human City organization leader
- Primary contact

**Project: Human City**  
Non-Profit Organization
- Focus on human inequality, social injustice and basic needs
- Initiatives for a more inclusive and equitable world

Their Projects
- Various projects undergoing development simultaneously
- Focused on providing social platforms for people
- Encourages social interaction and city exploration


## Description of the project
City building game is a web-based creative city-building game that aims to connect users closer to the real world. Users obtain inventory resources to build their city by scanning objects (ex: cars, buildings, infrastructures, etc.) in the real world, using the AR camera in the Spotstich mobile app (Spotstich mobile app is a multi-purpose social app developed by Project: Human City).  


Our product could potentially revolutionize how users engage their physical environment with virtual games. It will encourage people to explore the local environment and surrounding objects, capture and record them with a vivid 3D model instead of a 2D picture. For real life urban planners, this product might improve their efficiency of initializing city planning and inspire them with creative and effective design.  


With this interactive game developed, we hope to enhance users’ real world exploration and social interaction which could yield significant positive social impact. This also aligns with the mission of our partner, Project: Human City. As a non-profit organization, Project: Human City aims to enhance the lives of city residents on and off the app with various initiatives and projects. We together want to create a difference in people’s lives.  
​

## Key Features (User Stories Implemented)
Game play features:  
-  Generate new map  
	- As a first time user of the game, a random base map of a fixed size (50x50 tiles), will be generated for user to start building their city at the center and expand outwards. The map will include grassland, water bodies, trees, rocks, etc. at random locations.
- Placement system
	- As a user of the game, they can perform the following manipulations to the invenotry objects to build their city:
		- Add objects to the map from user's inventory
		- Delete objects from the map and store them back into user's inventory
		- Select objects on the game map and move their position and change their orientation
		- Cancel seletction of an object on the map
- Utility System
	- As a user of the game, they can view the utility requirements of house objects and grow population by satsfying the requirements
- Pollution System
	- Users are able to view amount of pollution in their world and maintain clean environment to prevent people from moving away
- Resources gathering inside the game world
	- Users are able to view and gather resource in game map by clearing out forests and rocks. These materials are all necessary for constructing buildings. The map will randomly spwan new resources.

Supporting features:
-  Login/Signup and Logout of the game  
	- As a new user of the game, they will be able to sign up for their account with name, email and password, and enter the game after sign up
	- As an existing user of the game, they will be able to login to the game with their email and password
	- As a user playing the game, they can log out of the game when they want to exit
- Save, load, and update game map and inventory information
	- Users' previous progress in the game map will be saved when they log out from the game. The previous progress will be loaded the next time they login
	- The save and load is done automatically. Game map saves to server every few seconds
	- Updates in inventory object and resources are send to server when the update takes place
 
# Instructions

## Server of the game 
The current application host remote server on Render. If you choose to use the remote server, you can skip to the [Start the Game](#start-the-game) section.  
Note that the server spins down if it did not receive any request for 15 minutes. It could take several minutes or more for the service to spin back up.  
If you meet any problem connecting to the remote server, please contact Project: Human City organization.

On the other hand, you can choose to start a local server for the backend. To do this: 
- First, 'cd' into the Server directory by using terminal
- Then run 'npm install' and wait for the Node Package Manager to complete the download.
- run 'npm -version' to ensure it is 10.1.0 or above.  
- After ensuring that all the environments are fully installed, run 'npm start'.  
- When the terminal displays 'Server is running on port 3000, Database Connection Established!', which indicates the Server is running.  
- If the output indicates an 'app crash', this suggests that the environment configuration is incomplete. To address this, one should refer to the 'development requirements' section of the documentation to verify the comprehensive fulfillment of all prerequisites.  



## Start the Game:
- Please navigate to the `Builds` folder and download the specific build you'd like to play with.
  
You can find our demo [here](https://youtu.be/tul3IDK8kkM) to learn how to play the game for the current version.

Here are all the keyboard and mouse features that you will need for playing:  
1. Press keys ```W``` ```A``` ```S``` ```D``` for moving the map  
2. Press ```esc``` for cancel selection of a building  
3. mouse scrolling for map zoom in/out  


## Licenses
We are using Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International Public License by the requirement of our partner. Anyone is free to share and adapt materials of this code base given that they do the following:

- give appropriate credit, provide a link to the license, and indicate if changes were made
- do not use the codebase for commercial purposes
- use the same license as this codebase, when you share and adapt the material
- do not restrict others from doing anything this license permits

Shield: [![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg
