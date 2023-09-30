# Spotstitch City Builder Game / Team-31-Code Connoissuers

## Product Details
 
#### Q1: What is the product?

A city building game involving bringing real world objects into a virtual world.
We are partnering with Project: Human City. They are developing an app named Spotstitch, a social app. 
Our team is responsible for developing a game feature as a part of the social app.
The game is a city builder game, where the user builds and manages a city. In order to build their city, the different components of infrastructure will be obtained by scanning objects in the real world with the app’s AR camera.
Spotstitch has a web client as well as a mobile app. The mobile app will be responsible for scanning the real world objects, while the game itself will take place on the web app.
Everyone who uses the Spostitch app will likely play the game as well. It is designed for people who enjoy city building games and want to bring the real world into their game. It also promotes real world interaction and incentivises people to explore their city.


#### Q2: Who are your target users?

User1: https://app.xtensio.com/8dlfki1b
User2: https://app.xtensio.com/hwv7hm1q

#### Q3: Why would your users choose your product? What are they using today to solve their problem/need?  
  
There are some city building games such as Cities: Skylines nowadays that involve the  experience of planning and building virtual cities by users themselves. There are also mobile apps like KIRI engine  that allow users to scan their surrounding objects or scenes to generate a 3D model. However, as far as we know, none of the existing products combines both, allowing users to build their virtual cities by scanning objects from real life.  
Our product could potentially revolutionize how users engage their physical environment with virtual games. It will encourage people to explore the local environment and surrounding objects, capture and record them with a vivid 3D model instead of a 2D picture. For real life urban planners, this product might improve their efficiency of initializing city planning and inspire them with creative and effective design.\
With this interactive game developed, we hope to enhance users’ real world exploration and social interaction which could yield significant positive social impact. This also aligns with the mission of our partner, Project: Human City. As a non-profit organization, Project: Human City aims to enhance the lives of city residents on and off the app with various initiatives and projects. We together want to create a difference in people’s lives.


#### Q4: What are the user stories that make up the Minumum Viable Product (MVP)?  

Web game features:

**User Story 1:** Place object into game map  
As someone who wants to design a city/town layout, I want to be able to select an item from my inventory and place it at a desired position on the map in the city builder game, in order to carry out my city plans and create a simulation of the city I want to design.  
Acceptance Criteria:  
Given the user is in the city builder game and has items in their inventory, when they select an item and choose a location on the map, then the item is placed at the specified location.

**User Story 2:** Delete object from game map  
As someone who wants to design a city/town layout, I want to delete objects from the map in the city builder game and have them reappear in my inventory, in order to refine/redo my city plans if I’m not satisfied with my previous design.  
Acceptance Criteria:  
Given the user is in the city builder game and has placed objects on the map, when they select an object on the map and choose to delete it, then the object is removed from the map and added back to the user's inventory.

**User Story 3:** Visit friend’s map  
As someone who likes to see other people’s designs of cities, I want to be able to visit my friends' cities in the city builder game, in order to tour their city designs and get some inspiration from other people’s work.  
Acceptance Criteria:  
Given the user is in the city builder game, when they access the friend's list and select a friend’s city to visit, then the corresponding friend’s city game map, according to the last time they saved, will be displayed on the screen. 

Mobile App features:  

**User Story 4:** Scan objects in real world and place into inventory  
As someone who likes to collect and record interesting things from the real world, I want to scan an item which is not included in my current inventory but exists in real-life world using Spotstitch's mobile app and then add it into my inventory. So that I can collect interesting items from the real world.
Each time I scan the object and add it into my inventory, I will get some virtual currency in my wallet.  
Acceptance Criteria:  
Given the user is in the city builder game and wants to add objects in their inventory, when they scan a real-life item and select “upload to inventory” option, then the item is added in their own inventory. 

**User Story 5:** Trading items in inventory  
As someone aspiring to design a city or town layout, my intention is to utilize the virtual currency available in the Spotstich mobile app. This will enable me to enhance the appearance or "skin" of various objects within my inventory, thus contributing to the overall aesthetic and appeal of my virtual city or townscape.  
Acceptance Criteria:  
When a user is within the Spotstich mobile app and navigates to the trade section, they can specify the desired item from their inventory that they wish to upgrade and confirm their order. At this point, the selected item in their inventory will undergo an upgrade process, and the virtual currency required for the upgrade will be deducted as per the price.

