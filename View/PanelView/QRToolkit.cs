using AForge.Video;
using AForge.Video.DirectShow;
using Spring.Pages.ChartsPages.ViewModel;
using Spring.StaticVM;
using Spring.ViewModel;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Forms.Tools.Win32API;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;


namespace Spring.View.PanelView
{
    public partial class QRToolkit : UserControl
    {
        /*
        FilterInfoCollection filterItemElements;
        VideoCaptureDevice videoCaptureDevice;
        Result res;
        QRCodeReader reader = new QRCodeReader();


        string _id = "";
        string IDTOBEREAD { get { return _id; }
            set {
                if (_id != value)
                {
                    _id = value;

                    VMCentral.changeOrTerminateCurrentUserViewModel.Id = (res.ToString());



                    VMCentral.changeOrTerminateCurrentUserViewModel.LoadCurrentUser.Execute(true);

                    //focus on id textbox get out from this thread to the main page thread
                    this.Invoke(new Action(() => { 
                    this.Visible = false;
                    this.Parent.Visible = false;
                    }));
                }
            } } 

        */

        QRToolKitViewModel QRToolKitViewModel = new QRToolKitViewModel();
        public QRToolkit()
        {
            InitializeComponent();

            Load += QRToolkit_Load;

            VisibleChanged += QRToolkit_VisibleChanged;

            QRToolKitViewModel.PropertyChanged += QRToolKitViewModel_PropertyChanged;
         
        }

        //notice vm changes specially for img bitmap
        private void QRToolKitViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //make sure we are in same VM and same property.
            if (e.PropertyName == nameof(QRToolKitViewModel.BitMapedCopyQr) && QRToolKitViewModel.GetType() == typeof(QRToolKitViewModel))
            {
                if ( QRToolKitViewModel.BitMapedCopyQr != null )
                {
                    //maybe needs dispatcher
                    pictureBox1.Invoke(new Action(async () => {
                      //  await Task.Delay(333);
                        pictureBox1.Image = QRToolKitViewModel.BitMapedCopyQr;
                       // pictureBox1.Update();
                    }));
                   
                }

            }
        }

        private void QRToolkit_VisibleChanged(object sender, EventArgs e)
        {
            if (!((UserControl)sender).Visible)
            {
                //here means the windwow is closing...
                if (QRToolKitViewModel.videoCaptureDevice != null)
                {
                    if (QRToolKitViewModel.videoCaptureDevice.IsRunning)
                    {
                       QRToolKitViewModel.KillCurrentInstanceOFRecoder.Execute(true);

                    }
                }
            }
        }

        private void QRToolkit_Load(object sender, EventArgs e)
        {
            //  filterItemElements = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            QRToolKitViewModel.InitiateVideoRecoder.Execute(true);


            this.sfComboBox1.DataSource =  (QRToolKitViewModel.filterItemElements).ToList<FilterInfo>();
            //Bind the Display member and Value member to the data source
            this.sfComboBox1.DisplayMember = "Name";
            this.sfComboBox1.ValueMember = "MonikerString";
            //
            this.sfComboBox1.SelectedIndex = 0;
            this.sfComboBox1.SelectedValueChanged += SfComboBox1_SelectedValueChanged;

         
        }

        private void SfComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            QRToolKitViewModel.videoCaptureDevice = new VideoCaptureDevice(((FilterInfo)this.sfComboBox1.SelectedItem).MonikerString);
            //videoCaptureDevice.VideoResolution = videoCaptureDevice.VideoCapabilities[0];
            QRToolKitViewModel.videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            QRToolKitViewModel.videoCaptureDevice.Start();
            */

            QRToolKitViewModel.videoCaptureDevice = new VideoCaptureDevice(((FilterInfo)this.sfComboBox1.SelectedItem).MonikerString);

            QRToolKitViewModel.ProcessFrameCaptured.Execute(true);
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            /*
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();


            LuminanceSource source;
            source = new ZXing.BitmapLuminanceSource(bitmap);
            var bitmapr = new BinaryBitmap(new GlobalHistogramBinarizer(source));

            res = reader.decode(bitmapr); 
            
            if (res != null)
            {
                Console.WriteLine($"{res.ToString()}");
                IDTOBEREAD = res.ToString();
              

                //stop we found it
              if (videoCaptureDevice != null)
              {
                  if (videoCaptureDevice.IsRunning)
                  {
                      ExitCamera();

                  }
              }
                
                //this.Visible = false;
                //this.Parent.Visible = false;
            }
            */
            //QRToolKitViewModel.
          //  pictureBox1.Image = bitmap;

        }
        /*
        private void ExitCamera()
        {
            videoCaptureDevice.SignalToStop();
            // FinalVideo.WaitForStop();  << marking out that one solved it
            //videoCaptureDevice.Stop();
            videoCaptureDevice.NewFrame -= new NewFrameEventHandler(VideoCaptureDevice_NewFrame); // as sugested
            videoCaptureDevice = null;
        }
        */
        //we need to bind visiblity to the view model for the whole page
        private void sfButton1_Click(object sender, EventArgs e)
        {
           /*
                if (videoCaptureDevice != null)
                {
                    if (videoCaptureDevice.IsRunning)
                    {
                        ExitCamera();

                    }
                }
           */
            this.Visible = false;
            this.Parent.Visible = false;

             
        }
    }
}
