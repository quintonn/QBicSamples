# Introduction
This **QBicSamples** project contains various examples of the capabilities of the QBic platform as well as sample code for implementing the various features.

The [QBic](https://github.com/quintonn/QBic) platform comprises a back-end web server, that is used to generate the web pages, or front-end, for a fully functional web-based application.  

Most of the relevant code is inside the **Models** and the **Samples** folders.  

The **Models** folder contains the entity classes which correspond to tables that are created in the database by NHibernate.  

The **Samples** folder contains example code for creating various input screens and screen views to display information to an end user.

# Pre-requisites
Before running this samples application, you need to make sure of the following:  

1. You have IIS installed on your development machine.  
2. You run Visual Studio as an administrator when working on QBic projects.
3. Add HTTPS binding to IIS for **Default Site**.
4. Install [URL REWRITE](https://www.iis.net/downloads/microsoft/url-rewrite) module into IIS.  
   
# Running the code
To run the samples:
1. Clone the project
2. Load the solution in Visual Studio (running as admin)
3. Rebuild the application (don't run it)  
   > this restores all nuget repositories
4. Run the application

# Current Samples
Some of the sample features in this repository include:
1. [A basic CRUD sample](https://github.com/quintonn/QBicSamples/tree/master/QBicSamples/Samples/BasicCrudSample)  
   This is the most basic method of creating a view with edit, add and delete input screens
2. [A relatively basic sample](https://github.com/quintonn/QBicSamples/tree/master/QBicSamples/Samples/BasicSample)  
   This example demonstrates how to create views, add, edit and delete screens with more control than the basic CRUD sample
3. [A more advanced sample](https://github.com/quintonn/QBicSamples/tree/master/QBicSamples/Samples/AdvancedSample)  
   This example also has a view and input screens, but allow a lot more control and customization
4. [A multiple screen example](https://github.com/quintonn/QBicSamples/tree/master/QBicSamples/Samples/MultipleViews)  
   This sample shows how to build sequential views and input screens that pass data back and forth
5. [An example where data is filtered depending on the current user](https://github.com/quintonn/QBicSamples/tree/master/QBicSamples/Samples/UserSensitiveData)  
   This example demonstrates how to change the data depending on which user is currently logged in.
6. [An example background process](https://github.com/quintonn/QBicSamples/tree/master/QBicSamples/Samples/BackgroundProcessing)  
   This is an example of setting up a background process that will run automatically
7. [Adding a basic API Controller](https://github.com/quintonn/QBicSamples/blob/master/QBicSamples/Controllers/BasicController.cs)  
   This shows how to add a regular .NET Controller and how to configure the routing
8. [Adding an additional Identity Service](https://github.com/quintonn/QBicSamples/blob/master/QBicSamples/Controllers/CustomUserAuthController.cs)  
   This example shows how it is possible to add and use a custom identity service to use the QBic web server to serve as an Identity Service and secure data repository for an external system such as a web site or mobile application.