

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
    public partial class BackupTool : BaseMessage
    {
        #region ModelView instance
        BackupToolViewModel BackupToolViewModel = new BackupToolViewModel();
        #endregion
        #region Constructor
        public BackupTool()
        {

            base.InitializeComponent();
            this.InitializeComponent();

            try
            {
                System.Drawing.Icon ico = new System.Drawing.Icon(base.GetIconFile(@"springserverTM.ico"));
                this.Icon = ico;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            base.Text = "BackupTool";
            this.TopMost = true;
            this.sfBarcode1.Text = "0";
            this.sfBarcode1.Visible = false;

            this.progressBarAdv1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv1.WaitingGradientEnabled = true;



            this.textBoxExt2.SetImgeForFirstTime(new Bitmap(Properties.Resources.icons8_eye_2_48, this.textBoxExt2.LabelEyeRevealler.Size));
            this.textBoxExt2.SetImageForHover(new Bitmap(Properties.Resources.icons8_eye_48, this.textBoxExt2.LabelEyeRevealler.Size));

            //link all properties to their controlers

            this.label2.DataBindings.Add(new Binding("Text", BackupToolViewModel, "CorpName"));

            //assign progressbar properties [visibility & Running for loading]
            progressBarAdv1.DataBindings.Add(new Binding("Visible", BackupToolViewModel, "Loading"));
            progressBarAdv1.DataBindings.Add(new Binding("WaitingGradientEnabled", BackupToolViewModel, "WaitingProgress"));
            gradientLabel1.DataBindings.Add(new Binding("Image", BackupToolViewModel, "Logo", true));

            gradientLabel1.Paint += GradientLabel1_Paint;

            this.Load += BackupTool_Load;
            
        }

        private void GradientLabel1_Paint(object sender, PaintEventArgs e)
        {
             var lbl = (Label)sender;
            lbl.Image = new Bitmap(lbl.Image, lbl.Size);
        }

        private void BackupTool_Load(object sender, EventArgs e)
        {
            BackupToolViewModel.UpdateCorporationNameCommand.Execute(true);
        }
        #endregion
        #region Events

        private void okbtn_Click(object sender, EventArgs e)
        {
            
        }

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
    public class BackupToolViewModel : BaseViewModel
    {
        #region Poperties
        /// <summary>
        /// Name of corp
        /// </summary>
        public string CorpName { get; set; }
        /// <summary>
        /// Waiting flag
        /// </summary>
        public bool WaitingProgress { get; set; }

        public Image Logo { get; set; }
        /// <summary>
        /// Loading flag for prgress bar visible or not
        /// </summary>
        public bool Loading { get { return WaitingProgress; } }
        #endregion

        #region Commands
        /// <summary>
        /// Command update for events 
        /// </summary>
        public ICommand UpdateCorporationNameCommand { get; set; }
        #endregion
        #region Constructor
        public BackupToolViewModel()
        {

            UpdateCorporationNameCommand = new RelyCommand(async () => await LoadParamsThenGetCorpName());

            WaitingProgress = false;

        }

        #endregion

        #region Methods Helpers
       
        /// <summary>
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task LoadParamsThenGetCorpName()
        {
           

            await RunCommand(() => this.WaitingProgress, async () => 
            {

                //must put awaitable task func connected to database

                CorpName = await UpdateCurrentCorporation(VMCentral.DockingManagerViewModel.MyAppOnlyObjctConn);


               Logo = ((Image)new BoolToImgConverter().Convert(true, null, null, null));


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
