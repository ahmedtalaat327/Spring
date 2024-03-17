using Spring.Helpers.Controls;
using Spring.Pages.ViewModel;
using Spring.Properties;
using Spring.View.MainView.LoginView;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.Pages
{
    public class BasePage : UserControl
    {


        #region Public Members
        public BasePageViewModel baseViewViewModel = new BasePageViewModel();

       

        #endregion
      
        public BasePage()
        {
            #region UI Enhancements
            DoubleBuffered = true;
            #endregion
            this.BackgroundImage = new Bitmap(Resources.desktopGrid_f324974d);

            #region Binding
            this.DataBindings.Add(new Binding("Visible", baseViewViewModel, "ActiveView"));
            #endregion


        }
        public void ResetAllOptionNodes(TreeViewAdv optionsTree)
        {
            if (optionsTree == null)
                return;

            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
            {
                
                    itemNode.Enabled = false;

            }

            #region property events
            baseViewViewModel.PropertyChanged += (sender, e) => propsAvailabiltyEvent(sender, e, optionsTree); ;
            #endregion
            SetPrivilages();
        }

        private void propsAvailabiltyEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e, TreeViewAdv optionsTree)
        {

            //make sure we are in same VM and same property.
            if (e.PropertyName == nameof(baseViewViewModel.ActiveView) && baseViewViewModel.GetType() == typeof(BasePageViewModel))
            {
                /*
                if (!baseViewViewModel.ActiveView)
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

               );
                */

                if (baseViewViewModel.ActiveView)
                {
                    string view_name_currently = (StaticVM.VMCentral.DockingManagerViewModel.ViewName);

                    for (int i = 0; i < PagesNodesNames.ALLLEFTPRIMARYTITLES.Count; i++)
                    {
                        if (PagesNodesNames.ALLLEFTPRIMARYTITLES[i] == view_name_currently)
                        {



                            var rightnodenames = PagesNodesNames.ALLRIGHTTITLES[i].ToArray();



                            for (int x = 0; x < optionsTree.Nodes.Count; x++)
                            {

                                if (optionsTree.Nodes[x].Text.Equals(rightnodenames[x]))
                                {
                                    optionsTree.Nodes[x].Enabled = true;
                                }
                            }
                        }
                    }

                

                }
            /*
                

                if (baseViewViewModel.ActiveView)
                {
                    string view_name_currently = (StaticVM.VMCentral.DockingManagerViewModel.ViewName);

                    switch (view_name_currently)
                    {
                        case "users":

                            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
                            {
                                 
                                #region USERS PAGE
                                if (itemNode.Text.Equals(PagesNodesNames.UsersFirstButtonTitle))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals(PagesNodesNames.UsersSecondButtonTitle))
                                {

                                    itemNode.Enabled = true;


                                }
                                #endregion
                            }
                            break;
                        case "adduser":

                            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
                            {
                                
                                #region ADD USER PAGE
                                if (itemNode.Text.Equals(PagesNodesNames.AddUserFirstButtonTitle))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals(PagesNodesNames.AddUserSecondButtonTitle))
                                {

                                    itemNode.Enabled = true;


                                }
                                #endregion
                            }
                            break;
                        case "usercard":

                            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
                            {
                                
                                #region USER CARD PAGE
                                if (itemNode.Text.Equals(PagesNodesNames.UserCardFirstButtonTitle))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals(PagesNodesNames.UserCardSecondButtonTitle))
                                {

                                    itemNode.Enabled = true;


                                }
                                #endregion
                            }
                            break;

                    }

                }

                */
            }
            
            
        }
        #region Added Events
    
        private void SetPrivilages()
        {
            //need to execute action here...
            baseViewViewModel.CheckViewStateOnRules.Execute(true);
        }
        public virtual void AddEventsToOptionsNodes(TreeViewAdv optionsTree)
        {
            /*to be override to add events*/
        }
        public virtual void OptionsTree_Click(object sender, EventArgs e)
        {
            /*to be override to add events*/
        }
        public virtual void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            /*to be override to add events*/
        }
        #endregion
    }
}
