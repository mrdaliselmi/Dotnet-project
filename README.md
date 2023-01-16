# <img src="Bikes/wwwroot/lib/images/logo.png" alt="logo" width="40px"/> MotoZOOM
![image](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![image](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![image](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
## Description
**MotoZOOM** is a bike selling website developed as an end of the semester project for a **Framework and Development university** course** by two software engineering students. The website allows users to browse and contact sellers of various types of bikes, as well as provide a platform for sellers to list their bikes for sale
The website features a user-friendly interface, a detailed search and filter system, and an admin dashboard to perform CRUD operation (Create, Retrieve, Update, Delete) and add users with executive role on the products. The website is built using **ASP.NET MVC** framework and **Entity Framework** for database management.
## Installation instructions
1. **Prerequisites:** 
   
   Make sure you have the following software installed on your machine before proceeding:
   - Visual Studio (preferably the latest version).
   - .NET Framework (version 4.5 or above).
   - SQL Server (or an alternative database management system)
2. **Clone the repository:** 
   
   You can clone the repository by running the following command in your terminal:
    - `git clone https://github.com/Lakhdher/Dotnet-project.git`
3. **Open the solution file:**
   
    Open the solution file (with the `.sln` extension) in Visual Studio.
4. **Restore Nuget packages:** 
   
   In the Solution Explorer, right-click on the solution file and select "Restore Nuget packages". This will download and install all the necessary packages for the project
5. **Build the project:** 
   
   In the menu, click on `Build` and then `Build Solution` to build the project.
6. **Update the connection string:** 
   
   The connection string for the database is located in the [Web.config file](Bikes/appsettings.json).Update the connection string with your database details.
   ```json
   "ConnectionStrings": { "Default": "server="<enter Server name>" ; database="<enter Database name>" ; integrated security="<integrated security>" ; TrustServerCertificate="<True/False>"" }
   ```
7. **Run the project:** 
   
   Press F5 or click on `Debug` and then `Start Debugging` to run the project. The application will launch in the browser.
8.  **Create database:**
   
    If you don't have the database created, please run the database script provided in the project or use the Entity framework database migration to create the database.
## How does it work

## Contributing
We welcome contributions to the MotoZOOM project. If you are interested in contributing, please follow these guidelines:

1. **Fork the repository:** Click on the "Fork" button at the top of the GitHub page to create a copy of the repository in your own account.
2. **Clone the repository:** Clone the repository to your local machine by running the following command:
   `git clone https://github.com/Lakhdher/Dotnet-project.git`
3. **Create a new branch:** Create a new branch on your local machine by running the following command:
   `git branch new-branch-name`
4. **Make changes:** Make the necessary changes to the code and test them thoroughly.
5. **Commit and push:** Commit your changes and push them to your fork by running the following commands:
   ```bash
   git add .
   git commit -m "your commit message"
   git push origin new-branch-name
   ```
6. **Create a pull request:** Go to the GitHub page of your fork, and click on the "New pull request" button. Select your branch, and submit the pull request.
   
Please make sure that your pull request adheres to the project's coding conventions and that it has a clear and concise commit message that describes the changes made.
## License information

Copyright 2023 Mohamed Ali Selmi & Nour Eddine Ben Nejma

*Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:*

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.*
