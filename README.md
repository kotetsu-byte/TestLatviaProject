1. Create PostreSQL Database, call it whatever you want.
2. Register the database into connection string adding properties correctly.
3. Remove old migrations (remove folder "Migrations")
4. Drop the database (In Package Manager console, insert "drop-database")
5. Add new migration (In Package Manager console, insert "add-migration <Name>")
6. Update database (In Package Manager console, insert "update-database")
7. Start the project
