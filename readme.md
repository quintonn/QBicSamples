# Introduction
This **QBicSamples** project contains various examples of the capabilities of the QBic platform as well as sample code for implementing the various features.

The [QBic](https://github.com/quintonn/QBic) platform comprises a back-end web server, that is used to generate the web pages, or front-end, for a fully functional web-based application.  

Most of the relevant code is inside the **Models** and the **Samples** folders.  

The **Models** folder contains the entity classes which correspond to tables that are created in the database by NHibernate.  

The **Samples** folder contains example code for creating various input screens and screen views to display information to an end user.

# Pre-requisites
Before running this samples application, you need to make sure of the following:  

1. You have added a Nuget Package Source that contains the QBic platform binaries.  
2. You have IIS installed on your development machine.  
3. You run Visual Studio as an administrator when working on QBic projects.
4. Add HTTPS binding to IIS for **Default Site**.
5. Install [URL REWRITE](https://www.iis.net/downloads/microsoft/url-rewrite) module into IIS.  
   
# Running the code
To run the samples:
1. Clone the project
2. Load the solution in Visual Studio (running as admin)
3. Rebuild the application (don't run it)  
   > this restores all nuget repositories
4. Run the application
