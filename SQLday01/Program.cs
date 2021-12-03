using System;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace SQLday01
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ajvan\source\repos\SQLday01\SQLday01\Database1.mdf;Integrated Security=True");
            connection.Open();

            while (run == true)

          
            {

                Console.WriteLine("Would you like to A)dd an entry into the database, S)earch for entries in the database, U)pdate the program, D)elete an entry or e)xit the program?");
                string menu = Console.ReadLine().ToLower();
                if (menu == "a")
                {
                    Console.WriteLine("What is the contact's name?");
                    string user_name = Console.ReadLine();
                    Console.WriteLine("What is the contact's phone number?");
                    string user_number = Console.ReadLine();
                    Console.WriteLine("What is the user's email address?");
                    string user_email = Console.ReadLine();
                    Console.WriteLine("What city does the user live in?");
                    string user_city = Console.ReadLine();

                    SqlCommand command = new SqlCommand($"INSERT INTO AddressBook(Name, Phone, Email, City) VALUES('{user_name}', '{user_number}', '{user_email}','{user_city}')", connection );

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                else if (menu == "s")
                {
                    Console.WriteLine("Did you want to seach by Name, phone, email, or city?");
                    menu = Console.ReadLine().ToLower();

                    if (menu == "name")
                    {
                        Console.WriteLine("What name are you looking for?");
                        string user_name = Console.ReadLine().ToLower();

                        SqlCommand command = new SqlCommand($"Select * From AddressBook Where Name = '{user_name}'", connection);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Console.WriteLine($"ID number:{reader["Id"]} Name:{reader["Name"]} Email:{reader["email"]} Phone:{reader["phone"]} City:{reader["city"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, no one is named {user_name} in our database");
                        }
                        reader.Close();
                    }
                    else if (menu == "city")
                    {
                        Console.WriteLine("What city would you like to search in");
                        string user_city = Console.ReadLine();

                        SqlCommand command = new SqlCommand($"Select * From AddressBook Where city = '{user_city}'", connection);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Console.WriteLine($"ID number:{reader["Id"]} Name:{reader["Name"]} Email:{reader["email"]} Phone:{reader["phone"]} City:{reader["city"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, no one lives in {user_city}");
                        }
                        reader.Close();
                    }
                    else if (menu == "phone")
                    {
                        Console.WriteLine("What number are you looking for?");
                        string user_number = Console.ReadLine();

                        SqlCommand command = new SqlCommand($"Select * From AddressBook Where phone = '{user_number}'", connection);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Console.WriteLine($"ID number:{reader["Id"]} Name:{reader["Name"]} Email:{reader["email"]} Phone:{reader["phone"]} City:{reader["city"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, no one has {user_number} in our database");
                        }
                        reader.Close();
                    }
                    else if (menu == "email")
                    {
                        Console.WriteLine("What email are you looking for?");
                        string user_email = Console.ReadLine();

                        SqlCommand command = new SqlCommand($"Select * From AddressBook Where email = '{user_email}'", connection);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Console.WriteLine($"ID number:{reader["Id"]} Name:{reader["Name"]} Email:{reader["email"]} Phone:{reader["phone"]} City:{reader["city"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, no one has  {user_email} in our database");
                        }
                        reader.Close();
                    }

              
                }
                else if (menu == "u")
                {
                    //UPDATE
                    string userName = ""; // this will eventually be the "where" clause in the sql command
                    string userField = ""; // this will be the (column)information that they want to be updated
                    string newData = ""; // this will be the replacement data

                    Console.WriteLine("Enter a name for the person who's information you'd like to update"); // ask user for thing
                    userName = Console.ReadLine(); // get thing
                    Console.WriteLine("What would you like to update about their entry in the database? Phone? Email? City?"); 
                    userField = Console.ReadLine();
                    Console.WriteLine($"set your the new data for {userField} here");
                    newData = Console.ReadLine();


                    SqlCommand command = new SqlCommand($"UPDATE AddressBook SET [{userField}] = '{newData}' WHERE Name = '{userName}'", connection);




                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine($"Your new contact has been saved as ID number:{reader["Id"]} Name:{reader["Name"]} Email:{reader["email"]} Phone:{reader["phone"]} City:{reader["city"]}");

                    reader.Close();


                }
                else if (menu == "d")
                {
                    string theDamned = "";
                    string makingSure = "";

                    Console.WriteLine("Do you want to seach by name or ID number?");
                    menu = Console.ReadLine().ToLower();

                    if (menu == "name")
                    {
                        Console.WriteLine("Enter the name of the person you want to delete");
                        theDamned = Console.ReadLine();

                        SqlCommand command = new SqlCommand($"Select * From AddressBook Where Name = '{theDamned}'", connection);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                Console.WriteLine($"you entered {reader["Name"]}... are you sure you want to delete them?"); //maybe disply some extra information relative to the reader name
                            }
                        }
                        reader.Close();
                        makingSure = Console.ReadLine().ToLower();
                        if (makingSure == "y")
                        {
                            SqlCommand command2 = new SqlCommand($"DELETE From AddressBook Where Name = '{theDamned}'", connection);
                            SqlDataReader reader2 = command2.ExecuteReader();
                            reader2.Close();
                        }
                        else
                        {
                            Console.WriteLine("Yeah. That's what I thought");
                        }
                    }
                    else if (menu == "id")
                    {
                        Console.WriteLine("Enter the ID number of the person you want to delete");
                        theDamned = Console.ReadLine();

                        SqlCommand command = new SqlCommand($"Select * From AddressBook Where Id = '{theDamned}'", connection);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                Console.WriteLine($"you entered {reader["Id"]} which is entry {reader["Name"]}... are you sure you want to delete them? Please type exactly 'I am of sound body and mind'"); //maybe disply some extra information relative to the reader name
                            }
                        }
                        reader.Close();
                        makingSure = Console.ReadLine();
                        if (makingSure == "I am of sound body and mind")
                        {
                            SqlCommand commandScary = new SqlCommand($"DELETE From AddressBook Where Id = '{theDamned}'", connection);
                            SqlDataReader reader2 = commandScary.ExecuteReader();
                            reader2.Close();
                        }
                        else
                        {
                            Console.WriteLine("Yeah. That's what I thought");
                        }
                    }    

                }
                else if (menu == "e")
                {
                    Console.WriteLine("Thank you for using the Database code I wrote.... sorry it kinda sucks!");
                    Console.WriteLine("Goodbye");
                    run = false;
                }
                else
                {
                    Console.WriteLine("Sorry... I don't think that's one of the options I gave you... wanna try that again?");
                }
            }
            connection.Close();
        }
    }
}


