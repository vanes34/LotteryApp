# LotteryApp

## You need the following tools during application development:
<br>.NET Core SDK (version 8.0.201 ) </br> 
<br>Microsoft SQL Server 2022 (RTM-GDR) (KB5032968) - 16.0.1110.1 (X64) <br/>
<br>SQL Server Management Studio (SSMS) </br>

## Open SQL Server Management Studio (SSMS):
<br>Open SSMS and connect to your local SQL Server instance.</br>
<br>In the Object Explorer in SSMS under Databases/System Databases/master </br>

## Open the Program.cs file in the LotteryApp project.
<br>Update the connectionString variable with your SQL Server connection string.</br>
<br>Make sure to replace Data Source with your server name if it's different.</br>

## Running the Application locally  Program.cs
<br>Enter one of the following commands:</br>
<br>Run: Executes a lottery draw, stores the result in the database, and displays the result.</br>
<br>History: Displays the history of lottery draws.</br>
<br>Exit: Exits the application.</br>
