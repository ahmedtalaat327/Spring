using Spring.Pages.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.IO;
using Syncfusion.Data;
using Spring.Helpers.Controls;

namespace Spring.Pages
{
    public partial class ChangeOrTerminateCurrentUserPage : BasePage
    {

        UsersViewModel usersViewModel = new UsersViewModel();
        public ChangeOrTerminateCurrentUserPage() 
        {
            InitializeComponent();

           
            //bindings
            //this.sfDataGrid1.DataSource = usersViewModel.CurrentUsers;

            //binding active flag to panel
            //this.DataBindings.Add(new Binding("Enabled", usersViewModel, "ActivePanel"));

            #region Events

            this.Load += ChangeOrTerminateCurrentUserPage_Load;

          
            /*
            optionsTree.BeforeSelect += (e, o) =>
            {

                Point pt = optionsTree.PointToClient(Cursor.Position);
                TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
                if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                    o.Cancel = true;
            };


            optionsTree.Click += (evt, obj) =>
            {

                Point pt = optionsTree.PointToClient(Cursor.Position);
                TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
                if (node != null && node == optionsTree.SelectedNode)
                {
                    RaiseClick(node);
                }


            };

            optionsTree.AfterSelect += (eve, ob) =>
            {
                //  RaiseClick(optionsTree.SelectedNode);
            };
            */

            #endregion

            //sfDataGrid1.Columns["Id"].FilterPredicates.Add(new FilterPredicate() { FilterType = FilterType.LessThan, FilterValue = "18" });
        }
        public override void AddEventsToOptionsNodes(TreeViewAdv optionsTree)
        {
            optionsTree.BeforeSelect += OptionsTree_BeforeSelect;

            optionsTree.Click += OptionsTree_Click;

            #region view define
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesNodesNames.ChangeorTerminateCurrentUserPrimaryPageButtonName;
            #endregion


            OnLoad(new EventArgs());
        }

        #region Methods Helpers
        public override void OptionsTree_Click(object sender, EventArgs e)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, false, false);
            if (node != null && node == optionsTree.SelectedNode)
            {
                RaiseClick(node);
            }
        }

        public  override void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, false, false);
            if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                o.Cancel = true;
        }

        private void ChangeOrTerminateCurrentUserPage_Load(object sender, EventArgs e)
        {
            usersViewModel.LoadAllUsers.Execute(true);
        }


        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled && adv != null)
            {
                if (adv.Text == PagesNodesNames.UsersFirstButtonTitle)
                {

                   
                }
                if (adv.Text == PagesNodesNames.UsersSecondButtonTitle)
                {
                  //  usersViewModel.LoadAllUsers.Execute(true); 
                }



            
        }
    }

       
        #endregion


    }
}
