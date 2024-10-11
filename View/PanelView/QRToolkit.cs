using AForge.Video;
using AForge.Video.DirectShow;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Forms.Tools.Navigation;
using Syncfusion.XlsIO.Implementation.PivotAnalysis;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;


namespace Spring.View.PanelView
{
    public partial class QRToolkit : UserControl
    {
        FilterInfoCollection filterItemElements;
        VideoCaptureDevice videoCaptureDevice;
        Result res;
        QRCodeReader reader = new QRCodeReader();


        public QRToolkit()
        {
            InitializeComponent();

            Load += QRToolkit_Load;

            VisibleChanged += QRToolkit_VisibleChanged;
        }

        private void QRToolkit_VisibleChanged(object sender, EventArgs e)
        {
           
        }

        private void QRToolkit_Load(object sender, EventArgs e)
        {
            filterItemElements = new FilterInfoCollection(FilterCategory.VideoInputDevice);



            this.sfComboBox1.DataSource =  (filterItemElements).ToList<FilterInfo>();
            //Bind the Display member and Value member to the data source
            this.sfComboBox1.DisplayMember = "Name";
            this.sfComboBox1.ValueMember = "MonikerString";
            //
            this.sfComboBox1.SelectedIndex = 0;
            this.sfComboBox1.SelectedValueChanged += SfComboBox1_SelectedValueChanged;

         
        }

        private void SfComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(((FilterInfo)this.sfComboBox1.SelectedItem).MonikerString);
            //videoCaptureDevice.VideoResolution = videoCaptureDevice.VideoCapabilities[0];
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();


            LuminanceSource source;
            source = new ZXing.BitmapLuminanceSource(bitmap);
            var bitmapr = new BinaryBitmap(new GlobalHistogramBinarizer(source));

            res = reader.decode(bitmapr); 
            
            if (res != null)
            {
                Console.WriteLine($"{res}");
            }

            pictureBox1.Image = bitmap;

        }
        private void Exitcamera()
        {
            videoCaptureDevice.SignalToStop();
            // FinalVideo.WaitForStop();  << marking out that one solved it
            videoCaptureDevice.NewFrame -= new NewFrameEventHandler(VideoCaptureDevice_NewFrame); // as sugested
            videoCaptureDevice = null;
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
           
                if (videoCaptureDevice != null)
                {
                    if (videoCaptureDevice.IsRunning)
                    {
                        Exitcamera();

                    }
                }
            this.Visible = false;
            this.Dispose();
        }
    }
}
