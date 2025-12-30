---
title: "Chirp! Project Report"
author: "Group 12"
date: "2025"
---

# Design and Architecture

## Domain Model
**Figure 1:** Class diagram. ASP.NET Identity package on the left side. It contains IdentityUser. On the right side is our program’s Chirp.Core1 folder. It contains the Author class and Cheep class.
In figure 1 it is shown that the Author class inherits from ASP.NET Identity’s class *IdentityUser* . Cheep has an association with the Author class as they both contain instances of each other.s

The two classes, Author and Cheep, are fundamental entities in our program as features build upon their attributes.

![Domain Model](images/DomainModel.png)

## Architecture — In the small
This application was build according to the 'Onion Architecture' to increase maintanablity and testability. Below here is an illustration of the architecture.

![Architecture Small](images/Architecture-in-the-small.png)

**Figur 2:** Onion Architecture. From left to right: UI layer, Service layer, Repository layer, Domain entities.

Onion Architecture divides our program into multiple layers. The outer layers depend on the layers under it. 

The innermost circle of the model is the Domain Entities. It contains the fundamental entities of our program; Author and Cheep. It has no external dependencies.
The next layer of the model is the Repository Interface. This layer contains our repository folder. ICheepRepository is an interface that CheepRepository implements. 
Moving one more layer out is the Service Interface. This is where our services folder is located. Ideally it would contain a CheepService and/or an AuthorService. Instead our Domain Services layer has taken this responsibility.
The last layer and the outermost circle of the model is the Infrastructure layer and here is the Chirp.Web folder. This layer contains all the razor pages, our database, UI and Program.cs. 
Domain Entities:
The innermost circle of the model is Chirp.Core1, which is were the fundamental entities of the program lies; Author and Cheep. 

Repository Layer:
The next layer of the model is the Repository Layer, which is a part of Chirp.Infrastructure in the code. This layers contains the repositories and the repository interfaces. Chirp.Infrastructure also contains the ChirpDBContext class.

Service Layer:
Chirp.Infrastructure

UI Layer:
The last layer and the outermost circle of the model is the UI layer and is represented by Chirp.Web in the code. This Layer contains all the razor pages, the database, and Program.cs.

## System Architecture Overview
------MANGLER-------

![System-Architecture](images/System-Architecture.png)

## Architecture of Deployed Application
Our Diagram shows how the Chirp! application is deployed in a running system. Users access the system through a web browser, and all their requests are sent to the Chirp.Web application (which is hosted on Azure App Service).

The SQLite database is hosted together with the web application, and is only accessible through Chirp.Web. It stores application data such as users and cheeps. 

Authentication is handled using GitHub OAuth. When a user logs in, Chirp.Web communicates with GitHub Auth to authenticate the user and receive the necessary identity information.

![Deployed Architecture](images/Deployed-Application.png)

## User Activities
------MANGLER-------

![Unauthorised/authorised-user](images/Unauthorised/authorised-user.png)

## Follow-Unfollow 
------MANGLER-------

![Follow-Unfollow-Diagram](images/Follow-User.png)

## Forget Me Feature
------MANGLER-------

![Forget-Me-Diagram](images/Forget-Me-Feature.png)

## Login
------MANGLER-------

![Login Diagram](images/Login.png)


## Sequence of Functionality / Calls Through Chirp!
------MANGLER-------

![Sequence Diagram](images/Sequence-of-functionality.png)

# Process

## Build, Test, Release, and Deployment
The activity diagram shows how our GitHub Actions workflow builds and deploys the Chirp! application. The workflow starts when code is pushed to the main branch or when it is started manually. First, the repository is checked out and the .NET environment is set up. Then the application is built and published in release mode, and the resulting files are saved so they can be used later during deployment. After that, the artifact is downloaded, the workflow logs in to Azure, and the application is deployed to the production Azure Web App.

![Build and Deploy Workflow](images/Deployed-Application.png)


