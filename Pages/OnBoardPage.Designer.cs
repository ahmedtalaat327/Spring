﻿namespace Spring.Pages
{
    partial class OnBoardPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnBoardPage));
            //Spring.Pages.ViewModel.BasePageViewModel basePageViewModel1 = new Spring.Pages.ViewModel.BasePageViewModel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.usersStatisticsPage1 = new Spring.Pages.ChartsPages.UsersStatisticsPage();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.usersStatisticsPage1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 397);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // usersStatisticsPage1
            // 
            this.usersStatisticsPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usersStatisticsPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("usersStatisticsPage1.BackgroundImage")));
            //basePageViewModel1.ActiveView = false;
            //this.usersStatisticsPage1.DataBindings.Add(new System.Windows.Forms.Binding("Visible", basePageViewModel1, "ActiveView", true));
            this.usersStatisticsPage1.Location = new System.Drawing.Point(3, 3);
            this.usersStatisticsPage1.Name = "usersStatisticsPage1";
            this.usersStatisticsPage1.Size = new System.Drawing.Size(386, 192);
            this.usersStatisticsPage1.TabIndex = 0;
            // 
            // OnBoardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OnBoardPage";
            this.Size = new System.Drawing.Size(784, 397);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public ChartsPages.UsersStatisticsPage usersStatisticsPage1;
    }
}
