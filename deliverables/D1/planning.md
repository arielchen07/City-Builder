# Spotstitch City Builder Game / Team-31-Code Connoissuers
> _Note:_ This document will evolve throughout your project. You commit regularly to this file while working on the project (especially edits/additions/deletions to the _Highlights_ section). 
 > **This document will serve as a master plan between your team, your partner and your TA.**

## Product Details
 
#### Q1: What is the product?

A city building game involving bringing real world objects into a virtual world.
We are partnering with Project: Human City. They are developing an app named Spotstitch, a social app. 
Our team is responsible for developing a game feature as a part of the social app.
The game is a city builder game, where the user builds and manages a city. In order to build their city, the different components of infrastructure will be obtained by scanning objects in the real world with the app’s AR camera.
Spotstitch has a web client as well as a mobile app. The mobile app will be responsible for scanning the real world objects, while the game itself will take place on the web app.
Everyone who uses the Spostitch app will likely play the game as well. It is designed for people who enjoy city building games and want to bring the real world into their game. It also promotes real world interaction and incentivises people to explore their city.


#### Q2: Who are your target users?

  > Short (1 - 2 min' read max)
 * Be specific (e.g. a 'a third-year university student studying Computer Science' and not 'a student')
 * **Feel free to use personas. You can create your personas as part of this Markdown file, or add a link to an external site (for example, [Xtensio](https://xtensio.com/user-persona/)).**

User1: https://app.xtensio.com/8dlfki1b
User2: https://app.xtensio.com/hwv7hm1q

#### Q3: Why would your users choose your product? What are they using today to solve their problem/need?

> Short (1 - 2 min' read max)
 * We want you to "connect the dots" for us - Why does your product (as described in your answer to Q1) fits the needs of your users (as described in your answer to Q2)?
 * Explain the benefits of your product explicitly & clearly. For example:
    * Save users time (how and how much?)
    * Allow users to discover new information (which information? And, why couldn't they discover it before?)
    * Provide users with more accurate and/or informative data (what kind of data? Why is it useful to them?)
    * Does this application exist in another form? If so, how does your differ and provide value to the users?
    * How does this align with your partner's organization's values/mission/mandate?
  
There are some city building games such as Cities: Skylines nowadays that involve the  experience of planning and building virtual cities by users themselves. There are also mobile apps like KIRI engine  that allow users to scan their surrounding objects or scenes to generate a 3D model. However, as far as we know, none of the existing products combines both, allowing users to build their virtual cities by scanning objects from real life. \
Our product could potentially revolutionize how users engage their physical environment with virtual games. It will encourage people to explore the local environment and surrounding objects, capture and record them with a vivid 3D model instead of a 2D picture. For real life urban planners, this product might improve their efficiency of initializing city planning and inspire them with creative and effective design.\
With this interactive game developed, we hope to enhance users’ real world exploration and social interaction which could yield significant positive social impact. This also aligns with the mission of our partner, Project: Human City. As a non-profit organization, Project: Human City aims to enhance the lives of city residents on and off the app with various initiatives and projects. We together want to create a difference in people’s lives.


#### Q4: What are the user stories that make up the Minumum Viable Product (MVP)?

 * At least 5 user stories concerning the main features of the application - note that this can broken down further
 * You must follow proper user story format (as taught in lecture) ```As a <user of the app>, I want to <do something in the app> in order to <accomplish some goal>```
 * User stories must contain acceptance criteria. Examples of user stories with different formats can be found here: https://www.justinmind.com/blog/user-story-examples/. **It is important that you provide a link to an artifact containing your user stories**.
 * If you have a partner, these must be reviewed and accepted by them. You need to include the evidence of partner approval (e.g., screenshot from email) or at least communication to the partner (e.g., email you sent)


Web game features:

User Story 1: Place object into game map  
As someone who wants to design a city/town layout, I want to be able to select an item from my inventory and place it at a desired position on the map in the city builder game, in order to carry out my city plans and create a simulation of the city I want to design.  
Acceptance Criteria:  
Given the user is in the city builder game and has items in their inventory, when they select an item and choose a location on the map, then the item is placed at the specified location.

User Story 2: Delete object from game map  
As someone who wants to design a city/town layout, I want to delete objects from the map in the city builder game and have them reappear in my inventory, in order to refine/redo my city plans if I’m not satisfied with my previous design.  
Acceptance Criteria:  
Given the user is in the city builder game and has placed objects on the map, when they select an object on the map and choose to delete it, then the object is removed from the map and added back to the user's inventory.

User Story 3: Visit friend’s map  
As someone who likes to see other people’s designs of cities, I want to be able to visit my friends' cities in the city builder game, in order to tour their city designs and get some inspiration from other people’s work.  
Acceptance Criteria:  
Given the user is in the city builder game, when they access the friend's list and select a friend’s city to visit, then the corresponding friend’s city game map, according to the last time they saved, will be displayed on the screen. 

Mobile App features:  
User Story 4: Scan objects in real world and place into inventory  
As someone who likes to collect and record interesting things from the real world, I want to scan an item which is not included in my current inventory but exists in real-life world using Spotstitch's mobile app and then add it into my inventory. So that I can collect interesting items from the real world.
Each time I scan the object and add it into my inventory, I will get some virtual currency in my wallet.  
Acceptance Criteria:  
Given the user is in the city builder game and wants to add objects in their inventory, when they scan a real-life item and select “upload to inventory” option, then the item is added in their own inventory. 

User Story 5: Trading items in inventory  
As someone aspiring to design a city or town layout, my intention is to utilize the virtual currency available in the Spotstich mobile app. This will enable me to enhance the appearance or "skin" of various objects within my inventory, thus contributing to the overall aesthetic and appeal of my virtual city or townscape.  
Acceptance Criteria:  
When a user is within the Spotstich mobile app and navigates to the trade section, they can specify the desired item from their inventory that they wish to upgrade and confirm their order. At this point, the selected item in their inventory will undergo an upgrade process, and the virtual currency required for the upgrade will be deducted as per the price.

Approval from partner for user stories:  

<img width="706" alt="User Story Partner Approval" src="https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/51133017/54af4ad3-1055-4fdb-9e06-aeec3b8627e5">


#### Q5: Have you decided on how you will build it? Share what you know now or tell us the options you are considering.

> Short (1-2 min' read max)
 * What is the technology stack? Specify languages, frameworks, libraries, PaaS products or tools to be used or being considered. 
 * How will you deploy the application?
 * Describe the architecture - what are the high level components or patterns you will use? Diagrams are useful here. 
 * Will you be using third party applications or APIs? If so, what are they?
 
 
 The project involves developing a cross-platform game that enables users to create and play augmented reality (AR) games with geolocation features, real-time interaction, and customizable maps. 
The frontend will be built using React Native for mobile apps and React.js for web apps. 
The mobile terminal is used for AR recognizations. (considering Aflaw for AR third-party API)
Backend (Unity, c , blender (may use AI tools) )will handle data storage, business logic, and user authentication. It will interact with databases like PostgreSQL and MongoDB. The backend will also incorporate AI/ML components for data analysis and natural language processing.
The project will also feature real-time processing for events and notifications, and analytics tools will track user behavior. 

TECHNOLOGY STACK:
1. Frontend:
    a. Web Apps:
        i. Languages & Libraries: React.js
        ii. Products: SpotStitch, Co-quest, Lotus Learning
    b. Mobile Apps:
        i. Languages & Libraries: React Native, Flutter
        ii. Products: SpotStitch, Co-quest, Lotus Learning
2. Backend:
    a. API Layer:
        i. Languages & Frameworks: Unix, C++, Node.js, Python (Django/Flask)
        ii. Types: RESTful, GraphQL
    b. Business Logic:
        i. Custom modules for SpotStitch, Co-quest, Lotus Learning
    c. AI and Machine Learning:
        i. Libraries: GPT-4 (for NLP tasks), TensorFlow, PyTorch
    d. Database:
        i. Relational Database: PostgreSQL
        ii. NoSQL Database: MongoDB
3. DevOps:
    a. Containerization: Docker
    b. Orchestration: Kubernetes
    c. Cloud Services: AWS, Azure
4. Security:
    a. Authentication: JWT, OAuth 2.0
    b. Data Encryption:
        i. SSL/TLS (for data in transit)
        ii. AES (for data at rest)
5. Blockchain:
    a. Smart Contracts: Solidity (for Ethereum-based contracts)
    b. Decentralized Storage: IPFS
6. Real-Time Processing:
    a. WebSockets: For real-time updates and transactions
    b. Message Queuing: RabbitMQ
7. Analytics:
    a. User Analytics: Google Analytics or custom analytics engine
    b. Monitoring Tools: Prometheus, Grafana
8. Patterns:
    a. Microservices Architecture: Separate microservices for products like SpotStitch, Co-quest, Lotus Learning
    b. Event-Driven Architecture: For real-time features and decoupling
    c. MVC (Model-View-Controller): Structuring frontend and backend
    d. Repository Pattern: For database interaction
    e. Factory Pattern: For AI/ML model creation
    f. Singleton Pattern: For shared resources
    g. Observer Pattern: For real-time updates
p.s. Will be modify during the process

![process](https://github.com/csc301-2023-fall/31-Project-Human-City-M/assets/80373621/c2297834-6a89-41b9-a164-c8ec5697f688)
Current 3rd Party API / Applications:
- Geolocation Services: Google Maps API
- Database Services: SQLserver, MongoDB
- AR services: A-flaw AR (considering ARCore/ARKit)
  
p.s. Additional may be added during process

----
## Intellectual Property Confidentiality Agreement 
> Note this section is **not marked** but must be completed briefly if you have a partner. If you have any questions, please ask on Piazza.
>  
**By default, you own any work that you do as part of your coursework.** However, some partners may want you to keep the project confidential after the course is complete. As part of your first deliverable, you should discuss and agree upon an option with your partner. Examples include:
1. You can share the software and the code freely with anyone with or without a license, regardless of domain, for any use.
2. You can upload the code to GitHub or other similar publicly available domains.
3. You will only share the code under an open-source license with the partner but agree to not distribute it in any way to any other entity or individual. 
4. You will share the code under an open-source license and distribute it as you wish but only the partner can access the system deployed during the course.
5. You will only reference the work you did in your resume, interviews, etc. You agree to not share the code or software in any capacity with anyone unless your partner has agreed to it.

**Your partner cannot ask you to sign any legal agreements or documents pertaining to non-disclosure, confidentiality, IP ownership, etc.**

Briefly describe which option you have agreed to.

----

## Teamwork Details

#### Q6: Have you met with your team?

Do a team-building activity in-person or online. This can be playing an online game, meeting for bubble tea, lunch, or any other activity you all enjoy.
* Get to know each other on a more personal level.
* Provide a few sentences on what you did and share a picture or other evidence of your team building activity.
* Share at least three fun facts from members of you team (total not 3 for each member).


#### Q7: What are the roles & responsibilities on the team?

Describe the different roles on the team and the responsibilities associated with each role. 
 * Roles should reflect the structure of your team and be appropriate for your project. One person may have multiple roles.  
 * Add role(s) to your Team-[Team_Number]-[Team_Name].csv file on the main folder.
 * At least one person must be identified as the dedicated partner liaison. They need to have great organization and communication skills.
 * Everyone must contribute to code. Students who don't contribute to code enough will receive a lower mark at the end of the term.

List each team member and:
 * A description of their role(s) and responsibilities including the components they'll work on and non-software related work
 * Why did you choose them to take that role? Specify if they are interested in learning that part, experienced in it, or any other reasons. Do no make things up. This part is not graded but may be reviewed later.


#### Q8: How will you work as a team?

Describe meetings (and other events) you are planning to have. 
 * When and where? Recurring or ad hoc? In-person or online?
 * What's the purpose of each meeting?
 * Other events could be coding sessions, code reviews, quick weekly sync meeting online, etc.
 * You should have 2 meetings with your project partner (if you have one) before D1 is due. Describe them here:
   * You must keep track of meeting minutes and add them to your repo under "documents/minutes" folder
   * You must have a regular meeting schedule established for the rest of the term.  
  
#### Q9: How will you organize your team?

List/describe the artifacts you will produce in order to organize your team.       

 * Artifacts can be To-Do lists, Task boards, schedule(s), meeting minutes, etc.
 * We want to understand:
   * How do you keep track of what needs to get done? (You must grant your TA and partner access to systems you use to manage work)
   * **How do you prioritize tasks?**
   * How do tasks get assigned to team members?
   * How do you determine the status of work from inception to completion?

#### Q10: What are the rules regarding how your team works?

**Communications:**
 * What is the expected frequency? What methods/channels will be used? 
 * If you have a partner project, what is your process for communicating with your partner?
 
**Collaboration: (Share your responses to Q8 & Q9 from A1)**
 * How are people held accountable for attending meetings, completing action items? Is there a moderator or process?
 * How will you address the issue if one person doesn't contribute or is not responsive? 
