﻿using Spring.Messages;
using Spring.Pages.ViewModel;
using Spring.StaticVM;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.Pages
{
    public partial class AddUserPage : BasePage
    {
        #region Members
        /// <summary>
        /// new instance from the VM
        /// </summary>
        AddUserViewModel addUserViewModel = new AddUserViewModel();
        #endregion
        #region ctr
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="optionsTree"></param>
        public AddUserPage(TreeViewAdv optionsTree)
        {
            #region UI customizations
            InitializeComponent();
 
            //some UI customizations
            this.authlvlcombo.ThemeName = "Metro";
            this.authlvlcombo.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.SingleSelection;
            this.authlvlcombo.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDown;
            this.authlvlcombo.BackColor = ColorTranslator.FromHtml("#eaf0ff");
           
            this.authlvlcombo.KeyPress += (bj,e) => {
                //prevent change text
                
                bool isBackspace = e.KeyChar == '\b';

                // If we get anything other than a backspace, tell the rest of
                // the event processing logic to ignore this event
                if (!isBackspace)
                {
                    e.Handled = true;
                }

            };

            this.deptcombo.ThemeName = "Metro";
            this.deptcombo.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.SingleSelection;
            this.deptcombo.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDown;
            this.deptcombo.BackColor = ColorTranslator.FromHtml("#eaf0ff");

            this.deptcombo.KeyPress += (bj, e) => {
                //prevent change text

                bool isBackspace = e.KeyChar == '\b';

                // If we get anything other than a backspace, tell the rest of
                // the event processing logic to ignore this event
                if (!isBackspace)
                {
                    e.Handled = true;
                }

            };


            this.sfDateTimeEdit1.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.sfDateTimeEdit1.Value = DateTime.Today;



            this.sfToolTip1.SetToolTip(this.idlabl, "this is the ID number generated by the system connected with barcode!");

            #endregion



            #region Bindings
            //properties bindings
            BindingSource aithtsbindingSource = new BindingSource();
            aithtsbindingSource.DataSource = addUserViewModel.AuthritiesUsed;
            this.authlvlcombo.DataSource = aithtsbindingSource.DataSource;
            //Bind the Display member and Value member to the data source
            this.authlvlcombo.DisplayMember = "Title";
            this.authlvlcombo.ValueMember = "DataFromDatabase";
            //
            this.authlvlcombo.DataBindings.Add(new Binding("SelectedItem", addUserViewModel, "SelectedAuth", true, DataSourceUpdateMode.OnPropertyChanged));

            //properties bindings
            BindingSource deptsbindingSource = new BindingSource();
            deptsbindingSource.DataSource = addUserViewModel.DeptsStored;
            this.deptcombo.DataSource = deptsbindingSource;
            //Bind the Display member and Value member to the data source
            this.deptcombo.DisplayMember = "Name";
            this.deptcombo.ValueMember = "Id";
            //
            this.deptcombo.DataBindings.Add(new Binding("SelectedItem", addUserViewModel, "SelectedDept", true, DataSourceUpdateMode.OnPropertyChanged));


            //binding active flag to panel
            this.DataBindings.Add(new Binding("Enabled", addUserViewModel, "ActivePanel"));

            //texts [id to bar code]
            this.sfBarcode1.DataBindings.Add(new Binding("Text", addUserViewModel, "Id"));
            //[id to label]
            this.idlabl.DataBindings.Add(new Binding("Text", addUserViewModel, "Id"));
            /////////////////
            //fname
            this.fnametxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "FirstPortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //sname
            this.snametxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "MiddlePortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //lname
            this.lnametxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "LastPortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //username [login]
            this.usernametxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "UserName", false, DataSourceUpdateMode.OnPropertyChanged));
            //pass
            this.passtxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "Password", false, DataSourceUpdateMode.OnPropertyChanged));
            //date
            this.sfDateTimeEdit1.DataBindings.Add("Value", addUserViewModel, "DateOfAdditon", true, DataSourceUpdateMode.OnPropertyChanged);
            //contact
            this.contactinfo.DataBindings.Add(new Binding("Text", addUserViewModel, "ContactNumber", true, DataSourceUpdateMode.OnPropertyChanged));

            //checkers icons
            //name portions checker lbl
            this.checkerfullname.DataBindings.Add(new Binding("Visible", addUserViewModel, "NamePortionsCheckerVisiblity"));
            //username..
            this.checkerusername.DataBindings.Add(new Binding("Visible", addUserViewModel, "UserNameCheckerVisibilty"));
            //password..
            this.checkerpass.DataBindings.Add(new Binding("Visible", addUserViewModel, "PassCheckerVisibilty"));
            //contact number here depending on country laws
            this.checkercontact.DataBindings.Add(new Binding("Visible", addUserViewModel, "ContactCheckerVisibilty"));


            #region Event as fix for timedatepicker
            sfDateTimeEdit1.ValueChanged += sfDateTimeEdit1_ValueChanged;


            void sfDateTimeEdit1_ValueChanged(object sender, Syncfusion.WinForms.Input.Events.DateTimeValueChangedEventArgs e)
            {
                sfDateTimeEdit1.DataBindings["Value"].WriteValue();
            }
            #endregion


            #endregion

            #region Events
            this.Load += AddUserPage_Load;
            
            optionsTree.BeforeSelect += (e, o) =>
            {

                Point pt = optionsTree.PointToClient(Cursor.Position);
                TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
                if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                    o.Cancel = true;
            };


            optionsTree.Click += (evt, obj) =>
            {

                Point pt = optionsTree.PointToClient(Cursor.Position);
                TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
                if (node != null && node == optionsTree.SelectedNode)
                {
                    RaiseClick(node);
                }


            };

            optionsTree.AfterSelect += (eve, ob) =>
            {
              //  RaiseClick(optionsTree.SelectedNode);
            };


            this.fnametxtbx.TextChanged += (s, e) => { addUserViewModel.FullNameChecking.Execute(true); };
            this.snametxtbx.TextChanged += (s, e) => { addUserViewModel.FullNameChecking.Execute(true); };
            this.lnametxtbx.TextChanged += (s, e) => { addUserViewModel.FullNameChecking.Execute(true); };

            this.usernametxtbx.TextChanged += (s, e) => { addUserViewModel.UserNameChecking.Execute(true);};
            this.passtxtbx.TextChanged += (s, e) => { addUserViewModel.PassChecking.Execute(true); };
            this.contactinfo.TextChanged += (s, e) => { addUserViewModel.ContactChecking.Execute(true); };

            #endregion
        }
        /// <summary>
        /// when page load 
        /// make all commnds executed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUserPage_Load(object sender, EventArgs e)
        {
            addUserViewModel.LoadDeptsCommand.Execute(true);
        }
        #endregion

        #region Methods Helpers
        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled&&adv!=null)
            {
                switch (adv.Text)
                {
                    case "Print out": VMCentral.DockingManagerViewModel.Loading = true; addUserViewModel.AuthritiesUsed.Add(new Data.Account() { DataFromDatabase = "test",Title = "Test"});  break;
                    case "Proceed to add": addUserViewModel.SetNewUser.Execute(true); break;


                }
            }
        }
        #endregion


    }
}
