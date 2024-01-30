using Spring.ViewModel;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
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


        #region constructor
        /// <summary>
        /// Constructor for current View (UI)
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();


            #region UI Cutsomization 
            this.BackColor = Color.White;
            this.Resize += (evt, obj) =>
            {

                this.tableLayoutPanel2.Location = new System.Drawing.Point((this.tableLayoutPanel1.Width - 350) / 2, (this.tableLayoutPanel1.Height - 400) / 2);
            };
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


                new DockingManagerForm();

            };
            #endregion

        }
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
        private void sfButton1_Click(object sender, EventArgs e)
        {
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
        public void ShowError_Connection(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;
          
            MessageBoxAdv.Show(parent, "Error! check init parameteres, and try again.", "Connection is failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void ShowFailur_AddUserContactNum(Form parent)
        {
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Metro;

            MessageBoxAdv.Show(parent, "You already set a non numeric set of strings and can not be a number!.", "Contact number is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }


}
