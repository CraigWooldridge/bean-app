All The Beans Bean App
======================

Software requirements
---------------------

* Visual Studio IDE
* .net Core 3.1 sdk
* Microsoft SQL server

App Structure
-------------------

The app attempts to follow Domain Driven Design (DDD)principles and is split into 5 different projects under the “TombolaWebApp” solution:

* BeanApp.API contains all the controllers and HTML views for the front end off the web app, it also contains the project Startup.cs and Program.cs files. Inside this projects wwwroot   
folder there is an uploads folder which is where all images associated with the app are stored.

* BeanApp.Domain contains the entity models and repository interfaces for the app. This is the core of the project where the bean properties are defined.

* BeanApp.Services acts as an intermediate layer between the domain and Infrastructure projects and protects the domain entities from direct exposure in the API.

* BeanApp.Infrastructure is where the entities are saved to the local database, this project also contains the database migration classes and the database seed data.

* BeanApp.Tests contains some examples of unit tests done on the beans Controller and the Bean Service, to demonstrate how Nunit and Moq are used to test the app. The app currently sits   
at 46% test coverage from these examples of testing.

Running the App
-------------------

**Adding a Migration**
To add a migration in visual studio navigate to tools => NuGet Package Manager => Package Manager Console
In here run Add-Migration NAME_OF_MIGRATION

**Running Database Migrations**
To run migrations in visual studio navigate to tools => NuGet Package Manager => Package Manager Console
In here run Update-Database

When Bulding the app for the first time, a database migration will need to be performed, follow information under "Running Database Migrations" for this.

Using the App
---------------------
The database seed data should have populated the local databse with a couple of beans upon building, once clicking on the bean of the day button this should reveal a bean.

Beans can also be created by navigating to the “Create Beans” tab, this will navigate you to a login page. The seed data does not contain any user data so a user account will need to be  
created for the bean seller. Navigating to “Register” will allow users to be created. 

From here, the create beans tab can be accessed and it will allow the logged in bean seller to create, view, update or delete upcoming beans of the day. Any beans added will be saved to the database and   
will still appear up after re-running the app.

