using Spring.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Pages.ChartsPages.ViewModel
{
    //this is a helper should be moved to ./Data/DeptsStatisticsPair
    public class DeptsStatisticsPair 
    {
        private string dept_name ;

        private DateTime date_founded ;
        public string DeptName
        {
            get { return dept_name; }
            set { dept_name = value; }
        }
        public DateTime DeptDatedToFound
        {
            get { return date_founded; }
            set { date_founded = value; }
        }


        public DeptsStatisticsPair(string deptName, DateTime deptDatedToFound)
        {
            DeptName = deptName;
            //OnPropertyChanged(nameof(DeptName));
            DeptDatedToFound = deptDatedToFound;
            //OnPropertyChanged(nameof(DeptDatedToFound));
        }
    }
    //real VM for DepartmentsStatistics view
    public class DepartmentsStatisticsViewModel : BaseViewModel
    {

    }
}
