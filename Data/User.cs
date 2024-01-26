using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using System;
using System.ComponentModel;


namespace Spring.Data
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int TelNo { get; set; }
        public DateTime LastSeen { get; set; }
        public string UserInSession { get; set; }
        public string UserAuthLevel { get; set; }

        private int deptId = 0;
        [Browsable(false)]
        public int DepartmentId { get { return deptId; } set { deptId = value; } }

        public string DepartmentName { get { return User.GetDeptName(deptId).Name; } }

        /// <summary>
        /// This function is used by many other classes main goal here to convert Id from [int] to [String] in fastway
        /// we made it static to not create many objects in memory.
        /// </summary>
        /// <param name="deptId">int no that represents PK for real string name</param>
        /// <returns>department model object</returns>
        public static Department GetDeptName(int deptId)
        {
            var myOpenedTunnel = AccioEasyHelpers.ReadParamsThenConnectToDB(false);

            var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "departments", new string[] { "dept_name" }, new string[] { "dept_id" }, new string[] { $"{deptId}" }, "=", "and");


            OracleDataReader dr = sqlCMD.ExecuteReader();

            //string Dep = "No Name";
            Department dept = new Department();

            if (dr.HasRows)
            {


                while (dr.Read())
                {
                    dept.Name = dr["dept_name"].ToString();
                }
            }

            myOpenedTunnel.Close();
            myOpenedTunnel.Dispose();

            return dept;
        }
    }
}
