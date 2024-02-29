using Spring.Properties;
using Syncfusion.DataSource.Extensions;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Spring.Pages
{
    public class BasePage : UserControl
    {

        public BasePage()
        {
            #region UI Enhancements
            DoubleBuffered = true;
            #endregion
            #region Events
            //add event to disable showing any content if this page is not enabled
            this.EnabledChanged += BasePage_EnabledChanged;
            #endregion
        }
        #region Added Events
        private void BasePage_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.Enabled)
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

                ) ;
        }
        #endregion
    }
}
