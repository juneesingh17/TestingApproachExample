using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApproachUnitTest
{
        public class DatabaseFixture : IDisposable
        {
        public SqlConnection DbConnection = new SqlConnection("Data Source=WIN81-6ZS1Y6YL\\MSSQLSERVER_2016;Initial Catalog=FixtureDb;User id=sa;Password=sa;");
        public DatabaseFixture()
        {     
           PerformTransaction("CREATE", "Insert into Person (Name, Age) values ('Junee', 26)", "1 record written to database.");
        }

        public void Dispose()
        { 
           PerformTransaction("DELETE", "Delete From Person;", "All records deleted from database.");
        }

        public DataTable ReturnRows()
        { 
            return PerformTransaction("SELECT", "SELECT * FROM PERSON;", "All records got from database.");
        }

        private DataTable PerformTransaction(string transactionName, string commandText, string printStatement)
        {
            // ... initialize data in the test database ...
            DataTable datatable = new DataTable();
            Tuple<SqlCommand, SqlTransaction> tupleCommandTransaction = PerformDBConnect(DbConnection, transactionName, commandText);
            SqlCommand command = tupleCommandTransaction.Item1;
            SqlTransaction transaction = tupleCommandTransaction.Item2;
            try
            {                
                if (transactionName.Equals("DELETE") || transactionName.Equals("UPDATE") || transactionName.Equals("CREATE"))
                {
                    CommitTransaction(command, transaction);
                }
                else
                {
                   datatable = GetRowsFromTable(command);
                }
                Console.WriteLine(printStatement);
            }
            catch (Exception ex)
            {
                HandleUpperLevelException(ex, transaction);
            }
            DbConnection.Close();
            return datatable; 
        }

        private Tuple<SqlCommand, SqlTransaction> PerformDBConnect(SqlConnection DbConnection, string transactionName, string commandText)
        {
            DbConnection.Open();
            SqlCommand command = DbConnection.CreateCommand();
            // Start a local transaction.
            SqlTransaction transaction = DbConnection.BeginTransaction(transactionName);

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            command.Connection = DbConnection;
            command.Transaction = transaction;
            command.CommandText = commandText;
            return new Tuple<SqlCommand, SqlTransaction>(command, transaction);
        }

        private void HandleUpperLevelException(Exception ex, SqlTransaction transaction)
        {
            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
            // Attempt to roll back the transaction.
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred
                // on the server that would cause the rollback to fail, such as
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }
        }

        private DataTable GetRowsFromTable(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            return dataTable;
        }

        private void CommitTransaction(SqlCommand command, SqlTransaction transaction)
        {
            command.ExecuteNonQuery();
            // Attempt to commit the transaction.
            transaction.Commit();
        }
    }
  }
