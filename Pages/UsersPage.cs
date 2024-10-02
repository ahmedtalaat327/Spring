using Spring.Pages.ViewModel;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.IO;
using Syncfusion.Data;
using Spring.Helpers.Controls;
using Cybele.Thinfinity;
using Spring.StaticVM;
using Syncfusion.XPS;

namespace Spring.Pages
{
    public partial class UsersPage : BasePage
    {

        UsersViewModel usersViewModel = new UsersViewModel();
        public UsersPage() 
        {
            InitializeComponent();

           
            //bindings
            this.sfDataGrid1.DataSource = usersViewModel.CurrentUsers;

            //binding active flag to panel
            //this.DataBindings.Add(new Binding("Enabled", usersViewModel, "ActivePanel"));

            #region Events

            this.Load += UsersPage_Load;

          
            /*
            optionsTree.BeforeSelect += (e, o) =>
            {

                Point pt = optionsTree.PointToClient(Cursor.Position);
                TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
                if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                    o.Cancel = true;
            };


            optionsTree.Click += (evt, obj) =>
            {

                Point pt = optionsTree.PointToClient(Cursor.Position);
                TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, true);
                if (node != null && node == optionsTree.SelectedNode)
                {
                    RaiseClick(node);
                }


            };

            optionsTree.AfterSelect += (eve, ob) =>
            {
                //  RaiseClick(optionsTree.SelectedNode);
            };
            */

            #endregion

            sfDataGrid1.Columns["Id"].FilterPredicates.Add(new FilterPredicate() { FilterType = FilterType.LessThan, FilterValue = "18" });
        }
        public override void AddEventsToOptionsNodes(TreeViewAdv optionsTree)
        {
            optionsTree.BeforeSelect += OptionsTree_BeforeSelect;

            optionsTree.Click += OptionsTree_Click;

            #region view define
            (StaticVM.VMCentral.DockingManagerViewModel.ViewName) = PagesNodesNames.UsersPrimaryPageButtonName;
            #endregion


            OnLoad(new EventArgs());
        }

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

        public  override void OptionsTree_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs o)
        {
            var optionsTree = (TreeViewAdv)sender;
            Point pt = optionsTree.PointToClient(Cursor.Position);
            TreeNodeAdv node = optionsTree.GetNodeAtPoint(pt, false, false);
            if (o.Action == TreeViewAdvAction.ByMouse && node == null)
                o.Cancel = true;
        }

        private void UsersPage_Load(object sender, EventArgs e)
        {
            usersViewModel.LoadAllUsers.Execute(true);
        }


        void RaiseClick(TreeNodeAdv adv)
        {
            // please use your code here
            if (this.Enabled && adv != null)
            {
                if (adv.Text == PagesNodesNames.UsersFirstButtonTitle)
                {

                    var options = new PdfExportingOptions();
                    //options.HeaderFooterExporting += options_HeaderFooterExporting;
                    var document = new PdfDocument();
                    document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                    var page = document.Pages.Add();
                    var PDFGrid = sfDataGrid1.ExportToPdfGrid(sfDataGrid1.View, options);
                    var format = new PdfGridLayoutFormat()
                    {
                        Layout = PdfLayoutType.Paginate,
                        Break = PdfLayoutBreakType.FitPage

                    };

                    

                    PDFGrid.Draw(page, new PointF(0, 55), format);

                    //Create a header and draw the image.
                    RectangleF bounds = new RectangleF(0, 0, page.Size.Width - 100, 50);
                    PdfTemplate header = new PdfTemplate(bounds);
                    PdfImage image = new PdfBitmap(@"init\\header_pdf.png");
                    //Draw the image in the header.
                    header.Graphics.DrawImage(image, new PointF(0, 0), new SizeF(page.Graphics.ClientSize.Width-20, 50));


                    PdfFont font = new PdfStandardFont(PdfFontFamily.Courier, 7f, PdfFontStyle.Regular);
                    header.Graphics.DrawString("for: HAAM Corp. Ltd.", font, PdfPens.Red, page.Size.Width - 200, 12);
                    header.Graphics.DrawString("USERS Report.", font, PdfPens.Red, page.Size.Width - 200, 2 * 12);
                    header.Graphics.DrawString($"exported: {(DateTime.Now.Date).ToString("MM/dd/yyyy")}.", font, PdfPens.Red, page.Size.Width - 200, 3 * 12);
                    //Add the header at the top.
                    //document.Template.Top = header;
                    page.Graphics.DrawPdfTemplate((header), new PointF());




                    RectangleF bounds_ = new RectangleF(0, 0, page.GetClientSize().Width - 100, 50);
                    //Create a Page template that can be used as footer.
                    PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds_);
                    PdfFont font_f = new PdfStandardFont(PdfFontFamily.Helvetica, 7);
                    PdfBrush brush = new PdfSolidBrush(Color.Red);
                    //Create page number field.
                    PdfPageNumberField pageNumber = new PdfPageNumberField(font_f, brush);
                    //Create page count field.
                    PdfPageCountField count = new PdfPageCountField(font_f, brush);
                    //Add the fields in composite fields.
                    PdfCompositeField compositeField = new PdfCompositeField(font_f, brush, "{0}/{1}", pageNumber, count);
                    compositeField.Bounds = new RectangleF(footer.Bounds.X, footer.Bounds.Y + 20, footer.Bounds.Width, footer.Bounds.Height + 20);
                    //Draw the composite field in footer.
                    compositeField.Draw(footer.Graphics, new PointF(page.Size.Width - 100, 12));

                    //Add the footer template at the bottom.
                    document.Template.Bottom = footer;

                    foreach (PdfPage _page in document.Pages)
                    {
                        //draw watermark
                        PdfImage imagewm = new PdfBitmap(@"init\\bg.png");
                        PdfGraphicsState state = _page.Graphics.Save();
                        _page.Graphics.SetTransparency(0.25f);
                        //Draw the image. 
                        _page.Graphics.DrawImage(imagewm, new PointF(0, 0), _page.Graphics.ClientSize);
                        //
                    }

                  //document.Save("Sample.pdf");
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
                            //Message box confirmation to view the created Pdf file.
                            if (MessageBox.Show("Do you want to view the Pdf file?", "Pdf file has been created", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                //Launching the Pdf file using the default Application.
                                System.Diagnostics.Process.Start(saveFileDialog.FileName);
                            }
                        }
                    } 
                }
                if (adv.Text == PagesNodesNames.UsersSecondButtonTitle)
                {
                    usersViewModel.LoadAllUsers.Execute(true); 
                }



            
        }
    }

       
        #endregion


    }
}
