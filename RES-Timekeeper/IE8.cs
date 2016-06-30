using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace RES_Timekeeper
{
    public class IE8
    {
        #region API CALLS

        [DllImport("user32.dll", EntryPoint = "GetClassNameA")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        /*delegate to handle EnumChildWindows*/
        public delegate int EnumProc(IntPtr hWnd, ref IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, EnumProc lpEnumFunc, ref IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "RegisterWindowMessageA")]
        public static extern int RegisterWindowMessage(string lpString);
        [DllImport("user32.dll", EntryPoint = "SendMessageTimeoutA")]
        public static extern int SendMessageTimeout(IntPtr hwnd, int msg, int wParam, int lParam, int fuFlags, int uTimeout, out int lpdwResult);
        [DllImport("OLEACC.dll")]
        public static extern int ObjectFromLresult(int lResult, ref Guid riid, int wParam, ref IHTMLDocument2 ppvObject);
        public const int SMTO_ABORTIFHUNG = 0x2;
        public static Guid IID_IHTMLDocument2 = new Guid("332C4425-26CB-11D0-B483-00C04FD90119");
        public static Guid IID_IHTMLDocument3 = new Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B");
        public static bool IsFrenchAgresso = false;
        #endregion


        public static IHTMLDocument2 document2FromDOM()
        {
            Process[] processes = Process.GetProcessesByName("iexplore");
            foreach (Process p in processes)
            {
                IHTMLDocument2 document2 = null;
                IntPtr hWnd = p.MainWindowHandle;
                int lngMsg = 0;
                int lRes;

                EnumProc proc = new EnumProc(EnumWindows);
                EnumChildWindows(hWnd, proc, ref hWnd);
                if (!hWnd.Equals(IntPtr.Zero))
                {
                    lngMsg = RegisterWindowMessage("WM_HTML_GETOBJECT");
                    if (lngMsg != 0)
                    {
                        SendMessageTimeout(hWnd, lngMsg, 0, 0, SMTO_ABORTIFHUNG, 1000, out lRes);
                        if (!(bool)(lRes == 0))
                        {
                            int hr = ObjectFromLresult(lRes, ref IID_IHTMLDocument2, 0, ref document2);
                            if ((bool)(document2 == null))
                                MessageBox.Show("No IHTMLDocument2 Found!", "Warning");
                            else if (!string.IsNullOrEmpty(document2.title))
                            {
                                IsFrenchAgresso = document2.title.Equals(Properties.Settings.Default.AgressoTitleFR);

                                if (document2.title.Equals(Properties.Settings.Default.AgressoTitle) || IsFrenchAgresso)
                                    return document2;
                            }
                        }
                    }
                }
            }

            return null;
        }


        private static int EnumWindows(IntPtr hWnd, ref IntPtr lParam)
        {
            int retVal = 1;
            StringBuilder classname = new StringBuilder(128);
            GetClassName(hWnd, classname, classname.Capacity);
            /// check if the instance we have found is Internet Explorer_Server
            if ((bool)(string.Compare(classname.ToString(), "Internet Explorer_Server") == 0))
            {
                lParam = hWnd;
                retVal = 0;
            }
            return retVal;
        }


        /// <summary>
        /// Returns an enumeration of tuples of <workorder code, description>
        /// </summary>
        public static IEnumerable<Tuple<string, string>> GetProjectCodesAndDescriptions(IHTMLDocument3 doc3)
        {
            return GetAllProjectCodesAndDescriptions(doc3).Distinct();
        }


        /// <summary>
        /// Returns an enumeration of tuples of <workorder code, description>
        /// </summary>
        private static IEnumerable<Tuple<string, string>> GetAllProjectCodesAndDescriptions(IHTMLDocument3 doc3)
        {
            IHTMLElementCollection allTableCells = null;
            IHTMLElement codeElement = null;
            IHTMLElement titleElement = null;
            try
            {
                Regex regex = new Regex("^[0-9]+-[0-9]+-[0-9]+$");
                allTableCells = doc3.getElementsByTagName("td");
                List<Tuple<string, string>> projectCodes = new List<Tuple<string, string>>();
                for (int i = 0; i < allTableCells.length - 1; i++)
                {
                    codeElement = allTableCells.item(i);
                    if (!string.IsNullOrWhiteSpace(codeElement.innerText))
                    {
                        if (regex.Match(codeElement.innerText).Success)
                        {
                            titleElement = allTableCells.item(i + 1);
                            projectCodes.Add(new Tuple<string, string>(codeElement.innerText, titleElement.innerText));
                            Marshal.ReleaseComObject(titleElement);
                            titleElement = null;
                        }
                    }
                    Marshal.ReleaseComObject(codeElement);
                    codeElement = null;
                }

                return projectCodes;
            }
            catch
            {
                return new List<Tuple<string, string>>();
            }
            finally
            {
                if (allTableCells != null) Marshal.ReleaseComObject(allTableCells);
                if (codeElement != null) Marshal.ReleaseComObject(codeElement);
                if (titleElement != null) Marshal.ReleaseComObject(titleElement);
            }
        }

        private static System.Globalization.CultureInfo _agressoCulture;
        private static System.Globalization.CultureInfo AgressoCulture
        {
            get
            {
                if (_agressoCulture == null)
                {
                    try
                    {
                        // Allow override using the culture settings available
                        _agressoCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.AgressoCulture);
                    }
                    catch (Exception)
                    {
                        // Value in properties settings is not valid - fallback to current UI culture
                        _agressoCulture = System.Globalization.CultureInfo.CurrentUICulture;
                    }
                }
                return _agressoCulture;
            }
        }

        /// <summary>
        /// Pushes the values from the specified line in the item array to the selected line in
        /// the browser
        /// </summary>
        public static void PushToSelectedLine(int rowIndex, string[] items, IHTMLDocument3 doc3, DataGridView dgvText)
        {
            // Iterate over all of the INPUT elements to find those wanting the project hours
            IHTMLElementCollection allInputs = doc3.getElementsByTagName("input");
            for (int i = 0; i < allInputs.length; i++)
            {
                // We're looking for an INPUT whose id contains "reg_value" and not "IsDirty"
                // this is enough to get the INPUTS for the hours
                IHTMLElement e = allInputs.item(i);
                int pos = e.id.IndexOf("reg_value");
                if (pos > -1 && !e.id.Contains("IsDirty"))
                {
                    // Get the single digit index of this current INPUT - this matches to our column index
                    string id = e.id.Substring(pos + 9);
                    if (char.IsDigit(id[0]))
                    {
                        int index = int.Parse(id.Substring(0, 1)) + 1;
                        Color originalColour = dgvText.Rows[rowIndex].Cells[index].Style.BackColor;
                        dgvText.Rows[rowIndex].Cells[index].Style.BackColor = Color.Gold;
                        e.click();
                        System.Threading.Thread.Sleep(100);

                        var stringToInsert = items[index];
                        try
                        {
                            // Try to use agresso culture setting
                            decimal value = decimal.Parse(items[index], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                            stringToInsert = value.ToString(AgressoCulture.NumberFormat);
                        }
                        catch (Exception)
                        {
                            // Revert to old behaviour
                        }

                        e.innerText = stringToInsert;
                        IHTMLElement3 e3 = e as IHTMLElement3;
                        if (e3 != null)
                        {
                            e3.FireEvent("onchange");
                            Marshal.ReleaseComObject(e3);
                        }
                        dgvText.Rows[rowIndex].Cells[index].Style.BackColor = originalColour;
                    }
                }
                Marshal.ReleaseComObject(e);
            }
            Marshal.ReleaseComObject(allInputs);
        }



        /// <summary>
        /// Selects a line in the Agresso webpage using either the code and description, or just the code 
        /// if the description is empty.
        /// </summary>
        public static void SelectAgressoLine(string code, string description, IHTMLDocument3 doc3)
        {
            IHTMLElement rowDivElement = null;
            try
            {
                // Get list of Agresso workorders that match the project code
                List<Tuple<string, string, string>> possibleMatches = GetIDsForProjectCode(code, doc3);

                // Determine which of those is the correct one.
                string idOfRowToSelect = string.Empty;
                if (possibleMatches.Count == 0)
                    throw new ApplicationException("SelectAgressoLine - Failed to find any Agresso projects with a code of " + code);
                else if (possibleMatches.Count == 1)
                    idOfRowToSelect = possibleMatches[0].Item3;
                else
                {
                    idOfRowToSelect = (from m in possibleMatches
                                       where String.Equals(description, m.Item2, StringComparison.OrdinalIgnoreCase)
                                       select m.Item3).FirstOrDefault();
                }
                if (string.IsNullOrEmpty(idOfRowToSelect))
                    throw new ApplicationException("SelectAgressoLine - Failed to find any Agresso projects with a code of " + code + " and a description of " + description);

                rowDivElement = doc3.getElementById(idOfRowToSelect);
                rowDivElement.click();
                System.Threading.Thread.Sleep(1000);
            }
            finally
            {
                if (rowDivElement != null)
                    Marshal.ReleaseComObject(rowDivElement);
            }
        }


        /// <summary>
        /// Returns a list of project code, agresso description and HTML id of project code that
        /// match the given project code with the body of the document
        /// </summary>
        public static List<Tuple<string, string, string>> GetIDsForProjectCode(string code, IHTMLDocument3 doc3)
        {
            List<Tuple<string, string, string>> projects = new List<Tuple<string, string, string>>();
            IHTMLElementCollection allTableCells = null, siblings = null;
            IHTMLElement e = null, s = null, parent = null;
            try
            {
                // get a list of all TD elements and find those whose text is the project code we want.
                allTableCells = doc3.getElementsByTagName("td");
                for (int i = 0; i < allTableCells.length; i++)
                {
                    try
                    {
                        e = allTableCells.item(i);
                        if (e.innerText == code)
                        {
                            // ok, we've found a TS with the project that we want. Now we need to find the description.
                            string idCode = e.id;
                            string descText = string.Empty;
                            try
                            {
                                parent = e.parentElement;
                                siblings = parent.children;
                                bool reachedCode = false;
                                for (int j = 0; j < siblings.length; j++)
                                {
                                    try
                                    {
                                        s = siblings.item(j);
                                        if (reachedCode && s.tagName.ToLower() == "td")
                                        {
                                            descText = s.innerText;
                                            break;
                                        }
                                        if (!reachedCode && s.id == idCode)
                                            reachedCode = true;
                                    }
                                    finally
                                    {
                                        Marshal.ReleaseComObject(s);
                                    }
                                }
                            }
                            finally
                            {
                                Marshal.ReleaseComObject(siblings);
                                Marshal.ReleaseComObject(parent);
                            }

                            projects.Add(new Tuple<string, string, string>(code, descText, idCode));
                        }
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(e);
                    }
                }
            }
            finally
            {
                Marshal.ReleaseComObject(allTableCells);
            }

            return projects;
        }


        public static IHTMLDocument3 GetTimesheetDocument()
        {
            IHTMLDocument2 htmlDoc = null;
            FramesCollection frames = null;
            try
            {
                htmlDoc = IE8.document2FromDOM();
                if (htmlDoc != null)
                    frames = htmlDoc.frames;

                if (frames != null)
                {
                    var truc = frames.item(0);
                    var truc1 = truc.document;
                    var truc2 = truc1.frames;
                    var truc3 = truc2.item(0);
                    return truc3.document as IHTMLDocument3;
                }

                return null;
            }
            finally
            {
                if (frames != null)
                    Marshal.ReleaseComObject(frames);
                if (htmlDoc != null)
                    Marshal.ReleaseComObject(htmlDoc);
            }
        }
    }
}
