using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1ATM
{
    class TransactionType
    {
        private string connectionString = ConnectionString.ATMConnectionString;
       
        public int TransactionTypeId
        {
            get;
            set;
        }

        public string Transaction
        {
            get;
            set;
        }

        public TransactionType()
        {
            TransactionTypeId = 0;
            Transaction = string.Empty;
        }

        public int GetTransactionTypeId(string Transaction)
        {
            string sqlQuery = "SELECT * FROM TransactionType WHERE Transaction = '" + Transaction + "'";

            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return int.Parse(reader["TransactionTypeId"].ToString());
                }
                else 
                {
                    Console.Out.WriteLine("Invalid Transaction Type");
                    return 0;
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
    }
}
