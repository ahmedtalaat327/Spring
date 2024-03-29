using Spring.Pages.ChartsPages;
using Spring.ViewControls.ViewChartsHelpers;
using System;
using System.Windows.Forms;

namespace Spring.Pages
{
    public partial class OnBoardPage : UserControl
    {
        public OnBoardPage()
        {
            InitializeComponent();
            
            this.Load += OnBoardPage_Load;

        }

        private void OnBoardPage_Load(object sender, EventArgs e)
        {
            try
            {
                PageChartManager pageChartManager = new PageChartManager();
                pageChartManager.SetupMainBoard(this.tableLayoutPanel1);
            }catch(Exception nil)
            {
                /****/
            }
        }

     
    }
    
}
