using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1ATM
{
    class Administrators
    {
        private string connectionString = ConnectionString.ATMConnectionString;
    

        public Administrators()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
        public string Username
        {
            get;
            set;

        }

        public string Password
        {
            get;
            set;
        }

        public void createNewAdmin()
        {
            string sqlQuery = "INSERT INTO Administrators (Username, Password) Values ('" + this.Username + "','" + this.Password.GetHashCode().ToString() + "')";

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
        public bool loginAsAdmin()
        {
            string sqlQuery = "SELECT * FROM Administrators WHERE Username = '" + this.Username + "' AND Password = '" + this.Password.GetHashCode().ToString() + "'";

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

        public void deleteDatabase()
        {
            string sqlQuery = "USE [master] DROP DATABASE [ATM]";


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

        public void backupDatabase()
        {
            string sqlQuery = "BACKUP DATABASE [ATM] TO DISK = 'C:/DatabaseBackup/ATMBackup.back' WITH INIT, NOUNLOAD, NAME = N'ATM Backup', NOSKIP, STATS = 10, NOFORMAT";

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

        public void restoreDatabase()
        {
            string sqlQuery = "USE [master] RESTORE DATABASE [ATM] FROM  DISK = 'C:/DatabaseBackup/ATMBackup.back' WITH FILE = 1,  NOUNLOAD,  STATS = 10";

            SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
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




    }
}