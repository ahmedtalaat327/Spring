
using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.StaticVM;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Spring.Data
{
    //this is exceptional datatype for only columns headers type ex: varchar , varchar2 ,...
    public class ColumnDataDef
    {
        /// <summary>
        /// detects the table name
        /// </summary>
        public string ParentTable { get; set; }
        /// <summary>
        /// order in horizontal for cols in one tabe
        /// </summary>
        public int Id {  get; set; }
        /// <summary>
        /// col name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// the col-data-type
        /// </summary>
        public string Description { get {


                 
                    var myOpenedTunnel = VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn;

                    var sqlCMD = Scripts.FetchMySQLText(myOpenedTunnel, $"select data_type from  all_tab_columns where  upper(table_name) = '{ParentTable.ToUpper(new CultureInfo("en-US", false))}' AND upper(column_name) = '{Name.ToUpper(new CultureInfo("en-US", false))}'");
                    OracleDataReader dr = null;
                    ObservableCollection<string> colsType = new ObservableCollection<string>();
                    try
                    {
                        dr = sqlCMD.ExecuteReader();
                        if (dr.HasRows)
                        {


                            while (dr.Read())
                            {


                            colsType.Add(dr["data_type"].ToString());
                                
                            }
                        }
                    }
                    catch (Exception xorcl)
                    {
                        //for debug purposes
                        Console.WriteLine(xorcl.Message);
                        //Connection error for somereason so aggresive close that connection
                        VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();
                        colsType.Add("failed to get any data type");
                    }

                    return colsType[0];

            } 
                
                
                }
    }
}
