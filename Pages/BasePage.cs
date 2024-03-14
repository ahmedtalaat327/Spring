using Spring.Helpers.Controls;
using Spring.Pages.ViewModel;
using Spring.Properties;
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

        [Obsolete("Designer only", true)]
        public BasePage()
        {
              /**this is for designer only beacuse the parameterized verion is outputting an error!**/
        }

        #endregion
      
        public BasePage(TreeViewAdv optionsTree)
        {
            #region UI Enhancements
            DoubleBuffered = true;
            #endregion
            this.BackgroundImage = new Bitmap(Resources.desktopGrid_f324974d);

            #region Binding
            this.DataBindings.Add(new Binding("Visible", baseViewViewModel, "ActiveView"));
           
            #endregion
            //all titles must be here right tree items or nodes [static object no change in memory]
            //logic to comare all tree items.
            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
            { 

                #region USERS PAGE
                if (itemNode.Text.Equals(PagesNodesNames.UsersFirstButtonTitle))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals(PagesNodesNames.UsersSecondButtonTitle))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                #endregion
                #region ADD USER PAGE
                if (itemNode.Text.Equals(PagesNodesNames.AddUserFirstButtonTitle))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals(PagesNodesNames.AddUserSecondButtonTitle))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                #endregion
                #region USER CARD PAGE
                if (itemNode.Text.Equals(PagesNodesNames.UserCardFirstButtonTitle))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals(PagesNodesNames.UserCardSecondButtonTitle))
                {
                     
                        itemNode.Enabled = false;
                    

                }
                #endregion
 

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

                    switch (StaticVM.VMCentral.DockingManagerViewModel.ViewName)
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


            }
            
        }
        #region Added Events
    
        private void SetPrivilages()
        {
            //need to execute action here...
            baseViewViewModel.CheckViewStateOnRules.Execute(true);
        }
        #endregion
    }
}
