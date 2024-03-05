


using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Pages.ValueConverter;
using Spring.StaticVM;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;

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
                System.Drawing.Icon ico = global::Spring.Properties.Resources.springTM;
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

            this.progressBarAdv2.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv2.WaitingGradientEnabled = true;


            this.passField.SetImgeForFirstTime(new Bitmap(Properties.Resources.icons8_eye_2_48, this.passField.LabelEyeRevealler.Size));
            this.passField.SetImageForHover(new Bitmap(Properties.Resources.icons8_eye_48, this.passField.LabelEyeRevealler.Size));

            //link all properties to their controlers
            this.label2.DataBindings.Add(new Binding("Text", BackupToolViewModel, "CorpName"));

            //assign progressbar properties [visibility & Running for loading]
            //for some reason visivilty not binding here [maybe its console based application]
            progressBarAdv1.DataBindings.Add(new Binding("Visible", BackupToolViewModel, "Loading"));
            progressBarAdv1.DataBindings.Add(new Binding("WaitingGradientEnabled", BackupToolViewModel, "WaitingProgress"));
            //for some reason visivilty not binding here [maybe its console based application]
            progressBarAdv2.DataBindings.Add(new Binding("Visible", BackupToolViewModel, "Loading"));
            progressBarAdv2.DataBindings.Add(new Binding("WaitingGradientEnabled", BackupToolViewModel, "WaitingProgress"));

            gradientLabel1.DataBindings.Add(new Binding("Image", BackupToolViewModel, "Logo", true));

            userFeild.DataBindings.Add(new Binding("Text", BackupToolViewModel, "UserName", true));
            passField.DataBindings.Add(new Binding("Text", BackupToolViewModel, "Password", true));
            instanceField.DataBindings.Add(new Binding("Text", BackupToolViewModel, "InstanceDB", true));



            gradientLabel1.Paint += GradientLabel1_Paint;
            //fixing issue with visivilty binding issue..
            BackupToolViewModel.PropertyChanged += BackupToolViewModel_PropertyChanged;

            this.Load += BackupTool_Load;


            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", false));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));
            this.tableLayoutPanel5.Controls.Add(NewRow("KIDO","ELECTICAL ENGINEERING", true));


        }

        private void BackupToolViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BackupToolViewModel.WaitingProgress))
            {
                if (BackupToolViewModel.WaitingProgress)
                {
                    progressBarAdv2.Visible = true;
                    progressBarAdv1.Visible = true;
                }
                else
                {
                    progressBarAdv2.Visible = false;
                    progressBarAdv1.Visible = true;

                }
            }
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

        private void proceedBtn_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "DUMP (*.dump)|*.dump"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(saveFileDialog.FileName);
                if (path != null)
                {
                    string dirPath = Directory.GetParent(path).FullName;
                    BackupToolViewModel.StartDBDumpProcessCommand.Execute($"{dirPath}");
                }
                else
                {
                    return;
                }
            }

        }
        private Control NewRow(string username, string deptname, bool active)
        {
            System.Windows.Forms.Label label8 = new Label();
            System.Windows.Forms.Label label9 = new Label();
            Syncfusion.Windows.Forms.Tools.ImageStreamer imageStreamer4 = new Syncfusion.Windows.Forms.Tools.ImageStreamer();

            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = System.Windows.Forms.DockStyle.Fill;
            label9.Location = new System.Drawing.Point(264, 1);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(254, 23);
            label9.TabIndex = 1;
            label9.Text = $"{deptname}";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // imageStreamer4
            // 
            imageStreamer4.AllowDragging = true;
            if(active)
            imageStreamer4.BackgroundImage = global::Spring.Properties.Resources.icons8_ok_48;
            else
            imageStreamer4.BackgroundImage = global::Spring.Properties.Resources.icons8_proactivity_48;



            imageStreamer4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            imageStreamer4.Dock = System.Windows.Forms.DockStyle.Fill;
            imageStreamer4.InternalBackColor = System.Drawing.Color.Transparent;
            imageStreamer4.Location = new System.Drawing.Point(524, 4);
            imageStreamer4.Name = "imageStreamer4";
            imageStreamer4.Size = new System.Drawing.Size(255, 17);
            imageStreamer4.TabIndex = 2;
            imageStreamer4.Text = "image_here";
            imageStreamer4.TextAnimationDirection = Syncfusion.Windows.Forms.Tools.ImageStreamer.TextStreamDirection.RightToLeft;

            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = System.Windows.Forms.DockStyle.Fill;
            label8.Location = new System.Drawing.Point(4, 1);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(254, 23);
            label8.TabIndex = 0;
            label8.Text = $"{username}";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 

            TableLayoutPanel tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
       | System.Windows.Forms.AnchorStyles.Left)
       | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel6.BackColor = System.Drawing.Color.LightSteelBlue;
            tableLayoutPanel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel5.SetColumnSpan(tableLayoutPanel6, 4);
            tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel6.Controls.Add(label8, 0, 0);
            tableLayoutPanel6.Controls.Add(label9, 1, 0);
            tableLayoutPanel6.Controls.Add(imageStreamer4, 2, 0);
            tableLayoutPanel6.Location = new System.Drawing.Point(35, 155);
            tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(5);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(1);
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel6.Size = new System.Drawing.Size(783, 25);
            tableLayoutPanel6.TabIndex = 3;

            return tableLayoutPanel6;
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
        /// <summary>
        /// Corpo logo image
        /// </summary>
        public Image Logo { get; set; }
        /// <summary>
        /// Loading flag for prgress bar visible or not
        /// </summary>
        public bool Loading { get { return WaitingProgress; } }
        /// <summary>
        /// User name for database admin for SQL*
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
        /// Pass for database admin for SQL*
        /// </summary>
        public string Password { get; set; } = "";
        /// <summary>
        /// @name for database admin for SQL*
        /// </summary>
        public string InstanceDB { get; set; } = "@instance";
        #endregion

        #region Commands
        /// <summary>
        /// Command update for events 
        /// </summary>
        public ICommand UpdateCorporationNameCommand { get; set; }
        /// <summary>
        /// Command for dumping current database 
        /// </summary>
        public ICommand StartDBDumpProcessCommand { get; set; }
        #endregion
        #region Constructor
        public BackupToolViewModel()
        {
            WaitingProgress = false;

            UpdateCorporationNameCommand = new RelyCommand(async () => await LoadParamsThenGetCorpName());

            StartDBDumpProcessCommand = new RelayParameterizedCommand(async (xpath) => await ExecuteDBDUMP(xpath));

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
        /// <summary>
        /// Update user info after assigning password from current property
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteDBDUMP(object pathToSave)
        {
            await RunCommand(() => this.WaitingProgress, async () =>
            {
                
                await Task.Delay(5000);
               
                await new OracleExp(UserName,Password,InstanceDB, $"{pathToSave as string}\\").StartProc();
            });
            

        }

        #endregion

    }
}
