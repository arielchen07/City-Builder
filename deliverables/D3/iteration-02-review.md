
# 31-Code-Connoisseurs


## Iteration 03 - Review & Retrospect

 * When:  November 15th 2023
 * Where: ONLINE

## Process - Reflection
**note**  
We used chatGPT to format and polish this document, but all ideas are original and the document has gone through human editing.  

#### Q1. What worked well

List **process-related** (i.e. team organization and how you work) decisions and actions that worked well.

1. Thorough Feature Planning and Task Assignment:
   - Importance: The initial brainstorming session to outline all features, followed by task splitting and assignment, is the most important step in our project workflow. It helps develop a clear roadmap for our team, ensuring every team member understands their role and contribution toward the collective project goal.
   - Supporting Argument: A well-defined plan in the beginning sets the tone for the entire development stage. It minimizes confusion, aligns team members with project objectives, and enhances overall working efficiency. Our task assignment process fosters a sense of ownership among team members, boosting individual accountability.


2. Making Branches for Individual Task Development:
   - Importance: The decision to create separate branches for each task streamlines individual and team-focused development efforts. This prevents potential conflicts that may arise when multiple team members are concurrently working on different features.
   - Supporting Argument: This decision significantly reduces the chances of code conflicts and ensures a smoother development process. Team members can work independently on their assigned tasks with autonomy while maintaining version control integrity. The structured approach minimizes frequent disruptions caused by conflicting code changes and supports a more organized development environment.


3. Task Connection Branches for Seamless Integration:
   - Importance: Creating branches specifically for connecting different tasks and subsequent merging, which improves code integration without affecting the stability of the main branch. This decision contributes to a more reliable and scalable codebase.
   - Supporting Argument: By avoiding direct merges with the main branch, we avoid potential errors and maintain a more robust code structure. The branches for task connection support a more systematic integration process, ensuring that the codebase remains cohesive and stable throughout the development.
   - See Pull Request and commits in github for reference.

4. Pull Requests with Collaborative Code Review:
   - Importance: Making pull requests and inviting other team members as reviewers is a good practice in software engineering. This collaborative review process ensures code quality, reduces the likelihood of introducing bugs while validating that new features integrate seamlessly with existing functionality.
   - Supporting Argument: The decision to have a code review process adds an extra layer of assurance to our development workflow. By inviting peers to review pull requests, we aim to have collective expertise, catch potential issues early, and maintain a high standard of code quality. This practice contributes to a more robust and error-resistant codebase.
   - See Pull Request section in github for reference.


#### Q2. What did not work well

List **process-related** (i.e. team organization and how you work) decisions and actions that did not work well.

1. Individual Branches causing Merging Difficulty:
The decision to implement separate branches for each task was intended to support individual development within the team. However, in practice, this approach has proven to be challenging during the merging phase. The primary issue arises from the fact that each individual or team operates on their own branch without regularly merging and updating to the main branch. This lack of regular integration leads to a significant divergence between each branch, complicating the merging process.
Furthermore, the choice of using GitHub instead of Unity Version Control exacerbates the difficulty of code merging. The conflicts during merge occur mostly at the unity main scene. The scene contains various objects needed for the game to function, and is encoded in a format that is hardly human readable, so during the merging process, the scene file often had tens to hundreds of conflicts, and resolving them was often unreliable and would result in the scene breaking.
While this decision improves the efficiency for individual development, it somehow negatively impacts the final merging process, as resolving conflicts and ensuring a smooth integration of changes becomes a time-consuming and error-prone task. 

2. Lack of Communication between tasks
The decision to assign tasks to individual team members/ subgroups was intended to foster clear goals for the whole team. However, we met challenges in practice. The primary issue arises from the fact that each individual or subteam implements their own tasks without regularly communicating with other team members. This lack of communication leads to unclear requirements or slightly different code structures that cost additional time to fix and merge.
For example, a challenge occurred when one sub team was tasked with developing login and register functionality. Ideally, the login page should be located in a scene separate from the game scene, but due to lack of communication, the subteam implemented the login page in the same scene, resulting in merge conflicts and bad project structure which lead to the expense of additional time to fix the issue.


