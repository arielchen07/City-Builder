# Game Engine/ Code Connoisseurs
Note: This document is intended to be relatively short. Be concise and precise. Assume the reader has no prior knowledge of your application and is non-technical.​
## Partner Intro
James Rhule, jamesrhule@projecthumancity.com, Project: Human City organization leader, primary contact  
The individuals listed below are members of the organization with whom we may collaborate in the later stages of development, and we communicate with them through Slack:  
Cheng-Ming Hsu, Spotstitch Ar Camera Lead  
Dushyant Mehul Lunechiya, Frontend Product lead  
Anupama Kadambi, Backend development Lead  
Ali Hassan Amin, Spotstitch Frontend developer  


Project: Human City is a non-profit organization who try to design and create an open ecosystem. They initiate ideas for global impact, focusing on addressing human inequality, social injustice, and access to basic needs.
## Description about the project
City building game is a web-based creative city-building game that aims to connect users closer to the real world. Users obtain inventory resources to build their city by scanning objects (ex: cars, buildings, infrastructures, etc.) in the real world, using the AR camera in the Spotstich mobile app (Spotstich mobile app is a multi-purpose social app developed by Project: Human City).  
Our product could potentially revolutionize how users engage their physical environment with virtual games. It will encourage people to explore the local environment and surrounding objects, capture and record them with a vivid 3D model instead of a 2D picture. For real life urban planners, this product might improve their efficiency of initializing city planning and inspire them with creative and effective design.  
With this interactive game developed, we hope to enhance users’ real world exploration and social interaction which could yield significant positive social impact. This also aligns with the mission of our partner, Project: Human City. As a non-profit organization, Project: Human City aims to enhance the lives of city residents on and off the app with various initiatives and projects. We together want to create a difference in people’s lives.  
​
## Key Features
City building game (Web game):  
-  Login/Signup and Logout of the game (Implemented in D3)
	-  New users can sign up for their account with name, email and password
	-  Existing users will be able to login to the game with their email and password
	-  Users can log out of the game when they want to exit
-  Start with a randomly generated empty map (Implemented in D3)
	- users will have different maps of a fixed size (10x10 tiles), generated from a perlin noise. The map will include grassland, water bodies, trees, etc. The user can start their city at the center and expand outwards.
- Design your city (Implemented in D3)
	- Add objects to the map from your inventory
	- Delete objects from the map and store them back into your inventory
	- Select objects on the game map and move their position and change their orientation, user can also deselect the item
- Save and load game map (Implemented in D3)
	- Users' previous progress on the game map will be saved when they log out from the game, The saved game map with previous progress will be loaded the next time they login
	- The save and load is done automatically
- Gain resources by collecting them inside the game and in the real world (Future development)
	- Things like wood can be gathered in game by clearing out forests, and can also be obtained in real life by scanning a tree. Other materials include ores, cement, stone, sand, and clay. These materials are all necessary for constructing buildings.
- Build different types of buildings and develop your city (Future development)
	- There are various building types, such as housing, utilities, production etc. Each type serves a key purpose in the expansion of the city. Each building also has different requirements to build.

 
# Instructions

## How to run server  
The current application host remote server on Render. However we used the free service, to the quality of connection cannot be guaranteed.


If user choose to use the remote server, they can simply skip to the “start the game” section. Note that the server spins down if it did not receive any request for 15 minutes. It could take several minutes or more for the service to spin back up.  


On the other hand, a user can choose to start a local server for the backend. To do this: 
- First, 'cd' into the Server directory by using terminal
- Then run 'npm install' and wait for the Node Package Manager to complete the download.
- run 'npm -version' to ensure it is 10.1.0 or above.  
- After ensuring that all the environments are fully installed, run 'npm start'.  
- When the terminal displays 'Server is running on port 3000, Database Connection Established!', which indicates the Server is running.  
- If the output indicates an 'app crash', this suggests that the environment configuration is incomplete. To address this, one should refer to the 'development requirements' section of the documentation to verify the comprehensive fulfillment of all prerequisites.  


## Start the Game:


- To commence your City Builder web game experience, please navigate to https://arkye7.itch.io/csc301-code-connoisseurs-city-builder in your web browser. Upon reaching the website, enter the password "codeconnoisseurs" to access the dedicated web page for the game.  

