using Spring.Data;
using Spring.Pages.ChartsPages.ViewModel;
using Spring.ViewControls.ViewChartsHelpers;
using Syncfusion.Data.Extensions;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections;
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

            /*
            this.chartControl1.Series[0].ConfigItems.PieItem.DoughnutCoeficient = (float)(0.6);

            this.chartControl1.Series[0].Points.Add(0D, ((double)(7D)));

            for(int i = 0; i < this.chartControl1.Series[0].Points.Count; i++)
            {
                this.chartControl1.Series[0].Styles[i].Text = string.Format("Labor {0}%", this.chartControl1.Series[0].Points[i].YValues[0]);

            }
            */
            this.Load += UsersStatisticsPage_Load;
            UsersStatisticsViewModel.PropertyChanged += UsersStatisticsViewModel_PropertyChanged;
            //this.VisibleChanged += UsersStatisticsPage_VisibleChanged;

            // ((ChartToolBarCommandItem)this.chartControl1.ToolBar.Items[6]).IsCheckable = false ;

            //Adding the custom Toolbar items.
            ChartToolBarCommandItem zoomIn = new ChartToolBarCommandItem();
            zoomIn.Command = ChartCommands.ZoomIn;
            zoomIn.ToolTip = "ZoomIn";
            this.chartControl1.ToolBar.Items.Add(zoomIn);

            ChartToolBarCommandItem zoomOut = new ChartToolBarCommandItem();
            zoomOut.Command = ChartCommands.ZoomOut;
            zoomOut.ToolTip = "ZoomOut";
            this.chartControl1.ToolBar.Items.Add(zoomOut);


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
                /*
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
                */
                chartControl1.Series.Clear();

                ArrayList populations = new ArrayList
                {
                    new DeptsStatisticsPair("IT", Convert.ToDateTime("04-04-2028")),
                    new DeptsStatisticsPair("HVC", Convert.ToDateTime("03-23-2022")),
                    new DeptsStatisticsPair("Plumers", Convert.ToDateTime("05-08-2026")),
                    new DeptsStatisticsPair("Mechanic Engineering", Convert.ToDateTime("04-06-2022")),
                    new DeptsStatisticsPair("HR", Convert.ToDateTime("11-16-2022"))
                };



                ChartSeries series = new ChartSeries("Creation");

 

                ChartDataBindModel dataSeriesModel = new ChartDataBindModel(populations);

                // If ChartDataBindModel.XName is empty or null, X value is index of point.

                //Here I have assigned the property name Population as Y axis name and ChartDataBindModel automatically detects the Population property and will bind the data from it.

                dataSeriesModel.YNames = new string[] { "DeptDatedToFound" };

                //Binding the ChartDataBindModel with the Series. This is the best practice for binding with the large amount of data since it will reduce the performance issue of Chart rendering and manipulating data.

                series.SeriesModel = dataSeriesModel;

                //Since we have specified YNames only for the DataBind model, it will take the data source is non indexed model and it will ignore the X axis values. We need to assign the X axis values what we need to show on X axis by ChartDataBindAxisLabelModel separately. 

                ChartDataBindAxisLabelModel dataLabelsModel = new ChartDataBindAxisLabelModel(populations);

                dataLabelsModel.LabelName = "DeptName";

                chartControl1.Model.Series.Add(series);

               

               


                chartControl1.PrimaryYAxis.RangeType = ChartAxisRangeType.Set;

                chartControl1.PrimaryYAxis.DateTimeRange = new ChartDateTimeRange(Convert.ToDateTime("01-01-2020"),
                      Convert.ToDateTime("01-01-2040"),
                     20, ChartDateTimeIntervalType.Years);

                chartControl1.PrimaryYAxis.DateTimeInterval.Type = ChartDateTimeIntervalType.Years;

                chartControl1.PrimaryYAxis.ValueType = ChartValueType.DateTime;

                chartControl1.PrimaryYAxis.DateTimeFormat = "MM-dd-yyyy";



                chartControl1.PrimaryXAxis.EdgeLabelsDrawingMode = ChartAxisEdgeLabelsDrawingMode.Center;

                chartControl1.PrimaryXAxis.LabelIntersectAction = ChartLabelIntersectAction.Rotate;

                chartControl1.PrimaryXAxis.ValueType = ChartValueType.Custom;

                chartControl1.PrimaryXAxis.LabelsImpl = dataLabelsModel;

                chartControl1.ChartFormatAxisLabel += (s, ev) => {
                 
                    int index = (int)ev.Value;


                    if (ev.AxisOrientation == ChartOrientation.Horizontal)
                    {
                        if (index >= 0 && index < populations.Count)
                        {
                            ev.Axis.PointOffset = -0.5;

                            ev.Label = ((DeptsStatisticsPair)populations[index]).DeptName.ToString();
                        }
                    }

                    ev.Handled = true;

                };
                /*
                this.chartControl1.Model.Series[0].Type = ChartSeriesType.Bar;
                this.chartControl1.PrimaryXAxis.TickLabelsDrawingMode = ChartAxisTickLabelDrawingMode.UserMode;
                //this.chartControl1.PrimaryXAxis.Labels.Clear();
                for (int i = 0; i<populations.Count;i++) { 

                    this.chartControl1.PrimaryYAxis.Labels.Add(new ChartAxisLabel(((DeptsStatisticsPair)populations[i]).DeptName, Color.Crimson, new Font("TimesNewRoman", 10), i, "", ChartValueType.Custom));
                     
                }
                
                */
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
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesChartsNodesNames.DeptsChartTitle;
            #endregion
 
        }

      
        #endregion

    }
}


