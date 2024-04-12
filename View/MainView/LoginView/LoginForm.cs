using Spring.StaticVM;
using Spring.ViewModel;
using Syncfusion.Windows.Forms;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

 

namespace Spring.View.MainView.LoginView
{

    public partial class LoginForm : MetroForm
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        #region Instance of View Model
        /// <summary>
        /// Creatw instance from the VM for Main login.
        /// </summary>
        private LoginViewModel dataContext = null;
        #endregion
        #region Private Members
        private int wWidth = 270, wHeight = 470;
        #endregion
        #region constructor
        /// <summary>
        /// Constructor for current View (UI)
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();


            #region UI Cutsomization 
            this.BackColor = Color.White;
           
            try
            {
                System.Drawing.Icon ico = global::Spring.Properties.Resources.springTM;
                this.Icon = ico;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.BorderThickness = 12;
            this.BorderColor = ColorTranslator.FromHtml("#d6dbe9");
            this.ShowIcon = true;
            this.MetroColor = ColorTranslator.FromHtml("#d6dbe9");
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.progressBarAdv1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdv1.WaitingGradientEnabled = true;

            this.textBoxExt2.SetImgeForFirstTime(new Bitmap(Properties.Resources.icons8_eye_2_48, this.textBoxExt2.LabelEyeRevealler.Size));
            this.textBoxExt2.SetImageForHover(new Bitmap(Properties.Resources.icons8_eye_48, this.textBoxExt2.LabelEyeRevealler.Size));

            try
            {
                this.label5.Image = this.Icon.ToBitmap();

            }
            catch (Exception ex)
            {
                this.label5.Text = "logo here";
                MessageBox.Show(ex.Message);
            }
            finally
            {

                this.label5.Text = "TM";
            }
            tableLayoutPanel1.Dock = DockStyle.Fill;

           // tableLayoutPanel2.Dock = DockStyle.None;
            tableLayoutPanel2.Size = new Size(wWidth, wHeight);
            this.Resize += (evt, obj) =>
            {
                this.tableLayoutPanel2.Margin = new Padding((this.tableLayoutPanel1.Width - wWidth) / 2, (this.tableLayoutPanel1.Height - wHeight) / 2, (this.tableLayoutPanel1.Width - wHeight) / 2, (this.tableLayoutPanel1.Height - wHeight) / 2);
             //   this.tableLayoutPanel2.Location = new System.Drawing.Point((this.tableLayoutPanel1.MaximumSize.Width - 350) / 2, (this.tableLayoutPanel1.Height - 400) / 2);
            };
            #endregion



            #region Load all properties to controles
            this.Load += (et, ob) =>
            {



                //Initialize the viewmodel instance..
                dataContext = new LoginViewModel();

                //assign visible propertty to current form
                this.DataBindings.Add(new Binding("Visible", dataContext, "Visible"));

                //assign properties to [username & password textboes]
                textBoxExt1.DataBindings.Add(new Binding("Text", dataContext, "CurrentUserName", false, DataSourceUpdateMode.OnPropertyChanged));
                textBoxExt2.DataBindings.Add(new Binding("Text", dataContext, "CurrentPassword", false, DataSourceUpdateMode.OnPropertyChanged));

                //assign progressbar properties [visibility & Running for loading]
                progressBarAdv1.DataBindings.Add(new Binding("Visible", dataContext, "Loading"));
                progressBarAdv1.DataBindings.Add(new Binding("WaitingGradientEnabled", dataContext, "WaitingProgress"));

               
                dataContext.CheckConnectivityCommand.Execute(true);

                this.dataContext.PropertyChanged += DataContext_PropertyChanged;

               


                new DockingManagerForm();

            };

            #endregion

        }
        #region Property Additonal Even Handle [Special to WINFORMS UI]
        // when some propwerty change [Reason: to take out any View UI from VM]
        private void DataContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //make sure we are in same VM and same property.
            if (e.PropertyName == nameof(this.dataContext.WaitingProgress) && this.dataContext.GetType() == typeof(LoginViewModel))
            {
                //After Loading property finish in RELYCOMMAND
                if (!this.dataContext.WaitingProgress)
                {
                    //Pick which phase we are in
                    if (this.dataContext.CurrentWait == LoginViewModel.LoginVMLoadingPhase.ConnectionCheckWaiting)
                    {
                        if (!this.dataContext.ValidConnection)
                        {
                            new AdvOptions().ShowError_Connection(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                        }
                        else
                        {
                            new AdvOptions().ShowValid_Connection(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                        }
                        //reset phase of loading fter all logic done!
                        this.dataContext.CurrentWait = LoginViewModel.LoginVMLoadingPhase.Non;
                    }
                    //Pick which phase we are in
                    if (this.dataContext.CurrentWait == LoginViewModel.LoginVMLoadingPhase.LoginCkeckWaiting)
                    {
                        if (!this.dataContext.ValidSession)
                        {
                            new AdvOptions().ShowFailur2_Login(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                        }
                        else if (this.dataContext.ValidLogin)
                        {

                            new AdvOptions().ShowSuccess_Login(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                            VMCentral.DockingManagerViewModel.FetchAllRulesGroupes.Execute(true);

                        }
                        else if (!this.dataContext.ValidLogin)
                        {
                            new AdvOptions().ShowFailur_Login(AdvOptions.GetForm(AdvOptions.GetHandleByTitle("Spring")));
                        }
                        //reset phase of loading fter all logic done!
                        this.dataContext.CurrentWait = LoginViewModel.LoginVMLoadingPhase.Non;
                        //reset the value of active account checker if it's changed in any phase
                        this.dataContext.ValidSession = true;
                    }
                }
            }
         }
        #endregion
        #endregion

        #region Get Form Icon
        private string GetIconFile(string bitmapName)
        {
            for (int n = 0; n < 10; n++)
            {
                if (System.IO.File.Exists(bitmapName))
                    return bitmapName;

                bitmapName = @"..\" + bitmapName;
            }

            return bitmapName;
        }
        #endregion

        #region Events
        /// <summary>
        /// when button clicked to login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sfButton1_Click(object sender, EventArgs e)
        {
            dataContext.LoginCommand.Execute(true);
        }
  
        /// <summary>
        /// when enter pressed on keyboard on username textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxExt1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            dataContext.LoginCommand.Execute(true);
        }
        /// <summary>
        /// when enter pressed on keyboard on password textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxExt2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            dataContext.LoginCommand.Execute(true);
        }
        #endregion
    }
    /// <summary>
    /// Some Advanced Methods that allow Us to use Messagesboxes in MVVM.
    /// </summary>
    public class AdvOptions
    {
        #region APIs
        [DllImport("USER32.DLL")]
        static extern IntPtr GetShellWindow();

