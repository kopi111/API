using Application.interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class CrimeAPIService : ICrimeAPIService
    {

        private readonly IConfiguration _configuration;
     
        private readonly ILogger<CrimeAPIService> _logger;

        public CrimeAPIService( ILogger<CrimeAPIService> logger, IConfiguration configuration)
        {
            
            _logger = logger ?? throw new ArgumentNullException();
            _configuration = configuration;
        }
       
     
        public async Task<String> addMissingPerson(missingPerson person)
        {

            try
            {
                string conectiionString = _configuration.GetSection("Authentication:ConnectionStrings:Session").Value;
                // Validate input
                if (person == null)
                {
                    string error1 = "Person is null. Insert aborted.";
                    return error1;
                }

                // Connection string
                //string connectionString = "Data Source=(localdb)\\local;Initial Catalog=JCFDataApi;Integrated Security=True;\r\n";

                // Open the connection using the using statement
                using (SqlConnection connection = new SqlConnection(conectiionString))
                {
                   string error2 = "Attempting to open the connection...";
                    await connection.OpenAsync(); // Use async version of Open method
                    string error3= "Connection opened successfully!";

                    // Perform the database insert operation
                    string insertQuery = @"
                INSERT INTO [JCFDataApi].[dbo].[MissingPersons] 
                ([ImagePath], [Alias], [FirstName], [LastName], [Age], [DateOfBirth],
                 [Community], [Parish], [StreetName],
                 [AreaFrequent], [LastSeen], [AdditionalNotes])
                VALUES 
                (@ImagePath, @Alias, @FirstName, @LastName, @Age, @DateOfBirth,
                 @Community, @Parish, @StreetName,
                 @AreaFrequent, @LastSeen, @AdditionalNotes)";

                    // Use the using statement for SqlCommand
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to the SQL query to prevent SQL injection
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = person.ImagePath.fileName;
                        command.Parameters.Add("@Alias", SqlDbType.NVarChar).Value = person.Alias;
                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = person.FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = person.LastName;
                        command.Parameters.Add("@Age", SqlDbType.Int).Value = person.Age;
                        command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = person.DateOfBirth;
                        command.Parameters.Add("@Community", SqlDbType.NVarChar).Value = person.HomeAddress.Community;
                        command.Parameters.Add("@Parish", SqlDbType.NVarChar).Value = person.HomeAddress.parish;
                        command.Parameters.Add("@StreetName", SqlDbType.NVarChar).Value = person.HomeAddress.StreetName;
                        command.Parameters.Add("@AreaFrequent", SqlDbType.NVarChar).Value = person.AreaFrequent;
                        command.Parameters.Add("@LastSeen", SqlDbType.NVarChar).Value = person.LastSeen;
                        command.Parameters.Add("@AdditionalNotes", SqlDbType.NVarChar).Value = person.AdditionalNotes;

                        // Execute the query
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            return "Data inserted successfully!";
                        }
                        else
                        {
                            return "Data insertion failed.";
                        }
                        
                    }

                    return error2;
                    return error3;
                }
            }
            catch (SqlException ex)
            {
                return $"SQL Error: {ex.Message}";
              
            }
            catch (Exception ex)
            {
               return $"Error: {ex.Message}";
            }
        }

        public Task addStolenItem(stolenItem item)
        {
            throw new NotImplementedException();
        }

        public Task addWantedPersonData(wantedPerson person)
        {
            throw new NotImplementedException();
        }

        public Task removeMissingPerson(missingPerson person)
        {
            throw new NotImplementedException();
        }

        public Task removeStolenItem(stolenItem item)
        {
            throw new NotImplementedException();
        }

        public Task removeWantedPersonData(wantedPerson person)
        {
            throw new NotImplementedException();
        }

        public async Task<List<missingPerson>> ReturnMissingdPersonData()
        {
            try
            {
                string connectionString = _configuration.GetSection("Authentication:ConnectionStrings:Session").Value;
                
              
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync(); // Use async version of Open method

                    // Perform the database select operation
                    string selectQuery = @"
                SELECT [ImagePath], [Alias], [FirstName], [LastName], [Age], [DateOfBirth],
                       [Community], [Parish], [StreetName],
                       [AreaFrequent], [LastSeen], [AdditionalNotes]
                FROM [JCFDataApi].[dbo].[MissingPersons]";

                    // Use the using statement for SqlCommand
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Execute the query and read the results
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<missingPerson> listOfMissingPersons = new List<missingPerson>();

                            while (await reader.ReadAsync())
                            {

                                missingPerson person = new missingPerson
                                {
                                    ImagePath = new picture { fileName = reader["ImagePath"].ToString() },
                                    Alias = reader["Alias"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"]),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    HomeAddress = new address  
                                    {
                                        Community = reader["Community"].ToString(),
                                        parish = reader["Parish"].ToString(),
                                        StreetName = reader["StreetName"].ToString()
                                    },
                                    AreaFrequent = reader["AreaFrequent"].ToString(),
                                    LastSeen = reader["LastSeen"].ToString(),
                                    AdditionalNotes = reader["AdditionalNotes"].ToString()
                                };

                                listOfMissingPersons.Add(person);
                            }

                            return listOfMissingPersons;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw; // Rethrow the exception
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow the exception
            }
        }

        public Task<missingPerson> returnMissingPersonData()
        {
            throw new NotImplementedException();
        }

        public Task<stolenItem> returnStolenItem()
        {
            throw new NotImplementedException();
        }

        public Task<wantedPerson> returnWantedPersonData()
        {
            throw new NotImplementedException();
        }

        public async Task updateMissingPerson(missingPerson person)
        {
            try
            {
                if (person != null)
                {
                    string connectionString = "Data Source=(localdb)\\local;Initial Catalog=JCFDataApi;Integrated Security=True;\r\n";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            Console.WriteLine("Attempting to open the connection...");
                            await connection.OpenAsync(); // Use async version of Open method
                            Console.WriteLine("Connection opened successfully!");

                            // Perform the database update operation here
                            string updateQuery = @"
                        UPDATE [JCFDataApi].[dbo].[MissingPersons]
                        SET [ImagePath] = @ImagePath, 
                            [Alias] = @Alias,
                            [FirstName] = @FirstName,
                            [LastName] = @LastName,
                            [Age] = @Age,
                            [DateOfBirth] = @DateOfBirth,
                            [Community] = @Community,
                            [Parish] = @Parish,
                            [StreetName] = @StreetName,
                            [AreaFrequent] = @AreaFrequent,
                            [LastSeen] = @LastSeen,
                            [AdditionalNotes] = @AdditionalNotes
                        WHERE [PersonId] = @PersonId"; // Assuming PersonId is the primary key

                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                // Add parameters to the SQL query to prevent SQL injection
                         
                                command.Parameters.AddWithValue("@ImagePath", person.ImagePath);
                                command.Parameters.AddWithValue("@Alias", person.Alias);
                                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                                command.Parameters.AddWithValue("@LastName", person.LastName);
                                command.Parameters.AddWithValue("@Age", person.Age);
                                command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                                command.Parameters.AddWithValue("@Community", person.HomeAddress.Community);
                                command.Parameters.AddWithValue("@Parish", person.HomeAddress.parish);
                                command.Parameters.AddWithValue("@StreetName", person.HomeAddress.StreetName);
                                command.Parameters.AddWithValue("@AreaFrequent", person.AreaFrequent);
                                command.Parameters.AddWithValue("@LastSeen", person.LastSeen);
                                command.Parameters.AddWithValue("@AdditionalNotes", person.AdditionalNotes);

                                // Add other parameters similarly

                                // Execute the query
                                int rowsAffected = await command.ExecuteNonQueryAsync();

                                // Check if the update was successful
                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Data updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Data update failed. Person not found.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        finally
                        {
                            // Ensure the connection is closed even if an exception occurs.
                            if (connection.State == System.Data.ConnectionState.Open)
                            {
                                connection.Close();
                                Console.WriteLine("Connection closed.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error outside try block: {ex.Message}");
            }
        }


        public Task updateStolenItem(stolenItem item)
        {
            throw new NotImplementedException();
        }

        public Task updateWantedPersonData(wantedPerson person)
        {
            throw new NotImplementedException();
        }
    }
}
