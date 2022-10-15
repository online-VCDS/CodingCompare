using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.Reflection;
using CodingCompare.Resources;
using System.Globalization;
using System.Collections;

namespace AutoScanCompare
{
    public partial class Form2 : Form
    {

        Dictionary<string, STG> sourceSTGs = new Dictionary<string, STG>();
        public bool AutoScanValid = false;
		public string serachCodingBasepath = "";
		public Dictionary<string, string> searchCodingValidAutoScans = new Dictionary<string, string>();
		public List<string> searchCodingMatchingCodings = new List<string>();
		public FileInfo fileInfo;
		bool dragdropValid = false;
		//public RichTextBox richTextBox1 = new RichTextBox();
		public Dictionary<string, RichTextBox> textBoxDict = new Dictionary<string, RichTextBox>();

		IniFile CodingCompareIni = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\CodingCompare.ini");

		//public Form2(string[] args)
		public Form2()
        {
            InitializeComponent();
			this.AllowDrop = true;
			tabControl1.ShowToolTips = true;
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.doDragEnter);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.doDragDrop);
			label14.Text = "V" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " by VCDS.de";

			labelSearchCoding.Text = WinFormStrings.str_searchCodingTitle;
			labelSearchCodingUnit.Text = WinFormStrings.str_searchCodingAdresse;
			labelSearchCodingFolderPath.Text = WinFormStrings.str_searchCodingBasePath;
			checkBoxSearchCodingBitSet.Text = WinFormStrings.str_searchCodingBitSet;
			buttonSearchCoding.Text = WinFormStrings.str_searchCodingButtonSearch;

			labelKeineTreffer.Text = WinFormStrings.str_searchCodingNoHits;
			labelKeineTreffer.Visible = false;