        [DllImport("USER32.DLL")]
        static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        #endregion
        #region Methods As Helpers
        public static IntPtr GetHandleByTitle(string windowTitle)
        {
            const int nChars = 256;

            IntPtr shellWindow = GetShellWindow();
            IntPtr found = IntPtr.Zero;

            EnumWindows(
                delegate (IntPtr hWnd, int lParam)
                {
                    //ignore shell window
                    if (hWnd == shellWindow) return true;

                    //get Window Title
                    StringBuilder Buff = new StringBuilder(nChars);

                    if (GetWindowText(hWnd, Buff, nChars) > 0)
                    {
                        //Case insensitive match
                        if (Buff.ToString().Equals(windowTitle, StringComparison.InvariantCultureIgnoreCase))
                        {
                            found = hWnd;
                            return true;
                        }
                    }
                    return true;

                }, 0
            );

            return found;
        }

        delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        public static Form GetForm(IntPtr handle)
        {
            return handle == IntPtr.Zero ?
                null :
                Control.FromHandle(handle) as Form;
        }
        #endregion
        #region MSGs Box FeedBack
        /// <summary>
        /// Only show when connection to server is failed
        /// </summary>
        public void ShowError_Connection(Form parent,string error_descriptor = "")
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;
          
            MessageBoxAdv.Show(parent, $"{error_descriptor}. Error! check init parameteres, and try again.", "Connection is failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /// <summary>
        /// Only show when connection to server is valid
        /// </summary>
        public void ShowValid_Connection(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;
          
            MessageBoxAdv.Show(parent, "Congratulations! you are connected to the server.", "Connection is Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Only show when connection to server is valid
        /// </summary>
        public void ShowSuccess_Login(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "You are Successfully in! you'er in session now.", "Login is Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Only show when connection to server is invalid
        /// </summary>
        public void ShowFailur_Login(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "You are not able to login! username or password is wrong.", "Login is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Only show when connection to server is invalid
        /// </summary>
        public void ShowFailur2_Login(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "Call your admininstrator urgently! Account is terminated.", "Login is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Only show when add new user is failed <see cref="addUserViewModel.cs"/>
        /// </summary>
        public void ShowFailur_AddUser(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "You already left some fileds empty or mis-entered values.", "User insertion is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Only show when add new user is success <see cref="addUserViewModel.cs"/>
        /// </summary>
        public void ShowSuccess_AddUser(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "You added new user with these entries!.", "User inserion is Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Only show when logout is done <see cref="addUserViewModel.cs"/>
        /// </summary>
        public void ShowSuccess_Logout(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

           var sout = MessageBoxAdv.Show(parent, "You are successfully out of the system!.", "Logout is Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
            MessageBoxAdv.DropShadow = true;
            if (sout == DialogResult.OK)
            {
                Application.Exit();
            }

        }
        /// <summary>
        /// Only show when logout is done <see cref="addUserViewModel.cs"/>
        /// </summary>
        public void ShowFailur_Logout(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "You are unsuccessfully to logout this time!.", "Logout is not-Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBoxAdv.DropShadow = true;
        }
        #endregion
    }


}
