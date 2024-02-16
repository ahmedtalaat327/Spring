using Spring.Pages.ViewModel;
using Spring.StaticVM;
using Spring.View.MainView.LoginView;
using Spring.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.Pages
{
    public partial class UserCardPage : BasePage
    {
        #region Members
        /// <summary>
        /// new instance from the VM
        /// </summary>
        AddUserViewModel addUserViewModel = new AddUserViewModel();
        #endregion
        #region ctr
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="optionsTree"></param>
        public UserCardPage(TreeViewAdv optionsTree)
        {
            #region UI customizations
            InitializeComponent();
 
            //some UI customizations
           
 
 
            #endregion



            #region Bindings
         
 

            #region Event as fix for timedatepicker
            
            #endregion


            #endregion

            #region Events
            this.Load += UserCardPage_Load;

            optionsTree.BeforeSelect += OptionsTree_BeforeSelect;

            optionsTree.Click += OptionsTree_Click;
         


            //properties handle this is not related to any other UI framework only WINFORMS
            VMCentral.DockingManagerViewModel.PropertyChanged += AddUserViewModel_PropertyChanged;
            #endregion
        }
        //when some property changed
        private void AddUserViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //make sure we are in same VM and same property.
            if (e.PropertyName == nameof(VMCentral.DockingManagerViewModel.Loading) && VMCentral.DockingManagerViewModel.GetType() == typeof(DockingManagerViewModel))
            {
                //After Loading property finish in RELYCOMMAND
                if (!VMCentral.DockingManagerViewModel.Loading)
                {
                    //Pick which phase we are in
                    if (this.addUserViewModel.CurrentWait == AddUserViewModel.AddUserVMLoadingPhase.AdditionCheckWaiting)
                    {
                        if (!this.addUserViewModel.AdditionSucceded)
                        {
                            new AdvOptions().ShowFailur_AddUser(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                        }
                        else
                        {
                            new AdvOptions().ShowSuccess_AddUser(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                        }
                        //reset phase of loading fter all logic done!
                        this.addUserViewModel.CurrentWait = AddUserViewModel.AddUserVMLoadingPhase.Non;
                    }
                    
                }
            }
        }


        #endregion

        #region Methods Helpers

        public void OptionsTree_Click(object sender, EventArgs e)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
            if (node != null && node == optionsTree.SelectedNode)
            {
                RaiseClick(node);
            }
        }

        public void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
            if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                o.Cancel = true;
        }

        /// <summary>
        /// when page load 
        /// make all commnds executed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserCardPage_Load(object sender, EventArgs e)
        {
            addUserViewModel.LoadInitialWithRefrshing.Execute(true);
        }
        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled&&adv!=null)
            {
                switch (adv.Text)
                {
                    case "Print out": break;
                    case "Proceed to add": addUserViewModel.SetNewUser.Execute(true); break;


                }
            }
        }

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
