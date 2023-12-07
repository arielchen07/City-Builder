# Developer Instructions

### Table of contents
#### [Architecture](#architecture)
#### [Development requirements](#development-requirements)
#### [DevOps and Github Workflow](#devops-and-github-workflow)
#### [Coding Standards and Guidelines](#coding-standards-and-guidelines)

## Architecture
The main components of this projects are Unity for game interactions, Node.js and React for server, and MongoDB Atlas for database.  
Here we provide an diagram of the overall architecture of the project.  
![30](https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/80373621/9604d3b5-5b64-4ec8-83f5-1abf787f171a)

### Unity: Game interactions
Unity plays the most important role in our project, handling not only the graphics and user inputs but also the !
game logic. It communicate with the server by sending request via RESTful APIs for state management and content updates.  
[Unity architecture diagram](https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/105243552/389dca7a-8cee-433f-b603-7efc44c14043)

### Node.js and React: Server framework
The server use Node.js and React frameworks and include the following major components:
- models: define schema for map, user, inventory in mongodb
- controllers: functions that handle different requests for map, user, inventory
- routes: provides RESTful API endpoint to trigger functions in controller

The server handles request from Unity and updates to Database. It will also receive feedback such as data information or error from Database and send back to Unity.

### MongoDB Atlas: Dadabase Design
MongoDB Atlas, as our chosen database service, offers:
- Cloud-based flexibility and scalability for game data needs.
- NoSQL allows us to handle a variety of data types efficiently.

> For a detailed understanding of the folder and file structure in this repository, please refer to `City-Builder-Game-technical-document.pdf`.  
> To gain insight into the current state and future plans of the City Builder Game, including what has been built and our envisioned features, please refer to `City-Builder-Game-design-document.pdf`.  

## Development requirements
### Server
For Developers, we suggest to use the local server so that it’s easier to access the log information. 
If you need access to remote server on Render or MangoDB datset, please contact Project: Human City. The deployed server on Render is at unity-game-server and you can checkout the log tab for the messages from the server.

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

### Unity
- Download Unity Hub
- In Unity Hub, download Unity Editor version 2020.3.20f1
- Developer should download unity’s WebGL build or desktop build support based on their need for the final product
- Developer can download Python to start a HTTP server to if they wish to view the deployed game website on localhost
To set up and run the software (in the Unity development environment):
- Clone the team repository
- Open the Unity/CityBuilder folder from Unity Hub using Editor version 2020.3.20f1
- After project loads in Unity, click “File” in the Unity ribbon > “Open Scene”, select from folder Scenes -> choose one of the .unity files, and click open to load the selected scene
- Go to the top and click on the play button to start the game
To edit scripts, go to Assets/Scripts and double-click on any script, the code will open up in your default editor for unity

For the Unity game to be able to login/logout, save/load map and update/load inventory properly, and test out the save/load map and update/load inventory features with the database, the developer also needs to start the server (described above).


## DevOps and Github Workflow
### Automated testing & deployment
We enabled automated testing and deployment for our project using github actions, triggered every commit to main. The process is divided into automatic testing and deployment/build after tests passed. Otherwise, github action will show failure cases for testing. Corresponding files for automated testing and deployment can be found in .github/workflows. 

Frontend (Unity) deployment
- Final result is zip file of desktop executable.
- Unity needs Licence for build, change Unity licence information in GitHub secrets (professional account: email, password and serial, personal account: license)

Backend (Derver) deployment:
- deployed to Render, name: unity-game-server
- can change repositories connected to server and other deployment info after logging on to the account

### Future Deployment and Integration
The game is planned to be embedded in a website, which is currently under development by Project: Human City and is not ready for integration. The server may also be deployed to a different platform. 

## Coding Standards and Guidelines
### Coding Standards
The web game (mainly done in Unity) will follow Google C# Style Guide.  
The mobile app will follow ​Google JavaScript Style Guide.

### Pull request Suggestion
We suggest future developers to continue using the pull request pratice as we did:
When a team member starts a pull request, at least one person from team needs to review the pull request in order to merge the code. The pull request needs to have detailed descriptions of what is done/changed in each file. Ideally one should create a pull request after each task they finish and name the pull request as the task they worked on.
