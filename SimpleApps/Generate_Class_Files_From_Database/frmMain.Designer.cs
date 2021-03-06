﻿namespace Generate_Class_Files_From_Database
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbConnectionString = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.clbTables = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.rdoCSharp = new System.Windows.Forms.RadioButton();
            this.rdoVB = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(277, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(92, 57);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnectionString.Location = new System.Drawing.Point(12, 6);
            this.tbConnectionString.Multiline = true;
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.Size = new System.Drawing.Size(259, 57);
            this.tbConnectionString.TabIndex = 2;
            this.tbConnectionString.Text = "Enter connection string here";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(430, 6);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(92, 57);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // clbTables
            // 
            this.clbTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbTables.CheckOnClick = true;
            this.clbTables.ColumnWidth = 250;
            this.clbTables.ContextMenuStrip = this.contextMenuStrip;
            this.clbTables.FormattingEnabled = true;
            this.clbTables.Location = new System.Drawing.Point(0, 73);
            this.clbTables.MultiColumn = true;
            this.clbTables.Name = "clbTables";
            this.clbTables.Size = new System.Drawing.Size(534, 377);
            this.clbTables.TabIndex = 5;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.unselectAllToolStripMenuItem.Text = "Unselect All";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.rdoVB);
            this.panel.Controls.Add(this.rdoCSharp);
            this.panel.Location = new System.Drawing.Point(375, 6);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(49, 57);
            this.panel.TabIndex = 6;
            // 
            // rdoCSharp
            // 
            this.rdoCSharp.AutoSize = true;
            this.rdoCSharp.Checked = true;
            this.rdoCSharp.Location = new System.Drawing.Point(5, 9);
            this.rdoCSharp.Name = "rdoCSharp";
            this.rdoCSharp.Size = new System.Drawing.Size(39, 17);
            this.rdoCSharp.TabIndex = 0;
            this.rdoCSharp.TabStop = true;
            this.rdoCSharp.Text = "C#";
            this.rdoCSharp.UseVisualStyleBackColor = true;
            // 
            // rdoVB
            // 
            this.rdoVB.AutoSize = true;
            this.rdoVB.Location = new System.Drawing.Point(5, 32);
            this.rdoVB.Name = "rdoVB";
            this.rdoVB.Size = new System.Drawing.Size(39, 17);
            this.rdoVB.TabIndex = 0;
            this.rdoVB.TabStop = true;
            this.rdoVB.Text = "VB";
            this.rdoVB.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 450);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.clbTables);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbConnectionString);
            this.Controls.Add(this.btnConnect);
            this.Name = "frmMain";
            this.Text = "Generate C#/VB class files from database (SQL Server)";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbConnectionString;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckedListBox clbTables;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.RadioButton rdoVB;
        private System.Windows.Forms.RadioButton rdoCSharp;
    }
}

