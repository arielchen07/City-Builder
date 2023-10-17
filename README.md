[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/AvkT738V)
[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-718a45dd9cf7e7f842a935f5ebbe5719a5e09af4491e668f4dbf3b35d5cca122.svg)](https://classroom.github.com/online_ide?assignment_repo_id=12313518&assignment_repo_type=AssignmentRepo)

##Run Windows Deployed version:
Requirement: 
- Download Node.js (for running the server)

Run Server: 
- Clone sub team repo
- Open the terminal, go into the sub team repo -> TestServer folder
- Run node server.js to start the server
- The server will print out messages when it receives a request from the client

Run Unity game:
- Download DeploymentWindows.zip and decompress it
- Double click on CityBuilder.exe to start the game


##Run WebGL Deployed version:
Requirement: 
- Download Node.js (for running the server)
- Download Python on your machine (this is used to start a simple HTTP server for viewing the game in a browser)

Run Server: Same as the server instruction for Windows deployment  

Run Unity game:
- Download DeploymentWeb.zip and decompress it
- Open a new terminal, go into the folder that you decompress the zip at
- Run py -m http.server
- Open up a web browser (recommend Chrome) and go to http://localhost:8000/, you should be able to see the game load

##Testing in Unity development environment:
Requirement: 
- Download Unity Hub
- In Unity Hub, download Unity Editor version 2020.3.20f1
- Download Node.js (for running the server)
- Download Python (for starting a HTTP server to view the WebGl deployment of the game)

Run Server: Same as the server instruction for Windows deployment 

Run Unity Game:
- Open the deliverable-2-31-2-chenru76-elgabray/Unity/CityBuilder folder from Unity Hub using Editor version 2020.3.20f1
- After project loads in Unity, go to File –> Open Scene, select Scenes -> MainScene.unity, click open load the main game scene
- Go to the top and click on the play button to start the game

##Instruction for playing the game
See sub team report (in deliverable-2-31-2-chenru76-elgabray\deliverables\D2) question 3 section for instruction of how to play/test the game

