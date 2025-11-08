using Cybele.Thinfinity;
using Spring.Helpers.Controls;
using Spring.Pages.ViewModel;
using Spring.StaticVM;
using Spring.View.MainView.LoginView;
using Spring.ViewModel;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.Win32API;
using Syncfusion.WinForms.DataGridConverter;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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

            this.sfBarcode1.DataBindings.Add(new Binding("Text", userCardViewModel, "IdOfCardUser"));
           
            this.textBoxExt1.DataBindings.Add(new Binding("Text", userCardViewModel, "IdOfCardUser", false, DataSourceUpdateMode.OnPropertyChanged));

            //fname
            this.fllblname.DataBindings.Add(new Binding("Text", userCardViewModel, "EmpFirstName", false, DataSourceUpdateMode.OnPropertyChanged));
            //sname
            this.slblname.DataBindings.Add(new Binding("Text", userCardViewModel, "EmpLastName", false, DataSourceUpdateMode.OnPropertyChanged));
            //lname
            this.abblbl.DataBindings.Add(new Binding("Text", userCardViewModel, "DeptAbbriviation", false, DataSourceUpdateMode.OnPropertyChanged));
            //this.lnametxtbx.DataBindings.Add(new Binding("Text", addUserViewModel, "LastPortionFName", false, DataSourceUpdateMode.OnPropertyChanged));
            //this.label3.DataBindings.Add(new Binding("ImageSource", userCardViewModel, "PersonalPhotoUser", false, DataSourceUpdateMode.OnPropertyChanged));
            this.datlbl.DataBindings.Add(new Binding("Text", userCardViewModel, "DateOfAdditon", false, DataSourceUpdateMode.OnPropertyChanged));
            this.terminationlbl.DataBindings.Add(new Binding("Text", userCardViewModel, "TerminationStatus", false, DataSourceUpdateMode.OnPropertyChanged));
            
            //checkers icons
            //name portions checker lbl
            this.checkerusercardid.DataBindings.Add(new Binding("Visible", userCardViewModel, "IdCheckerVisiblity"));

            #endregion

            #region Events

            this.textBoxExt1.TextChanged += (s, e) => { userCardViewModel.CheckIdValidityForSearch.Execute(true); };

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

                        //Pick which phase we are in
                        if (this.userCardViewModel.CurrentWait == UserCardViewModel.UserCardVMLoadingPhase.EditCheckWaiting)
                        {
                            if (userCardViewModel.EditSucceded)
                            {
                                MessageBoxAdv.Show("Photo Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBoxAdv.Show("We couldn't update that photo", "Failure!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            //reset phase of loading fter all logic done!
                            this.userCardViewModel.CurrentWait = UserCardViewModel.UserCardVMLoadingPhase.Non;
                        }
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
                              //extract the card.. pdf
                              var options = new PdfExportingOptions();
                              var document = new PdfDocument();
                              document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                              var page = document.Pages.Add();
                              var PDFGrid = new PdfGrid();
                              PDFGrid.Columns.Add(2);
                              PDFGrid.Headers.Add(1);

                              PDFGrid.Headers[0].Cells[0].Value = "Id";
                              PDFGrid.Headers[0].Cells[1].Value = "Card View";

                             // PDFGrid.Headers[0].Cells[1].Value = "Id";
                             // PDFGrid.Headers[0].Cells[1].Value = "Back View";

                              PDFGrid.Rows.Add(); PDFGrid.Rows[0].Height = 250;
                              PDFGrid.Rows.Add(); PDFGrid.Rows[1].Height = 250;
                              PDFGrid.Rows[0].Cells[0].Value = PDFGrid.Rows[1].Cells[0].Value =  userCardViewModel.IdOfCardUser;

                              // Convert System.Drawing.Image to Syncfusion.Pdf.Graphics.PdfImage
                              using (MemoryStream ms = new MemoryStream())
                              {
                                    var bmp = new Bitmap(this.tableLayoutPanel2.Width, this.tableLayoutPanel2.Height);
                                    this.tableLayoutPanel2.DrawToBitmap(bmp,new Rectangle(0,0,bmp.Width,bmp.Height));
                                     bmp.Save(ms, ImageFormat.Png);
                                    ms.Position = 0;
                                    PdfImage pdfImage = PdfImage.FromStream(ms);
                                    PDFGrid.Rows[0].Cells[1].Style.BackgroundImage = pdfImage;


                                    MemoryStream ms2 = new MemoryStream();
                                    var bmp2 = new Bitmap(this.label6.Width, this.label6.Height);
                                    this.label6.DrawToBitmap(bmp2, new Rectangle(5, 0, bmp2.Width-10, bmp2.Height-40));
                                    bmp2.Save(ms2, ImageFormat.Png);
                                    ms2.Position = 0;
                                    PdfImage pdfImage2 = PdfImage.FromStream(ms2);
                                    PDFGrid.Rows[1].Cells[1].Style.BackgroundImage = pdfImage2;
                              }

                              var format = new PdfGridLayoutFormat()
                              {
                                    Layout = PdfLayoutType.Paginate,
                                    Break = PdfLayoutBreakType.FitPage
                              };

                              PDFGrid.Draw(page, new PointF(0, 55), format);

                              //Create a header and draw the image.
                              RectangleF bounds = new RectangleF(0, 0, page.Size.Width - 80, 50);
                              PdfTemplate header = new PdfTemplate(bounds);
                              PdfImage image = new PdfBitmap(@"init\\header_pdf.png");
                              header.Graphics.DrawImage(image, new PointF(0, 0), new SizeF(page.Graphics.ClientSize.Width, 50));

                              PdfFont font = new PdfStandardFont(PdfFontFamily.Courier, 7f, PdfFontStyle.Regular);
                              header.Graphics.DrawString("for: HAAM Corp. Ltd.", font, PdfPens.Red, page.Size.Width - 200, 12);
                              header.Graphics.DrawString("Ready Printed User Card.", font, PdfPens.Red, page.Size.Width - 200, 2 * 12);
                              header.Graphics.DrawString($"exported: {(DateTime.Now.Date).ToString("MM/dd/yyyy")}.", font, PdfPens.Red, page.Size.Width - 200, 3 * 12);
                              page.Graphics.DrawPdfTemplate((header), new PointF());

                              RectangleF bounds_ = new RectangleF(0, 0, page.GetClientSize().Width - 100, 50);
                              PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds_);
                              PdfFont font_f = new PdfStandardFont(PdfFontFamily.Helvetica, 7);
                              PdfBrush brush = new PdfSolidBrush(Color.Red);
                              PdfPageNumberField pageNumber = new PdfPageNumberField(font_f, brush);
                              PdfPageCountField count = new PdfPageCountField(font_f, brush);
                              PdfCompositeField compositeField = new PdfCompositeField(font_f, brush, "{0}/{1}", pageNumber, count);
                              compositeField.Bounds = new RectangleF(footer.Bounds.X, footer.Bounds.Y + 20, footer.Bounds.Width, footer.Bounds.Height + 20);
                              compositeField.Draw(footer.Graphics, new PointF(page.Size.Width - 100, 12));
                              document.Template.Bottom = footer;

                              foreach (PdfPage _page in document.Pages)
                              {
                                    PdfImage imagewm = new PdfBitmap(@"init\\bg.png");
                                    PdfGraphicsState state = _page.Graphics.Save();
                                    _page.Graphics.SetTransparency(0.25f);
                                    _page.Graphics.DrawImage(imagewm, new PointF(0, 0), _page.Graphics.ClientSize);
                              }

                              SaveFileDialog saveFileDialog = new SaveFileDialog
                              {
                                    Filter = "PDF Files(*.pdf)|*.pdf"
                              };
                              if (saveFileDialog.ShowDialog() == DialogResult.OK)
                              {
                                    using (Stream stream = saveFileDialog.OpenFile())
                                    {
                                          document.Save(stream);
                                    }
                                    if (VMCentral.DockingManagerViewModel.PlatformTypeUsed == Spring.ViewModel.DockingManagerViewModel.PlatformType.VirtualWeb)
                                    {
                                          VirtualUI vui = new VirtualUI();
                                          vui.DownloadFile(saveFileDialog.FileName);
                                    }
                                    else
                                    {
                                          if (MessageBox.Show("Do you want to view the Pdf file?", "Pdf file has been created", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                          {
                                                System.Diagnostics.Process.Start(saveFileDialog.FileName);
                                          }
                                    }
                              }

                        }
                        if (adv.Text == PagesNodesNames.UserCardSecondButtonTitle)
                {

                   userCardViewModel.LoadCurrentUserCard.Execute(true);

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
                            //VirtualUI vui = new VirtualUI();
                            //vui.UploadFile(openFileDialog.FileName);
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
                        userCardViewModel.SavePhotoUserCard.Execute(true);
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
