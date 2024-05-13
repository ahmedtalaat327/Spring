using Spring.Pages.ChartsPages.ViewModel;
using Spring.ViewControls.ViewChartsHelpers;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace Spring.Pages.ChartsPages
{
    public partial class DepartmentsStatisticsPage : BaseChartPage
    {
        UsersStatisticsViewModel UsersStatisticsViewModel = new UsersStatisticsViewModel();
        public DepartmentsStatisticsPage()
        {
            InitializeComponent();

            //assign progressbar properties [visibility & Running for loading]
            this.progressBarAdvUsersStatistics.DataBindings.Add(new Binding("Visible", UsersStatisticsViewModel, "LoadingChartData"));
            this.progressBarAdvUsersStatistics.DataBindings.Add(new Binding("WaitingGradientEnabled", UsersStatisticsViewModel, "LoadingChartData"));

            this.chartControl1.Series[0].ConfigItems.PieItem.DoughnutCoeficient = (float)(0.6);

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
                    var list_point = this.chartControl1.Series[0].Points;
                    List<double> yvals = new List<double>();
                    foreach(ChartPoint point in list_point)
                    {
                        yvals.Add(point.YValues[0]);
                    }
                    var cumsum = yvals.ToList<double>().Sum();
                    this.chartControl1.Series[0].Styles[i].Text = string.Format("{0} {1}%", UsersStatisticsViewModel.AllDepartments[i].Name, Convert.ToInt32((((this.chartControl1.Series[0].Points[i].YValues[0]) / cumsum) * 100)).ToString());

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


