using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Windows.Forms;
using CodingCompare.Resources;

namespace AutoScanCompare
{
    static class Helper
    {
        static List<string> autoscanTexts = new List<string>();

        static Helper()
        {
			/*
            autoscanTexts.Add("Windows-basierter VAG/VAS-Emulator");
            autoscanTexts.Add("Datenstand:");
            autoscanTexts.Add("Fahrzeug-Ident.-Nr.:");
            autoscanTexts.Add("Ende-");
            autoscanTexts.Add("Fahrzeugtyp");
            autoscanTexts.Add("Kein(e) Fehlercode(s) gefunden.");
			*/

			autoscanTexts.Add(WinFormStrings.str_autoscanValidation_1);
			autoscanTexts.Add(WinFormStrings.str_autoscanValidation_2);
			autoscanTexts.Add(WinFormStrings.str_autoscanValidation_3);
			autoscanTexts.Add(WinFormStrings.str_autoscanValidation_4);
			autoscanTexts.Add(WinFormStrings.str_autoscanValidation_5);
			autoscanTexts.Add(WinFormStrings.str_autoscanValidation_6);


		}

        public static bool ValidateAutoScan(string content)
        {
            foreach (string text in autoscanTexts)
            {
                if (content.IndexOf(text) == -1) return false;
            }

            return true;
        }

        public static void RegisterContextMenu()
        {
            try
            {
                RegistryKey rk = Registry.ClassesRoot;
                RegistryKey sk1 = rk.OpenSubKey("txtfile\\shell\\Auto-Scan GUI\\", true);

                if (sk1 != null)
                {
                    rk.DeleteSubKey("txtfile\\shell\\Auto-Scan GUI\\command");
                    rk.DeleteSubKey("txtfile\\shell\\Auto-Scan GUI");
                    MessageBox.Show(WinFormStrings.str_contextMenue_Deleted, WinFormStrings.str_Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    RegistryKey autoScanKey = rk.CreateSubKey("txtfile\\shell\\Auto-Scan GUI\\command");
                    autoScanKey.SetValue("", Application.ExecutablePath + " \"%1\"");
                    MessageBox.Show(WinFormStrings.str_contextMenue_Added, WinFormStrings.str_Success, MessageBoxButtons.OK, MessageBoxIcon.Information);

				}
            }
            catch
            {
                MessageBox.Show(WinFormStrings.str_ContextMenuError);
            }
        }

        public static string StringBetween(string STR, string FirstString, string LastString)
        {
            try
            {
                string FinalString;
                int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
                int Pos2 = STR.IndexOf(LastString);
                FinalString = STR.Substring(Pos1, Pos2 - Pos1);
                return FinalString;
            }
            catch
            {
                return String.Empty;
            }
        }

        public static string GetModelljahr(string autoscan)
        {

            string fin = StringBetween(autoscan, WinFormStrings.str_autoscanValidation_3, WinFormStrings.str_autoscanValidation_licensePlate).TrimStart().TrimEnd();

            if (fin.Length != 17 && fin.Length != 11) return String.Empty;
            if (!Regex.IsMatch(fin[9].ToString(), "[A-Y0-9]")) return String.Empty;

            string Modelljahr = fin[9].ToString();

            switch (Modelljahr)
            {
                case "A": return "2010";
                case "B": return "2011";
                case "C": return "2012";
                case "D": return "2013";
                case "E": return "2014";
                case "F": return "2015";
                case "G": return "2016";
                case "H": return "2017";
                case "J": return "2018";
                case "K": return "2019";
                case "L": return "2020";

                case "M": return "2021";
                case "N": return "2022";
                case "P": return "2023";
                case "R": return "2024";
                case "S": return "2025";
                case "T": return "2026";
                case "V": return "2027";
                case "W": return "2028";
                case "X": return "2029";
                case "Y": return "2030";

                case "1": return "2001";
                case "2": return "2002";
                case "3": return "2003";
                case "4": return "2004";
                case "5": return "2005";
                case "6": return "2006";
                case "7": return "2007";
                case "8": return "2008";
                case "9": return "2009";

                default: return String.Empty;

            }
        }
    }
}
