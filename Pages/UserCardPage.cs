﻿using Spring.Helpers.Controls;
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
        UserCardViewModel userCardViewModel =  new UserCardViewModel();
        #endregion
        #region ctr
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="optionsTree"></param>
        public UserCardPage()
        {
          
            #region UI customizations
            InitializeComponent();

            #endregion

           
            #region Bindings

            //binding active flag to panel
            //this.DataBindings.Add(new Binding("Enabled", userCardViewModel, "ActivePanel"));

            this.sfBarcode1.DataBindings.Add(new Binding("Text", userCardViewModel, "IdOfCardUser"));


            #endregion

            #region Events
          


            //properties handle this is not related to any other UI framework only WINFORMS
            VMCentral.DockingManagerViewModel.PropertyChanged += AddUserViewModel_PropertyChanged;
            #endregion
        }
        public override void AddEventsToOptionsNodes(TreeViewAdv optionsTree)
        {
            optionsTree.BeforeSelect += OptionsTree_BeforeSelect;

            optionsTree.Click += OptionsTree_Click;

            #region view define
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesNodesNames.UserCardPrimaryPageButtonName;
            #endregion

            OnLoad(new EventArgs());
        }

        //when some property changed
        private void AddUserViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {/*
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
            */
        }


        #endregion

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

        public override void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, false, false);
            if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                o.Cancel = true;
        }

       
        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled&&adv!=null)
            {
                

                if(adv.Text == PagesNodesNames.UserCardFirstButtonTitle)
                {

                }
                if(adv.Text == PagesNodesNames.UserCardSecondButtonTitle)
                {

                }
            }
        }

        #endregion
 
    }
}
