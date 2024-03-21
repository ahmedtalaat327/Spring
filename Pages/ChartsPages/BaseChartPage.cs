using Spring.Pages.ViewModel;
using Spring.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.Pages.ChartsPages
{
    public class BaseChartPage : UserControl
    {
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarAdv3;
        #region Public Members
        public BasePageViewModel baseViewViewModel = new BasePageViewModel();

        #endregion


        public BaseChartPage()
        {
            #region UI Enhancements
            InitializeComponent();
            DoubleBuffered = true;
            #endregion
            this.BackgroundImage = new Bitmap(Resources.desktopGrid_f324974d);

            #region Binding
            this.DataBindings.Add(new Binding("Visible", baseViewViewModel, "ActiveView"));
            #endregion
        }
        public virtual void SetViewName()
        {
            /*to be overrided in real derived class*/
        }
        public void CheckAvailability()
        {
            //need to execute action here...
            baseViewViewModel.CheckViewStateOnRules.Execute(true);

             
        }

        private void InitializeComponent()
        {
            this.progressBarAdv3 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv3)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarAdv3
            // 
            this.progressBarAdv3.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.BackSegments = false;
            this.progressBarAdv3.CustomText = null;
            this.progressBarAdv3.CustomWaitingRender = false;
            this.progressBarAdv3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarAdv3.ForegroundImage = null;
            this.progressBarAdv3.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBarAdv3.GradientStartColor = System.Drawing.Color.Orange;
            this.progressBarAdv3.Location = new System.Drawing.Point(0, 137);
            this.progressBarAdv3.Margin = new System.Windows.Forms.Padding(10);
            this.progressBarAdv3.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.Name = "progressBarAdv3";
            this.progressBarAdv3.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv3.SegmentWidth = 12;
            this.progressBarAdv3.Size = new System.Drawing.Size(150, 13);
            this.progressBarAdv3.TabIndex = 14;
            this.progressBarAdv3.Text = "progressBarAdv3";
            this.progressBarAdv3.TextShadow = false;
            this.progressBarAdv3.TextVisible = false;
            this.progressBarAdv3.ThemeName = "WaitingGradient";
            this.progressBarAdv3.TubeEndColor = System.Drawing.SystemColors.MenuHighlight;
            this.progressBarAdv3.TubeStartColor = System.Drawing.Color.IndianRed;
            this.progressBarAdv3.WaitingGradientWidth = 400;
            // 
            // BaseChartPage
            // 
            this.Controls.Add(this.progressBarAdv3);
            this.Name = "BaseChartPage";
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv3)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
