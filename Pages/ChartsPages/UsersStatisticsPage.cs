using Spring.Pages.ChartsPages.ViewModel;
using Spring.ViewControls.ViewChartsHelpers;
using System.Windows.Forms;



namespace Spring.Pages.ChartsPages
{
    public partial class UsersStatisticsPage : BaseChartPage
    {
        UsersStatisticsViewModel UsersStatisticsViewModel = new UsersStatisticsViewModel();
        public UsersStatisticsPage()
        {
            InitializeComponent();

            //assign progressbar properties [visibility & Running for loading]
            this.progressBarAdvUsersStatistics.DataBindings.Add(new Binding("Visible", UsersStatisticsViewModel, "LoadingChartData"));
            this.progressBarAdvUsersStatistics.DataBindings.Add(new Binding("WaitingGradientEnabled", UsersStatisticsViewModel, "LoadingChartData"));
          

            this.chartControl1.Series[0].Points.Add(0D, ((double)(7D)));

            for(int i = 0; i < this.chartControl1.Series[0].Points.Count; i++)
            {
                this.chartControl1.Series[0].Styles[i].Text = string.Format("Labor {0}%", this.chartControl1.Series[0].Points[i].YValues[0]);

            }
            this.Load += UsersStatisticsPage_Load;
            UsersStatisticsViewModel.PropertyChanged += UsersStatisticsViewModel_PropertyChanged;
            //this.VisibleChanged += UsersStatisticsPage_VisibleChanged;

            
        }

        private void UsersStatisticsPage_VisibleChanged(object sender, System.EventArgs e)
        {
            UsersStatisticsViewModel.GetCountingAndStoreThem.Execute(true);
        }

        private void UsersStatisticsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //make sure we are in same VM and same property.
            if (e.PropertyName == nameof(UsersStatisticsViewModel.LoadingChartData) && UsersStatisticsViewModel.GetType() == typeof(UsersStatisticsViewModel))
            {
                this.chartControl1.Series[0].Points.Clear();
                this.chartControl1.Series[0].ResetSeriesModel();
                 
                this.chartControl1.Series[0].Style.ResetLabel();

                foreach (string item in UsersStatisticsViewModel.AllCounters)
                {
                    this.chartControl1.Series[0].Points.Add(0D, double.Parse(item));
                }
                for (int i = 0; i < this.chartControl1.Series[0].Points.Count; i++)
                {
                    this.chartControl1.Series[0].Styles[i].Text = string.Format("{0} {1}%", UsersStatisticsViewModel.AllDepartments[i].Name,this.chartControl1.Series[0].Points[i].YValues[0]);

                }
            }

        }

        private void UsersStatisticsPage_Load(object sender, System.EventArgs e)
        {
            UsersStatisticsViewModel.GetCountingAndStoreThem.Execute(true);
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


