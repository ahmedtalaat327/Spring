

using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Data;
using Spring.Pages.ValueConverter;
using Spring.Pages.ViewModel;
using Spring.StaticVM;
using Spring.ViewModel;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Spring.Messages
{
    public partial class CardsDesigner : BaseMessage
    {
        #region ModelView instance
        CardsDesignerViewModel CardsDesignerViewModel = new CardsDesignerViewModel();
        #endregion
        #region Constructor
        public CardsDesigner()
        {

            base.InitializeComponent();
            this.InitializeComponent();
            


            base.Text = "CardsDesigner";
            this.TopMost = true;
            this.sfBarcode1.Text = "0";
            this.sfBarcode1.Visible = false;

            this.progressBarAdv1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv1.WaitingGradientEnabled = true;

            #region UI Customizations
            //some UI customizations
            this.tablescolscombo1.ThemeName = "Metro";
            this.tablescolscombo1.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.SingleSelection;
            this.tablescolscombo1.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDown;
            this.tablescolscombo1.BackColor = ColorTranslator.FromHtml("#eaf0ff");

            this.tablescolscombo1.KeyPress += (bj, e) => {
                //prevent change text

                bool isBackspace = e.KeyChar == '\b';

                // If we get anything other than a backspace, tell the rest of
                // the event processing logic to ignore this event
                if (!isBackspace)
                {
                    e.Handled = true;
                }

            };
            this.tablescolscombo2.ThemeName = "Metro";
            this.tablescolscombo2.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.SingleSelection;
            this.tablescolscombo2.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDown;
            this.tablescolscombo2.BackColor = ColorTranslator.FromHtml("#eaf0ff");

            this.tablescolscombo2.KeyPress += (bj, e) => {
                //prevent change text

                bool isBackspace = e.KeyChar == '\b';

                // If we get anything other than a backspace, tell the rest of
                // the event processing logic to ignore this event
                if (!isBackspace)
                {
                    e.Handled = true;
                }

            };
            this.tablescolscombo3.ThemeName = "Metro";
            this.tablescolscombo3.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.SingleSelection;
            this.tablescolscombo3.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDown;
            this.tablescolscombo3.BackColor = ColorTranslator.FromHtml("#eaf0ff");

            this.tablescolscombo3.KeyPress += (bj, e) => {
                //prevent change text

                bool isBackspace = e.KeyChar == '\b';

                // If we get anything other than a backspace, tell the rest of
                // the event processing logic to ignore this event
                if (!isBackspace)
                {
                    e.Handled = true;
                }

            };
            #endregion
            #region Binding
            ///////////////////////////////////[     first combobox     ]/////////////////////
            //link all properties to their controlers
            //properties bindings----------
            BindingSource aithtsbindingSource = new BindingSource();
            aithtsbindingSource.DataSource = CardsDesignerViewModel.ColumnsHeaders;
            this.tablescolscombo1.DataSource = aithtsbindingSource.DataSource;
            //Bind the Display member and Value member to the data source
            this.tablescolscombo1.DisplayMember = "Name";
            this.tablescolscombo1.ValueMember = "Id";
            //-----------------------------
            this.tablescolscombo1.DataBindings.Add(new Binding("SelectedItem", CardsDesignerViewModel, "RelationFName", true, DataSourceUpdateMode.OnPropertyChanged));
            ///////////////////////////////////[     second combobox     ]///////////////////////
            BindingSource aiathtsbindingSource = new BindingSource();
            aiathtsbindingSource.DataSource = CardsDesignerViewModel.ColumnsHeaders;
            this.tablescolscombo2.DataSource = aiathtsbindingSource.DataSource;
            //Bind the Display member and Value member to the data source
            this.tablescolscombo2.DisplayMember = "Name";
            this.tablescolscombo2.ValueMember = "Id";
            //-----------------------------
            this.tablescolscombo2.DataBindings.Add(new Binding("SelectedItem", CardsDesignerViewModel, "RelationSName", true, DataSourceUpdateMode.OnPropertyChanged));
            ///////////////////////////////////[     third combobox     ]///////////////////////
            BindingSource aiaathtsbindingSource = new BindingSource();
            aiaathtsbindingSource.DataSource = CardsDesignerViewModel.ColumnsHeaders;
            this.tablescolscombo3.DataSource = aiaathtsbindingSource.DataSource;
            //Bind the Display member and Value member to the data source
            this.tablescolscombo3.DisplayMember = "Name";
            this.tablescolscombo3.ValueMember = "Id";
            //-----------------------------
            this.tablescolscombo3.DataBindings.Add(new Binding("SelectedItem", CardsDesignerViewModel, "RelationTName", true, DataSourceUpdateMode.OnPropertyChanged));
            //////////////\\\\\\\\\\\\\\\\\------------------------------------------------\\\\\\\\\\\\\\\\\\\\\\///////////////////////

            //------assign progressbar properties [visibility & Running for loading]-------
            progressBarAdv1.DataBindings.Add(new Binding("Visible", CardsDesignerViewModel, "Loading"));
            progressBarAdv1.DataBindings.Add(new Binding("WaitingGradientEnabled", CardsDesignerViewModel, "WaitingProgress"));
            label1.DataBindings.Add(new Binding("Text", CardsDesignerViewModel, "CurrentDepartment", true));
            textBoxExt5.DataBindings.Add(new Binding("Text", CardsDesignerViewModel, "RelationFNameType", true));
            textBoxExt6.DataBindings.Add(new Binding("Text", CardsDesignerViewModel, "RelationSNameType", true));
            textBoxExt7.DataBindings.Add(new Binding("Text", CardsDesignerViewModel, "RelationTNameType", true));
            #endregion
            this.tablescolscombo1.Enabled = false;
            this.tablescolscombo1.SelectedIndexChanged += (s, e) => { CardsDesignerViewModel.GiveDataType.Execute(1); };
            this.tablescolscombo2.SelectedIndexChanged += (s, e) => { CardsDesignerViewModel.GiveDataType.Execute(2); };
            this.tablescolscombo3.SelectedIndexChanged += (s, e) => { CardsDesignerViewModel.GiveDataType.Execute(3); };
            this.tablescolscombo1.DropDownClosed += (s, e) => { this.relationstobeusedtxtbx.Focus(); };
            this.tablescolscombo2.DropDownClosed += (s, e) => { this.relationstobeusedtxtbx.Focus(); };
            this.tablescolscombo3.DropDownClosed += (s, e) => { this.relationstobeusedtxtbx.Focus(); };

            this.Load += CardsDesigner_Load;
            
        }

        private void GradientLabel1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void CardsDesigner_Load(object sender, EventArgs e)
        {
            CardsDesignerViewModel.CheckValuntertyForCard.Execute(true);
        }
        #endregion
        #region Events

        private void editbtn_Click(object sender, EventArgs e)
        {
             

        }
        private void passtxtbx_TextChanged(object sender, EventArgs e)
        {
           
        }

        #endregion

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    //this View model may be moved to a seperate file .cs 
    public class CardsDesignerViewModel : BaseViewModel
    {
        #region Poperties
        /// <summary>
        /// Name of corp
        /// </summary>
        public string CurrentDepartment { get; set; }
        /// <summary>
        /// Waiting flag
        /// </summary>
        public bool WaitingProgress { get; set; }
 
        public string NoRelations { get; set; }
        public ColumnDataDef RelationFName { get; set; }
        public ColumnDataDef RelationSName { get; set; }
        public ColumnDataDef RelationTName { get; set; }

        public string RelationFnameType { get; set; }
        public string RelationSnameType { get; set; }
        public string RelationTnameType { get; set; }

        public ObservableCollection<ColumnDataDef> ColumnsHeaders { get; set; }
        public bool Loading { get { return WaitingProgress; } }
        #endregion

        #region Commands
        /// <summary>
        /// Command update for events 
        /// </summary>
        public ICommand CheckValuntertyForCard { get; set; }
        public ICommand GiveDataType { get; set; }
        #endregion
        #region Constructor
        public CardsDesignerViewModel()
        {

            CheckValuntertyForCard = new RelyCommand(async () => await LoadDeptForCurrentUser());

            WaitingProgress = false;

            ColumnsHeaders = new ObservableCollection<ColumnDataDef>() { new ColumnDataDef() { Id = 0, Name = "user_name" , ParentTable = "users" } };

            GiveDataType = new RelayParameterizedCommand(async(x)=> await GetDataTypeForRelation(x));
          
         
        }

        #endregion

        #region Methods Helpers
       
        /// <summary>
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task LoadDeptForCurrentUser()
        {
           

            await RunCommand(() => this.WaitingProgress, async () => 
            {
                await Task.Delay(1);
                //must put awaitable task func connected to database

               
                this.CurrentDepartment = VMCentral.DockingManagerViewModel.loggedUser.DepartmentName;

                ColumnsHeaders.Clear();
                var _columnsHeaders = await GetColumnsNamesFromTable((object)"users");
                foreach (var column in _columnsHeaders) { 
                    ColumnsHeaders.Add(column);
                }

                if (ColumnsHeaders.Count > 0)
                {
                    RelationFName = ColumnsHeaders[0];
                    RelationSName = ColumnsHeaders[0];
                    RelationTName = ColumnsHeaders[0];
                }

            });
           
        }
      private async Task GetDataTypeForRelation(object caseOfRel)
        {
            var casR = (Int32)caseOfRel;
            await Task.Delay(2);
            if(casR == 1)
                this.RelationFnameType = this.RelationFName.Description;
            if(casR == 2)
                this.RelationSnameType = this.RelationSName.Description;
            if(casR == 3)
                this.RelationTnameType = this.RelationTName.Description;
        }

        private async Task<ObservableCollection<ColumnDataDef>> GetColumnsNamesFromTable(object tableName)
        {
            int c = 0;
            ObservableCollection<ColumnDataDef> colsNames = new ObservableCollection<ColumnDataDef>();

            await Task.Run(() =>
            {


                var myOpenedTunnel = VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn;

                var sqlCMD = Scripts.FetchMySQLText(myOpenedTunnel, $"select column_name from USER_TAB_COLUMNS where upper(table_name) = '{tableName.ToString().ToUpper(new CultureInfo("en-US", false))}'");
                OracleDataReader dr = null;

                try
                {
                    dr = sqlCMD.ExecuteReader();
                    if (dr.HasRows)
                    {


                        while (dr.Read())
                        {


                            colsNames.Add(new ColumnDataDef()
                            {

                                Id = c,
                                Name = dr["COLUMN_NAME"].ToString(),
                                ParentTable = tableName.ToString()


                            });
                            c++;
                        }
                       


                    }

                }
                catch (Exception xorcl)
                {
                    //for debug purposes
                    Console.WriteLine(xorcl.Message);
                    //Connection error for somereason so aggresive close that connection
                    VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Dispose(); VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn.Close();
                    //colsType.Add("failed to get any data type");
                 

                }
              //  return colsNames;

            });
            return colsNames;
        }

        #endregion

    }
}
