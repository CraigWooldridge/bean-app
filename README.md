All The Beans Bean App
======================
The “All the Beans” Web app is a ASP.NET Core MVC web application that is runnable locally. It saves bean data to a local database which should be created with some seed data on your  
machine when running the app for the first time. 

Software requirements
---------------------

* Visual Studio IDE
* .NET Core 3.1 sdk
* Microsoft SQL server

App Structure
-------------------

The app attempts to follow Domain Driven Design (DDD) principles and is split into 5 different projects under the “TombolaWebApp” solution:

* ```BeanApp.API``` contains all the controllers and HTML views for the front end off the web app, it also contains the project ```Startup.cs``` and ```Program.cs``` files. Inside this   
* projects ```wwwroot``` folder there is an uploads folder which is where all images associated with the app are stored.

* ```BeanApp.Domain``` contains the entity models and repository interfaces for the app. This is the core of the project where the bean properties are defined.

* ```BeanApp.Services``` acts as an intermediate layer between the domain and Infrastructure projects and protects the domain entities from direct exposure in the API.

* ```BeanApp.Infrastructure``` is where the entities are saved to the local database, this project also contains the database migration classes and the database seed data.

* ```BeanApp.Tests``` contains some examples of unit tests done on the beans Controller and the Bean Service, to demonstrate how ```Nunit``` and ```Moq``` are used to test the app. The app currently  
* sits at 46% test coverage from these examples of testing.

Running the App
-------------------
**Clone the Application**
To clone the repository, in the git command line run:
```
git clone https://gitlab.com/craig.wooldridge/tombola-bean-app.git
```

**Viewing/ Editing connection String**
In Visual Studio click on the ```BeanApp.API``` project and select the ```appsettings.json``` file. Fill the default connection variable with the correct database connection string.
```
 "ConnectionStrings": {
    "DefaultConnection": LOCAL_DB_STRING or DEV_DB_STRING
  }
  ```
Since this app will only ever be ran locally, the connection string variable has been stored in the ```appsettings.json``` file so a new one does not need to be entered when first running the app.   
If this app were to be deployed, this variable would be set in the environment variables.

**Starting the app**
The ```TombolaWebApp``` solution file will open the project. When opening the solution for the first time, a database migration will need to be performed to create and populate a local database   
with some bean data, follow information under ```Running Database Migrations``` to complete this. From there, the app should be ready to build, making sure build mode is set to ISS Express.

**Adding a Migration**  
To add a database migration in Visual Studio, navigate to ```tools => NuGet Package Manager => Package Manager Console```
In here run:
```
Add-Migration NAME_OF_MIGRATION
```

**Running Database Migrations**  
To run database migrations in Visual Studio, navigate to ```tools => NuGet Package Manager => Package Manager Console```
In here run: 
```
Update-Database
```
This will need to be performed before building the app for the first time.

Using the App
---------------------
The database seed data should have populated the local database with a few beans upon building, once clicking on the bean of the day button this should reveal a bean.

Beans can also be created by navigating to the “Create Beans” tab, this will navigate you to a login page. The seed data does not contain any user data so a user account will need to be 
created for the bean seller. Navigating to “Register” will allow users to be created. In a real word example this register tab would not be accessible, but it has been left in to demonstrate  
how new users of the platform would be created.

Once a user is created and logged in, the create beans tab can be accessed and it will allow the logged in bean seller to create, view, update or delete upcoming beans of the day. Any beans   
added will be saved to the database and will still appear up after re-running the app.