## Team Work
**Insert a screenshot of your project board before hand-in:**
![Project Board](images/project-board.png)

   **Which tasks are unresolved?**
   
As a user you are able to download data about your account, however it is not completely resolved, as the user will not be able to download a list of their cheeps, who they follow and who follows them. We have created a CheepDTO class but it is not in use. 
Our program has not been security hardened and is vulnerable to XSS and SQL injection attacks. Therefore, the program is not as secure as we would ideally like it to be. 

   **What features are missing?**
   
Additionally we are missing wild-style features. We would have liked to implement the "like" button. First we would have added a way to store likes in our database, and then make sure a user only can like a cheep once, by storing which user liked the cheep. The button itself would be added next to each cheep in the webpage, and when a user clicks it, the page sends a POST request to the server. The server sees who is logged-in and which cheep is liked, then saves it in the database. The page should reload then and show the "like" on the cheep.

   **Your workflow from issue creation → development → review → merge**
   
The way we would aim to work was by meeting and talking about the requirements of the current week. We would then randomly distribute tasks and each group member would be responsible for creating issues corresponding to their weekly task. 
Some weeks we would meet to code together, depending on what the week required, where one person would host a Code With Me and everyone would join to code. Other weeks we worked separately where each member would do their assigned tasks. Since some weeks required everything to be done in order it would cause members to wait for each other to finish tasks. Later in the development a lot of meetings aimed to be online where members worked separately on features that were missing.
When a new feature was made or a new bug was corrected, a pull request would be made. The team member, who opened it, would write in our group chat “I opened a pull request” and whoever would be available to review the code first would do it. In some instances multiple people have reviewed a code but in most cases only one reviewed before merging the pull request.


## How to Make Chirp! Work Locally
**Describe step-by-step how to clone and run the Chirp! application locally:**

   - Clone instructions
   - Commands to run
   - Required tools or dependencies
   - Expected output or behavior
  
**If you want to run the Chirp! application locally you will have to follow these steps:**

   1. Clone the repository to your laptop by using the command git clone https://github.com/ITU-BDSA2025-GROUP12/Chirp in your windows or 
   2. You will need .NET version 8 installed on your device to run the program.
   3. To run the program change your directory to "Chirp/Src/Chirp.Web"
   4. Write dotnet run in the command line.
   
You should expect a localhost link in your terminal. Press it to open up a webpage, where you can start making an account and send cheeps.


## How to Run Test Suite Locally
----MANGLER-----

# Ethics

## License
The software license we chose for our application is the MIT License. It is a license that .NET uses, so we chose it as well for our project.

## LLMs, ChatGPT, CoPilot, and Others

In our project we have used DeepSeek, ChatGPT and CoPilot.
DeepSeek and ChatGPT have been used for the same purposes. They have been used in our project for debugging and understanding tasks. They have been helpful tools to understand more about the weekly tasks and how to tackle them. While in the case of debugging they have been helpful at times and made no improvement or changes in others.

In some cases DeepSeek or ChatGPT has been used to write code. They would be shown the details of our program that they need as well as the task at hand. Afterwards a prompt would be typed in requesting what specifically is difficult with the implementation. This has actually been less helpful than intended as it has caused us to debug more than write code. Some of the code it has produced has been faulty and the issues have been hard to spot.

It can be hard to understand some of the code DeepSeek and ChatGPT produces as they can overcomplicate things or use packages we have not been introduced to. The person who produced this code with a prompt may be able to understand it, but it makes it harder for another member to refactor as the code might not be similar to what was taught in the lecture. The development of our code has been slowed down due to these issues. The book itself has been more helpful to developing working code than DeepSeek and ChatGPT has. However they have been helpful to bounce ideas off of.

CoPilot has been used for smaller things like assisting some commit messages, which can be quite helpful so that is not the place you get stuck. It has helped speed up the process a little more.

