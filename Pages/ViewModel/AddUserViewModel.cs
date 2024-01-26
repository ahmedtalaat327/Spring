﻿
using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Data;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public string LastPortionFName { get; set;}
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// user name used
        /// </summary>
        public string UserName { get; set;}
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
        /// Checker for any portion in textbox
        /// </summary>
        public bool NamePortionIsValid { get; set; }

        #region Commands
        /// <summary>
        /// First commnad to beloaded
        /// </summary>
        public ICommand LoadDeptsCommand { get; set; }
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


            SelectedDept = DeptsStored[0];


            //init commands
            LoadDeptsCommand = new RelyCommand(async () => await LoadDepsToComboBox());

          

        }
        #endregion

        #region Methods Helpers
        /// <summary>
        /// Get the oracle Connection
        /// </summary>
        /// <param name="closeOrNot">Flag to close the connection or not before using in other commands statements</param>
        /// <returns></returns>
        private Task<OracleConnection> GetOracleConnection(bool closeOrNot)
        {
            return Task.Run(() =>
            {
                return AccioEasyHelpers.ReadParamsThenConnectToDB(closeOrNot);
            });
        }
        /// <summary>
        /// get all depts in List of <see cref="Department"/>
        /// </summary>
        /// <returns></returns>
        private async Task LoadDepsToComboBox()
        {
            OracleConnection openedTunnel = null;
          
             await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
                try
                {
                    openedTunnel = await GetOracleConnection(false);
                    DeptsStored.Clear();
                    //this is bug you can't use assigning method you need to add as tree with childs
                    var _depts = await ReadAllDepartments(openedTunnel);

                    for(int x=0;x< _depts.Count;x++)
                    {
                        DeptsStored.Add(new Department() { Id = _depts[x].Id, Name = _depts[x].Name });
                    }

                    SelectedDept = DeptsStored[0];
                }
                catch (Exception ex)
                {
                    //Connection error for somereason
                    openedTunnel.Dispose(); openedTunnel.Close();
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
                        depts.Add(new Department() { Id = Int32.Parse(dr["dept_id"].ToString()), Name = dr["dept_name"].ToString() }) ;

                    }
                }

                myOpenedTunnel.Close();
                myOpenedTunnel.Dispose();



                return depts;

            });

        }
        #endregion
    }
}
