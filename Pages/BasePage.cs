using Spring.Helpers.Controls;
using Spring.Messages;
using Spring.Pages.ViewModel;
using Spring.Properties;
using Spring.View.MainView.LoginView;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Core.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.Pages
{
    public class BasePage : UserControl
    {
        private ProgressBarAdv progressBarAdv3;


        #region Public Members
        public BasePageViewModel baseViewViewModel = new BasePageViewModel();
         



        #endregion

        public BasePage()
        {
            #region UI Enhancements
            DoubleBuffered = true;
            // progressBarAdv3
            // 
            this.progressBarAdv3 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.progressBarAdv3.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.BackSegments = false;
            this.progressBarAdv3.CustomText = null;
            this.progressBarAdv3.CustomWaitingRender = false;
            this.progressBarAdv3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarAdv3.ForegroundImage = null;
            this.progressBarAdv3.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBarAdv3.GradientStartColor = System.Drawing.Color.Orange;
            this.progressBarAdv3.Location = new System.Drawing.Point(3, 433);
            this.progressBarAdv3.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.Name = "progressBarAdv3";
            this.progressBarAdv3.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv3.SegmentWidth = 12;
            this.progressBarAdv3.Size = new System.Drawing.Size(794, 14);
            this.progressBarAdv3.TabIndex = 6;
            this.progressBarAdv3.Text = "progressBarAdv3";
            this.progressBarAdv3.TextShadow = false;
            this.progressBarAdv3.TextVisible = false;
            this.progressBarAdv3.ThemeName = "WaitingGradient";
            this.progressBarAdv3.TubeEndColor = System.Drawing.SystemColors.MenuHighlight;
            this.progressBarAdv3.TubeStartColor = System.Drawing.Color.IndianRed;
            this.progressBarAdv3.WaitingGradientWidth = 400;
            this.Controls.Add(this.progressBarAdv3);
            #endregion
            this.BackgroundImage = new Bitmap(Resources.desktopGrid_f324974d);
           
          
            #region Binding
            this.DataBindings.Add(new Binding("Visible", baseViewViewModel, "ActiveView"));

            //assign progressbar properties [visibility & Running for loading]
            this.progressBarAdv3.DataBindings.Add(new Binding("Visible", baseViewViewModel, "Loading"));
            this.progressBarAdv3.DataBindings.Add(new Binding("WaitingGradientEnabled", baseViewViewModel, "WaitingProgress"));
            #endregion

        }
        public void DisableRightOptionsAndTestPrevilages(TreeViewAdv optionsTree)
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

        private void InitializeComponent()
        {
            this.progressBarAdv3 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv3)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarAdv3
            // 
            this.progressBarAdv3.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.BackSegments = false;
            this.progressBarAdv3.CustomText = null;
            this.progressBarAdv3.CustomWaitingRender = false;
            this.progressBarAdv3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarAdv3.ForegroundImage = null;
            this.progressBarAdv3.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBarAdv3.GradientStartColor = System.Drawing.Color.Orange;
            this.progressBarAdv3.Location = new System.Drawing.Point(0, 137);
            this.progressBarAdv3.Margin = new System.Windows.Forms.Padding(10);
            this.progressBarAdv3.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.Name = "progressBarAdv3";
            this.progressBarAdv3.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv3.SegmentWidth = 12;
            this.progressBarAdv3.Size = new System.Drawing.Size(150, 13);
            this.progressBarAdv3.TabIndex = 13;
            this.progressBarAdv3.Text = "progressBarAdv3";
            this.progressBarAdv3.TextShadow = false;
            this.progressBarAdv3.TextVisible = false;
            this.progressBarAdv3.ThemeName = "WaitingGradient";
            this.progressBarAdv3.TubeEndColor = System.Drawing.SystemColors.MenuHighlight;
            this.progressBarAdv3.TubeStartColor = System.Drawing.Color.IndianRed;
            this.progressBarAdv3.WaitingGradientWidth = 400;
            // 
            // BasePage
            // 
            this.Controls.Add(this.progressBarAdv3);
            this.Name = "BasePage";
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv3)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
