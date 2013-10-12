using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1ATM
{
    class People
    {
        private string connectionString = ConnectionString.ATMConnectionString;

        public People()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            EmailAddress = string.Empty;
            Password = string.Empty;
            SocialInsuranceNumber = string.Empty;
        }
        public int PersonId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string EmailAddress
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;

        }

        public string SocialInsuranceNumber
        {
            get;
            set;

        }


        /// <summary>
        /// Method, Searches the database to find if a user already exists
        /// </summary>
        public bool DoesAccountExist()
        {
            string sqlQuery = "SELECT COUNT(*) FROM People WHERE EmailAddress = '" + this.EmailAddress + "' AND [Social Insurance Number] = '" + this.SocialInsuranceNumber.GetHashCode().ToString() + "'";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            try
            {
                connection.Open();

                if((int)command.ExecuteScalar() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method, creates a new user account in the People table
        /// </summary>
        public void CreateAccount()
        {
            string sqlQuery = "INSERT INTO People (FirstName, LastName, EmailAddress, Password, [Social Insurance Number]) VALUES('" + this.FirstName + "', '" + this.LastName + "', '" + this.EmailAddress + "', '" + this.Password.GetHashCode().ToString() + "', '" + this.SocialInsuranceNumber.GetHashCode().ToString() + "')";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
       
            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method, returns whether the login credentials were found in the database
        /// </summary>
        public bool ValidateCredentials()
        {
            string sqlQuery = "SELECT * FROM People WHERE EmailAddress = '" + this.EmailAddress + "' AND Password = '" + this.Password.GetHashCode().ToString() + "'";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }           
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method, Deletes a user account with the specified credentials
        /// </summary>
        public void DeleteUserAccount(int personId)
        {
            string sqlQuery = "DELETE FROM People WHERE PersonId = '" + personId + "'";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// Method, Retrieves PersonID for use as a foreign key
        /// </summary>
        public int SelectPersonID()
        {
            string sqlQuery = "SELECT PersonID FROM People WHERE EmailAddress = '" + this.EmailAddress +  "' AND Password = '" + this.Password.GetHashCode().ToString() + "'";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = null;
            
            int personID = 0;

            try
            {

                connection.Open();

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    personID = int.Parse(reader["PersonId"].ToString());
                }
                else
                {
                    Console.Out.WriteLine("Unable to find person");
                    return -1;
                }
                return personID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

       
 
    }
}
