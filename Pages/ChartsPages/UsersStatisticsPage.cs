using Spring.Helpers.Controls;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.Pages.ChartsPages
{
    public partial class UsersStatisticsPage : BasePage
    {
        public UsersStatisticsPage()
        {
            InitializeComponent();


        }
        public override void AddEventsToOptionsNodes(TreeViewAdv optionsTree)
        {
            optionsTree.BeforeSelect += OptionsTree_BeforeSelect;

            optionsTree.Click += OptionsTree_Click;

            #region view define
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesNodesNames.UsersStatisticsChartPrimaryPageButtonName;
            #endregion

          
            OnLoad(new EventArgs());
        }

        #region Methods Helpers
        public override void OptionsTree_Click(object sender, EventArgs e)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
            if (node != null && node == optionsTree.SelectedNode)
            {
                RaiseClick(node);
            }
        }

        public override void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
            if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                o.Cancel = true;
        }

        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled && adv != null)
            {
                if (adv.Text == PagesNodesNames.UsersChartFirstButtonTitle)
                {
                }
            }
        }
        #endregion
    }
}


