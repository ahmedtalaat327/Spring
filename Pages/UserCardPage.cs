using Cybele.Thinfinity;
using Spring.Helpers.Controls;
using Spring.Pages.ViewModel;
using Spring.StaticVM;
using Spring.View.MainView.LoginView;
using Spring.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.Win32API;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

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

            //this.sfBarcode1.DataBindings.Add(new Binding("Text", userCardViewModel, "IdOfCardUser"));
           
            this.textBoxExt1.DataBindings.Add(new Binding("Text", userCardViewModel, "IdOfCardUser", false, DataSourceUpdateMode.OnPropertyChanged));

            //fname
            this.fllblname.DataBindings.Add(new Binding("Text", userCardViewModel, "EmpFirstName", false, DataSourceUpdateMode.OnPropertyChanged));
            //sname
            this.slblname.DataBindings.Add(new Binding("Text", userCardViewModel, "EmpLastName", false, DataSourceUpdateMode.OnPropertyChanged));
            //lname
            this.abblbl.DataBindings.Add(new Binding("Text", userCardViewModel, "DeptAbbriviation", false, DataSourceUpdateMode.OnPropertyChanged));
            //this.lnametxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "LastPortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //this.label3.DataBindings.Add(new Binding("ImageSource", userCardViewModel, "PersonalPhotoUser", false, DataSourceUpdateMode.OnPropertyChanged));
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
        {
            //make sure we are in same VM and same property.
            if (e.PropertyName == nameof(VMCentral.DockingManagerViewModel.Loading) && VMCentral.DockingManagerViewModel.GetType() == typeof(DockingManagerViewModel))
            {
                //After Loading property finish in RELYCOMMAND
                if (!VMCentral.DockingManagerViewModel.Loading)
                {
                   // if (e.PropertyName == nameof(userCardViewModel.PersonalPhotoUser))
                    {
                      var  bitmap = new Bitmap(userCardViewModel.PersonalPhotoUser, label3.Width, label3.Height);
                        
                        label3.Image = bitmap;
                    }
                    
                    
                }
            }
            
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
                if (adv.Text == PagesNodesNames.UserCardThirdButtonTitle)
                {
                    // image load
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Filter = "Photos (*.jpg)|*.jpg"
                    };
                    Bitmap bitmap = null;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (Stream stream = openFileDialog.OpenFile())
                        {
                            //document.Save(stream);
                             bitmap = new Bitmap(stream);
                        }
                        if (VMCentral.DockingManagerViewModel.PlatformTypeUsed == Spring.ViewModel.DockingManagerViewModel.PlatformType.VirtualWeb)
                        {
                            VirtualUI vui = new VirtualUI();
                            vui.UploadFile(openFileDialog.FileName);
                            //database command to add depening on the id and bitmap
                            bitmap = new Bitmap(bitmap, label3.Width, label3.Height);
                            userCardViewModel.PersonalPhotoUser = bitmap;
                            label3.Image = bitmap;
                        }
                        else
                        {
                            //database command to add depening on the id and bitmap
                            bitmap = new Bitmap(bitmap, label3.Width, label3.Height);
                            userCardViewModel.PersonalPhotoUser = bitmap;
                            label3.Image = bitmap;
                        }
                    }
                }
            }
        }

        #endregion

        private void textBoxExt1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                userCardViewModel.LoadCurrentUserCard.Execute(true);

                 
            }
        }
    }
}
