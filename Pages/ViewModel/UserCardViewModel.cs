using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.Data;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Spring.Pages.ViewModel
{
    public class UserCardViewModel : BaseViewModel
    {
        #region Members

        #endregion
        #region Public Properties
        /// <summary>
        /// determines if panel is active or not
        /// </summary>
        //public bool ActivePanel { get => VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin") ? true : false; }
        /// <summary>
        /// corp-logo property
        /// </summary>
        public Image CorporationLogo { get; set; }  
        /// <summary>
        /// user photo property
        /// </summary>
        public Image UserPicture { get; set; }
        /// <summary>
        /// Name [first] portion in card
        /// </summary>
        public string EmpFirstName { get; set; }
        /// <summary>
        /// Name [last] portion in card
        /// </summary>
        public string EmpLastName { get; set; }
        /// <summary>
        /// department abbriviation
        /// </summary>
        public string DeptAbbriviation { get; set; }
        /// <summary>
        /// iputed id
        /// </summary>
        public string IdOfCardUser { get; set; } 
        /// <summary>
        /// checker for id
        /// </summary>
        public bool IdCheckerVisiblity { get; set; } = true;

        #endregion
        #region Commands
        /// <summary>
        /// Command to load using event of writing depending on wht id wrote in
        /// </summary>
        public ICommand LoadCurrentUserCard { get; set; }
        #endregion

        #region Constructor
        public UserCardViewModel() {

            //init cmmds
            LoadCurrentUserCard = new RelyCommand(async () => await RefreshWithNewIdtoUserProbs());
        }
        #endregion
        #region Methods
        /// <summary>
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task RefreshWithNewIdtoUserProbs()
        {
            int idRes = 0;
            if (IdOfCardUser == null || !Int32.TryParse(IdOfCardUser, out idRes))
                return;

            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {

                var encounteredusers = await LoadUserFromDataBase(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn, (idRes));

                if (encounteredusers.Count > 0)
                {
                    EmpFirstName = GetParts(encounteredusers[0].FullName)[0];
                    EmpLastName = GetParts(encounteredusers[0].FullName)[1];
                   // LastPortionFName = GetParts(encounteredusers[0].FullName)[2];
                   // UserName = encounteredusers[0].UserName;
                  //  Password = encounteredusers[0].Password;
                  //  ContactNumber = encounteredusers[0].TelNo.ToString();
                 //   SelectedAuth = AuthritiesUsed.Where(x => x.DataFromDatabase == encounteredusers[0].UserAuthLevel).FirstOrDefault();
                ///   SelectedDept = DeptsStored.Where(x => x.Id == encounteredusers[0].DepartmentId).FirstOrDefault();
                  //  DateOfAdditon = encounteredusers[0].LastSeen.ToString();
                    DeptAbbriviation = await GetDeptAbbriviation(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn, encounteredusers[0].DepartmentId);
                   
                }
            });
        }

        /// <summary>
        /// This is a brilliant task method can return all/some Users as instance.
        /// </summary>
        /// <param name="myOpenedTunnel">Current connection object</param>
        /// <returns></returns>
        private Task<List<User>> LoadUserFromDataBase(OracleConnection myOpenedTunnel, int id)
        {


            List<User> usersRemote = new List<User>();



            return Task.Run(() =>
            {


                var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "users", new string[] { "user_id", "user_name", "user_password", "user_auth", "user_full_name", "dept_id", "user_session", "user_tel", "user_seen_date" }, new string[] { "user_id" }, new string[] { $"{id}" }, "=", "and");

                try
                {
                    OracleDataReader dr = sqlCMD.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {


                            usersRemote.Add(
                                new User()
                                {
                                    Id = Int32.Parse(dr["user_id"].ToString()),
                                    UserName = dr["user_name"].ToString(),
                                    Password = dr["user_password"].ToString(),
                                    FullName = dr["user_full_name"].ToString(),
                                    UserAuthLevel = dr["user_auth"].ToString(),
                                    DepartmentId = Int32.Parse(dr["dept_id"].ToString()),
                                    UserInSession = dr["user_session"].ToString(),
                                    TelNo = Int32.Parse(dr["user_tel"].ToString()),
                                    LastSeen = DateTime.Parse(dr["user_seen_date"].ToString())


                                });



                        }
                    }
                }
                catch (Exception xorcl)
                {
                    //ErrorDescription = xorcl.Message;
                    //for debug purposes
                    Console.WriteLine(xorcl.Message);
                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();

                }


                return usersRemote;
            });


        }


        /// <summary>
        /// get parts from fullname
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        private string[] GetParts(string fullname)
        {
            string[] parts = fullname.Split(' ');

            List<string> partsAsList = new List<string>();

            if (parts.Length == 3)
                partsAsList.Add(parts[0]); partsAsList.Add(parts[1]); partsAsList.Add(parts[2]);

            return partsAsList.ToArray();

        }

        private Task<string> GetDeptAbbriviation(OracleConnection myOpenedTunnel, int deptid)
        {
            string _abbr = "string.Empty";


            return Task.Run(() =>
            {


                var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "departments", new string[] { "dept_abbriv" }, new string[] { "dept_id" }, new string[] { $"{deptid.ToString()}" }, "=", "and");

                try
                {
                    OracleDataReader dr = sqlCMD.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                        

                            _abbr = dr["dept_abbriv"].ToString();

                        }
                    }
                }
                catch (Exception xorcl)
                {
                    //ErrorDescription = xorcl.Message;
                    //for debug purposes
                    Console.WriteLine(xorcl.Message);
                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();

                }


                return _abbr;
            }
            );

           
        }
        #endregion

    }
}
