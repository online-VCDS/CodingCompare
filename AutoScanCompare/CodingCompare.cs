using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodingCompare.Resources;

namespace AutoScanCompare
{
    public partial class CodingCompare : Form
    {
        int lastposition;
        string parentTitle;
        
        List<string> differentBits = new List<string>();

        public CodingCompare(TreeNode node, string parentText, string coding1 = "", string coding2 = "")
        {
            parentTitle = parentText;
            InitializeComponent();
            //label11.Select();
            this.BackColor = ColorTranslator.FromHtml("#CDE1E2");
            
            this.Text = parentTitle;

			label15.Text = WinFormStrings.str_lblCodeForum;
			CopyToClipboardBtn.Text = WinFormStrings.str_copyClipboard;

            if(node == null)
            {
                TreeNode parentnode = new TreeNode();
                parentnode.Nodes.Add(new TreeNode("Direktvergleich", new TreeNode[2] { new TreeNode(coding1), new TreeNode(coding2) }));
                node = parentnode.Nodes[0].Nodes[0];
				label1.Text = WinFormStrings.str_coding1;
				label2.Text = WinFormStrings.str_coding2;
            }

            lastposition = 140;
            int lastchars = 0;
            string codierung1bbcode;
            string codierung2bbcode;
            for(int i = 0; i < Convert.ToInt64(node.Text.Length) / 2; i++)
            {
                TextBox tb1 = new TextBox();

                tb1.Location = new System.Drawing.Point(lastposition, 12);
                tb1.Name = "textBoxScan1_" + i;
                tb1.BackColor = Color.Black;
                tb1.Font = new Font(tb1.Font, FontStyle.Bold);
                tb1.ForeColor = Color.LightGreen;
                tb1.Size = new System.Drawing.Size(22, 20);
                tb1.TabIndex = 0;
                tb1.Click += new EventHandler(tbClick);
                tb1.GotFocus += new EventHandler(tbClick);
                tb1.LostFocus += new EventHandler(tbUnFocus);
                tb1.ReadOnly = true;
                tb1.Text = node.Text.Substring(lastchars, 2);
                
                this.Controls.Add(tb1);

                TextBox tb2 = new TextBox();

                tb2.Location = new System.Drawing.Point(lastposition, 45);
                tb2.Name = "textBoxScan2_" + i;
                tb2.BackColor = Color.Black;
                tb2.ReadOnly = true;
                tb2.Click += new EventHandler(tbClick);
                tb2.GotFocus += new EventHandler(tbClick);
                tb2.LostFocus += new EventHandler(tbUnFocus);
                tb2.ForeColor = Color.LightGreen;
                tb2.Font = new Font(tb2.Font, FontStyle.Bold);
                tb2.Size = new System.Drawing.Size(22, 20);
                tb2.TabIndex = 0;
                tb2.Text = node.NextNode.Text.Substring(lastchars, 2);

				if(tb1.Text != tb2.Text)
				{
					tb1.BackColor = Color.Red;
					tb2.BackColor = Color.Red;
				}

                codierung1bbcode += (tb1.Text != tb2.Text) ? "[color=red]" + tb1.Text + "[/color]" : tb1.Text;
                codierung2bbcode += (tb1.Text != tb2.Text) ? "[color=red]" + tb2.Text + "[/color]" : tb2.Text;

                if(tb1.Text != tb2.Text)
                {
                    string currentbyte = Convert.ToString(Convert.ToByte(tb1.Text, 16), 2).PadLeft(8, '0');
                    string currentbyte2 = Convert.ToString(Convert.ToByte(tb2.Text, 16), 2).PadLeft(8, '0');

                    for (int j = 7; j >= 0; j--)
                    {
                        if(currentbyte[j] != currentbyte2[j])
                        {
                            differentBits.Add("Byte " + i + ", Bit " + (7-j));
                        }

                    }
                }

                this.Controls.Add(tb2);

                lastchars += 2;
                lastposition += 25;


            }

            //tbClick(this.Controls.Find("textBoxScan1_0", false)[0], new EventArgs());
            //this.Controls.Find("textBoxScan2_0", false)[0].Select();
            TextBox tb = (TextBox)this.Controls.Find("textBoxScan1_0", false)[0];
            tb.SelectionStart = 0;

            richTextBox1.Text = "[quote]" + Environment.NewLine + "[i][u]" + parentTitle + "[/i][/u]" + Environment.NewLine + Environment.NewLine
                     + WinFormStrings.str_coding1 + ": " + codierung1bbcode + Environment.NewLine + WinFormStrings.str_coding2 + ": " + codierung2bbcode + Environment.NewLine + Environment.NewLine
                     + "[size=3]" + string.Join(Environment.NewLine, differentBits) + "[/size]" + Environment.NewLine
                     + "[/quote]";

            

        }

