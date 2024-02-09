using Spring.Pages.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Spring.Pages
{
    public partial class UsersPage : BasePage
    {
        
        UsersViewModel usersViewModel = new UsersViewModel();
        public UsersPage(TreeViewAdv optionsTree)
        {
            InitializeComponent();
            
            //bindings
            this.sfDataGrid1.DataSource = usersViewModel.CurrentUsers;


            #region Events
            this.Load += UsersPage_Load;

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
            #endregion
        }

        private void UsersPage_Load(object sender, EventArgs e)
        {
            usersViewModel.LoadAllUsers.Execute(true);
           
        }

        #region Methods Helpers
        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled && adv != null)
            {
                switch (adv.Text)
                {
                    case "Print out":
                     //   var document = this.sfDataGrid1.ExportToPdf();
                     //   document.Save("Sample.pdf");
                        break;
                   


                }
            }
        }
        #endregion


    }
}
