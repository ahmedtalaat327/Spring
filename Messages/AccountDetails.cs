

using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;

using Spring.Data;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Spring.Messages
{
    public partial class AccountDetails : BaseMessage
    {
        #region ModelView instance
        AccountDetailsViewModel accountDetailsViewModel = new AccountDetailsViewModel();
        #endregion
        #region Constructor
        public AccountDetails()
        {

            base.InitializeComponent();
            this.InitializeComponent();

            base.Text = "My Account";
            this.TopMost = true;
            this.sfBarcode1.Text = "0";

            this.label1.Image = (new Bitmap(this.label1.Image, new Size(24, 24)));

            this.progressBarAdv1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv1.WaitingGradientEnabled = true;

            this.passtxtbx.SetImgeForFirstTime(new Bitmap(Properties.Resources.icons8_eye_2_48, this.passtxtbx.LabelEyeRevealler.Size));
            this.passtxtbx.SetImageForHover(new Bitmap(Properties.Resources.icons8_eye_48, this.passtxtbx.LabelEyeRevealler.Size));
            this.passtxtbx.StayUnrevealed();

            this.sfToolTip1.SetToolTip(this.passwordchklbl, "Password is short!");

            //link all properties to their controlers
            this.sfBarcode1.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "MyId"));
            this.idtxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "MyId"));
            this.fnametxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "FirstPart"));
            this.snametxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "SecondPart"));
            this.lnametxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "ThirdPart"));
            this.usernametxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "UserName"));
            this.passtxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "Password", false, DataSourceUpdateMode.OnPropertyChanged));
            this.depttxtbx.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "Departmanet"));
            this.label1.DataBindings.Add(new Binding("Text", accountDetailsViewModel, "AuthorityLevel"));

            //link enable flag
            this.passtxtbx.DataBindings.Add(new Binding("ReadOnly", accountDetailsViewModel, "ReadOnly"));
            //link to update button
            this.okbtn.DataBindings.Add(new Binding("Enabled", accountDetailsViewModel, "UpdateButtonEnable"));

            //assign progressbar properties [visibility & Running for loading]
            progressBarAdv1.DataBindings.Add(new Binding("Visible", accountDetailsViewModel, "Loading"));
            progressBarAdv1.DataBindings.Add(new Binding("WaitingGradientEnabled", accountDetailsViewModel, "WaitingProgress"));
            //pass checker lbl
            passwordchklbl.DataBindings.Add(new Binding("Visible", accountDetailsViewModel, "PassCheckerVisiblity"));
        }
        #endregion
        #region Events

        private void okbtn_Click(object sender, EventArgs e)
        {
            accountDetailsViewModel.UpdateCommand.Execute(true);
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            accountDetailsViewModel.ReadOnly = false;

        }
        private void passtxtbx_TextChanged(object sender, EventArgs e)
        {
            accountDetailsViewModel.PassCheching.Execute(true);
        }

        #endregion

        private void sfButton1_Click(object sender, EventArgs e)
        {
            accountDetailsViewModel.ReloadInitialize.Execute(true);
        }
    }
    //this View model may be moved to a seperate file .cs 
    public class AccountDetailsViewModel : BaseViewModel
    {
        #region Poperties
        /// <summary>
        /// Current user instance loggedin.
        /// </summary>
        public User MyUsre { get => VMCentral.DockingManagerViewModel.loggedUser; }
        /// <summary>
        /// Id currently used in user
        /// </summary>
        public string MyId { get => MyUsre.Id.ToString(); }
        /// <summary>
        /// Full Name currently in user
        /// </summary>
        public string MyFullName { get => MyUsre.FullName; }
        /// <summary>
        /// first part of the fullname
        /// </summary>
        public string FirstPart { get => GetParts(MyFullName)[0]; }
        /// <summary>
        /// second part of fullname
        /// </summary>
        public string SecondPart { get => GetParts(MyFullName)[1]; }
        /// <summary>
        /// third part of fullname
        /// </summary>
        public string ThirdPart { get => GetParts(MyFullName)[2]; }
        /// <summary>
        /// username
        /// </summary>
        public string UserName { get => MyUsre.UserName; }
        /// <summary>
        /// pass
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// department
        /// </summary>
        public string Departmanet { get => MyUsre.DepartmentName; }
        /// <summary>
        /// determmines if its user or admin
        /// </summary>
        public string AuthorityLevel { get => MyUsre.UserAuthLevel; }
        /// <summary>
        /// determines if the edit is enabled or not  
        /// Specially for password box
        /// </summary>
        public bool ReadOnly { get; set; } = true;
        /// <summary>
        /// determines if the update button is on or off
        /// </summary>
        public bool UpdateButtonEnable { get => !ReadOnly; }

        /// <summary>
        /// Loading flag for prgress bar visible or not
        /// </summary>
        public bool Loading { get { return WaitingProgress; } }
        /// <summary>
        /// Current progress bar state
        /// </summary>
        public bool WaitingProgress { get; set; }
        /// <summary>
        /// determines if the password checker label is visible or not
        /// </summary>
        public bool PassCheckerVisiblity { get; set; } = false;
        #endregion
        #region Private members
        /// <summary>
        /// determins if update sql command succeeded or not?
        /// [(-1) means fails, (any other value) means something else.]
        /// </summary>
        int _validUpdate = -1;
        #endregion

        #region Commands
        /// <summary>
        /// Command update for events 
        /// </summary>
        public ICommand UpdateCommand { get; set; }
        /// <summary>
        /// Command pass checker for event key down
        /// </summary>
        public ICommand PassCheching { get; set; }
        /// <summary>
        /// Load user object again 
        /// </summary>
        public ICommand ReloadInitialize { get; set; }
        #endregion
        #region Constructor
        public AccountDetailsViewModel()
        {
            Password = MyUsre.Password;
            UpdateCommand = new RelyCommand(async () => await Update());
            PassCheching = new RelyCommand(async () => await PassProcedure());
            ReloadInitialize = new RelyCommand(async () => await Refresh());
        }

        #endregion

        #region Methods Helpers

        /// <summary>
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task Update()
        {


            await RunCommand(() => this.WaitingProgress, async () =>
            {

                //must put awaitable task func connected to database
                try
                {


                    if (!PassCheckerVisiblity)
                        MyUsre.Password = Password;

                    _validUpdate = await UpdateCurrentUser(MyUsre, DateTime.Now, VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);

                }
                catch (Exception ex)
                {
                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();
                }
                finally
                {

                    ReadOnly = true;



                }
                if (_validUpdate == -1)
                    //error for sql
                    return;
                else
                {
                    //show success message

                }



            });
        }
        /// <summary>
        /// update current user 
        /// </summary>
        /// <param name="_user">current user</param>
        /// <param name="inputDate">current date</param>
        /// <param name="myOpenedTunnel">connection object</param>
        /// <returns></returns>
        private Task<int> UpdateCurrentUser(User _user, DateTime inputDate, OracleConnection myOpenedTunnel)
        {

            return Task.Run(() =>
            {

                var replyFromUpdate = Scripts.EditMyDataRow(myOpenedTunnel, "users",
                    new string[] { "user_name", "user_full_name", "user_password", "user_tel", "user_seen_date", "user_session", "user_auth", "dept_id" },
                    new string[] { "'" + _user.UserName + "'", "'" + _user.FullName + "'", "'" + _user.Password + "'", _user.TelNo.ToString(), "DATE '" + inputDate.Year.ToString() + "-" + inputDate.Month.ToString() + "-" + inputDate.Day.ToString() + "'", "''", "'" + _user.UserAuthLevel + "'", _user.DepartmentId.ToString() },
                    new string[] { "user_id" },
                    new string[] { _user.Id.ToString() },
                    "=",
                    ","
                    );

                return replyFromUpdate;

            });

        }



        /// <summary>
        /// Checing algorithm for pass if it passes the conditions we have here
        /// </summary>
        /// <returns></returns>
        private async Task PassProcedure()
        {
            await Task.Delay(1000);



            if (Password.Length < 7)
            {
                PassCheckerVisiblity = true;
            }
            else
            {
                PassCheckerVisiblity = false;
            }

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
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task Refresh()
        {


            await RunCommand(() => this.WaitingProgress, async () =>
            {

                var encounteredusers = await LoadUserFromDataBase(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn, Int32.Parse(MyId));
                VMCentral.DockingManagerViewModel.loggedUser = encounteredusers[0];
                OnPropertyChanged(nameof(MyUsre));
            });
        }

        /// <summary>
        /// This is a brilliant task method can return all Users as instance.
        /// </summary>
        /// <param name="myOpenedTunnel">Current connection object</param>
        /// <returns></returns>
        private Task<List<User>> LoadUserFromDataBase(OracleConnection myOpenedTunnel,int id)
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
            #endregion

        }
    }
}
