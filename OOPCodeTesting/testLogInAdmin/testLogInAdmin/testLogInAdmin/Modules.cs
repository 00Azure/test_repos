using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataManagement
{
    public class Student
    {
        private decimal _perUnit = 453;
        public string IDNumber { get; set; }
        public string Name { get; set; }

        public decimal TotalUnits { get; set; }
        public virtual decimal getTuitionFee()
        {
            return this.TotalUnits * _perUnit;
        }
    }
    public class Scholar : Student
    {
        public string ScholarshipGrants{ get; set; }
        public decimal Discount { get; set; }

        public decimal PercentageDiscount { get; set; }

        public override decimal getTuitionFee()
        {
            return base.getTuitionFee() - Discount;
        }
    }
    public struct Credentials // struct
    {
        public string Server {  get; set; }
        public string Database{ get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Driver;
    }
    public struct Users 
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string LevelOfAccess { get; set; }    
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Repository // for database 
    {
        OdbcConnection connection = new OdbcConnection(); // instantiate objects for data.odbc
        OdbcCommand command;
        OdbcDataAdapter adapter;
        OdbcDataReader reader;
        Credentials credentials = new Credentials(); // credentials
        string connectionString = "";
        
        public string ErrorMessage = ""; // fields
        public bool isErrorPresent = false;
        public DataTable result;
        public Repository() { } // default constructor
        
        public Repository(Credentials cred)  // parameterized constructor
        { 
            credentials = cred;
        }
        private void Connect() // Connnection to database
        {
            try
            {
                connection = new OdbcConnection();
                connectionString = string.Format("" +
                    "Driver={0};" +
                    "Server={1};" +
                    "UID={2};" +
                    "PWD={3};" +
                    "Database={4};",
                    credentials.Driver, credentials.Server, credentials.Username, credentials.Password, credentials.Database);
                connection.ConnectionString = connectionString;
                connection.Open();
                isErrorPresent = false;
            }
            catch (Exception ex)
            {
                isErrorPresent = true;
                ErrorMessage = ex.Message;
            }
        }

        public void Record(string sqlCommand) // Record or Save
        {
            try { 
                Connect();
                command = new OdbcCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
                isErrorPresent = false;
                Disconnect();
            } catch (Exception ex){
                isErrorPresent = true;
                ErrorMessage = ex.Message;
            }
        }
        public void Select(string sqlCommand) // Select from tables
        {
            try 
            { 
                if(sqlCommand == null) { return; }
                Connect();
                adapter = new OdbcDataAdapter(sqlCommand, connection);
                result = new DataTable();
                adapter.Fill(result);
                isErrorPresent = false;
                Disconnect();
            } catch (Exception ex) { 
                isErrorPresent = true;
                ErrorMessage = ex.Message;
            }
        }
        public void Update(string sqlCommand) // Update records
        {
            try
            {
                if (sqlCommand == null) { return; } // Ensure the SQL command is not null
                Connect(); // Establish database connection
                command = new OdbcCommand(sqlCommand, connection);
                command.ExecuteNonQuery(); // Execute the update command
                isErrorPresent = false; // No error occurred
                Disconnect(); // Close the connection
            }
            catch (Exception ex)
            {
                isErrorPresent = true; // Flag the error
                ErrorMessage = ex.Message; // Store the error message
            }
        }
        public void Delete(string sqlCommand) // Delete from specific row
        {
            try
            {
                if (sqlCommand == null) { return; } // Ensure the SQL command is not null
                Connect(); // Establish database connection
                command = new OdbcCommand(sqlCommand, connection);
                int rowsAffected = command.ExecuteNonQuery(); // Execute the delete command

                if (rowsAffected == 0)
                {
                    throw new Exception("No record was deleted. Please verify the query.");
                }

                isErrorPresent = false; // No error occurred
                Disconnect(); // Close the connection
            }
            catch (Exception ex)
            {
                isErrorPresent = true; // Flag the error
                ErrorMessage = ex.Message; // Store the error message
            }
        }
        public bool ValidateUser(string sqlCommand) // Validate user from the database
        {
            try
            {
                Connect();
                command = new OdbcCommand(sqlCommand, connection);
                reader = command.ExecuteReader();

                bool isValidUser = reader.HasRows; // returns true if any rows are returned, indicating a valid user
                reader.Close();

                isErrorPresent = false;
                Disconnect();

                return isValidUser;
            }
            catch (Exception ex)
            {
                isErrorPresent = true;
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public void UpdateQuery(string query, params OdbcParameter[] parameters) // Update records
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("No record was updated. Please verify the provided data.");
                        }
                    }
                }

                isErrorPresent = false; // Reset error flag
            }
            catch (OdbcException ex)
            {
                isErrorPresent = true;
                ErrorMessage = "Database error during update: " + ex.Message;
            }
            catch (Exception ex)
            {
                isErrorPresent = true;
                ErrorMessage = "Error updating record: " + ex.Message;
            }
        }
        public void DeleteQuery(string query, params OdbcParameter[] parameters) // Delete from specific row
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("No record was deleted. Please verify the provided username.");
                        }
                    }
                }
            }
            catch (OdbcException ex)
            {
                throw new Exception("Database error during deletion: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting record: " + ex.Message);
            }
        }
        
        private void Disconnect() // Close : Terminate 
        { 
            connection.Close();
        }

    }
}
