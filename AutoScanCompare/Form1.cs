using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Resources;
using CodingCompare.Resources;

namespace AutoScanCompare
{


    public partial class Form1 : Form
    {
        
        Dictionary<string, STG> sourceSTGs = new Dictionary<string, STG>();
        Dictionary<string, STG> destSTGs = new Dictionary<string, STG>();
        Dictionary<string, MergedSTG> mergedSTGs = new Dictionary<string, MergedSTG>();
        bool dragdropValid = false;
        bool doScroll = false;

        public Form1()
        {
            InitializeComponent();
            labelMJ1.Visible = false;
            labelMJ2.Visible = false;

			labelMJ1.Text = WinFormStrings.str_modelYear;
			labelMJ2.Text = WinFormStrings.str_modelYear;

			label14.Text = "V" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " by VCDS.de";
			
			this.Text = WinFormStrings.str_winformTitle;
			label1.Text = WinFormStrings.str_autoscan1;
			label2.Text = WinFormStrings.str_autoscan2;
			label3.Text = WinFormStrings.str_compareResults;
			checkBox3.Text = WinFormStrings.str_synchronousScrolling;
			checkBox1.Text = WinFormStrings.str_ignoreIndex;
			checkBox2.Text = WinFormStrings.str_ignoreCarDerivate;
			checkBox4.Text = WinFormStrings.str_onlyDifferences;
			label4.Text = WinFormStrings.str_CodingQuickCompare;
			label5.Text = WinFormStrings.str_coding1;
			label6.Text = WinFormStrings.str_coding2;
			button2.Text = WinFormStrings.str_btnCompare;
			button3.Text = WinFormStrings.str_contextMenue;


			this.BackColor = ColorTranslator.FromHtml("#CDE1E2");
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(richTextBox1.Text == "")
            {
                richTextBox1.BackColor = Color.White;
            }
            else
            { 
                if(Helper.ValidateAutoScan(richTextBox1.Text))
                {
                    richTextBox1.BackColor = Color.LightGreen;
                    string mj1 = Helper.GetModelljahr(richTextBox1.Text);
                    if(!String.IsNullOrEmpty(mj1))
                    {
                        labelMJ1.Text = WinFormStrings.str_modelYear + " " + mj1;
                        labelMJ1.Visible = true;
                    }
                }
                else
                {
                    richTextBox1.BackColor = Color.OrangeRed;
                    labelMJ1.Visible = false;
                }
            }

        }



        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "")
            {
                richTextBox2.BackColor = Color.White;
            }
            else
            {
                if (Helper.ValidateAutoScan(richTextBox2.Text))
                {
                    richTextBox2.BackColor = Color.LightGreen;
                    string mj2 = Helper.GetModelljahr(richTextBox2.Text);
                    if (!String.IsNullOrEmpty(mj2))
                    {
                        labelMJ2.Text = WinFormStrings.str_modelYear + " " + mj2;
                        labelMJ2.Visible = true;
                    }
                }
                else
                {
                    richTextBox2.BackColor = Color.OrangeRed;
                    labelMJ1.Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sourceSTGs.Clear();
            destSTGs.Clear();
            mergedSTGs.Clear();
            treeView1.Nodes.Clear();

            if(richTextBox1.BackColor != Color.LightGreen || richTextBox2.BackColor != Color.LightGreen || richTextBox1.Text == "" || richTextBox2.Text == "")
            {
                MessageBox.Show(WinFormStrings.str_autoscanNotValid, WinFormStrings.str_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            initializeSourceScan();
            initializeDestScan();

			foreach (KeyValuePair<string, STG> kvp in sourceSTGs)
			{

				if (checkBox4.Checked)
				{ 
					if (destSTGs.ContainsKey(kvp.Key) && destSTGs[kvp.Key].Teilenummer == kvp.Value.Teilenummer && destSTGs[kvp.Key].Codierung != kvp.Value.Codierung)
					{
						mergedSTGs.Add(kvp.Key + ": " + kvp.Value.TeilenummerOriginal + " <--> " + destSTGs[kvp.Key].TeilenummerOriginal, new MergedSTG() { Codierung1 = kvp.Value.Codierung, Codierung2 = destSTGs[kvp.Key].Codierung });
					}
				}
				else
				{
					if (destSTGs.ContainsKey(kvp.Key))
					{
						mergedSTGs.Add(kvp.Key + ": " + kvp.Value.TeilenummerOriginal + " <--> " + destSTGs[kvp.Key].TeilenummerOriginal, new MergedSTG() { Codierung1 = kvp.Value.Codierung, Codierung2 = destSTGs[kvp.Key].Codierung });
					}
				}
            }

           // MessageBox.Show(mergedSTGs.Count().ToString());

            foreach(KeyValuePair<string, MergedSTG> kvp in mergedSTGs)
            {
                treeView1.Nodes.Add(new TreeNode(kvp.Key, new TreeNode[2] { new TreeNode(kvp.Value.Codierung1), new TreeNode(kvp.Value.Codierung2) }));
            }


            if(mergedSTGs.Count == 0)
            {
                MessageBox.Show(WinFormStrings.str_noDifferencesFound, WinFormStrings.str_Hint, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }


        public void initializeSourceScan()
        {
			//List<string> adressen = Regex.Matches(richTextBox1.Text, "^Adresse [0-9A-Z][0-9A-Z](?=\\:)", RegexOptions.Multiline).Cast<Match>().Select(match => match.Value).ToList();
			List<string> adressen = Regex.Matches(richTextBox1.Text, WinFormStrings.str_autoscanValidation_Regex, RegexOptions.Multiline).Cast<Match>().Select(match => match.Value).ToList();
			using (StringReader reader = new StringReader(richTextBox1.Text))
            {
                string line = string.Empty;
                string currentAddress = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        foreach (string adresse in adressen)
                        {
                            if (line.StartsWith(adresse) && !sourceSTGs.ContainsKey(adresse))
                            {
                                currentAddress = adresse;
                                sourceSTGs.Add(currentAddress, new STG());
                            }
                        }

						//if (line.Trim().StartsWith("Teilenummer SW: ") || line.Trim().StartsWith("Teilenummer: "))
						if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_TeilenummerSW) || line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Teilenummer))
						{
							//sourceSTGs[currentAddress].TeilenummerOriginal = Helper.StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd();
							sourceSTGs[currentAddress].TeilenummerOriginal = Helper.StringBetween(line, WinFormStrings.str_autoscanValidation_TeilenummerSW, "HW").TrimEnd();
							if (String.IsNullOrEmpty(sourceSTGs[currentAddress].TeilenummerOriginal))
                            {
                                sourceSTGs[currentAddress].TeilenummerOriginal = line.Substring(line.LastIndexOf(":") + 1).TrimEnd().TrimStart();
                            }

                            if (!checkBox1.Checked)
                            {
                                sourceSTGs[currentAddress].Teilenummer = sourceSTGs[currentAddress].TeilenummerOriginal;
                                //sourceSTGs[currentAddress].Teilenummer = StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd();
                            }
                            else
                            {
                                sourceSTGs[currentAddress].Teilenummer = sourceSTGs[currentAddress].TeilenummerOriginal.Substring(0, 10);
                                //sourceSTGs[currentAddress].Teilenummer = StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd().Substring(0, 10);
                            }
                        
                            if(checkBox2.Checked)
                            {
                                sourceSTGs[currentAddress].Teilenummer = sourceSTGs[currentAddress].Teilenummer.Substring(4);
                            }

                        }

						//if (line.Trim().StartsWith("Codierung: ") && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung))
						if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Codierung) && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung))
						{
                            sourceSTGs[currentAddress].Codierung = line.Trim().Substring(line.LastIndexOf(": ") - 1);
                        }

						//if ((line.StartsWith("----------------") || line.StartsWith("Ende------------")) && sourceSTGs.ContainsKey(currentAddress) && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung))
						if ((line.StartsWith("----------------") || line.StartsWith(WinFormStrings.str_autoscanValidation_End)) && sourceSTGs.ContainsKey(currentAddress) && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung))
						{
                            sourceSTGs.Remove(currentAddress);
                        }

                    }

                } while (line != null);
            }
        }

        public void initializeDestScan()
        {
			//List<string> adressen = Regex.Matches(richTextBox2.Text, "^Adresse [0-9A-Z][0-9A-Z](?=\\:)", RegexOptions.Multiline).Cast<Match>().Select(match => match.Value).ToList();
			List<string> adressen = Regex.Matches(richTextBox2.Text, WinFormStrings.str_autoscanValidation_Regex, RegexOptions.Multiline).Cast<Match>().Select(match => match.Value).ToList();
			using (StringReader reader = new StringReader(richTextBox2.Text))
            {
                string line = string.Empty;
                string currentAddress = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        foreach (string adresse in adressen)
                        {
                            if (line.StartsWith(adresse) && !destSTGs.ContainsKey(adresse))
                            {
                                currentAddress = adresse;
                                destSTGs.Add(currentAddress, new STG());
                            }
                        }

                        //if (line.Trim().StartsWith("Teilenummer SW: ") || line.Trim().StartsWith("Teilenummer: "))
						if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_TeilenummerSW) || line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Teilenummer))
							{
                            destSTGs[currentAddress].TeilenummerOriginal = Helper.StringBetween(line, WinFormStrings.str_autoscanValidation_TeilenummerSW, "HW").TrimEnd();
							if (String.IsNullOrEmpty(destSTGs[currentAddress].TeilenummerOriginal))
                            {
                                destSTGs[currentAddress].TeilenummerOriginal = line.Substring(line.LastIndexOf(":") + 1).TrimEnd().TrimStart();
                            }
                            if (!checkBox1.Checked)
                            {
                                destSTGs[currentAddress].Teilenummer = destSTGs[currentAddress].TeilenummerOriginal;
                                //destSTGs[currentAddress].Teilenummer = StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd();
                            }
                            else
                            {
                                destSTGs[currentAddress].Teilenummer = destSTGs[currentAddress].TeilenummerOriginal.Substring(0, 10);
                                //destSTGs[currentAddress].Teilenummer = StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd().Substring(0, 10);
                            }

                            if (checkBox2.Checked)
                            {
                                destSTGs[currentAddress].Teilenummer = destSTGs[currentAddress].Teilenummer.Substring(4);
                            }

                         }

                        if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Codierung) && String.IsNullOrEmpty(destSTGs[currentAddress].Codierung))
                        {
                            destSTGs[currentAddress].Codierung = line.Trim().Substring(line.LastIndexOf(": ") - 1);
                        }

                        if ((line.StartsWith("----------------") || line.StartsWith(WinFormStrings.str_autoscanValidation_End)) && destSTGs.ContainsKey(currentAddress) && String.IsNullOrEmpty(destSTGs[currentAddress].Codierung))
                        {
                            destSTGs.Remove(currentAddress);
                        }
                    }

                } while (line != null);
            }
        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Parent != null)
            {
                if((e.Node.NextVisibleNode != null && e.Node.NextVisibleNode.Text.Contains(WinFormStrings.str_Adresse) && e.Node.Text.Length != e.Node.PrevVisibleNode.Text.Length)
                    || (e.Node.NextVisibleNode == null && !e.Node.PrevVisibleNode.Text.Contains(WinFormStrings.str_Adresse) && e.Node.Text.Length != e.Node.PrevVisibleNode.Text.Length)
                    || (e.Node.NextVisibleNode != null && !e.Node.NextVisibleNode.Text.Contains(WinFormStrings.str_Adresse) && e.Node.Text.Length != e.Node.NextVisibleNode.Text.Length))

                {
                    MessageBox.Show(WinFormStrings.str_codingsNotSameLength, WinFormStrings.str_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                if (e.Node.NextVisibleNode != null && (e.Node.NextVisibleNode.Text.Contains(WinFormStrings.str_Adresse) && e.Node.Text.Length == e.Node.PrevVisibleNode.Text.Length))
                {
                    DialogResult result = new CodingCompare(e.Node.PrevVisibleNode, e.Node.Parent.Text).ShowDialog();
                }
                else if (e.Node.NextVisibleNode == null && e.Node.Text.Length == e.Node.PrevVisibleNode.Text.Length)
                {
                    DialogResult result = new CodingCompare(e.Node.PrevVisibleNode, e.Node.Parent.Text).ShowDialog();
                }
                else if(!e.Node.NextVisibleNode.Text.Contains(WinFormStrings.str_Adresse) && e.Node.Text.Length == e.Node.NextVisibleNode.Text.Length)
                {  
                    DialogResult result = new CodingCompare(e.Node, e.Node.Parent.Text).ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Regex.IsMatch(textBox1.Text, "^[0-9a-fA-F]+$") && Regex.IsMatch(textBox2.Text, "^[0-9a-fA-F]+$") && textBox1.Text.Length == textBox2.Text.Length)
            {
                DialogResult result = new CodingCompare(null, WinFormStrings.str_lblDirectCompare, textBox1.Text.ToUpper(), textBox2.Text.ToUpper()).ShowDialog();
            }
        }

        


        private void richTextBox_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            dragdropValid = GetFilename(out filename, e);

            if(!dragdropValid) e.Effect = DragDropEffects.None;
        }

        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if(dragdropValid)
            {
                string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
                richTextBox1.Text = File.ReadAllText(filename[0], Encoding.Default);
            }
        }

        private void richTextBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (dragdropValid)
            {
                string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
                richTextBox2.Text = File.ReadAllText(filename[0], Encoding.Default);
            }
        }

        private void myRichTextBox1_VScroll(object sender, EventArgs e)
        {
            if(!doScroll && checkBox3.Checked)
            {
                doScroll = true;
                int nPos = GetScrollPos(richTextBox1.Handle, (int)ScrollBarType.SbVert);
                nPos <<= 16;
                uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
                SendMessage(richTextBox2.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
                doScroll = false;
            }
        }

        private void myRichTextBox2_VScroll(object sender, EventArgs e)
        {
            
            if(!doScroll && checkBox3.Checked)
            {
                doScroll = true;
                int nPos = GetScrollPos(richTextBox2.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(richTextBox1.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
                doScroll = false;
            }

            
        }

        protected bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileName") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if (ext == ".txt")
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Helper.RegisterContextMenu();
        }

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			int difference = this.Size.Width - 730;
			richTextBox1.Width = 324 + difference / 2;
			richTextBox2.Left = 372 + difference / 2;
			richTextBox2.Width = 324 + difference / 2;
			label2.Left = 369 + difference / 2;
			labelMJ2.Left = 606 + difference;
			labelMJ1.Left = 245 + difference / 2;

		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			if(!checkBox4.Checked)
			{
				checkBox1.Enabled = false;
				checkBox2.Enabled = false;
			}
			else
			{
				checkBox1.Enabled = true;
				checkBox2.Enabled = true;
			}
		}
	}

	public class STG
    {
        public string Teilenummer;
        public string TeilenummerOriginal;
        public string Codierung;
        public string AdpPfad;
    }

    public class MergedSTG
    {
        public string Codierung1;
        public string Codierung2;
    }

    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }

    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
        //WM_VSCROLL = 0x020a
    }

    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }



}
