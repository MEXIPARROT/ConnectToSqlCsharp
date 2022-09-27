using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ConnectingToLocalSQLServer.Contracts;
using ConnectingToLocalSQLServer.Operations;
using System.Dynamic;

namespace ConnectingToLocalSQLServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            string PrimaryKey = "Id";
            int CurrentVersion;//= 6;
            string RecentlyChangedTable;

            List<CHANGETableTracker> table = new List<CHANGETableTracker>();            
            //DataAccess temp = new DataAccess();

            CHANGETableOperations temp2 = new CHANGETableOperations();//new
            var result = temp2.Table1Tracker(0);//new


            dynamic eo;
            eo = new ExpandoObject();
            eo.amountdue = 2.20; //c# 



            //table = temp.ConnectDataAccess(table); OLD
            
            Console.WriteLine("We will try printing from class:");            
            int j = 1;
            foreach (CHANGETableTracker i in result)//table OLD
            {
                i.print(j);
                j++;
            }
            Console.ReadLine();

        }

    }
    
}
