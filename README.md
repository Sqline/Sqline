![Sqline](Wiki/Sqline.png) 
Sqline
======

Sqline is a SQL-first DataAccess Framework for .NET

### Download

[Milestone 1 Release v0.1](https://github.com/Sqline/Sqline/releases/download/m1-release-v0.1/Sqline-v0.1-m1.vsix) (not recommended for production use)

### Quick Start Guide
https://github.com/Sqline/Sqline/wiki

### Features
- Generates classes to handle common Insert, Update and Delete scenarios
- Write SQL statements and have all the boring work taken care of while still enjoying strongly-typed C# classes for your results.
- Sqline generates pure "hand-written" ADO.NET - no complicated framework on top, full transparency.
- Uses T4 templates for all code generation (custom templates will be supported later!) also allowing full transparency into the code generation process.
- Pure ADO.NET also means no memory or performance hit of using Sqline
- Transaction support
- Can be used for any type of C#/.NET application, but is especially suitable for high performance and scalable webapplications or server solutions where you need to be in control of what exactly gets executed against your DB systems without any memory or performance overhead that most other dataccess frameworks add.

### Why another framework?

Most DataAccess Frameworks today have a "code-first" approach, treating databases as a persistence medium for code objects and trying to hide the use of SQL. They usually make it very easy to perform CRUD actions on full table rows with 1 to 1 mappings to class objects, but lack good support for optimized SQL statements. Furthermore they add layers of overhead and abstractions to the process, making them both slow and less transparent.

For Enterprise solutions we believe this is backwards and Sqline is therefore build to exploit the full power of SQL statements and will help generate supportive code objects instead.

Sqline is a code generator (taking a small hit at compile time instead of at run time!) that generates hand-written ADO.NET code, both ensuring optimal peformance and full transparency into how your statements are executed against the database.
Using a templating system based on the T4-standard, Sqline is fully customizable and advanced users will be able to change the code generation to match the requirements of their specific projects.

### How does it work?

Sqline uses XML at the core, consider the following sample:
```xml
	<viewitem name="User">
		<field name="Firstname" type="varchar" />
		<field name="Lastname" type="varchar" />
		<field name="Age" type="int" />
		
		<method name="GetUsers">
			<sql>
				SELECT U.Firstname, U.Lastname, UD.Age 
				FROM [User] U 
				INNER JOIN [UserDetail] UD ON U.UserID = UD.UserID
			</sql>
		</method>
	</viewitem>
```
This will generate a User object with the properties Firstname, Lastname and Age and a method that can read users based on the specified query.

We consider this a "viewitem" - the SQL represents the "view" of data you want to grab (which can be very complex SQL statements) and a code object is created that models this "view". No need to pull out all fields of a table, like other DA solutions tends to - just grab the data you are interested in using a standard SQL query.

### Insert, Update, Delete?

While all operations can be performed using plain SQL defined in the XML files, Sqline does come with a bit more code-support for common operations. 

Sqline extracts all schema-data from the database and creates what we call "dataitems" based on this schema - these items maps directly to the tables of your database.

Here's how we can Insert, Update and Delete users in our sample user table:
```cs
new UserInsert("Henry", "James").Execute();

new UserUpdate { Firstname = "William", WhereLastname = "James", }.Execute();

new UserDelete { WhereLastname = "James" }.Execute();
```

### Modular design
Sqline consists of several independent applications:
* **Schemalizer**: Responsible for extracting database schema information into an XML representation
* **T4Compiler**: Custom implementation of a  [T4 template](http://msdn.microsoft.com/en-us/library/bb126478.aspx) compiler independent of Visual Studio
* **Sqlingo** [upcoming]: SQL Parser (which will eventually allow us to deduce the types of columns in select statements so we can skip the field definitions)
* **Sqline**: DataAccess Framework and Visual Studio plugin (utilizing the other applications)
 
Schemalizer and T4Compiler can be used in any project that needs to extract database schema information and transform that data into Code or other documents - feel free to use.

### State of project
Sqline is still a work-in-progress, but feel free to play around with it.

### Current limitations
* Sqline is not in a production ready state, **use at own risk**.
* Sqline can currently only produce C# code
* While some MySQL support is implemented, Sqline only supports MSSQL at this stage.
* A console version is planned, but currently Sqline is only working through the supplied Visual Studio Package.
* T4Compiler is not fully compatible with the entire T4 standard, but have support for all of the most common features.
* Schemalizer currently only supports MySQL and MSSQL.
* Documentation is still lacking as it is not yet ready for professional use, however see our [Quick Start Guide](https://github.com/Sqline/Sqline/wiki)
