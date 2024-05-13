namespace Spring.Pages.ChartsPages
{
    partial class DepartmentsStatisticsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.Chart.ChartSeries chartSeries1 = new Syncfusion.Windows.Forms.Chart.ChartSeries();
            Syncfusion.Windows.Forms.Chart.ChartCustomShapeInfo chartCustomShapeInfo1 = new Syncfusion.Windows.Forms.Chart.ChartCustomShapeInfo();
            Syncfusion.Windows.Forms.Chart.ChartLineInfo chartLineInfo1 = new Syncfusion.Windows.Forms.Chart.ChartLineInfo();
            this.chartControl1 = new Syncfusion.Windows.Forms.Chart.ChartControl();
            this.progressBarAdvUsersStatistics = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdvUsersStatistics)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarAdv3
            // 
            this.progressBarAdv3.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.Location = new System.Drawing.Point(0, 392);
            this.progressBarAdv3.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv3.Size = new System.Drawing.Size(693, 13);
            this.progressBarAdv3.Text = "progressBarUsersStatistics";
            // 
            // chartControl1
            // 
            this.chartControl1.BackgroundImage = global::Spring.Properties.Resources.desktopGrid_f324974d;
            this.chartControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chartControl1.BackInterior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(220)))), ((int)(((byte)(220))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(241)))), ((int)(((byte)(231))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))))});
            this.chartControl1.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.ChartArea.CursorLocation = new System.Drawing.Point(0, 0);
            this.chartControl1.ChartArea.CursorReDraw = false;
            this.chartControl1.ChartInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.CustomPalette = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(69)))), ((int)(((byte)(153))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(44)))), ((int)(((byte)(36))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(88)))), ((int)(((byte)(167))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(95)))), ((int)(((byte)(47))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(83)))), ((int)(((byte)(27))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(31)))), ((int)(((byte)(64)))))};
            this.chartControl1.DataSourceName = "[none]";
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(103)))), ((int)(((byte)(103)))));
            this.chartControl1.IsWindowLess = false;
            // 
            // 
            // 
            this.chartControl1.Legend.Location = new System.Drawing.Point(628, 82);
            this.chartControl1.LegendsPlacement = Syncfusion.Windows.Forms.Chart.ChartPlacement.Outside;
            this.chartControl1.Localize = null;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.Palette = Syncfusion.Windows.Forms.Chart.ChartColorPalette.Custom;
            this.chartControl1.PrimaryXAxis.GridLineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryXAxis.LineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryXAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            this.chartControl1.PrimaryXAxis.Margin = true;
            this.chartControl1.PrimaryXAxis.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryXAxis.TitleColor = System.Drawing.SystemColors.ControlText;
            this.chartControl1.PrimaryYAxis.GridLineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryYAxis.LineType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryYAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            this.chartControl1.PrimaryYAxis.Margin = true;
            this.chartControl1.PrimaryYAxis.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(158)))), ((int)(((byte)(205)))));
            this.chartControl1.PrimaryYAxis.TitleColor = System.Drawing.SystemColors.ControlText;
            chartSeries1.FancyToolTip.ResizeInsideSymbol = true;
            chartSeries1.Name = "usrs";
            chartSeries1.Points.Add(0D, ((double)(23D)));
            chartSeries1.Points.Add(0D, ((double)(34D)));
            chartSeries1.Points.Add(0D, ((double)(50D)));
            chartSeries1.Points.Add(0D, ((double)(47D)));
            chartSeries1.Resolution = 0D;
            chartSeries1.StackingGroup = "Default Group";
            chartSeries1.Style.AltTagFormat = "";
            chartSeries1.Style.DrawTextShape = false;
            chartLineInfo1.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
            chartLineInfo1.Color = System.Drawing.SystemColors.ControlText;
            chartLineInfo1.DashPattern = null;
            chartLineInfo1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartLineInfo1.Width = 1F;
            chartCustomShapeInfo1.Border = chartLineInfo1;
            chartCustomShapeInfo1.Color = System.Drawing.SystemColors.HighlightText;
            chartCustomShapeInfo1.Type = Syncfusion.Windows.Forms.Chart.ChartCustomShape.Square;
            chartSeries1.Style.TextShape = chartCustomShapeInfo1;
            chartSeries1.Text = "usrs";
            this.chartControl1.Series.Add(chartSeries1);
            this.chartControl1.ShowToolbar = true;
            this.chartControl1.Size = new System.Drawing.Size(693, 392);
            this.chartControl1.TabIndex = 0;
            this.chartControl1.Text = "Users";
            // 
            // 
            // 
            this.chartControl1.Title.Name = "Default";
            this.chartControl1.Titles.Add(this.chartControl1.Title);
            this.chartControl1.ToolBar.Visible = true;
            this.chartControl1.VisualTheme = "";
            // 
            // progressBarAdvUsersStatistics
            // 
            this.progressBarAdvUsersStatistics.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdvUsersStatistics.BackSegments = false;
            this.progressBarAdvUsersStatistics.CustomText = null;
            this.progressBarAdvUsersStatistics.CustomWaitingRender = false;
            this.progressBarAdvUsersStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarAdvUsersStatistics.ForegroundImage = null;
            this.progressBarAdvUsersStatistics.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBarAdvUsersStatistics.GradientStartColor = System.Drawing.Color.Orange;
            this.progressBarAdvUsersStatistics.Location = new System.Drawing.Point(0, 379);
            this.progressBarAdvUsersStatistics.Margin = new System.Windows.Forms.Padding(10);
            this.progressBarAdvUsersStatistics.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdvUsersStatistics.Name = "progressBarAdvUsersStatistics";
            this.progressBarAdvUsersStatistics.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarAdvUsersStatistics.SegmentWidth = 12;
            this.progressBarAdvUsersStatistics.Size = new System.Drawing.Size(693, 13);
            this.progressBarAdvUsersStatistics.TabIndex = 15;
            this.progressBarAdvUsersStatistics.Text = "progressBarAdv1";
            this.progressBarAdvUsersStatistics.TextShadow = false;
            this.progressBarAdvUsersStatistics.TextVisible = false;
            this.progressBarAdvUsersStatistics.ThemeName = "WaitingGradient";
            this.progressBarAdvUsersStatistics.TubeEndColor = System.Drawing.SystemColors.MenuHighlight;
            this.progressBarAdvUsersStatistics.TubeStartColor = System.Drawing.Color.IndianRed;
            this.progressBarAdvUsersStatistics.WaitingGradientWidth = 400;
            // 
            // DepartmentsStatisticsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBarAdvUsersStatistics);
            this.Controls.Add(this.chartControl1);
            this.Name = "DepartmentsStatisticsPage";
            this.Size = new System.Drawing.Size(693, 405);
            this.Controls.SetChildIndex(this.progressBarAdv3, 0);
            this.Controls.SetChildIndex(this.chartControl1, 0);
            this.Controls.SetChildIndex(this.progressBarAdvUsersStatistics, 0);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdvUsersStatistics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Chart.ChartControl chartControl1;
        public Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarAdvUsersStatistics;
    }
}
