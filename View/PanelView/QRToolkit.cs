using AForge.Video.DirectShow;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Forms.Tools.Navigation;
using Syncfusion.XlsIO.Implementation.PivotAnalysis;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace Spring.View.PanelView
{
    public partial class QRToolkit : UserControl
    {
        FilterInfoCollection filterItemElements;
        VideoCaptureDevice videoCaptureDevice;
        
        public QRToolkit()
        {
            InitializeComponent();

            Load += QRToolkit_Load;
        }

        private void QRToolkit_Load(object sender, EventArgs e)
        {
            filterItemElements = new FilterInfoCollection(FilterCategory.VideoInputDevice);



            this.sfComboBox1.DataSource =  (filterItemElements).ToList<FilterInfo>();
            //Bind the Display member and Value member to the data source
            this.sfComboBox1.DisplayMember = "Name";
            this.sfComboBox1.ValueMember = "MonikerString";
            //
        }
    }
}
