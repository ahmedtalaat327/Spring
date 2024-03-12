using Spring.Pages.ViewModel;
using Spring.Properties;
using Spring.StaticVM;
using Spring.ViewModel;
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
        public BasePageViewModel baseViewViewModel = new BasePageViewModel();

        public string viewName = "none";

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
                if (itemNode.Text.Equals("Print out current records"))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals("Refresh users table"))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                #endregion
                #region ADD USER PAGE
                if (itemNode.Text.Equals("Print out user details"))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals("Proceed to add new record"))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                #endregion
                #region USER CARD PAGE
                if (itemNode.Text.Equals("Print out user card"))
                {
                    
                        itemNode.Enabled = false;
                    

                }
                if (itemNode.Text.Equals("Find a User"))
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

                    switch (viewName)
                    {
                        case "users":

                            foreach (Syncfusion.Windows.Forms.Tools.TreeNodeAdv itemNode in optionsTree.Nodes)
                            {
                                 
                                #region USERS PAGE
                                if (itemNode.Text.Equals("Print out current records"))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals("Refresh users table"))
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
                                if (itemNode.Text.Equals("Print out user details"))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals("Proceed to add new record"))
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
                                if (itemNode.Text.Equals("Print out user card"))
                                {

                                    itemNode.Enabled = true;


                                }
                                if (itemNode.Text.Equals("Find a User"))
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
            baseViewViewModel.CheckViewStateOnRules.Execute(viewName);
        }
        #endregion
    }
}