Approval from partner for user stories:  

<img width="706" alt="User Story Partner Approval" src="https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/51133017/54af4ad3-1055-4fdb-9e06-aeec3b8627e5">


#### Q5: Have you decided on how you will build it? Share what you know now or tell us the options you are considering.  

Technology Stack
- Frontend:
    - Web Apps:
        - Languages & Libraries: 
            - React.js
    - Mobile Apps:
        - Languages & Libraries: 
            - React Native (JS)

- Backend:
    - API Layer:
        - Languages & Frameworks: 
            - Unity
            - Blender
            - MagicaVoxel
    - Business Logic:
        - (optional) AI: 
            - GPT-4 API 
        - (optional) Machine Learning: 
            - TensorFlow
        - Relational Database: 
            - PostgreSQL
        - NoSQL Database: 
            - MongoDB (if needed)

- DevOps:
    - Containerization: 
        - Docker
    - Orchestration: 
        - Kubernetes
    - Cloud Services: 
        - AWS
        - Azure

- Security: (optional)
    - Authentication: 
        - JWT
        - OAuth 2.0
    - Data Encryption:
        - SSL/TLS (for data in transit)
        - AES (for data at rest)

- Blockchain: (optional)
    - Smart Contracts: 
        - Solidity (for Ethereum-based contracts)
    - Decentralized Storage: 
        - IPFS

- Real-Time Processing:
    - WebSockets: 
        - For real-time updates and transactions
    - Message Queuing: 
        - RabbitMQ

- Analytics: (optional)
    - User Analytics: 
        - Google Analytics or custom analytics engine
    - Monitoring Tools: 
        - Prometheus
        - Grafana

- Patterns:
    - Event-Driven Architecture: 
        - For real-time features and decoupling
    - MVC (Model-View-Controller): 
        - Structuring frontend and backend
    - Repository Pattern: 
        - For database interaction
    - Factory Pattern: 
        - ML model creation
    - Singleton Pattern: 
        - For shared resources
    - Observer Pattern: 
        - For real-time updates

p.s. Will be modify during the process
 
 
 The project involves developing a cross-platform game that enables users to create and play augmented reality (AR) games with geolocation features, real-time interaction, and customizable maps. 