- (If user chooses to use local server, after starting the server, navigate to https://arkye7.itch.io/csc301-code-connoisseurs-city-builder-online-server-version in your web browser, enter password "codeconnoisseurs" to access the game with local server)  

We also made a demo introducing current features to play: ​​ 
https://utoronto.zoom.us/rec/share/_riVgGoHgsMYBZ7QFPMr6-m60EPLWRVH3y2t6pL5YxuA3yi9OfarXGS3r5vGuWC5.4Kn6_NsqB7Em5PDs?startTime=1700117523000  
Passcode: s2z@HxE**z  

You can either play around the game following the demo or the description below:


- At the start of the game, a new user will need to sign up in order to enter the game. A user who had already registered for an account for the game previously will only need to provide their email and password to login.  


- After a successful login, the game will load with the corresponding game map. New users will experience an automatically generated, random game map. Returning users will have the game map from their last session automatically loaded upon login.  


- Once the user enters the game with the generated/loaded map, they can access the inventory bar on the side to select categories of items for placement on the game map.  
	- Clicking on each category in the inventory menu yields the following options:  
	1. Road: The road button is a work in progress (WIP), and users can press the 'r' key for placement. More updates expected in the future.
	2. Housing: Clicking on the housing button directs users to an inventory containing all types of houses. Currently, only the Single Family House is available. More updates expected in the future.
	3. Utilities: Clicking on the utilities button leads to a submenu with subcategories such as energy, sewage, and water. Each subcategory offers different types of buildings to choose from.  
	More categories are anticipated in future updates!  
	- Each building item icon displays a number in the right corner, representing the available quantity for placement. If the number reaches zero, users can no longer place that type of building.  
	- Once a building is placed on the map, users have the option to select/deselect, move, delete, or change the orientation of the building by interacting with it on the game map.  
When the user decides to conclude their game session and exit, they can click on the logout button located in the top right corner of the screen. Exiting the game at this point ensures that their progress on the game map and changes in the inventory are saved.  
Following logout, the user will be redirected to the login page, where they can log in again to resume playing.  
## Development requirements

For Developers, we suggest to use the local server, so that it’s easier to access the log information. 


If one chooses to use the remote server for development, one can access by going to the Render website, https://render.com/ . And login with email: ruiting.chen.soc@gmai.com and password: 1234567890. The deployed server is at unity-game-server.


### Server:


1. Developers need to install .Net and Node.js before running the project.
	- For macOS:
		1. Installing .NET:  open terminal and run :-brew install --cask dotnet-sdk
		2. Installing Node.js: run: brew install node

	- For Windows:
		1. Installing .NET: 
			- Go to the webpage:https://dotnet.microsoft.com/zh-cn/download and download the installer of .NET. 
			- Run the installer and follow the prompts.
		2. Installing NVM: 
			- Go to the repository: https://github.com/coreybutler/nvm-windows.
			- Download and run the NVM installer
			- Open a new command prompt after installation, type:
		  		- nvm list available
				- nvm install latest
2. After get .Net and Node.js downloaded, open the project and cd to Server folder, run the following commands:
	- npm install
	- npm start
3. Once that it is running, you can access the backend at http://localhost:3000
4. You can access the database through: https://cloud.mongodb.com/v2/651f66de2b31664012c78bb6#/metrics/replicaSet/651f6806fc79f50af45b831a/explorer/lalala 


Unity:
Technology requirements: 
Download Unity Hub
In Unity Hub, download Unity Editor version 2020.3.20f1
Developer should download unity’s WebGL build support if they wish to build the product to a web app
Developer can download Python to start a HTTP server to if they wish to view the deployed game website on localhost
To set up and run the software (in the Unity development environment):
Clone the team repository
Open the Unity/CityBuilder folder from Unity Hub using Editor version 2020.3.20f1
After project loads in Unity, click “File” in the Unity ribbon > “Open Scene”, select from folder Scenes -> choose one of the .unity files, and click open to load the selected scene
Go to the top and click on the play button to start the game
To edit scripts, go to Assets/Scripts and double-click on any script, the code will open up in your default editor for unity
To test out the save/load map and update/load inventory features of the Unity game together with the database, the developer needs to connect to the backend server (described above)
For the Unity game to be able to login/logout, save/load map and update/load inventory properly, the developer also needs to start the server (described above).


## Deployment and Github Workflow
Pull request:
We separated ourselves into two subteams (web game and mobile app), three people per team. When a team member starts a pull request, at least one person from their subteam needs to review the pull request in order to merge the code. The pull request needs to have detailed descriptions of what is done/changed in each file. Ideally one should create a pull request after each task they finish and name the pull request as the task they worked on.
Naming conventions:
We will follow the standard naming conventions for each language/framework (ie. Camel case for C#)
Deployment:
Our game is supposed to be embedded in a website, which is currently under development by the partner and is not ready for integration. Thus, for deployment, we have temporarily uploaded the game to a third party website (itch.io) for the demo. This website is accessible via a link and password. (See Instruction part above) We have also created a locally hosted server to test the game’s save, load, and update features. Similar to the website, the partner is developing a backend for their product, but that server is not currently available for us. We are expecting to be able to move our server code and integrate it into theirs, but if that doesn’t happen for reasons outside of our control then we will move on with our own server.
## Coding Standards and Guidelines
Keep this section brief, a maximum of 2-3 lines. You would want to read through this article to get more context about what this section is for before attempting to answer.
These are 2 optional resources that you might want to go through: article with High level explanation and this article with Detailed Explanation
The web game (mainly done in Unity) will follow Google C# Style Guide. The mobile app will follow ​Google JavaScript Style Guide.
## Licenses
We are using Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International Public License by the requirement of our partner. Anyone is free to share and adapt materials of this code base given that they do the following:
give appropriate credit, provide a link to the license, and indicate if changes were made
do not use the codebase for commercial purposes
use the same license as this codebase, when you share and adapt the material
do not restrict others from doing anything this license permits	
