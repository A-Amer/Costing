using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Pricing
{
    
    public class DbManager
    {
        OleDbConnection dbConnection;

        public DbManager(string connectionString)
        {

            dbConnection = new OleDbConnection(connectionString);
            try
            {
                dbConnection.Open();
                Console.WriteLine("The DB connection is opened successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("The DB connection is failed");
                Console.WriteLine(e.ToString());
            }
            
        }
        public int ExecuteNonQuery(string query)
        {
            try
            {

                OleDbCommand myCommand = new OleDbCommand(query, dbConnection);
                
                return myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public int ExecuteNonQuery(OleDbCommand myCommand)
        {
            try
            {
                myCommand.Connection = dbConnection; 
                return myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public DataTable ExecuteReader(string selectQuery)
        {
            if (selectQuery.Length <= 1)
            {
                return null;
            }
            //create new connection
            try
            {
                DataSet dataSet = new DataSet();
                //create new adapter and command builder
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);
                dataAdapter.SelectCommand = new OleDbCommand(selectQuery, dbConnection);
                //fills our datatable with retreived table
                dataAdapter.Fill(dataSet);
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        

        public object ExecuteScalar(string query)
        {
            try
            {
                OleDbCommand myCommand = new OleDbCommand(query, dbConnection);
                return myCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public void CloseConnection()
        {
            try
            {
                dbConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
