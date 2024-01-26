using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace Spring.Helpers.Controls

{
    public class PasswordBoxExt : TextBoxExt
    {
        //API for Message HWND
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        //Func loaded from the API
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        //Label Eye-Revealler
        public Label LabelEyeRevealler = new Label();
        //Two images for overlapping
        private Image Image_1 = null;
        private Image Image_2 = null;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PasswordBoxExt() {

            //define all new contols because when intisalize new instance we need to be added onece and also register all Event at once
            LabelEyeRevealler.Cursor = Cursors.Default;
            //add the label
            this.Controls.Add(LabelEyeRevealler);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(this.Handle, 0xd3, (IntPtr)2, (IntPtr)(LabelEyeRevealler.Width << 16));
            //register events
            LabelEyeRevealler.MouseDown += PasswordBoxExt_Click;
            LabelEyeRevealler.MouseLeave += LabelEyeRevealler_MouseLeave;
            this.TextChanged += PasswordBoxExt_TextChanged;
            this.VisibleChanged += PasswordBoxExt_VisibleChanged;
            this.EnabledChanged += PasswordBoxExt_EnabledChanged;

            //----------- act as mouse leaving---------------------//
            LabelEyeRevealler.Image = this.Image_1;

            LabelEyeRevealler.Size = new Size(25, this.ClientSize.Height + 2);
            LabelEyeRevealler.Location = new Point(this.ClientSize.Width - LabelEyeRevealler.Width, -1);

            if (this.Image_1 != null && this.Image_2 != null)
                LabelEyeRevealler.Image = new Bitmap(LabelEyeRevealler.Image, this.LabelEyeRevealler.Size);

            StayUnrevealed();
            //-------------------- end ----------------------------// 
        }

        private void PasswordBoxExt_EnabledChanged(object sender, EventArgs e)
        {
            LabelEyeRevealler.Enabled = this.Enabled;
        }

        private void PasswordBoxExt_VisibleChanged(object sender, EventArgs e)
        {
            LabelEyeRevealler.Visible = this.Visible;
        }

        private void PasswordBoxExt_TextChanged(object sender, EventArgs e)
        {
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(this.Handle, 0xd3, (IntPtr)2, (IntPtr)(LabelEyeRevealler.Width << 16));
            
        }
        #endregion
        #region Events and Methods
        private void LabelEyeRevealler_MouseLeave(object sender, EventArgs e)
        {
            LabelEyeRevealler.Image = this.Image_1;

            LabelEyeRevealler.Size = new Size(25, this.ClientSize.Height + 2);
            LabelEyeRevealler.Location = new Point(this.ClientSize.Width - LabelEyeRevealler.Width, -1);

            if (this.Image_1 != null && this.Image_2 != null)
                LabelEyeRevealler.Image = new Bitmap(LabelEyeRevealler.Image, this.LabelEyeRevealler.Size);

            StayUnrevealed();
        }

        private void PasswordBoxExt_Click(object sender, EventArgs e)
        {
            LabelEyeRevealler.Image = this.Image_2;

            LabelEyeRevealler.Size = new Size(25, this.ClientSize.Height + 2);
            LabelEyeRevealler.Location = new Point(this.ClientSize.Width - LabelEyeRevealler.Width, -1);

            if (this.Image_1 != null && this.Image_2 != null)
                LabelEyeRevealler.Image = new Bitmap(LabelEyeRevealler.Image, this.LabelEyeRevealler.Size);

            RevealPass();

            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(this.Handle, 0xd3, (IntPtr)2, (IntPtr)(LabelEyeRevealler.Width << 16));
        }

        private void PasswordBoxExt_VisiChanged(object sender, EventArgs e)
        {
            LabelEyeRevealler.Size = new Size(25, this.ClientSize.Height + 2);
            LabelEyeRevealler.Location = new Point(this.ClientSize.Width - LabelEyeRevealler.Width, -1);

            if(this.Image_1!=null && this.Image_2!=null)
            LabelEyeRevealler.Image = new Bitmap(LabelEyeRevealler.Image, this.LabelEyeRevealler.Size);
        }

        public void SetImgeForFirstTime(Image eyeImage)
        {
            this.Image_1 = eyeImage;
            LabelEyeRevealler.Image = this.Image_1;

            this.VisibleChanged += PasswordBoxExt_VisiChanged;
        }
        public void SetImageForHover(Image eyeImageHovering) {
            this.Image_2 = eyeImageHovering;
           
        }
        public void RevealPass()
        {
            this.PasswordChar = '\0';
        }
        public void StayUnrevealed()
        {
            this.PasswordChar = '●';
        }
        #endregion
        /*
        protected override void OnLoad(EventArgs e)
        {
            var btn = new Button();
            btn.Size = new Size(25, textBox1.ClientSize.Height + 2);
            btn.Location = new Point(textBox1.ClientSize.Width - btn.Width, -1);
            btn.Cursor = Cursors.Default;
            btn.Image = Properties.Resources.star;
          
            //base.OnLoad(e);
        }
        */
    }
}
