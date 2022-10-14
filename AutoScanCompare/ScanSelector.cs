using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoScanCompare
{
	public partial class ScanSelector : Form
	{

		public string selectedScan;
		private int hoverIndex = -1;


		public ScanSelector()
		{
			InitializeComponent();

			this.BackColor = ColorTranslator.FromHtml("#CDE1E2");
		}

		public ScanSelector(List<string> autoscans, string basePath)
		{
			InitializeComponent();

			this.BackColor = ColorTranslator.FromHtml("#CDE1E2");
			this.Text = autoscans.Count + " Auto-Scans in " + basePath;
			
			foreach (string scan in autoscans)
			{
				listBox1.Items.Add(scan);
			}
		}

		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem != null)
			{
				//MessageBox.Show(listBox1.SelectedItem.ToString());
				selectedScan = listBox1.SelectedItem.ToString();
				this.Close();
			}
		}

		private void listBox1_MouseMove(object sender, MouseEventArgs e)
		{
			int newHoverIndex = listBox1.IndexFromPoint(e.Location);

			if (hoverIndex != newHoverIndex)
			{
				hoverIndex = newHoverIndex;
				if (hoverIndex > -1)
				{
					toolTip1.Active = false;
					string strListBoxWert = listBox1.Items[hoverIndex].ToString();
					// Dann etwas tun. Hier wird einfach dieser Wert dem Tooltip hinzugefügt.
					toolTip1.SetToolTip((Control)sender, strListBoxWert);
					toolTip1.Active = true;
				}
			}

		}
	}
}
