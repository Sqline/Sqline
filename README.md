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