The frontend will be built using React Native for mobile apps and React.js for web apps. 
The mobile terminal is used for AR recognizations. (considering Aflaw for AR third-party API)
Backend (Unity, C#, blender (may use AI tools) )will handle data storage, business logic, and user authentication. It will interact with databases like PostgreSQL and MongoDB. The backend will also incorporate AI/ML components for data analysis and natural language processing (Optional).
The project will also feature real-time processing for events and notifications, and analytics tools will track user behavior. 
![2](https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/80373621/a1448609-d23f-444e-a9ff-ae4c1542a811)

Current 3rd Party API / Applications:
- Geolocation Services: Google Maps API
- Database Services: SQLserver, MongoDB
- AR services: A-flaw AR (considering ARCore/ARKit)
  
p.s. Additional may be added during process

----
## Intellectual Property Confidentiality Agreement  

We agreed to option 5 for now. Our partner shared the <em>Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International Public License</em> document with us. We will discuss further with our partner about this license and will probably move to option 3 later.

----

## Teamwork Details

#### Q6: Have you met with your team?  

![Picture1](https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/51133017/67195d12-499a-4dc0-8257-ee1b2838c135)  
We played a few online icebreakers such as codenames, bombparty, and among us while video calling on Zoom so everyone got to know each other more.  
Ricky has already made 5 games!!  
Yahya has a substantial board game library in his dorms.  
Krystal never rode a rollercoaster before!  



#### Q7: What are the roles & responsibilities on the team?  

Roles:  
Partner Liaison: main point of contact with partner  
Scrummaster: sub-team lead  
Mobile app developer: member of the mobile app team  
Web app developer: member of the web app team  
Meeting Minutes tracker: keeps track of the meeting minutes  

Ricky: Software Developer and Scrummaster for the web game using Unity for front end. Has previous experience with game development and using unity. Partner liaison by self nomination.  
Ruiting: Web App developer, will be working mainly on web game Unity backend that includes the game logic and connection to the mobile app database. Interested in gaining experience with Unity. Has previous experience in computer graphics (CSC317) and some experience in Blender. 
KeGong: Mobile App developer, will be working mainly on mobile app frontend and inventory, had experience on web full stack development.  
Krystal: Mobile App developer, will be working mainly on mobile app React frontend and Django Backend with experience on full stack development and testing.  
Yahya: Mobile App developer, Meeting Minutes Tracker, will work on integrating the API for the AR camera and processing scanned objects. Interested in this technology the most, but have no previous experience.  
Sirui (Ariel): Web App developer, will be working on web game Unity backend that includes the game logic. During the meantime, I will also contribute to the AR camera part. I am interested in developing apps and also AR. Has experience in computer vision research and is familiar with image recognition and object matching techniques that might be helpful for the game overall.  

#### Q8: How will you work as a team?  
Weekly meetings with partner at Fridays 12:30 - 13:30, online  
Weekly TA meetings in the range of 20:00 - 21:00 on Mondays, online  
ad hoc internal meetings, online  

Partner meetings are to make sure we are on the same page with the partner and to communicate any design changes and clarify implementations.  
TA meetings are to ask any course-specific questions and also technical questions related to the project.  
Internal meetings are for making significant decisions together such as code architecture, design choices and progress updates. Subteams will arrange their own meeting time to coordinate details.  
   
During the first meeting with our partner, we went over the app that they were currently developing, and clarified what they wanted us to build. We were presented with two choices of the project topic: following the existing ganme design documents to create a web-based game or ustilizing AI to generate games with specified features (which involves some research on our own to start). We decided to implement the pre-designed game as the scope will be more achievable in the given time frame. So, we received documentations for the designs of the game that the previous team had worked on. We also decided on future weekly meeting schedules and communication methods going forward. If we find that we have more issues to discuss on our own, we will schedule an internal meeting at a different time.  

During the second meeting on September 29th, we went over the mockups we had created and talked about some details regarding the functionalities of the AR camera that the organization had already built. We also confirmed our confidentiality policies and user stories. Our partner will introduce one more developer to assist our development on AR camera API and one project manager to facilitate the team work. We are ready to move onto more in depth design and development stages.  

  
#### Q9: How will you organize your team?  
We will use Notion to keep track of a todo list, task boards etc. and will update every meeting minutes to this github repository. 

We will prioritize fundamental features of the whole game, which are described in our user stories. If extra time remains after complete these features, we will consider adding other features to enhance gaming experiences.  

Tasks are assigned based on each memebers' interests, skills and prior experiences. We split into 2 subteams working on web-app and mobile app. Each team has at least 1 member with prior experience in this field. For example, web game team has member with game development experience in Unity.

To determine the status of work from inception to completion, we will first split tasks into key milestones. By setting the objectives and expected deadlines for each milestone, we can better determine the progress and make corresponding changes to our schedule if needed.

#### Q10: What are the rules regarding how your team works?

**Communications:**
 * What is the expected frequency? What methods/channels will be used?

Slack: primary mode of contact, everyone is expected to read all messages on the channels related to the team project within 24h.  
Zoom: synchronous meetings when necessary 

 * If you have a partner project, what is your process for communicating with your partner?

Weekly Zoom meetings with partner, Slack for more urgent problems/simple questions

**Collaboration**
 * How are people held accountable for attending meetings, completing action items? Is there a moderator or process?  

We will remind the team about the incoming meeting in our slack channel. We expect everyone to let us know beforehand if they won’t be there for a meeting etc. If someone fails to join meetings without justified reasons or fails to complete assigned tasks without informing the team on time, then they should know that it will reflect on their peer reviews and individual marks.

 * How will you address the issue if one person doesn't contribute or is not responsive?

Attempt to reach out to them and if they still refuse to respond or change their attitude and behaviors, then bring it up with TAs and reflect it in peer reviews.

