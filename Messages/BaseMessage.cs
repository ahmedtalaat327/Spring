using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spring.Messages
{
    public class BaseMessage : MetroForm 
    {
        #region Constructor
        public BaseMessage() { 
          base.CaptionAlign = MessageBoxAdv.CaptionAlign;
            this.BackColor = Color.White;
            base.DropShadow = MessageBoxAdv.DropShadow;
            try
            {
                System.Drawing.Icon ico = new System.Drawing.Icon(GetIconFile(@"..\\..\\\springTM.ico"));
                this.Icon = ico;
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }
        #endregion
        #region UI Customs
        public void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseMessage
            // 

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.CaptionBarHeight = 35;
            this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSize = new System.Drawing.Size(1123, 580);
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.Text = "Spring";

            this.ClientSize = new System.Drawing.Size(288, 263);
            this.Name = "BaseMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

            
            this.BorderThickness = 12;
            this.BorderColor = ColorTranslator.FromHtml("#d6dbe9");
            this.ShowIcon = true;
            this.MetroColor = ColorTranslator.FromHtml("#d6dbe9");
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

           

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
    }
}