			serachCodingBasepath = CodingCompareIni.Read("LastDir", "CodingCompare");

		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string[] args = Environment.GetCommandLineArgs();
			LoadNewFile(args);
		}

		public void LoadNewFile(string[] args)
		{
			if (args[1].LastIndexOf(".txt") == -1)
			{
				MessageBox.Show(WinFormStrings.str_errorNoTextFile, WinFormStrings.str_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
			else
			{

				fileInfo = new FileInfo(args[1]);
				//label1.Text = fileInfo.Name;
				string name = fileInfo.Name;

				int tapToUse = 0;
				if (tabControl1.TabCount == 1 && textBoxDict.Count == 0)
				{
					tapToUse = 0;
					tabControl1.TabPages[0].Text = fileInfo.Name;
					tabControl1.TabPages[0].Name = fileInfo.Name;
				}
				else if(textBoxDict.ContainsKey(name))
				{
					tabControl1.SelectedTab = tabControl1.TabPages[name];
					return;
				}
				else
				{
					tabControl1.TabPages.Add(fileInfo.Name, fileInfo.Name);
					tapToUse = tabControl1.TabCount - 1;
					
					//MessageBox.Show(tabControl1.TabCount.ToString());
				}

				textBoxDict.Add(name, new RichTextBox());
				//textBoxDict[name].Anchor = AnchorStyles.Right & AnchorStyles.Left;
				textBoxDict[name].Dock = DockStyle.Fill;
				textBoxDict[name].Margin = new Padding(0, 0, 0, 0);
				//textBoxDict[name].Size = tabControl1.Size;

				textBoxDict[name].Size = tabControl1.TabPages[tapToUse].Size;
				tabControl1.TabPages[tapToUse].Controls.Add(textBoxDict[name]);
				tabControl1.TabPages[tapToUse].ToolTipText = WinFormStrings.str_rightClickToClose;
				
				textBoxDict[name].BackColor = Color.White;
				textBoxDict[name].ReadOnly = true;
				//richTextBox1.Size = tabControl1.TabPages[0].Size;
				//tabControl1.Controls["tabPage1"].Controls.Add(richTextBox1);
				//richTextBox1.BackColor = Color.White;
				//richTextBox1.ReadOnly = true;

				//tabControl1.TabPages[0].Text = fileInfo.Name;
				this.BackColor = ColorTranslator.FromHtml("#CDE1E2");

				//richTextBox1.Text = System.IO.File.ReadAllText(args[1], Encoding.Default);
				textBoxDict[name].Text = System.IO.File.ReadAllText(args[1], Encoding.Default);
				//richTextBox1_TextChanged(new object(), new EventArgs());
				richTextBox1_TextChanged(textBoxDict[name], new EventArgs());
				tabControl1.SelectedTab = tabControl1.TabPages[tapToUse];
				if (AutoScanValid && tapToUse == 0)
					AnalyseScan(name);
			}
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
			RichTextBox rtb = sender as RichTextBox;
            if (rtb.Text == "")
            {
				rtb.BackColor = Color.White;
            }
            else
            {
                if (Helper.ValidateAutoScan(rtb.Text))
                {
                    //richTextBox1.BackColor = Color.LightGreen;
                    AutoScanValid = true;
                }
                else
                {
					rtb.BackColor = Color.OrangeRed;
                }
            }

        }

        public void AnalyseScan(string name)
        {
            initializeSourceScan(name);
			treeView1.Nodes.Clear();		
            foreach (KeyValuePair<string, STG> kvp in sourceSTGs)
            {
                if(!String.IsNullOrEmpty(kvp.Value.AdpPfad))
                { 
                    treeView1.Nodes.Add(new TreeNode(kvp.Key + ": " + kvp.Value.TeilenummerOriginal, new TreeNode[2] { new TreeNode(kvp.Value.Codierung), new TreeNode(kvp.Value.AdpPfad) }));
                }
                else
                {
                    treeView1.Nodes.Add(new TreeNode(kvp.Key + ": " + kvp.Value.TeilenummerOriginal, new TreeNode[1] { new TreeNode(kvp.Value.Codierung) }));
                }
            }
        }

        void ScrollToLine(string searchString)
        {
            textBoxDict[tabControl1.SelectedTab.Text].SelectionStart = textBoxDict[tabControl1.SelectedTab.Text].Find(searchString);
			textBoxDict[tabControl1.SelectedTab.Text].ScrollToCaret();
        }

		public string GetCodingForAddress(string autoScanText, string adresse)
		{
			string adresseString = WinFormStrings.str_Adresse + " " + adresse;
			List<string> adressen = Regex.Matches(autoScanText, WinFormStrings.str_autoscanValidation_Regex, RegexOptions.Multiline).Cast<Match>().Select(match => match.Value).ToList();

			if (!adressen.Contains(adresseString)) return "";

			string codingForAddress = "";

			using (StringReader reader = new StringReader(autoScanText))
			{
				string line = string.Empty;
				string currentAddress = string.Empty;
				bool unitReached = false;
				do
				{
					bool subsystemReached = false;
					line = reader.ReadLine();
					

					if (line.StartsWith(adresseString))
					{
						currentAddress = adresse;
						subsystemReached = false;
						unitReached = true;
					}

					if(!line.StartsWith(adresseString) && adressen.Any(x =>  line.StartsWith(x)) && unitReached)
					{
						return ""; // STG gefunden, aber schon beim nächsten angelangt, ergo keine Codierung
					}

					if (line.Trim().StartsWith("Subsystem") && unitReached)
					{
						return ""; //STG gefunden, hat aber keine Codierung
					}

					if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Codierung) && !subsystemReached && unitReached)
					{
						codingForAddress = line.Trim().Substring(line.LastIndexOf(": ") - 1);
						break;
					}

				} while (line != null);
			}

			return codingForAddress;

		}

        public void initializeSourceScan(string name)
        {

			sourceSTGs.Clear();

            List<string> adressen = Regex.Matches(textBoxDict[name].Text, WinFormStrings.str_autoscanValidation_Regex, RegexOptions.Multiline).Cast<Match>().Select(match => match.Value).ToList();
            using (StringReader reader = new StringReader(textBoxDict[name].Text))
            {
                string line = string.Empty;
                string currentAddress = string.Empty;
                string adpPath = string.Empty;
                do
                {
                    bool subsystemReached = false;
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        foreach (string adresse in adressen)
                        {
                            if (line.StartsWith(adresse) && !sourceSTGs.ContainsKey(adresse))
                            {
                                currentAddress = adresse;
                                sourceSTGs.Add(currentAddress, new STG());
                                subsystemReached = false;
                            }
                        }

                        if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_TeilenummerSW) || line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Teilenummer))
                        {
                            sourceSTGs[currentAddress].TeilenummerOriginal = Helper.StringBetween(line, WinFormStrings.str_autoscanValidation_TeilenummerSW, "HW").TrimEnd();
                            //adpPath = fileInfo.DirectoryName + "\\adpmap-" + currentAddress.Substring(8) + "-" + sourceSTGs[currentAddress].TeilenummerOriginal.Replace(' ', '-') + ".csv";
                            //string[] adpPaths = Directory.GetFiles(fileInfo.DirectoryName, "adpmap-" + currentAddress.Substring(8) + " - " + sourceSTGs[currentAddress].TeilenummerOriginal.Replace(' ', '-') + "_?([A-Z0-9]+)?\\.CSV");
                            string[] adpPaths = Directory.GetFiles(fileInfo.DirectoryName + "\\", "adpmap-" + currentAddress.Substring(8) + "-" + sourceSTGs[currentAddress].TeilenummerOriginal.Replace(' ', '-') + "*.CSV", SearchOption.TopDirectoryOnly);
                            if (adpPaths != null && adpPaths.Count() != 0 && File.Exists(adpPaths[0]))
                            sourceSTGs[currentAddress].AdpPfad = adpPaths[0];

                            if (String.IsNullOrEmpty(sourceSTGs[currentAddress].TeilenummerOriginal))
                            {
                                sourceSTGs[currentAddress].TeilenummerOriginal = line.Substring(line.LastIndexOf(":") + 1).TrimEnd().TrimStart();
                                
                            }

                            /*if (!checkBox1.Checked)
                            {
                                sourceSTGs[currentAddress].Teilenummer = sourceSTGs[currentAddress].TeilenummerOriginal;
                                //sourceSTGs[currentAddress].Teilenummer = StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd();
                            }
                            else
                            {
                                sourceSTGs[currentAddress].Teilenummer = sourceSTGs[currentAddress].TeilenummerOriginal.Substring(0, 10);
                                //sourceSTGs[currentAddress].Teilenummer = StringBetween(line, "Teilenummer SW: ", "HW").TrimEnd().Substring(0, 10);
                            }

                            if (checkBox2.Checked)
                            {
                                sourceSTGs[currentAddress].Teilenummer = sourceSTGs[currentAddress].Teilenummer.Substring(4);
                            }*/

                        }

                        if (line.Trim().StartsWith("Subsystem") && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung))
                        {
                            subsystemReached = true;
							sourceSTGs[currentAddress].Codierung = WinFormStrings.str_notSupported;
                        }

                        if (line.Trim().StartsWith(WinFormStrings.str_autoscanValidation_Codierung) && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung) && !subsystemReached)
                        {
                            sourceSTGs[currentAddress].Codierung = line.Trim().Substring(line.LastIndexOf(": ") - 1);
                        }

                        if ((line.StartsWith("----------------") || line.StartsWith(WinFormStrings.str_autoscanValidation_End)) && sourceSTGs.ContainsKey(currentAddress) && String.IsNullOrEmpty(sourceSTGs[currentAddress].Codierung))
                        {
                            sourceSTGs.Remove(currentAddress);
                        }

                    }

                } while (line != null);
            }
        }

		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Parent == null)
            { 
                ScrollToLine(e.Node.Text.Substring(0, 10));
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

		private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                if (Regex.IsMatch(e.Node.Text, "[0-9A-Z]+") && !e.Node.Text.Contains(fileInfo.DirectoryName))
                {
					try
					{ 
						var MyIni = new IniFile(WinFormStrings.str_VCDSpath + "LCode.ini");
						MyIni.Write("Last-dir", fileInfo.DirectoryName, "Software");
						Process.Start(WinFormStrings.str_VCDSpath + "LCode.exe", e.Node.Text);
					}
					catch
					{
						MessageBox.Show(WinFormStrings.str_LCodeError, WinFormStrings.str_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

				}
                else if(e.Node.Text.Contains(fileInfo.DirectoryName))
                {
                    Process.Start(e.Node.Text);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			
			//AnalyseScan(tabControl1.SelectedTab.Name);
		}

		private void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			//MessageBox.Show(tabControl1.SelectedTab.Text);
			AnalyseScan(tabControl1.SelectedTab.Text);
		}

		private void doDragEnter(object sender, DragEventArgs e)
		{
			string filename;
			dragdropValid = GetFilename(out filename, e);

			if (!dragdropValid) e.Effect = DragDropEffects.None;
			else e.Effect = DragDropEffects.Move;

		}

		private void doDragDrop(object sender, DragEventArgs e)
		{
			if (dragdropValid)
			{
				string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
				LoadNewFile(new string[] { "", filename[0] });
				//richTextBox1.Text = File.ReadAllText(filename[0], Encoding.Default);
			}
		}

		private void tabControl1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (tabControl1.TabCount == 1) Application.Exit();
				else
				{
					for (int i = 0; i < tabControl1.TabCount; i++)
					{
						if (tabControl1.GetTabRect(i).Contains(e.Location))
						{

							tabControl1.TabPages[i].Dispose();
						}
					}
				}
			}

		}

		private void SearchCodingBasePathBtn_Click(object sender, EventArgs e)
		{

			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					serachCodingBasepath = fbd.SelectedPath;
					CodingCompareIni.Write("LastDir", fbd.SelectedPath, "CodingCompare");
						
				}
			}
			

			if (!string.IsNullOrWhiteSpace(serachCodingBasepath))
			{
				searchCodingValidAutoScans.Clear();
				fillSearchCodingValidAutoScans(serachCodingBasepath);

				if (searchCodingValidAutoScans.Count == 0) labelKeineTreffer.Visible = true;
			}

		}

		private void fillSearchCodingValidAutoScans(string path)
		{
			string[] files = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);

			string fileContent = "";

			foreach (string file in files)
			{
				fileContent = File.ReadAllText(file);
				if (Helper.ValidateAutoScan(fileContent))
				{
					searchCodingValidAutoScans.Add(file, fileContent);
				}
			}
		}

		private void buttonSearchCoding_Click(object sender, EventArgs e)
		{

			labelKeineTreffer.Visible = false;
			searchCodingMatchingCodings.Clear();

			serachCodingBasepath = CodingCompareIni.Read("LastDir", "CodingCompare");

			if (String.IsNullOrEmpty(serachCodingBasepath))
				SearchCodingBasePathBtn_Click(null, new EventArgs());

			if (!String.IsNullOrEmpty(serachCodingBasepath) && Regex.IsMatch(textBoxSearchAddress.Text, "[0-9A-F]{1,2}") && Regex.IsMatch(textBoxSearchByte.Text, "[0-9]") && Regex.IsMatch(textBoxSearchBit.Text, "[0-9]{1}"))
			{
				if (textBoxSearchAddress.Text.Length == 1)
					textBoxSearchAddress.Text = "0" + textBoxSearchAddress.Text;


				if (searchCodingValidAutoScans.Count == 0) fillSearchCodingValidAutoScans(serachCodingBasepath);

				foreach(KeyValuePair<string,string> kvp in searchCodingValidAutoScans)
				{
					//get coding for address
					try
					{
						string codingForSearchedUnit = GetCodingForAddress(kvp.Value, textBoxSearchAddress.Text);

						if(!String.IsNullOrEmpty(codingForSearchedUnit))
						{
						
								BitArray codingBitArray = ConvertHexToBitArray(codingForSearchedUnit);

								if (codingBitArray.Count >= ((Convert.ToInt32(textBoxSearchByte.Text) + 1) * 8)) // Codierug hat genug Bytes für Byte-Angabe??
								{
									int indexToSearch = (((Convert.ToInt32(textBoxSearchByte.Text) + 1) * 8) - Convert.ToInt32(textBoxSearchBit.Text)) - 1;
									if ((checkBoxSearchCodingBitSet.Checked && codingBitArray[indexToSearch]) || (!checkBoxSearchCodingBitSet.Checked && !codingBitArray[indexToSearch]))
									{
										searchCodingMatchingCodings.Add(kvp.Key);
									}
								}
							}
						
						}
					catch { }
				}
			}

			if(searchCodingMatchingCodings.Count > 0)
			{
				ScanSelector scanSelector = new ScanSelector(searchCodingMatchingCodings, serachCodingBasepath);
				scanSelector.StartPosition = FormStartPosition.CenterParent;
				DialogResult result = scanSelector.ShowDialog();

				if(!String.IsNullOrEmpty(scanSelector.selectedScan))
					LoadNewFile(new string[] { "1", scanSelector.selectedScan });
			}
			else
			{
				labelKeineTreffer.Visible = true;
			}
			//MessageBox.Show(String.Join("\r\n", searchCodingMatchingCodings.ToArray()));


		}


		public static BitArray ConvertHexToBitArray(string hexData)
		{
			if (hexData == null)
				return null; // or do something else, throw, ...

			BitArray ba = new BitArray(4 * hexData.Length);
			for (int i = 0; i < hexData.Length; i++)
			{
				byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
				for (int j = 0; j < 4; j++)
				{
					ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
				}
			}
			return ba;
		}

		private void buttonSearchCodingBasePath_MouseHover(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(SearchCodingBasePathBtn, serachCodingBasepath);
		}

		private void buttonSearchCoding_MouseHover(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(buttonSearchCoding, serachCodingBasepath);
		}
	}
}
