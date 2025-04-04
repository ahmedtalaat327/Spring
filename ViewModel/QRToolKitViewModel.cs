
using AForge.Video.DirectShow;
using Spring.ViewModel.Base;
using ZXing.QrCode;
using ZXing;
using Spring.StaticVM;
using System.Windows.Input;
using System.Threading.Tasks;
using Spring.ViewModel.Command;
using AForge.Video;
using System;
using ZXing.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Spring.ViewModel
{
    public class QRToolKitViewModel : BaseViewModel
    {



        #region Private Members

       
        QRCodeReader reader = new QRCodeReader();

        string _id = "";
        #endregion
        #region Public Property
        #region Commands
        public ICommand InitiateVideoRecoder { get; set; }
        public ICommand KillCurrentInstanceOFRecoder { get; set; }
        public ICommand ProcessFrameCaptured { get; set; }
        public ICommand KOperateOnUploaededBM { get; set; }
        #endregion
        public string IDTOBEREAD
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;

                    VMCentral.changeOrTerminateCurrentUserViewModel.Id = (res.ToString());



                    VMCentral.changeOrTerminateCurrentUserViewModel.LoadCurrentUser.Execute(true);
                    /*
                    //focus on id textbox get out from this thread to the main page thread
                    this.Invoke(new Action(() => {
                        this.Visible = false;
                        this.Parent.Visible = false;
                    
                    }));
                    */
                    VisiblePanel = false;
                   // OnPropertyChanged(nameof(VisiblePanel));    
                }
            }
        }
        public Bitmap BitMapedQR { get; set; } 
        public Bitmap BitMapedCopyQr { get; set; }
        public bool BitMapLocked { get; set; }  

        public bool VisiblePanel { get; set; }
        public VideoCaptureDevice videoCaptureDevice;

        public FilterInfoCollection filterItemElements { get; set; }
        public Result res { get; set; }



        //k-> knop instead of auto scan
        //public Bitmap KBitMapedQR { get; set; }
        public Result KRes { get; set; }
        #endregion
        #region constructor
        public QRToolKitViewModel()
        {
            VisiblePanel = true;
            InitiateVideoRecoder = new RelyCommand(async () => {await LoadInitValues(); });

            KillCurrentInstanceOFRecoder = new RelyCommand(async () => { await EndVideoRecord(); });

            ProcessFrameCaptured = new RelyCommand(async () => { await StartVideoRecord(); });

            KOperateOnUploaededBM = new RelayParameterizedCommand(async(bm) => { await DecodeImage(bm); });
        }
        #endregion
        private async Task LoadInitValues()
        {

            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
                filterItemElements = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            });
        }
        private async Task StartVideoRecord()
        {
            BitMapLocked = true;
            
                await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
               
                    //videoCaptureDevice.VideoResolution = videoCaptureDevice.VideoCapabilities[0];
                    videoCaptureDevice.NewFrame += new NewFrameEventHandler(async (s, e) => await FramedCaptured(s, e)); // as sugested;
                    videoCaptureDevice.Start();
                
            });
        }
        private async Task EndVideoRecord()
        {
            await RunCommand(() => VMCentral.DockingManagerViewModel.Loading, async () =>
            {
                videoCaptureDevice.SignalToStop();
                // FinalVideo.WaitForStop();  << marking out that one solved it
                //videoCaptureDevice.Stop();
                videoCaptureDevice.NewFrame -= new NewFrameEventHandler(async (s,e)=>await FramedCaptured(s,e)); // as sugested
                videoCaptureDevice = null;
                BitMapLocked = false;
            });
        }

        private async Task FramedCaptured(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (BitMapLocked)
            {

                BitMapedQR = (Bitmap)eventArgs.Frame.Clone();

                BitMapedCopyQr = (Bitmap)BitMapedQR.Clone();
                //  OnPropertyChanged(nameof(BitMapedCopyQr));

                try
                {
                    LuminanceSource source;

                    source = new ZXing.BitmapLuminanceSource(BitMapedQR);

                    var bitmapr = new BinaryBitmap(new GlobalHistogramBinarizer(source));

                    res = reader.decode(bitmapr);


                    if (res != null)
                    {


                        //stop we found it
                        if (videoCaptureDevice != null)
                        {
                            if (videoCaptureDevice.IsRunning)
                            {
                                await EndVideoRecord();

                                Console.WriteLine($"{res.ToString()}");
                                IDTOBEREAD = res.ToString();
                                OnPropertyChanged(nameof(IDTOBEREAD));

                            }
                        }

                        //this.Visible = false;
                        //this.Parent.Visible = false;
                    }
                }
                catch { /**/}

            }
        }
        private async Task DecodeImage(object urlToBM)
        {
            await Task.Delay(2);

            try
            {
                LuminanceSource source;

                Bitmap KBM = new Bitmap(urlToBM.ToString());

                source = new ZXing.BitmapLuminanceSource(KBM);

                var bitmapr = new BinaryBitmap(new GlobalHistogramBinarizer(source));

                res = reader.decode(bitmapr);


                if (res != null)
                {


                     
                       
                            Console.WriteLine($"{res.ToString()}");
                            IDTOBEREAD = res.ToString();
                            OnPropertyChanged(nameof(IDTOBEREAD));
 

                    //this.Visible = false;
                    //this.Parent.Visible = false;
                }
            }
            catch { /**/}
        }
    }
}
