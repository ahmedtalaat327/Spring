

using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Pages.ValueConverter;
using Spring.StaticVM;
using Spring.ViewModel;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Drawing;
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
            //link all properties to their controlers

            // this.label2.DataBindings.Add(new Binding("Text", CardsDesignerViewModel, "CorpName"));

            //assign progressbar properties [visibility & Running for loading]
            progressBarAdv1.DataBindings.Add(new Binding("Visible", CardsDesignerViewModel, "Loading"));
            progressBarAdv1.DataBindings.Add(new Binding("WaitingGradientEnabled", CardsDesignerViewModel, "WaitingProgress"));
            label1.DataBindings.Add(new Binding("Text", CardsDesignerViewModel, "CurrentDepartment", true));

         //   gradientLabel1.Paint += GradientLabel1_Paint;

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
        public string RelationFName { get; set; }
        public string RelationSName { get; set; }
        public string RelationTName { get; set; }

        public string RelationFnameType { get; set; }
        public string RelationSnameType { get; set; }
        public string RelationTnameType { get; set; }

        public bool Loading { get { return WaitingProgress; } }
        #endregion

        #region Commands
        /// <summary>
        /// Command update for events 
        /// </summary>
        public ICommand CheckValuntertyForCard { get; set; }
        #endregion
        #region Constructor
        public CardsDesignerViewModel()
        {

            CheckValuntertyForCard = new RelyCommand(async () => await LoadDeptForCurrentUser());

            WaitingProgress = false;

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

                // CorpName = await UpdateCurrentCorporation(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);


                // Logo = ((Image)new BoolToImgConverter().Convert(true, null, null, null));
                this.CurrentDepartment = VMCentral.DockingManagerViewModel.loggedUser.DepartmentName;

            });
           
        }
        /// <summary>
        /// update current user 
        /// </summary>
        /// <param name="_user">current user</param>
        /// <param name="inputDate">current date</param>
        /// <param name="myOpenedTunnel">connection object</param>
        /// <returns></returns>
        private Task<string> UpdateCurrentCorporation(OracleConnection myOpenedTunnel)
        {
            //
            return Task.Run(() =>
            {

                string[] data;
                try
                {
                    //read params from config
                    data = AccioEasyHelpers.ReadTxTFiles(AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring.exe").Length) + "init\\params.info");
                }
                catch (Exception excF)
                {
                    data = AccioEasyHelpers.ReadTxTFiles(AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring for Server.exe").Length) + "init\\params.info");
                }

                var corp_name = AccioEasyHelpers.GetTxTBettwen(data[6], "::", ",");
              

                return corp_name;

            });
            
        }


        #endregion

    }
}
