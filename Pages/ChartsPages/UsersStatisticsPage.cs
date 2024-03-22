using Spring.ViewControls.ViewChartsHelpers;



namespace Spring.Pages.ChartsPages
{
    public partial class UsersStatisticsPage : BaseChartPage
    {
        public UsersStatisticsPage()
        {
            InitializeComponent();


            this.chartControl1.Series[0].Points.Add(0D, ((double)(7D)));

            for(int i = 0; i < this.chartControl1.Series[0].Points.Count; i++)
            {
                this.chartControl1.Series[0].Styles[i].Text = string.Format("Labor {0}%", this.chartControl1.Series[0].Points[i].YValues[0]);

            }


        }
        #region Methods Helpers
        public override void SetViewName()
        {
            
            #region view define
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesChartsNodesNames.UsersChartTitle;
            #endregion
 
        }
       
       
        #endregion
    }
}


