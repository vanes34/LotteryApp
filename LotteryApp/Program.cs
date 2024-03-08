using System.Data.SqlClient;

namespace LotteryApp
{
    class Program
    {
        // the connection string to the database
        static string connectionString = @"Data Source=WINDOWS-21H2GFU;Initial Catalog=master;Integrated Security=True;";
        static int[] drawHistory = new int[0]; // Define drawHistory that we are going to store the numbers

        static void Main(string[] args)
        {
             Console.ForegroundColor = ConsoleColor.Blue;
            // check if the table exists, if not, create a new one
            EnsureTableExists();

            // infinite loop for user input
            while (true)
            {
                Console.WriteLine("Enter command (Run/History/Exit):");
                // Read user input 
                string command = Console.ReadLine().Trim().ToLower();

                switch (command)
                {
                    case "run":
                        RunLotteryDraw();
                        break;
                    case "history":
                        DisplayDrawHistory();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }

        // function to ensure that the table exists in the database
        static void EnsureTableExists()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Check if the table exists in the database
                    string query = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DrawResults')" +
                                   "BEGIN" +
                                   "    CREATE TABLE DrawResults (" +
                                   "        Id INT PRIMARY KEY IDENTITY," +
                                   "        DrawDateTime DATETIME," +
                                   "        Number1 INT," +
                                   "        Number2 INT," +
                                   "        Number3 INT," +
                                   "        Number4 INT," +
                                   "        Number5 INT" +
                                   "    )" +
                                   "END";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring table exists: {ex.Message}");
                Environment.Exit(1);
            }
        }

        
        // method to execute the lottery draw
        static void RunLotteryDraw()
        {
            Console.WriteLine("Running lottery draw... ");

            // simulate a loading delay
            System.Threading.Thread.Sleep(4000); // 4 seconds delay 

            int[] drawResult = GenerateRandomNumbers();
            StoreDrawResultInDatabase(drawResult);
            DisplayDrawResult(drawResult);
            AddToDrawHistory(drawResult); // Add the draw result to the drawHistory array
            Console.WriteLine("Lottery draw completed.");
        }


        // method to display the lottery draw history
        static void DisplayDrawHistory()
        {
            Console.WriteLine("Lottery Draw History:");
            if (drawHistory.Length == 0)
            {
                Console.WriteLine("No draw history available.");
                return;
            }

            // display draw history in groups of 5 numbers,when to insert a comma and when to start a new line
            for (int i = 0; i < drawHistory.Length; i++)
            {
                Console.Write(drawHistory[i]);
                if ((i + 1) % 5 != 0 && i != drawHistory.Length - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        // method to generate random numbers for the lottery draw
        static int[] GenerateRandomNumbers()
        {
            int[] drawResult = new int[5];
            Random random = new Random();
            int index = 0;

            // generate random numbers, until we have 5 unique numbers
            while (index < 5)
            {
                int number = random.Next(1, 51);
                if (!ArrayContains(drawResult, number))
                {
                    drawResult[index++] = number;
                }
            }
            return drawResult;
        }

        // method to check if a number exists in an array
        static bool ArrayContains(int[] array, int value)
        {
            foreach (int num in array)
            {
                if (num == value)
                {
                    return true;
                }
            }
            return false;
        }

        // method to display the lottery draw result
        static void DisplayDrawResult(int[] drawResult)
        {
            Console.WriteLine("Lottery Draw Result:");
            Console.WriteLine(string.Join(", ", drawResult)); // Displaying the numbers with comma
        }

        // method to add the draw result to the draw history
        static void AddToDrawHistory(int[] drawResult)
        {
            int[] newDrawHistory = new int[drawHistory.Length + drawResult.Length];
            Array.Copy(drawHistory, newDrawHistory, drawHistory.Length);
            Array.Copy(drawResult, 0, newDrawHistory, drawHistory.Length, drawResult.Length);
            drawHistory = newDrawHistory;
        }

        // method to store the draw result in the database
        static void StoreDrawResultInDatabase(int[] drawResult)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Console.WriteLine("Connected to the database successfully");
                    string query = "INSERT INTO DrawResults (DrawDateTime, Number1, Number2, Number3, Number4, Number5) " +
                                   "VALUES (@DrawDateTime, @Number1, @Number2, @Number3, @Number4, @Number5)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DrawDateTime", DateTime.Now);
                    for (int i = 0; i < drawResult.Length; i++)
                    {
                        command.Parameters.AddWithValue($"@Number{i + 1}", drawResult[i]);
                    }
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error storing draw result in database: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}