        private void tbUnFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.ForeColor = Color.LightGreen;
            TextBox tb2;
            if (tb.Name.Contains("textBoxScan1"))
            {
                tb2 = (TextBox)this.Controls.Find("textBoxScan2_" + tb.Name.Substring(tb.Name.LastIndexOf('_') + 1), false)[0];
                tb2.ForeColor = Color.LightGreen;
                
            }
            else
            {
                tb2 = (TextBox)this.Controls.Find("textBoxScan1_" + tb.Name.Substring(tb.Name.LastIndexOf('_') + 1), false)[0];
                tb2.ForeColor = Color.LightGreen;
            }

        }

        private void tbClick(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            TextBox tb2;
            this.MaximumSize = new Size(lastposition + 180, 333);
            this.MinimumSize = new Size(870, 333);
            tb.ForeColor = Color.Yellow;
            if(tb.Text != "")
            {
                label13.Text = "Byte " + tb.Name.Substring(tb.Name.LastIndexOf('_') + 1);
                string bits1;
                string bits2;
                if (tb.Name.Contains("textBoxScan1"))
                {
                    tb2 = (TextBox)this.Controls.Find("textBoxScan2_" + tb.Name.Substring(tb.Name.LastIndexOf('_')+1), false)[0];
                    tb2.ForeColor = Color.Yellow;
                    bits1 = Convert.ToString(Convert.ToByte(tb.Text, 16), 2).PadLeft(8, '0');
                    bits2 = Convert.ToString(Convert.ToByte(tb2.Text, 16), 2).PadLeft(8, '0');
                }
                else
                {
                    tb2 = (TextBox)this.Controls.Find("textBoxScan1_" + tb.Name.Substring(tb.Name.LastIndexOf('_')+1), false)[0];
                    tb2.ForeColor = Color.Yellow;
                    bits1 = Convert.ToString(Convert.ToByte(tb2.Text, 16), 2).PadLeft(8, '0');
                    bits2 = Convert.ToString(Convert.ToByte(tb.Text, 16), 2).PadLeft(8, '0');
                }

                

                for (int i = 0; i < 8; i++)
                {
                    CheckBox checkbox1 = (CheckBox)this.Controls.Find("scan1bit" + (7 - i).ToString(), false)[0];
                    CheckBox checkbox2 = (CheckBox)this.Controls.Find("scan2bit" + (7 - i).ToString(), false)[0];
                    if (bits1.Substring(i, 1) == "1")
                    {
                        checkbox1.Checked = true;
                    }
                    else
                    {
                        checkbox1.Checked = false;
                    }

                    if (bits2.Substring(i, 1) == "1")
                    {
                        checkbox2.Checked = true;
                    }
                    else
                    {
                        checkbox2.Checked = false;
                    }
                }
            }

        }

		private void CopyToClipboardBtn_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(richTextBox1.Text);
		}

        private void CodingCompare_Load(object sender, EventArgs e)
        {

        }
    }
}
