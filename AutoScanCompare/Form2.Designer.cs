namespace AutoScanCompare
{
    partial class Form2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.label14 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.labelSearchCoding = new System.Windows.Forms.Label();
			this.labelSearchCodingUnit = new System.Windows.Forms.Label();
			this.textBoxSearchAddress = new System.Windows.Forms.TextBox();
			this.labelSearchCodingLabel = new System.Windows.Forms.Label();
			this.textBoxSearchByte = new System.Windows.Forms.TextBox();
			this.textBoxSearchBit = new System.Windows.Forms.TextBox();
			this.labelSearchCodingBit = new System.Windows.Forms.Label();
			this.checkBoxSearchCodingBitSet = new System.Windows.Forms.CheckBox();
			this.buttonSearchCoding = new System.Windows.Forms.Button();
			this.labelSearchCodingFolderPath = new System.Windows.Forms.Label();
			this.buttonSearchCodingBasePath = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.labelKeineTreffer = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeView1.Location = new System.Drawing.Point(19, 385);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(771, 201);
			this.treeView1.TabIndex = 3;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(697, 592);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(96, 13);
			this.label14.TabIndex = 31;
			this.label14.Text = "V2.0    by VCDS.de";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(19, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(771, 367);
			this.tabControl1.TabIndex = 32;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
			this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(763, 341);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// labelSearchCoding
			// 
			this.labelSearchCoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSearchCoding.AutoSize = true;
			this.labelSearchCoding.Location = new System.Drawing.Point(16, 591);
			this.labelSearchCoding.Name = "labelSearchCoding";
			this.labelSearchCoding.Size = new System.Drawing.Size(96, 13);
			this.labelSearchCoding.TabIndex = 33;
			this.labelSearchCoding.Text = "Codierung suchen:";
			// 
			// labelSearchCodingUnit
			// 
			this.labelSearchCodingUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSearchCodingUnit.AutoSize = true;
			this.labelSearchCodingUnit.Location = new System.Drawing.Point(118, 591);
			this.labelSearchCodingUnit.Name = "labelSearchCodingUnit";
			this.labelSearchCodingUnit.Size = new System.Drawing.Size(48, 13);
			this.labelSearchCodingUnit.TabIndex = 34;
			this.labelSearchCodingUnit.Text = "Adresse:";
			// 
			// textBoxSearchAddress
			// 
			this.textBoxSearchAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxSearchAddress.Location = new System.Drawing.Point(166, 588);
			this.textBoxSearchAddress.Name = "textBoxSearchAddress";
			this.textBoxSearchAddress.Size = new System.Drawing.Size(26, 20);
			this.textBoxSearchAddress.TabIndex = 35;
			// 
			// labelSearchCodingLabel
			// 
			this.labelSearchCodingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSearchCodingLabel.AutoSize = true;
			this.labelSearchCodingLabel.Location = new System.Drawing.Point(198, 591);
			this.labelSearchCodingLabel.Name = "labelSearchCodingLabel";
			this.labelSearchCodingLabel.Size = new System.Drawing.Size(31, 13);
			this.labelSearchCodingLabel.TabIndex = 36;
			this.labelSearchCodingLabel.Text = "Byte:";
			// 
			// textBoxSearchByte
			// 
			this.textBoxSearchByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxSearchByte.Location = new System.Drawing.Point(230, 588);
			this.textBoxSearchByte.Name = "textBoxSearchByte";
			this.textBoxSearchByte.Size = new System.Drawing.Size(26, 20);
			this.textBoxSearchByte.TabIndex = 37;
			// 
			// textBoxSearchBit
			// 
			this.textBoxSearchBit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxSearchBit.Location = new System.Drawing.Point(289, 588);
			this.textBoxSearchBit.Name = "textBoxSearchBit";
			this.textBoxSearchBit.Size = new System.Drawing.Size(26, 20);
			this.textBoxSearchBit.TabIndex = 39;
			// 
			// labelSearchCodingBit
			// 
			this.labelSearchCodingBit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSearchCodingBit.AutoSize = true;
			this.labelSearchCodingBit.Location = new System.Drawing.Point(267, 592);
			this.labelSearchCodingBit.Name = "labelSearchCodingBit";
			this.labelSearchCodingBit.Size = new System.Drawing.Size(22, 13);
			this.labelSearchCodingBit.TabIndex = 38;
			this.labelSearchCodingBit.Text = "Bit:";
			// 
			// checkBoxSearchCodingBitSet
			// 
			this.checkBoxSearchCodingBitSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxSearchCodingBitSet.AutoSize = true;
			this.checkBoxSearchCodingBitSet.Location = new System.Drawing.Point(322, 591);
			this.checkBoxSearchCodingBitSet.Name = "checkBoxSearchCodingBitSet";
			this.checkBoxSearchCodingBitSet.Size = new System.Drawing.Size(75, 17);
			this.checkBoxSearchCodingBitSet.TabIndex = 40;
			this.checkBoxSearchCodingBitSet.Text = "Bit gesetzt";
			this.checkBoxSearchCodingBitSet.UseVisualStyleBackColor = true;
			// 
			// buttonSearchCoding
			// 
			this.buttonSearchCoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSearchCoding.Location = new System.Drawing.Point(524, 588);
			this.buttonSearchCoding.Name = "buttonSearchCoding";
			this.buttonSearchCoding.Size = new System.Drawing.Size(75, 20);
			this.buttonSearchCoding.TabIndex = 41;
			this.buttonSearchCoding.Text = "Suchen";
			this.buttonSearchCoding.UseVisualStyleBackColor = true;
			this.buttonSearchCoding.Click += new System.EventHandler(this.buttonSearchCoding_Click);
			this.buttonSearchCoding.MouseHover += new System.EventHandler(this.buttonSearchCoding_MouseHover);
			// 
			// labelSearchCodingFolderPath
			// 
			this.labelSearchCodingFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSearchCodingFolderPath.AutoSize = true;
			this.labelSearchCodingFolderPath.Location = new System.Drawing.Point(413, 592);
			this.labelSearchCodingFolderPath.Name = "labelSearchCodingFolderPath";
			this.labelSearchCodingFolderPath.Size = new System.Drawing.Size(62, 13);
			this.labelSearchCodingFolderPath.TabIndex = 42;
			this.labelSearchCodingFolderPath.Text = "Basispfad...";
			// 
			// buttonSearchCodingBasePath
			// 
			this.buttonSearchCodingBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSearchCodingBasePath.Location = new System.Drawing.Point(474, 588);
			this.buttonSearchCodingBasePath.Name = "buttonSearchCodingBasePath";
			this.buttonSearchCodingBasePath.Size = new System.Drawing.Size(29, 20);
			this.buttonSearchCodingBasePath.TabIndex = 43;
			this.buttonSearchCodingBasePath.Text = "...";
			this.buttonSearchCodingBasePath.UseVisualStyleBackColor = true;
			this.buttonSearchCodingBasePath.Click += new System.EventHandler(this.buttonSearchCodingBasePath_Click);
			this.buttonSearchCodingBasePath.MouseHover += new System.EventHandler(this.buttonSearchCodingBasePath_MouseHover);
			// 
			// labelKeineTreffer
			// 
			this.labelKeineTreffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelKeineTreffer.AutoSize = true;
			this.labelKeineTreffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelKeineTreffer.Location = new System.Drawing.Point(605, 592);
			this.labelKeineTreffer.Name = "labelKeineTreffer";
			this.labelKeineTreffer.Size = new System.Drawing.Size(35, 13);
			this.labelKeineTreffer.TabIndex = 44;
			this.labelKeineTreffer.Text = "label1";
			// 
			// Form2
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(805, 610);
			this.Controls.Add(this.labelKeineTreffer);
			this.Controls.Add(this.buttonSearchCodingBasePath);
			this.Controls.Add(this.labelSearchCodingFolderPath);
			this.Controls.Add(this.buttonSearchCoding);
			this.Controls.Add(this.checkBoxSearchCodingBitSet);
			this.Controls.Add(this.textBoxSearchBit);
			this.Controls.Add(this.labelSearchCodingBit);
			this.Controls.Add(this.textBoxSearchByte);
			this.Controls.Add(this.labelSearchCodingLabel);
			this.Controls.Add(this.textBoxSearchAddress);
			this.Controls.Add(this.labelSearchCodingUnit);
			this.Controls.Add(this.labelSearchCoding);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.treeView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(821, 648);
			this.Name = "Form2";
			this.Text = "VCDS Auto-Scan GUI";
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label labelSearchCoding;
		private System.Windows.Forms.Label labelSearchCodingUnit;
		private System.Windows.Forms.TextBox textBoxSearchAddress;
		private System.Windows.Forms.Label labelSearchCodingLabel;
		private System.Windows.Forms.TextBox textBoxSearchByte;
		private System.Windows.Forms.TextBox textBoxSearchBit;
		private System.Windows.Forms.Label labelSearchCodingBit;
		private System.Windows.Forms.CheckBox checkBoxSearchCodingBitSet;
		private System.Windows.Forms.Button buttonSearchCoding;
		private System.Windows.Forms.Label labelSearchCodingFolderPath;
		private System.Windows.Forms.Button buttonSearchCodingBasePath;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label labelKeineTreffer;
	}
}
