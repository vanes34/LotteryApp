# LotteryApp

## You need the following tools during application development:
.NET Core SDK (version 8.0.201 )
Microsoft SQL Server 2022 (RTM-GDR) (KB5032968) - 16.0.1110.1 (X64) 
SQL Server Management Studio (SSMS)

## Open SQL Server Management Studio (SSMS):
Open SSMS and connect to your local SQL Server instance.
In the Object Explorer in SSMS under Databases/System Databases/master 

## Open the Program.cs file in the LotteryApp project.
Update the connectionString variable with your SQL Server connection string.
Make sure to replace Data Source with your server name if it's different.

## Running the Application locally
Enter one of the following commands:
Run: Executes a lottery draw, stores the result in the database, and displays the result.
History: Displays the history of lottery draws.
Exit: Exits the application.
