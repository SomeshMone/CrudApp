using System;
using System.Data.SqlClient;

namespace CrudApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection; 
            string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=CoDb;Integrated Security=True;TrustServerCertificate=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            try
            {
                Console.WriteLine("Connection Established Successfully");
                string answer;
                do
                {
                    Console.WriteLine("Select from the options below \n1. Creation\n2. Retrieve\n3. Update\n4. Delete");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {   //Create => C
                        case 1:
                            Console.WriteLine("Enter user name:");
                            string user_name = Console.ReadLine();

                            Console.WriteLine("Enter user age:");
                            int user_age = int.Parse(Console.ReadLine());

                            string insertQuery = "INSERT INTO DETAILS(user_name, user_age) VALUES('" + user_name + "', " + user_age + ")";
                            SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                            insertCommand.ExecuteNonQuery();
                            Console.WriteLine("Data is inserted successfully");
                            break;

                        //Retrieve data=> R
                        case 2:
                            string displayQuery = "SELECT * FROM Details";
                            SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
                            SqlDataReader dataReader = displayCommand.ExecuteReader();
                            while (dataReader.Read())
                            {
                                Console.WriteLine("Id: " + dataReader.GetValue(0));
                                Console.WriteLine("Name: " + dataReader.GetValue(1));
                                Console.WriteLine("Age: " + dataReader.GetValue(2));
                            }
                            dataReader.Close();
                            break;
                        //Update => U
                        case 3:
                            Console.WriteLine("Enter user id that you want to update:");
                            int u_id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter user age to update:");
                            int u_age = int.Parse(Console.ReadLine());
                            string updateQuery = "UPDATE Details SET user_age = " + u_age + " WHERE user_id = " + u_id;
                            SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                            updateCommand.ExecuteNonQuery();
                            Console.WriteLine("Update Successful");
                            break;
                        //Delete => D
                        case 4:
                            Console.WriteLine("Enter user id you want to delete from table:");
                            int user_id = int.Parse(Console.ReadLine());
                            string deleteQuery = "DELETE FROM Details WHERE user_id = " + user_id;
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                            deleteCommand.ExecuteNonQuery();
                            Console.WriteLine("Deletion Successful");
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                    Console.WriteLine("Do you want to continue? (Yes)");
                    answer = Console.ReadLine();
                } while (answer.ToLower() == "yes");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
