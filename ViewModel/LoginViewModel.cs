using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Data;
using Spring.View.MainView.LoginView;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Spring.StaticVM;

namespace Spring.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Poperties
        /// <summary>
        /// Property for input username
        /// </summary>
        public string CurrentUserName { get; set; } = "";
        /// <summary>
        /// property for input passpord
        /// </summary>
        public string CurrentPassword { get; set; } = "";

        /// <summary>
        /// Loading flag for prgress bar visible or not
        /// </summary>
        public bool Loading { get { return WaitingProgress; } }
        /// <summary>
        /// Current progress bar state
        /// </summary>
        public bool WaitingProgress { get; set; } 
        /// <summary>
        /// Is logging in valid or not
        /// </summary>
        private bool ValidLogin { get; set; }
        /// <summary>
        /// Is Connection to DB is valid or not
        /// </summary>
        private bool ValidConnection { get; set; }
        /// <summary>
        /// Current local user
        /// </summary>
        private User UserLocal { get { return new User { Id = 0 , DepartmentId = 0 , FullName = "" , UserName = CurrentUserName,Password = CurrentPassword, LastSeen = new DateTime(),TelNo = 11111111, UserAuthLevel="any",
         UserInSession = "no"}; } }
        /// <summary>
        /// All users currently avilable in database
        /// </summary>
        private List<User> UsersInAllDataBase = new List<User>(); 
        /// <summary>
        /// Dummy User to hold info to another way MVVM
        /// </summary>
        public static User UserLogged { get; set; }
        /// <summary>
        /// Visible flag to determine whether if the current form is visile or not
        /// </summary>
        public bool Visible { get { return ValidLogin ? false : true; }  }
        #endregion


        #region Commands
        /// <summary>
        /// First commnad to beloaded
        /// </summary>
        public ICommand CheckConnectivityCommand { get; set; }
        /// <summary>
        /// Our unique way to handle login comparison
        /// </summary>
        public ICommand LoginCommand { get; set; }
        #endregion


      
        #region Constructor
        /// <summary>
        /// Constructor of VM
        /// </summary>
        /// <param name="contol">This is the Vuew UI conroler May change to another thing</param>
        public LoginViewModel()
        {
 
            CheckConnectivityCommand = new RelyCommand(async () => await CheckConndctionToServer());

            LoginCommand = new RelyCommand(async () => await Login());

        }
        #endregion


        #region Procedural methods
        /// <summary>
        /// Check whether connection valid or not?
        /// </summary>
        private async Task CheckConndctionToServer()
        {
            await RunCommand(() => this.WaitingProgress, async () =>
            {
             //   await Task.Delay(1);   
                //test oracle db connection
                if (await VMCentral.DockingManagerViewModel.ReadyMyDatabaseConn() == null)
                {
                    ValidConnection = false;
                }
                else
                {
                    ValidConnection = true;
                }


            });

               


                if (!ValidConnection)
                {
                    new AdvOptions().ShowError_Connection(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                }
                else
                {
                    new AdvOptions().ShowValid_Connection(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                }
        }
       
        /// <summary>
        /// All login methods step by step efficient way to get the cardential comparing
        /// </summary>
        public async Task Login()
        {
            

            await RunCommand(() =>  this.WaitingProgress, async () =>
            {
                try
                {
                     
                    UsersInAllDataBase = await LoadUsersFromDataBase(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);
                }
                catch (Exception ex)
                {
                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();
                }
                finally
                {

                //    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();


                    if (await ComparisonProcedure(UsersInAllDataBase))
                    {

                        ValidLogin = true;

                    }
                    else
                    {
                        ValidLogin = false;
                    }
                }
            
            });
            if (!WaitingProgress && ValidLogin) {

                new AdvOptions().ShowSuccess_Login(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));

                VMCentral.DockingManagerViewModel.WindowVisible = true;
                VMCentral.DockingManagerViewModel.loggedUser = UserLogged;
            }
            else if (!WaitingProgress && !ValidLogin)
            {
                new AdvOptions().ShowFailur_Login(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
            }


           
           
        }

        /// <summary>
        /// This is a brilliant task method can return all Users as instance.
        /// </summary>
        /// <param name="myOpenedTunnel">Current connection object</param>
        /// <returns></returns>
        private Task<List<User>> LoadUsersFromDataBase(OracleConnection myOpenedTunnel)
        {
           

          List<User> usersRemote = new List<User>();

          

            return Task.Run(() =>
            {

               
                var sqlCMD = Scripts.FetchMyData(myOpenedTunnel, "users", new string[] { "user_id", "user_name", "user_password", "user_auth", "user_full_name" }, new string[] { "user_id", "user_auth" }, new string[] { "999", "'power'" }, "!=", "and");

                OracleDataReader dr = sqlCMD.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {


                        usersRemote.Add(
                            new User() { 
                                Id = Int32.Parse(dr["user_id"].ToString()),
                                UserName = dr["user_name"].ToString(),
                                Password = dr["user_password"].ToString(),
                                FullName = dr["user_full_name"].ToString(),
                                UserAuthLevel = dr["user_auth"].ToString()
                            });


                  
                    }
                }

            

                return usersRemote;
            });


        }
        /// <summary>
        /// Compare netween logged in user and the usrs in the database
        /// </summary>
        /// <param name="allUsers">List full of all uers from DB</param>
        /// <returns></returns>
        private Task<bool> ComparisonProcedure(List<User> allUsers)
        {

            bool feedBack = false;
            return Task.Run(() =>
            {


             foreach(User usr in allUsers)
                {
                    if (usr.UserName.Equals(UserLocal.UserName) && usr.Password == UserLocal.Password)
                    {
                        UserLogged = usr;
                        feedBack = true;
                        break;
                    }
                    else
                    {
                        feedBack = false;
                    }
                }



                return feedBack;
            });
              

        }
            #endregion
        }
}
