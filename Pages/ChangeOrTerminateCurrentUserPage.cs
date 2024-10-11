using Spring.Pages.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;
using Spring.Helpers.Controls;
using Spring.View.PanelView;
using Spring.ViewControls;
using Syncfusion.Windows.Forms.Tools.XPMenus;

namespace Spring.Pages
{
    public partial class ChangeOrTerminateCurrentUserPage : BasePage
    {

        ChangeOrTerminateCurrentUserViewModel changeOrTerminateCurrentUserViewModel = new ChangeOrTerminateCurrentUserViewModel();
        public ChangeOrTerminateCurrentUserPage() 
        {
            #region UI customizations
            InitializeComponent();

            //some UI customizations
            this.authlvlcombo.ThemeName = "Metro";
            this.authlvlcombo.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.SingleSelection;
            this.authlvlcombo.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDown;
            this.authlvlcombo.BackColor = ColorTranslator.FromHtml("#eaf0ff");

            this.authlvlcombo.KeyPress += (bj, e) => {
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


            tableLayoutPanel2.Visible = false;
            this.splitButton1.MouseClick += SplitButton1_DropDowItemClicked;

            

            #endregion


            #region Bindings
            //properties bindings
            BindingSource aithtsbindingSource = new BindingSource();
            aithtsbindingSource.DataSource = changeOrTerminateCurrentUserViewModel.AuthritiesUsed;
            this.authlvlcombo.DataSource = aithtsbindingSource.DataSource;
            //Bind the Display member and Value member to the data source
            this.authlvlcombo.DisplayMember = "Title";
            this.authlvlcombo.ValueMember = "DataFromDatabase";
            //
            this.authlvlcombo.DataBindings.Add(new Binding("SelectedItem", changeOrTerminateCurrentUserViewModel, "SelectedAuth", true, DataSourceUpdateMode.OnPropertyChanged));

            //properties bindings
            BindingSource deptsbindingSource = new BindingSource();
            deptsbindingSource.DataSource = changeOrTerminateCurrentUserViewModel.DeptsStored;
            this.deptcombo.DataSource = deptsbindingSource;
            //Bind the Display member and Value member to the data source
            this.deptcombo.DisplayMember = "Name";
            this.deptcombo.ValueMember = "Id";
            //
            this.deptcombo.DataBindings.Add(new Binding("SelectedItem", changeOrTerminateCurrentUserViewModel, "SelectedDept", true, DataSourceUpdateMode.OnPropertyChanged));


            //binding active flag to panel
            //this.DataBindings.Add(new Binding("Enabled", changeOrTerminateCurrentUserViewModel, "ActivePanel"));

             
            /////////////////
            ///input id
            this.idTobeSearchedInput.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "Id", false, DataSourceUpdateMode.OnPropertyChanged));

            //fname
            this.fnametxtbx.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "FirstPortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //sname
            this.snametxtbx.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "MiddlePortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //lname
            this.lnametxtbx.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "LastPortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //username [login]
            this.usernametxtbx.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "UserName", false, DataSourceUpdateMode.OnPropertyChanged));
            //pass
            this.passtxtbx.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "Password", false, DataSourceUpdateMode.OnPropertyChanged));
            //date
            this.sfDateTimeEdit1.DataBindings.Add("Value", changeOrTerminateCurrentUserViewModel, "DateOfAdditon", true, DataSourceUpdateMode.OnPropertyChanged);
            //contact
            this.contactinfo.DataBindings.Add(new Binding("Text", changeOrTerminateCurrentUserViewModel, "ContactNumber", true, DataSourceUpdateMode.OnPropertyChanged));

            /*
            //checkers icons
            //name portions checker lbl
            this.checkerfullname.DataBindings.Add(new Binding("Visible", changeOrTerminateCurrentUserViewModel, "NamePortionsCheckerVisiblity"));
            //username..
            this.checkerusername.DataBindings.Add(new Binding("Visible", changeOrTerminateCurrentUserViewModel, "UserNameCheckerVisibilty"));
            //password..
            this.checkerpass.DataBindings.Add(new Binding("Visible", changeOrTerminateCurrentUserViewModel, "PassCheckerVisibilty"));
            //contact number here depending on country laws
            this.checkercontact.DataBindings.Add(new Binding("Visible", changeOrTerminateCurrentUserViewModel, "ContactCheckerVisibilty"));
            //auth 
            this.checkerauth.DataBindings.Add(new Binding("Visible", changeOrTerminateCurrentUserViewModel, "AuthCheckerVisibilty"));
            //Dept
            this.checkerdept.DataBindings.Add(new Binding("Visible", changeOrTerminateCurrentUserViewModel, "DeptCheckerVisibilty"));
            */
            #endregion
            #region Event as fix for timedatepicker
            sfDateTimeEdit1.ValueChanged += sfDateTimeEdit1_ValueChanged;


            void sfDateTimeEdit1_ValueChanged(object sender, Syncfusion.WinForms.Input.Events.DateTimeValueChangedEventArgs e)
            {
                sfDateTimeEdit1.DataBindings["Value"].WriteValue();
            }
            #endregion
            #region Events

            this.Load += ChangeOrTerminateCurrentUserPage_Load;


            //fix bug in UI related to change selected index when timepicker selected
            this.authlvlcombo.DropDownClosed += (s, e) => { this.passtxtbx.Focus(); };
            this.deptcombo.DropDownClosed += (s, e) => { this.passtxtbx.Focus(); };

            /*
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
            */

            #endregion

            //sfDataGrid1.Columns["Id"].FilterPredicates.Add(new FilterPredicate() { FilterType = FilterType.LessThan, FilterValue = "18" });
        }

        private void SplitButton1_DropDowItemClicked(object sender, EventArgs e)
        {
 
           var qr = new QRToolkit();

           tableLayoutPanel2.Visible = true;
           
          
           
            
        }

        public override void AddEventsToOptionsNodes(TreeViewAdv optionsTree)
        {
            optionsTree.BeforeSelect += OptionsTree_BeforeSelect;

            optionsTree.Click += OptionsTree_Click;

            #region view define
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesNodesNames.ChangeorTerminateCurrentUserPrimaryPageButtonName;
            #endregion


            OnLoad(new EventArgs());
        }

        #region Methods Helpers
        public override void OptionsTree_Click(object sender, EventArgs e)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, false, false);
            if (node != null && node == optionsTree.SelectedNode)
            {
                RaiseClick(node);
            }
        }

        public  override void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, false, false);
            if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                o.Cancel = true;
        }

        private void ChangeOrTerminateCurrentUserPage_Load(object sender, EventArgs e)
        {
            changeOrTerminateCurrentUserViewModel.LoadInitialWithRefrshing.Execute(true);
        }


        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled && adv != null)
            {
                if (adv.Text == PagesNodesNames.UsersFirstButtonTitle)
                {

                   
                }
                if (adv.Text == PagesNodesNames.UsersSecondButtonTitle)
                {
                  //  usersViewModel.LoadAllUsers.Execute(true); 
                }



            
        }
    }



        #endregion

        private void idTobeSearchedInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

        private void idTobeSearchedInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                changeOrTerminateCurrentUserViewModel.LoadCurrentUser.Execute(true);
        }

        private void idTobeSearchedInput_TextChanged(object sender, EventArgs e)
        {
             
        }
    }
}
