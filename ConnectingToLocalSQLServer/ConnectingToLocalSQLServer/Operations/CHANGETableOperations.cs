using ConnectingToLocalSQLServer.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectingToLocalSQLServer.Operations
{
    class CHANGETableOperations
    {
        public List<CHANGETableTracker> Table1Tracker(int VersionNum)
        {
            var items = new List<CHANGETableTracker>();

            using( var connection = DataFactory.CreateSqlConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                str.AppendLine("*");// SYS_CHANGE_VERSION, SYS_CHANGE_CREATION_VERSION, SYS_CHANGE_OPERATION, ISNULL(SYS_CHANGE_COLUMNS, 0), ISNULL(SYS_CHANGE_CONTEXT, 0), Id");//"*"
                str.AppendLine("FROM CHANGETABLE(CHANGES TrackingTable1, @VersionNum) AS CHTBl;");

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VersionNum", VersionNum);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new CHANGETableTracker();
                    Console.WriteLine(item.SYS_CHANGE_COLUMNS);
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<CHANGETableTracker> Table2Tracker(int VersionNum)
        {
            var items = new List<CHANGETableTracker>();

            using (var connection = DataFactory.CreateSqlConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                str.AppendLine("*");// SYS_CHANGE_VERSION, SYS_CHANGE_CREATION_VERSION, SYS_CHANGE_OPERATION, ISNULL(SYS_CHANGE_COLUMNS, 0), ISNULL(SYS_CHANGE_CONTEXT, 0), Id");//"*"
                str.AppendLine("FROM CHANGETABLE(CHANGES TrackingTable2, @VersionNum) AS CHTBl;");

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VersionNum", VersionNum);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new CHANGETableTracker();
                    Console.WriteLine(item.SYS_CHANGE_COLUMNS);
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }
            return items;
        }
        public List<CHANGETableTracker> Table3Tracker(int VersionNum)
        {
            var items = new List<CHANGETableTracker>();

            using (var connection = DataFactory.CreateSqlConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                str.AppendLine("*");// SYS_CHANGE_VERSION, SYS_CHANGE_CREATION_VERSION, SYS_CHANGE_OPERATION, ISNULL(SYS_CHANGE_COLUMNS, 0), ISNULL(SYS_CHANGE_CONTEXT, 0), Id");//"*"
                str.AppendLine("FROM CHANGETABLE(CHANGES TrackingTable1, @VersionNum) AS CHTBl;");

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VersionNum", VersionNum);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new CHANGETableTracker();
                    Console.WriteLine(item.SYS_CHANGE_COLUMNS);
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
