![Sqline](Wiki/Sqline.png) 
Sqline
======

Sqline is a SQL-first DataAccess Framework for .NET (in a very alpha-state)

### Why another framework?

Most DataAccess Frameworks today have a "code-first" approach, treating databases as a persistence medium for code objects and trying to hide the use of SQL to the point where developers don't have to know SQL at all!

We believe this is totally backwards for any professional use - Sqline is therefore built to let you exploit the full power of SQL and will instead help you generate supportive code objects.

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
Schemalizer and T4Compiler are considered stable.

Sqline is still a work-in-progress, basic features work, but more advances features are not yet fully implemented.

### Current limitations
* Sqline is not in a production ready state, **do only use it at an educational and experimental level**.
* Sqline can currently only produce C# code
* While some MySQL support is implemented, currently only MSSQL is considered supported.
* A console version is planned, but currently Sqline is only working through the supplied Visual Studio Package.
* T4Compiler is not fully compatible with the entire T4 standard, but most basic syntax is supported.
* Schemalizer only works against MySQL and MSSQL servers.
* No documentation, installers or configuration guides are currently available.
