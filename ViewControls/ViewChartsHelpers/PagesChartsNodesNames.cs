﻿using Spring.Pages.ChartsPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.ViewControls.ViewChartsHelpers
{
    public class PagesChartsNodesNames
    {
        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
        #region ALL NAMES TITLES FOR CHARTS [PAGES]
        //userscharta page 
        public static string UsersChartTitle { get; set; } = "userschart";
        //deptscharta page 
        public static string DeptsChartTitle { get; set; } = "deptschart";
        #endregion

        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
        #region Lists FOR TITLES IN RULES
        public static List<string> ALLLEFTCHARTSTITLES { get; set; } = new List<string>() {
            UsersChartTitle,DeptsChartTitle
        };
        #endregion
        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
        #region Lists VIEW CHARTS OBJECTS
        public static List<BaseChartPage> ALLCHARTSINSTANCES { get; set; } = new List<BaseChartPage>() {
            new UsersStatisticsPage() , new DepartmentsStatisticsPage() ,
        };

        #endregion
    }
}
