# Game Engine/ Code Connoisseurs

### Table of contents
#### [Partner Intro](#partner-intro)
#### [Description of the project](#description-of-the-project)
#### [Key Features](#key-features)
#### [Project architecture](#project-architecture)
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

## Project architecture 
This guide provides an in-depth look at the architecture and technical components of our project. It covers the integration of Unity for client-side interactions, Node.js and React for server-side operations, and MongoDB Atlas as our database solution. Understanding these elements is crucial for efficient development and maintenance of the project.
![30](https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/80373621/9604d3b5-5b64-4ec8-83f5-1abf787f171a)

#### Unity: Advanced Client-Side Interface
Unity plays a pivotal role in our project, handling not just graphics and user inputs but also orchestrating client-side logic. Its advanced capabilities allow for:
- Real-time interaction with dynamic content delivery.
- Efficient communication with the server via RESTful APIs for state management and content updates.
- Enhanced user experience through sophisticated graphical rendering and input management.

#### Node.js and React: Robust Server-Side Framework
The combination of Node.js and React forms the backbone of our server-side operations. They bring to the table:
- Efficient request handling and business logic implementation using Node.js, crucial for real-time applications and multiplayer game environments.
- Secure authentication processes and streamlined data handling capabilities.
- React's contribution in data formatting ensures user data is relayed in an intuitive and user-friendly format to the client-side.

#### MongoDB Atlas: Scalable Database Solution
MongoDB Atlas, as our chosen database service, offers:
- Cloud-based flexibility and scalability, catering to our growing data needs.
- NoSQL capabilities, allowing us to handle a variety of data types efficiently.
- Real-time data processing and analysis, enhancing overall application performance.

#### Comprehensive System Integration
The integration of Unity, Node.js/React, and MongoDB Atlas is the cornerstone of our architecture. This integration facilitates:
- Seamless data exchange between Unity and Node.js, vital for real-time communication and gameplay experience.
- Efficient CRUD operations between Node.js and MongoDB Atlas, ensuring data integrity and accessibility.
- A unified development environment, leveraging JavaScript's capabilities across the stack.

#### Advanced Controller Design Strategies
Our controller design is crafted with precision, focusing on:
- Optimal server-side logic for effective user request handling and data manipulation.
- Ensuring a clear separation of concerns across game logic, data handling, and presentation layers.
- Enhancing system performance by offloading intensive data processing and storage tasks to the server and database layers.
- Scalability and flexibility in handling increasing loads and complex game scenarios.

This enriched architecture not only capitalizes on the unified environment of JavaScript but also sets a new standard in real-time data processing and efficient data management. It is a comprehensive solution that addresses the complexities of modern game development and interactive applications, ensuring scalability, dynamic interaction, and broad accessibility.




 
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
- To commence your City Builder web game experience, please navigate to the web version on [itch.io](https://arkye7.itch.io/csc301-code-connoisseurs-city-builder-online-server-version) in your web browser. Upon reaching the website, enter the password "codeconnoisseurs" to access the dedicated web page for the game.   

You can find this demo to learn the current features to play: ​​ 
https://utoronto.zoom.us/rec/share/_riVgGoHgsMYBZ7QFPMr6-m60EPLWRVH3y2t6pL5YxuA3yi9OfarXGS3r5vGuWC5.4Kn6_NsqB7Em5PDs?startTime=1700117523000  
Passcode: s2z@HxE**z  

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
