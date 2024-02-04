
using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Data;
using Spring.StaticVM;
using Spring.View.MainView.LoginView;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Spring.Pages.ViewModel
{
    public class AddUserViewModel : BaseViewModel
    {
        /// <summary>
        /// determines if panel is active or not
        /// </summary>
        public bool ActivePanel { get => VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin") ? true : false; }
        /// <summary>
        /// id user currently must be added
        /// </summary>
        public int Id { get; set; }
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
        public bool NamePortionIsValid { get; set; }
        /// <summary>
        /// this property is for new user you are about to add
        /// </summary>
        private User DummyNewUser { get; set; }

        #region Commands
        /// <summary>
        /// First commnad to beloaded
        /// </summary>
        public ICommand LoadDeptsCommand { get; set; }
        public ICommand SetNewUser { get; set; }
        #endregion
        #region constructor
        /// <summary>
        /// Intialize all Properties and Commands.
        /// </summary>
        public AddUserViewModel()
        {
            Id = 0;

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
            LoadDeptsCommand = new RelyCommand(async () => await LoadRealTimeValues());

            SetNewUser = new RelyCommand(async()=>await AddNewUser());

        }
        #endregion

        #region Methods Helpers


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
                   // department section
                   DeptsStored.Clear();
                   //this is bug you can't use assigning method you need to add as tree with childs
                   var _depts = await ReadAllDepartments(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);

                   for (int x = 0; x < _depts.Count; x++)
                   {
                       DeptsStored.Add(new Department() { Id = _depts[x].Id, Name = _depts[x].Name });
                   }

                   SelectedDept = DeptsStored[0];

                   //id section 
                   Id = await GetNextId("user_id", "users", VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);


                   //date
                   DateOfAdditon = DateTime.Now.ToString();

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


                OracleDataReader dr = sqlCMD.ExecuteReader();

                ObservableCollection<Department> depts = new ObservableCollection<Department>();

                if (dr.HasRows)
                {


                    while (dr.Read())
                    {
                        depts.Add(new Department() { Id = Int32.Parse(dr["dept_id"].ToString()), Name = dr["dept_name"].ToString() });

                    }
                }





                return depts;

            });

        }
        /// <summary>
        /// Get the highest id in any table then increment to be the next id. usully used by insertion methods.
        /// </summary>
        /// <returns></returns>
        private Task<int> GetNextId(string idColumn, string tableName, OracleConnection myOpenedTunnel)
        {
            return Task.Run(() =>
            {
                int id_comu = Scripts.GetHighestNOofRow(myOpenedTunnel, tableName, idColumn);
                //increment for next row always.
                return id_comu + 1;
            });
        }
        /// <summary>
        /// This method mainly designed for colecting all fields or props inside one object which is property too!.
        /// </summary>
        /// <returns></returns>
        private User CreateDummyUser()
        {
            //this field to carry the number for cheker!
            int mobileNumber = 000000000;

            DummyNewUser = new User();
            DummyNewUser.Id = this.Id;
            DummyNewUser.UserName = this.UserName;
            DummyNewUser.FullName = FirstPortionFName + " " + MiddlePortionFName + " " + LastPortionFName;
            DummyNewUser.Password = this.Password;
            DummyNewUser.DepartmentId = this.SelectedDept.Id;
            DummyNewUser.UserAuthLevel = this.SelectedAuth.DataFromDatabase;
            DummyNewUser.TelNo = int.TryParse(this.ContactNumber, out mobileNumber) ? mobileNumber : mobileNumber = 123456789;
            DummyNewUser.LastSeen = Convert.ToDateTime(this.DateOfAdditon);
            DummyNewUser.UserInSession = "no";
            var x = DummyNewUser;
            AccioEasyHelpers.InspectMyObject<User>(x);
            return DummyNewUser;
        }
        /// <summary>
        /// This function can hanlde all <see cref="T"/> data types to chech whether null or empty in size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="props"></param>
        /// <returns></returns>
        private bool CheckValidityToAcceptVals<T>(List<T> props) where T : new()
        {

            List<bool> signature = new List<bool>();
            
            foreach (T t in props)
            {
                if (t == null)
                {
                    signature.Add(false);
                    break;
                  
                }
                if (t.GetType() == typeof(string))
                {
                    string value = (t as string);
                    if (string.IsNullOrEmpty(value))
                    {
                        signature.Add(false);
                    }
                }
                if (t.GetType() == typeof(DateTime))
                {
                    DateTime value = Convert.ToDateTime(t);
                    if (value == null)
                    {
                        signature.Add(false);
                    }
                }
                if (t.GetType() == typeof(int))
                {
                    int value = Convert.ToInt32(t);
                    if (value == null)
                    {
                        signature.Add(false);
                    }
                }
                if (t.GetType() == typeof(Department))
                {
                    Department value = t as Department;
                    if (value == null)
                    {
                        signature.Add(false);
                    }
                }

            }
            foreach (bool fs in signature)
            {
                if (!fs) return false;
            }
            return true;
        }
        /// <summary>
        /// The real deal method with adding to oracle table.
        /// </summary>
        /// <param name="newUsr"></param>
        /// <returns></returns>
        private async Task<int> InsertUserRow(User newUsr)
        {
            await Task.Delay(1);
            var replyOfOracle = Scripts.InsertMyDataRow(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn, "users",
            new string[]
            {
                    newUsr.Id.ToString(),"'"+newUsr.UserName+"'","'"+newUsr.FullName+"'","'" + newUsr.Password + "'",newUsr.TelNo.ToString(),"DATE '"+newUsr.LastSeen.Year.ToString()+"-"+newUsr.LastSeen.Month.ToString()+"-"+newUsr.LastSeen.Day.ToString()+"'","''","'" + newUsr.UserAuthLevel + "'" ,newUsr.DepartmentId.ToString()
                   }

            );
            return replyOfOracle;
        }
        /// <summary>
        /// THe whole comibation to add new user.
        /// </summary>
        /// <returns></returns>
        private async Task AddNewUser()
        {
            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
                //check values
                var valid = CheckValidityToAcceptVals(new List<object> { FirstPortionFName, MiddlePortionFName, LastPortionFName, UserName, Password, ContactNumber });
                 
                if (valid)
                {

                    var newUser = CreateDummyUser();

                    
                    int replyOfCMD_Insertion = await InsertUserRow(newUser);

                    //success
                    if (replyOfCMD_Insertion >= 0)
                    {
                        new AdvOptions().ShowSuccess_AddUser(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                    }
                    //fail
                    else
                    {
                        new AdvOptions().ShowFailur_AddUser(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                    }
                    

                }
                else
                {
                    new AdvOptions().ShowFailur_AddUser(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));

                }
            });
        }
        #endregion
    }
}
