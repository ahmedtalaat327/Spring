using Spring.Properties;
using Spring.StaticVM;
using Syncfusion.DataSource.Extensions;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Spring.Pages
{
    public class BasePage : UserControl
    {

        public BasePage(TreeViewAdv optionsTree)
        {
            #region UI Enhancements
            DoubleBuffered = true;
            #endregion
            #region Events
            //add event to disable showing any content if this page is not enabled
            this.EnabledChanged += BasePage_EnabledChanged;
            #endregion


            //all titles must be here right tree items or nodes [static object no change in memory]
            //logic to comare all tree items.
            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
            {
                #region ADMINS AUTH LEVEL CONSTRAINS for..
                #region USERS PAGE
                if (itemNode.Text.Equals("Print out current records"))
                {
                    //check what auth-level rquired her [ADMINS only]
                    if (!VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin"))
                    {
                        itemNode.Enabled = false;
                    }

                }
                if (itemNode.Text.Equals("Refresh users table"))
                {
                    //check what auth-level rquired her [ADMINS only]
                    if (!VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin"))
                    {
                        itemNode.Enabled = false;
                    }

                }
                #endregion
                #region ADD USER PAGE
                if (itemNode.Text.Equals("Print out user details"))
                {
                    //check what auth-level rquired her [ADMINS only]
                    if (!VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin"))
                    {
                        itemNode.Enabled = false;
                    }

                }
                if (itemNode.Text.Equals("Proceed to add new record"))
                {
                    //check what auth-level rquired her [ADMINS only]
                    if (!VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin"))
                    {
                        itemNode.Enabled = false;
                    }

                }
                #endregion
                #region USER CARD PAGE
                if (itemNode.Text.Equals("Print out user card"))
                {
                    //check what auth-level rquired her [ADMINS only]
                    if (!VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin"))
                    {
                        itemNode.Enabled = false;
                    }

                }
                if (itemNode.Text.Equals("Find a User"))
                {
                    //check what auth-level rquired her [ADMINS only]
                    if (!VMCentral.DockingManagerViewModel.loggedUser.UserAuthLevel.Contains("admin"))
                    {
                        itemNode.Enabled = false;
                    }

                }
                #endregion
                #endregion

            }
        }
        #region Added Events
        private void BasePage_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.Enabled)
            {
                foreach (var item in this.Controls.ToList<Control>())
                {
                    this.Controls.Remove(item);
                }
            }
            this.Controls.Add(
                new Panel()
                {
                    Dock = DockStyle.Fill,
                    BackgroundImage = new Bitmap(Resources.not_available)
                }

                ) ;
           
        }
        #endregion
    }
}
