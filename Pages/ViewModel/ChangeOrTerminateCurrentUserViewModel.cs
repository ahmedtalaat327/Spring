using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.Data;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spring.Pages.ViewModel
{
    public class ChangeOrTerminateCurrentUserViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// determines if panel is active or not
        /// </summary>
        //public bool ActivePanel { get => VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin") ? true : false; }
        /// <summary>
        /// id user currently must be added
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// first portion text of full name
        /// </summary>
        public string FirstPortionFName { get; set; }
        /// <summary>
        /// second part of text full name
        /// </summary>
        public string MiddlePortionFName { get; set; }
        /// <summary>
        /// last part of text full name
        /// </summary>
        public string LastPortionFName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// user name used
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// auth types used in logging in
        /// </summary>
        public ObservableCollection<Account> AuthritiesUsed { get; set; }
        /// <summary>
        /// Selected item from <see cref="AuthritiesUsed"/> list
        /// </summary>
        public Account SelectedAuth { get; set; }
        /// <summary>
        /// Current depts loaded from the databse
        /// </summary>
        public ObservableCollection<Department> DeptsStored { get; set; }
        /// <summary>
        /// Selected item from <see cref="DeptsStored"/> 
        /// </summary>
        public Department SelectedDept { get; set; }
        /// <summary>
        /// Date to be added
        /// </summary>
        public string DateOfAdditon { get; set; }
        /// <summary>
        /// Some contact info
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// Checker for any portion in textbox
        /// </summary>
        public User CurrentUsr { get; set; }


        #endregion

        #region Commands
        /// <summary>
        /// First commnad to beloaded
        /// </summary>
        public ICommand LoadInitialWithRefrshing { get; set; }
        /// <summary>
        /// Command to load using event of writing depending on wht id wrote in
        /// </summary>
        public ICommand LoadCurrentUser { get; set; }
        #endregion
        #region Constructor
        public ChangeOrTerminateCurrentUserViewModel()
        {

            AuthritiesUsed = new ObservableCollection<Account>
            {
               new Account(){DataFromDatabase = "admin",Title = "Admin"},
               new Account(){DataFromDatabase = "user", Title = "User"}
            };

            SelectedAuth = AuthritiesUsed[0];

            DeptsStored = new ObservableCollection<Department> {
                new Department() { Id = 0, Name = "IT" }
            };

            //init commands
            LoadInitialWithRefrshing = new RelyCommand(async () => await LoadRealTimeValues());

            LoadCurrentUser = new RelyCommand(async () => await RefreshWithNewIdtoUserProbs());

        }
        #endregion



        /// <summary>
        /// Load updated values for VM properties
        /// </summary>
        /// <returns></returns>
        private async Task LoadRealTimeValues()
        {

            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
                try
                {
                    FirstPortionFName = ""; MiddlePortionFName = ""; LastPortionFName = "";

                    UserName = ""; Password = ""; ContactNumber = "";

                    // department section
                    DeptsStored.Clear();
                    //this is bug you can't use assigning method you need to add as tree with childs
                    var _depts = await ReadAllDepartments(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);

                    for (int x = 0; x < _depts.Count; x++)
                    {
                        DeptsStored.Add(new Department() { Id = _depts[x].Id, Name = _depts[x].Name });
                    }

                    SelectedDept = DeptsStored[0];

                
                     

                }
                catch (Exception ex)
                {

                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();

                }

            });
        }

        /// <summary>
        /// Read all depts from sql-exec
        /// </summary>
        /// <param name="myOpenedTunnel"></param>
        /// <returns></returns>
        private Task<ObservableCollection<Department>> ReadAllDepartments(OracleConnection myOpenedTunnel)
        {


            return Task.Run(() =>
            {

                var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "departments", new string[] { "dept_id", "dept_name" }, new string[] { "dept_id" }, new string[] { "-1" }, ">", "and");
                OracleDataReader dr = null;
                ObservableCollection<Department> depts = new ObservableCollection<Department>();
                try
                {
                    dr = sqlCMD.ExecuteReader();
                    if (dr.HasRows)
                    {


                        while (dr.Read())
                        {
                            depts.Add(new Department() { Id = Int32.Parse(dr["dept_id"].ToString()), Name = dr["dept_name"].ToString() });

                        }
                    }
                }
                catch (Exception xorcl)
                {
                    //for debug purposes
                    Console.WriteLine(xorcl.Message);
                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();

                }









                return depts;

            });

        }

        /// <summary>
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task RefreshWithNewIdtoUserProbs()
        {


            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {

                var encounteredusers = await LoadUserFromDataBase(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn, Int32.Parse(Id));

                if (encounteredusers.Count > 0)
                {
                    UserName = encounteredusers[0].UserName;
                }
            });
        }

        /// <summary>
        /// This is a brilliant task method can return all Users as instance.
        /// </summary>
        /// <param name="myOpenedTunnel">Current connection object</param>
        /// <returns></returns>
        private Task<List<User>> LoadUserFromDataBase(OracleConnection myOpenedTunnel, int id)
        {


            List<User> usersRemote = new List<User>();



            return Task.Run(() =>
            {


                var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "users", new string[] { "user_id", "user_name", "user_password", "user_auth", "user_full_name", "dept_id", "user_session" }, new string[] { "user_id" }, new string[] { $"{id}" }, "=", "and");

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
                                    UserInSession = dr["user_session"].ToString()

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
    }
}
