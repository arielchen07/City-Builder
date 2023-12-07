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

1. James Rhule, jamesrhule@projecthumancity.com, Project: Human City organization leader, primary contact  

2. The individuals listed below are members of the organization with whom we may collaborate in the later stages of development, and we communicate with them through Slack:  
	- Cheng-Ming Hsu, Spotstitch Ar Camera Lead  
	- Dushyant Mehul Lunechiya, Frontend Product lead  
	- Anupama Kadambi, Backend development Lead  
	- Ali Hassan Amin, Spotstitch Frontend developer  


3. Project: Human City is a non-profit organization who try to design and create an open ecosystem. They initiate ideas for global impact, focusing on addressing human inequality, social injustice, and access to basic needs.

## Description of the project
City building game is a web-based creative city-building game that aims to connect users closer to the real world. Users obtain inventory resources to build their city by scanning objects (ex: cars, buildings, infrastructures, etc.) in the real world, using the AR camera in the Spotstich mobile app (Spotstich mobile app is a multi-purpose social app developed by Project: Human City).  


Our product could potentially revolutionize how users engage their physical environment with virtual games. It will encourage people to explore the local environment and surrounding objects, capture and record them with a vivid 3D model instead of a 2D picture. For real life urban planners, this product might improve their efficiency of initializing city planning and inspire them with creative and effective design.  


With this interactive game developed, we hope to enhance users’ real world exploration and social interaction which could yield significant positive social impact. This also aligns with the mission of our partner, Project: Human City. As a non-profit organization, Project: Human City aims to enhance the lives of city residents on and off the app with various initiatives and projects. We together want to create a difference in people’s lives.  
​
## Key Features
City building game (Web game):  
-  Login/Signup and Logout of the game  
	-  New users can sign up for their account with name, email and password
	-  Existing users will be able to login to the game with their email and password
	-  Users can log out of the game when they want to exit
-  Start with a randomly generated empty map  
	- users will have different maps of a fixed size (10x10 tiles), generated from a perlin noise. The map will include grassland, water bodies, trees, etc. The user can start their city at the center and expand outwards.
- Design your city  
	- Add objects to the map from your inventory
	- Delete objects from the map and store them back into your inventory
	- Select objects on the game map and move their position and change their orientation, user can also deselect the item
- Save and load game map  
	- Users' previous progress on the game map will be saved when they log out from the game, The saved game map with previous progress will be loaded the next time they login
	- The save and load is done automatically
- Gain resources by collecting them inside the game and in the real world (Future development)
	- Things like wood can be gathered in game by clearing out forests, and can also be obtained in real life by scanning a tree. Other materials include ores, cement, stone, sand, and clay. These materials are all necessary for constructing buildings.
- Build different types of buildings and develop your city (Future development)
	- There are various building types, such as housing, utilities, production etc. Each type serves a key purpose in the expansion of the city. Each building also has different requirements to build.

 
# Instructions

## How to run server  
The current application host remote server on Render. If you choose to use the remote server, you can skip to the “start the game” section.  
Note that the server spins down if it did not receive any request for 15 minutes. It could take several minutes or more for the service to spin back up.  
If you meet any problem connecting to the remote server, please contact Project: Human City organization.

On the other hand, a user can choose to start a local server for the backend. To do this: 
- First, 'cd' into the Server directory by using terminal
- Then run 'npm install' and wait for the Node Package Manager to complete the download.
- run 'npm -version' to ensure it is 10.1.0 or above.  
- After ensuring that all the environments are fully installed, run 'npm start'.  
- When the terminal displays 'Server is running on port 3000, Database Connection Established!', which indicates the Server is running.  
- If the output indicates an 'app crash', this suggests that the environment configuration is incomplete. To address this, one should refer to the 'development requirements' section of the documentation to verify the comprehensive fulfillment of all prerequisites.  



## Start the Game:
- Please navigate to the `Builds` folder and choose the specific build you'd like to play with.
  
You can find our demo [here](https://youtu.be/tul3IDK8kkM) to learn the current UI features to play.

Here are all the keyboard and mouse features that you should know:  
1. WASD for moving the map  
2. esc for cancel selection of a building  
3. mouse scrolling for map zoom in/out  


## Licenses
We are using Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International Public License by the requirement of our partner. Anyone is free to share and adapt materials of this code base given that they do the following:

- give appropriate credit, provide a link to the license, and indicate if changes were made
- do not use the codebase for commercial purposes
- use the same license as this codebase, when you share and adapt the material
- do not restrict others from doing anything this license permits	