3. Communication with partner
We set the weekly meeting schedule with our partner at the beginning of the semester to foster regular communications and updates. However, challenges arise as our partner has busy schedules or occasionally conflicts. These issues led to last-minute postponements of scheduled meetings, adversely impacting our development process.
This could be partially due to our ignorance for maintaining open and continuous communication with the partner. The lack of proactive confirmation and synchronization with their schedule resulted in disruptions to our planned communication routine. Though we scheduled Friday noon meetings, persistent challenges emerged. The most recent meeting was postponed to Monday, and, on occasions, our partner remained unresponsive throughout the entire day.



#### Q3(a). Planned changes

List any **process-related** (i.e. team organization and/or how you work) changes you are planning to make (if there are any)

Regarding the points mentioned in Q2, we planned to have the following changes:
1. Schedule Regular Check-ins and Task Dependency Clarification:
Make regular check-ins and clearly define task dependencies to enhance team communication and project efficiency. This change aims to reduce potential misunderstandings, ensure everyone is on the same page, and facilitate a smoother workflow. With this planned change, we hope to have a significant reduction in rework and an overall improvement in productivity.

2. Enhance Documentation Practices and Clear Sub-Branch Creation Plan:
Comprehensive documentation for clarity and provide a clearer reference/plans for team members. This change involves efforts to document processes, decisions, and code logic thoroughly. By doing so, we hope to decrease the likelihood of errors and conflicts between subteams and have a more efficient development process overall.
Evaluate Unity Version Control for Unity Merging:
Exploring Unity Version Control for Unity side merging, while maintaining GitHub for the server side, could potentially streamline our version control processes. This change aims to investigate the feasibility of using specialized tools for Unity development, potentially reducing the complexity of merging tasks. Still need some detailed exploration of this approach to ensure a seamless integration that aligns with our project requirements and development workflow.

3. Confirm Meeting Times with the Partner:
We will try our best to make more timely and consistent communication with the project partner. Confirming meeting times in advance helps prevent last-minute changes and ensures that everyone is available and prepared. This change aims to uphold the commitment to regular meetings, facilitating a smoother collaboration with the partner and minimizing disruptions to the project timeline.

#### Q3(b). Integration & Next steps
Briefly explain how you integrated the previously developed individuals components as one product (i.e. How did you be combine the code from 3 sub-repos previously created) and if/how the assignment was helpful or not helpful.

We scheduled a sequential merging process, starting with the Database subteam, followed by the Unity subteam. The Server subteam merged last, addressing bugs to ensure seamless integration between the database and Unity components. This approach facilitated a structured combination of the three sub-repos into one cohesive product.

## Product - Review

#### Q4. How was your product demo?
  In preparation for the demo, we identified key features and outlined a step-by-step demonstration plan in the meeting: 
    1. register
    2. generate map
    3. show inventory
    4. manipulate inventory
        a. place 2 different item on to map (show decrease of item quantity in inventory)
        b. select, move and deselect items
        c. rotate items
        d. select and delete items (show increased item quantity in inventory)
        e. place items until quantity reaches zero (show cannot place that  item any more)
    5. logout (automatic save)
    6. log-in
        a. re-login with previous info
        b. confirm map and inventory state persistence
    7. show login/signup with incomplete/incorrect info will trigger corresponding error messages

Due to time constraints and our partner's busy schedule, we recorded the demo and shared it with our partner for feedback. (demo link can be found in README)

We have yet to receive feedback from our partner. We will update this document once we hear back from them.

This experience reinforced the importance of clear communication and streamlined processes in collaborative projects. Beyond feature development, delivering a clear and easy-to-understand demonstration is equally important to engage potential users, ensuring they understand how to use the product and enticing them to stay informed about subsequent updates.
