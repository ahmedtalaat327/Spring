using Spring.Pages.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using System.Windows.Forms;

namespace Spring.Pages
{
    public class BasePage : UserControl
    {


        #region Public Members
        public BasePageViewModel baseViewViewModel = new BasePageViewModel();

        #endregion
        #region ALL NAMES TITLES FOR RIGHT MENU OPTIONS [NODES}
        //=>users nodes
        private string first_option_users_view = "Print out current records";
        private string second_option_users_view = "Refresh users table";
        //=>adduser nodes
        private string first_option_adduser_view = "Print out user details";
        private string second_option_adduser_view = "Proceed to add new record";
        //=>usercard nodes
        private string first_option_usercrad_view = "Print out user card";
        private string second_option_usercard_view = "Find a User";
        #endregion
        public BasePage(TreeViewAdv optionsTree)
        {
            #region UI Enhancements
            DoubleBuffered = true;
            #endregion
           

            #region Binding
            this.DataBindings.Add(new Binding("Visible", baseViewViewModel, "ActiveView"));
           
            #endregion
            //all titles must be here right tree items or nodes [static object no change in memory]
            //logic to comare all tree items.
            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
            { 

                #region USERS PAGE
                if (itemNode.Text.Equals(first_option_users_view))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals(second_option_users_view))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                #endregion
                #region ADD USER PAGE
                if (itemNode.Text.Equals(first_option_adduser_view))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals(second_option_adduser_view))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                #endregion
                #region USER CARD PAGE
                if (itemNode.Text.Equals(first_option_usercrad_view))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals(second_option_usercard_view))
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
                                if (itemNode.Text.Equals(first_option_users_view))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals(second_option_users_view))
                                {

                                    itemNode.Enabled = true;


                                }
                                #endregion
                            }
                            break;
                        case "adduser":

                            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
                            {
                                
                                #region USERS PAGE
                                if (itemNode.Text.Equals(first_option_adduser_view))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals(second_option_adduser_view))
                                {

                                    itemNode.Enabled = true;


                                }
                                #endregion
                            }
                            break;
                        case "usercard":

                            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
                            {
                                
                                #region USERS PAGE
                                if (itemNode.Text.Equals(first_option_usercrad_view))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals(second_option_usercard_view))
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
