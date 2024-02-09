using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.Data;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spring.Pages.ViewModel
{
    public class UsersViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// determines if panel is active or not
        /// </summary>
        public bool ActivePanel { get => VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin") ? true : false; }
        /// <summary>
        /// Current users list
        /// </summary>
        public ObservableCollection<User> CurrentUsers = new ObservableCollection<User> { };


        #endregion
        #region Commands
        public ICommand LoadAllUsers { get; set; }
        #endregion
        public UsersViewModel() {
            //init props
            CurrentUsers.Add(new User() { Id=4, DepartmentId=2,FullName="Kamal Mohamed Ali"});
            //init commands
            LoadAllUsers = new RelyCommand(async() =>await LoadCurrentUsers());
        }
        /// <summary>
        /// Fetching all users with condition id>-1.
        /// thwn assign them to <see cref="CurrentUsers"/>
        /// </summary>
        /// <returns></returns>
        private async Task LoadCurrentUsers()
        {
            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
                await Task.Delay(1);
                var sqlCMD = Scripts.FetchMyData(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn,
                "users",
                new string[] { "user_id", "user_name", "user_full_name", "user_password", "user_tel", "user_seen_date", "user_session", "user_auth", "dept_id" }, new string[] { "user_id" }, new string[] { "-1" }, ">", "and", true, "user_id");

                OracleDataReader dr = sqlCMD.ExecuteReader();

                if (dr.HasRows)
                {


                    while (dr.Read())
                    {
                        CurrentUsers.Add(new User()
                        {
                            Id = Int32.Parse(dr["user_id"].ToString()),
                            UserName = dr["user_name"].ToString(),
                            FullName = dr["user_full_name"].ToString(),
                            Password = dr["user_password"].ToString(),
                            TelNo = Int32.Parse(dr["user_tel"].ToString()),
                            LastSeen = (DateTime)dr["user_seen_date"],
                            UserInSession = dr["user_session"].ToString(),
                            UserAuthLevel = dr["user_auth"].ToString(),
                            DepartmentId = Int32.Parse(dr["dept_id"].ToString())
                        });

                    }
                }
            });
        }
    }
}
