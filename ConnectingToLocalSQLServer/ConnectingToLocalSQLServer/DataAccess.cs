using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ConnectingToLocalSQLServer.Contracts;

namespace ConnectingToLocalSQLServer
{
    public class DataAccess //this class is in charge of doing connection conn, so other classes just send queries to this one
    { 
        public static string connectionString1 = "Server=localhost,1600; Database=ChangeTrackingDB; Trusted_Connection=True;";
        public static string command = "SELECT * FROM CHANGETABLE(CHANGES TrackingTable1, 0) AS CHTBl;";
        public static SqlConnection conn = new SqlConnection(connectionString1);

        DataFactory temp9 = new DataFactory();
        //SqlConnection temp8 = temp9.
        SqlCommand cmd = new SqlCommand(command, conn);

        //string PrimaryKey = "Id";
        //int CurrentVersion;//= 6;
        //string RecentlyChangedTable; //= "TrackingTable3";
                                     //use above to edit command sent to sql server! 
        public List<CHANGETableTracker> ConnectDataAccess(List<CHANGETableTracker> table)
        {
            try
            {
                conn.Open();
                var num = cmd.ExecuteReader();//
                                              //var num2 = cmd.ExecuteScalar(); //returns first collumn, first row in that table! to get CurrentVersion!
                                              //.ToString();            

                Int64 version = num.GetOrdinal("SYS_CHANGE_VERSION");// stores COLUMN # of this column given name of column as given in database
                //Console.WriteLine(version);

                //if (!num.Read())
                //    throw new InvalidOperationException("No records were returned.");
                while (num.Read())//while there is still rows being recieved, store them in the list so store them in this c# console application
                {
                    CHANGETableTracker temp = new CHANGETableTracker();

                    temp.SYS_CHANGE_VERSION = num.GetInt64(0);
                    temp.SYS_CHANGE_CREATION_VERSION = num.GetInt64(1);
                    //var operation = num.GetString(2);
                    temp.SYS_CHANGE_OPERATION = CHANGETableTracker.StringToOperation(num.GetString(2));//.GetInt32(0);//GetOrdinal("Id");//"SYS_CHANGE_VERSION");
                    temp.SYS_CHANGE_COLUMNS = num.GetSqlBinary(3);
                    temp.SYS_CHANGE_CONTEXT = num.GetSqlBinary(4);
                    temp.Id = num.GetInt32(5);
                    //Console.WriteLine(temp.SYS_CHANGE_VERSION);
                    //Console.WriteLine(temp.SYS_CHANGE_CREATION_VERSION);
                    //Console.WriteLine(temp.SYS_CHANGE_OPERATION);
                    //Console.WriteLine(temp.SYS_CHANGE_COLUMNS);
                    //Console.WriteLine(temp.SYS_CHANGE_CONTEXT);
                    //Console.WriteLine(temp.Id);
                    //what was just appended into list!
                    //temp.print(0);
                    table.Add(temp);
                }
                //Console.WriteLine(table.SYS_CHANGE_VERSION);
                conn.Close();//maybe unnecessary since "finally"
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();                
            }
            return table;
        }
    }
}
