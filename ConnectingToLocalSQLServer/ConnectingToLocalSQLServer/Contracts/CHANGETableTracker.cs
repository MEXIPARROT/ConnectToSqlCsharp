using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectingToLocalSQLServer.Contracts
{
    public enum SysOperation
    {
        Insert,
        Update,
        Delete
    }
    public class CHANGETableTracker //: IMappable why this?
    {
        public Int64 SYS_CHANGE_VERSION { get; set; } //long or Int64
        public Int64 SYS_CHANGE_CREATION_VERSION { get; set; } //long or Int64
        public SysOperation SYS_CHANGE_OPERATION { get; set; } //originally string
        public SqlBinary SYS_CHANGE_COLUMNS { get; set; } //not in PCLawData
        public SqlBinary SYS_CHANGE_CONTEXT { get; set; } //not in PCLawData
        public int Id { get; set; } // or long? PCLawData is long here instead

        public string FullInfo
        {
            get 
            {
                // 1 1 1
                return $"{ SYS_CHANGE_VERSION } { SYS_CHANGE_CREATION_VERSION } { SYS_CHANGE_OPERATION }"; 
            }
        }
        //static// call it without needing to create a class instace of TestCHANGETable
        static public List<CHANGETableTracker> GetList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ChangeTrackingDB")))
            {
                //return 
                var temp = connection.Query<CHANGETableTracker>($"select * from TestCHANGETable").ToList();
                return temp;
            }
            //Console.WriteLine("failed or something");
        }

        static public SysOperation StringToOperation(string operation) //private in PCLawData
        {
            switch(operation)
            {
                case "I":
                    return SysOperation.Insert;
                case "U":
                    return SysOperation.Update;
                case "D":
                    return SysOperation.Delete;
                default:
                    return SysOperation.Insert;
            }
        }

        public void print(int i)
        {
            //Console.WriteLine("ROW {0}...",i);
            Console.WriteLine("ROW {0} | {1}\t{2}\t{3}\t{4}\t{5}\t{6}", i, SYS_CHANGE_VERSION, SYS_CHANGE_CREATION_VERSION, SYS_CHANGE_OPERATION, SYS_CHANGE_COLUMNS, SYS_CHANGE_CONTEXT, Id);
            Console.WriteLine("----------------------------------------------------");
            //Console.WriteLine(SYS_CHANGE_VERSION);
            //Console.WriteLine(SYS_CHANGE_CREATION_VERSION);
            //Console.WriteLine(SYS_CHANGE_OPERATION);
            //Console.WriteLine(SYS_CHANGE_COLUMNS);
            //Console.WriteLine(SYS_CHANGE_CONTEXT);
            //Console.WriteLine(Id);
            //Console.WriteLine("ROW COMPLETE");
        }

        public void MapFromDataRow(DataRow row)
        {
            this.SYS_CHANGE_VERSION = (long)row["SYS_CHANGE_VERSION"];
            this.SYS_CHANGE_CREATION_VERSION = (long)row["SYS_CHANGE_CREATION_VERSION"];
            this.SYS_CHANGE_OPERATION = StringToOperation((string)row["SYS_CHANGE_OPERATION"]);
            //this.SYS_CHANGE_COLUMNS = (SqlBinary)row["SYS_CHANGE_COLUMNS"]; //"NULL" here
            //this.SYS_CHANGE_CONTEXT = (SqlBinary)row["SYS_CHANGE_CONTEXT"]; //I commented these out and they work if ignored!
            this.Id = (int)row["Id"];
            //this.TablePKName = (string)row["TablePKName"];
        }
    }
}
