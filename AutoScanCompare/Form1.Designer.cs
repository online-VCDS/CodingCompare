using System;
using System.Windows.Forms;

namespace AutoScanCompare
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RunAutoscanCompareBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OpenCodingCompareBtn = new System.Windows.Forms.Button();
            this.labelMJ1 = new System.Windows.Forms.Label();
            this.labelMJ2 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.EnableContexMenuBtn = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.Name = "treeView1";
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AllowDrop = true;
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.richTextBox1_DragDrop);
            this.richTextBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.richTextBox_DragEnter);
            this.richTextBox1.VScroll += new System.EventHandler(this.myRichTextBox1_VScroll);
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.myRichTextBox1_VScroll);
            // 
            // richTextBox2
            // 
            this.richTextBox2.AllowDrop = true;
            resources.ApplyResources(this.richTextBox2, "richTextBox2");
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.richTextBox2_DragDrop);
            this.richTextBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.richTextBox_DragEnter);
            this.richTextBox2.VScroll += new System.EventHandler(this.myRichTextBox2_VScroll);
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            this.richTextBox2.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.myRichTextBox2_VScroll);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // RunAutoscanCompareBtn
            // 
            resources.ApplyResources(this.RunAutoscanCompareBtn, "RunAutoscanCompareBtn");
            this.RunAutoscanCompareBtn.Name = "RunAutoscanCompareBtn";
            this.RunAutoscanCompareBtn.UseVisualStyleBackColor = true;
            this.RunAutoscanCompareBtn.Click += new System.EventHandler(this.RunAutoscanCompareBtn_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // OpenCodingCompareBtn
            // 
            resources.ApplyResources(this.OpenCodingCompareBtn, "OpenCodingCompareBtn");
            this.OpenCodingCompareBtn.Name = "OpenCodingCompareBtn";
            this.OpenCodingCompareBtn.UseVisualStyleBackColor = true;
            this.OpenCodingCompareBtn.Click += new System.EventHandler(this.OpenCodingCompareBtn_Click);
            // 
            // labelMJ1
            // 
            resources.ApplyResources(this.labelMJ1, "labelMJ1");
            this.labelMJ1.Name = "labelMJ1";
            // 
            // labelMJ2
            // 
            resources.ApplyResources(this.labelMJ2, "labelMJ2");
            this.labelMJ2.Name = "labelMJ2";
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // EnableContexMenuBtn
            // 
            resources.ApplyResources(this.EnableContexMenuBtn, "EnableContexMenuBtn");
            this.EnableContexMenuBtn.Name = "EnableContexMenuBtn";
            this.EnableContexMenuBtn.UseVisualStyleBackColor = true;
            this.EnableContexMenuBtn.Click += new System.EventHandler(this.EnableContexMenuBtn_Click);
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.EnableContexMenuBtn);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.labelMJ2);
            this.Controls.Add(this.labelMJ1);
            this.Controls.Add(this.OpenCodingCompareBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RunAutoscanCompareBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RunAutoscanCompareBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button OpenCodingCompareBtn;
        private Label labelMJ1;
        private Label labelMJ2;
        private CheckBox checkBox3;
        private Button EnableContexMenuBtn;
		private CheckBox checkBox4;
	}
}

