---
title: "Chirp! Project Report"
author: "Group 12"
date: "2025"
---

# Design and Architecture

## Domain Model
Provide an illustration of your domain model.  
Make sure it is correct and complete.

Insert diagram here:
![Domain Model](images/domain-model.pdf)

## Architecture — In the small
Illustrate the organization of your code base.  
Explain which layers exist in your onion architecture and what belongs to each layer.

Insert diagram here:
![Architecture Small](images/architecture-small.pdf)

## Architecture of Deployed Application
Illustrate the architecture of your deployed client-server application.  
Show where the server is deployed and how the client communicates with it.

Insert diagram here:
![Deployed Architecture](images/deployed-architecture.pdf)

## User Activities
Illustrate typical user journeys:

- First page shown to an unauthorized user  
- What an unauthorized user can do  
- What an authenticated user can do  

Insert activity diagrams or screenshots here:
![User Activities](images/user-activities.pdf)

## Sequence of Functionality / Calls Through Chirp!
Create a UML sequence diagram showing message flow:

- Start with an HTTP request from an unauthorized user  
- End with the fully rendered web page returned  

Include all relevant calls (HTTP, C#, etc.)

Insert diagram here:
![Sequence Diagram](images/sequence-diagram.pdf)

# Process

## Build, Test, Release, and Deployment
Add a UML activity diagram showing your GitHub Actions workflow.  
Briefly describe how your application is built, tested, released, and deployed.

Insert diagram here:
![Build Workflow](images/build-workflow.pdf)

## Team Work
Insert a screenshot of your project board before hand-in:
![Project Board](images/project-board.png)

Briefly describe:

- Which tasks are unresolved  
- What features are missing  
- Your workflow from issue creation → development → review → merge  

## How to Make Chirp! Work Locally
Describe step-by-step how to clone and run the Chirp! application locally:

- Clone instructions  
- Commands to run  
- Required tools or dependencies  
- Expected output or behavior  

## How to Run Test Suite Locally
Describe how to run your test suites:

- Required steps  
- How to execute tests  
- What types of tests you have (unit, integration, etc.)  
- What they test  

# Ethics

## License
State the software license you chose for your application.

The software license we chose for our application is the **MIT License**. It is a license that .NET uses, so we chose it as well for our project.

## LLMs, ChatGPT, CoPilot, and Others
Describe:

- Which LLM(s) you used (if any)  
- How and when you used them  
- How helpful they were  
- Whether they sped up or slowed down development

In our project we have used **DeepSeek** and **CoPilot**.

DeepSeek has been used in our project for debugging and understanding tasks. In some cases it has been used to write code, however this has actually been less helpful as it has caused us to debug more than write code. It can be hard to understand some of the code DeepSeek produces as it can overcomplicate things or use packages we have not been introduced to. The person who produced this code may be able to understand it but it makes it harder for another member to refactor without removing function. This has all caused the development of our code to be slowed down. The book itself has been more helpful to developing the code than DeepSeek has. Using AI as a debugging tool has been helpful in some cases and done no improvement or change in others.

CoPilot has been used for smaller things like assisting some commit messages, which can be quite helpful so that is not the place you get stuck. It has helped speed up the process a little more.
