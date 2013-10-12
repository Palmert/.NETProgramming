using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment1ATM
{
    class Transactions
    {
        private string connectionString = ConnectionString.ATMConnectionString;

        public Transactions()
        {
            TransactionTypeId = 0; 
            AmountTransferred = 0;
        }

        public int TransactionTypeId
        {
            get;
            set;
        }

        public decimal AmountTransferred
        {
            get;
            set;
        }


        /// <summary>
        /// Method, Inserts a transaction into the user account. Handles both withdrawals and deposits
        /// </summary>
        public void PerformTransaction(int personID)
        {
            string sqlQuery = "INSERT INTO Transactions (PersonId, TransactionTypeId, AmountTransferred) VALUES('" + personID + "', '" + this.TransactionTypeId + "', '" + this.AmountTransferred + "')";
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
        /// Method, Returns to total balance of all transactions for the specified user
        /// </summary>
        public decimal CheckAccountBalance(int personID)
        {
            string sqlQuery = "SELECT TransactionTypeId, AmountTransferred FROM Transactions WHERE PersonID = '" + personID + "'";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = null;

            decimal accountBalance = 0;


            try
            {
                connection.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["TransactionTypeID"].Equals(1))
                    {
                        accountBalance += Decimal.Parse(reader["AmountTransferred"].ToString());
                    }
                    else
                    {
                        accountBalance -= Decimal.Parse(reader["AmountTransferred"].ToString());
                    }
                }
                return accountBalance;
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
        /// Method, Retrieves the last 5,10 or 15 transactions for the specified user
        /// </summary>
        public void ReportTransactions(int numberToShow, int personId)
        {
            string sqlQuery = string.Empty;
            if (numberToShow == 5)
            {
                sqlQuery = "SELECT TOP 5 * FROM Transactions";
            }
            else if (numberToShow == 10)
            {
                sqlQuery = "SELECT TOP 10 * FROM Transactions";
            }
            else if (numberToShow == 15)
            {
                sqlQuery = "SELECT TOP 15 * FROM Transactions";
            }
            else
            {
                Console.Out.WriteLine("Invalid Number of Transactions Selected");
            }
            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = null;
            int  record = 1;

            try
            {
           
                connection.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.Out.WriteLine(record + ": Transaction Type = " + reader["TransactionTypeId"].ToString() + " - Amount Transferred = " + reader["AmountTransferred"].ToString());
                    ++record;
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
        /// Method, Deletes all transactions for the specified user
        /// </summary>
        public void DeleteTransactions(int personId)
        {
            string sqlQuery = "DELETE FROM Transactions WHERE PersonId = '" + personId + "'";

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





    }
}
