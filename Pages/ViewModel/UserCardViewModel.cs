using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Data;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Spring.Pages.ViewModel.AddUserViewModel;



namespace Spring.Pages.ViewModel
{
    public class UserCardViewModel : BaseViewModel
    {
        #region Members

        #endregion
        #region ENUM for Phases
        /// <summary>
        /// Progress bar porperty phases set
        /// <see cref="VMCentral.DockingManagerViewModel.Loading"/> <see cref="CurrentWait"/>
        /// </summary>
        public enum UserCardVMLoadingPhase
        {
            Non, //reset
            EditCheckWaiting,//first relycommand

        }
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
        /// <summary>
        /// CRD PHOTO
        /// </summary>
        public Bitmap PersonalPhotoUser { get; set; }
        /// <summary>
        /// simple flag to determines if edit succeeded or not
        /// </summary>
        public bool EditSucceded { get; set; } = false;
        /// <summary>
        /// this for helping wait property when changing in VIEW 
        /// </summary>
        public UserCardVMLoadingPhase CurrentWait { get; set; } = UserCardVMLoadingPhase.Non;
        #endregion
        #region Commands
        /// <summary>
        /// Command to load using event of writing depending on wht id wrote in
        /// </summary>
        public ICommand LoadCurrentUserCard { get; set; }

        public ICommand SavePhotoUserCard { get; set; }     
        #endregion

        #region Constructor
        public UserCardViewModel() {

            //init cmmds
            LoadCurrentUserCard = new RelyCommand(async () => await RefreshWithNewIdtoUserProbs());

            SavePhotoUserCard = new RelyCommand(async () => await UpdateUserRowPhoto());

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
                    PersonalPhotoUser = new Bitmap(new MemoryStream(encounteredusers[0].FaceImageBlob));
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


                var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "users", new string[] { "user_id", "user_name", "user_password", "user_auth", "user_full_name", "dept_id", "user_session", "user_tel", "user_seen_date" , "user_photo" }, new string[] { "user_id" }, new string[] { $"{id}" }, "=", "and");

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
                                    LastSeen = DateTime.Parse(dr["user_seen_date"].ToString()),
                                    //convert from string to hex then to byte array
                                    FaceImageBlob = (dr["user_photo"]==DBNull.Value) ? null : AccioEasyHelpers.ConvertHexStringToByteArray(dr["user_photo"].ToString())






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
        /// <summary>
        /// this func to get the short name for department only
        /// </summary>
        /// <param name="myOpenedTunnel"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
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
            /// <summary>
            /// this func to update user photo in row
            /// ipmorant here we use parameterized query to avoid SQL Injection or BLOB issues limits
            /// Dated : 2025-11-01
            /// </summary>
            /// <returns></returns>
            private async Task UpdateUserRowPhoto()
            {
            CurrentWait = UserCardVMLoadingPhase.EditCheckWaiting;

                 var bit_arr_img = AccioEasyHelpers.ImageToByte(PersonalPhotoUser);

                  //hex convertable
                  StringBuilder hexBuilder = new StringBuilder(bit_arr_img.Length * 2);
                  foreach (byte b in bit_arr_img)
                  {
                        hexBuilder.AppendFormat("{0:X2}", b); // "X2" for uppercase hex, "x2" for lowercase
                  }

                  string hexString = hexBuilder.ToString();


                await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
                {
                await Task.Delay(100); //simulating wait
                      /*
                          var replyOfOracle = Scripts.EditMyDataRow(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn, "users",
                            new string[]
                            {
                                      "USER_PHOTO"
                            },
                            new string[]
                            {
                                      $"HEXTORAW('{hexString}')"
                            },
                            new string[]
                            {
                                      "USER_ID"
                            },
                            new string[]
                            {
                                      $"{IdOfCardUser}"
                            },
                            "=", "and"

                            );
                           if (replyOfOracle >= 0)
                           {
                               EditSucceded = true;
                           }
                           else
                           {
                               EditSucceded = false;
                           }
                      */

                      var myOpenedTunnel = VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn;
                      var sqlCMD = Scripts.FetchMySQLText(myOpenedTunnel, 
                      $"UPDATE USERS SET USER_PHOTO=:BlobParameter WHERE USER_ID={IdOfCardUser}");
                      sqlCMD.Parameters.Add(new OracleParameter("BlobParameter", Oracle.ManagedDataAccess.Client.OracleDbType.Clob)).Value = hexString;
                      
                     
                      int replyOfOracle = -1;
                      try
                      {
                         replyOfOracle =   sqlCMD.ExecuteNonQuery();
                      }
                      catch (Exception xorcl)
                      {
                            //ErrorDescription = xorcl.Message;
                            //for debug purposes
                            Console.WriteLine(xorcl.Message);
                            EditSucceded = false;
                            //Connection error for somereason so aggresive close that connection
                            VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();
                      }
                       
                      if(replyOfOracle >= 0)
                        {
                            EditSucceded = true;
                        }
                        else
                        {
                            EditSucceded = false;
                      }
                });
                 

             }

        #endregion

    }
}
