using Spring.Helpers.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spring.ViewControls.ViewChartsHelpers
{
    public class PageChartManager
    {
       
        public void SetupMainBoard(TableLayoutPanel parent)
        {
            parent.Controls.Clear();


            for (int i = 0; i < PagesChartsNodesNames.ALLLEFTCHARTSTITLES.Count; i++)
            {
                 
                //add controller
                var view = PagesChartsNodesNames.ALLCHARTSINSTANCES[i];
                view.Dock = DockStyle.Fill;
               
                view.SetViewName();
                view.CheckAvailability();
                   
              

            
                parent.Controls.Add(view);


            }
            
            
        }
    }
}
