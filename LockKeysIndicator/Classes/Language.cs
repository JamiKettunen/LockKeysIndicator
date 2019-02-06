using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LockKeysIndicator
{
    public class Language
    {
        private Dictionary<string, string> _tokens;
        public Dictionary<string, string> Tokens
        {
            get => _tokens;
            set
            {
                try
                {
                    // Check if contains rtl, icon, locale etc. keys and try to change current values
                    if (value.ContainsKey("rtl")) { Boolean.TryParse(value["rtl"], out IsLayoutRTL); }
                    if (value.ContainsKey("language")) { LocalizedName = value["language"]; }
                    if (value.ContainsKey("language_en")) { EnglishName = value["language_en"]; }
                    if (value.ContainsKey("locale")) { Locale = value["locale"]; }

                    _tokens = value;

                    //Console.WriteLine($"=> Language.Tokens.Set(): Loaded language '{LocalizedName} ({EnglishName})'!");
                }
                catch { Console.WriteLine("=> Language.Tokens.Set(): Mandatory property missing; language not loaded!"); } // Unload() ?
            }
        }

        public bool IsLayoutRTL;
        public string LocalizedName;
        public string EnglishName;
        public string Locale;
        //public Encoding Encoding;

        public Language()
        {
            _tokens = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates a usable .lng file from a language template .txt file & returns a boolean representing the success
        /// </summary>
        /// <param name="pathToTxt"></param>
        public bool Create(string fullTxtPath, string fullSavePath = null)
        {
            bool toReturn = false;

            try
            {
                string txtContent = FileIO.ReadFromFile(fullTxtPath);                                                       // Load language TXT file contents
                if (txtContent == "#IO_ERROR") { Debug.WriteLine($"Language > Loading '{fullTxtPath}' failed!"); }
                Debug.WriteLine("Language > Loaded language TXT definitions: " + Path.GetFileNameWithoutExtension(fullTxtPath));
                this.Tokens = ParseTokens(txtContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None), false, true); // Parse tokens from the file
                Debug.WriteLine("Language > Language tokens parsed!");

                string parsedContent = String.Empty;
                foreach (KeyValuePair<string, string> entry in Tokens)
                {
                    parsedContent += (String.IsNullOrEmpty(parsedContent) ? "" : Environment.NewLine) + entry.Key + "=" + entry.Value;
                }

                // Encode in BASE64
                //string encB64 = Convert.ToBase64String(Encoding.Unicode.GetBytes(parsedContent));
                //Debug.WriteLine("Language > Parsed TXT encoded in BASE64!");

                string savePath = (!String.IsNullOrEmpty(fullSavePath) ? fullSavePath :
                    Path.GetDirectoryName(fullTxtPath) + "\\" + Path.GetFileNameWithoutExtension(fullTxtPath) + ".lng");
                Debug.WriteLine("Language > Saving create LNG file to '" + savePath + "'...");

                if (!String.IsNullOrEmpty(parsedContent)) { toReturn = FileIO.WriteToFile(savePath, parsedContent, false); }
                Debug.WriteLine("Language > Saving succeeded: " + ((toReturn) ? "Yes" : "No"));
            }
            catch { }

            return toReturn;
        }

        /// <summary>
        /// If localizeableParent != null, will localizeAfterLoad
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputIsPath"></param>
        /// <param name=""></param>
        /// <param name="localizeableParent"></param>
        /// <returns></returns>
        public bool Load(string input, bool inputIsPath = true, Control localizeableParent = null)
        {
            bool toReturn = false;

            try
            {
                string txtContent = inputIsPath ? ((File.Exists(input) && input.EndsWith(".lng") ? FileIO.ReadFromFile(input) : null)) : input;

                if (txtContent != null && txtContent != "#IO_ERROR")
                {
                    // Decode BASE64 encoded lng
                    //txtContent = Encoding.UTF8.GetString(Convert.FromBase64String(txtContent));

                    Dictionary<string, string> backup = _tokens;
                    _tokens = null;
                    this.Tokens = ParseTokens(txtContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None), true); // Parse tokens from the file
                    if (_tokens != null)
                    {
                        if (localizeableParent != null) { Localize(localizeableParent); }
                        toReturn = true;
                    }
                    else { _tokens = backup; toReturn = false; }
                }
                else { Debug.WriteLine($"Language.Load() > Loading {(inputIsPath ? "'{fullLngPath}' " : "")}failed!"); }
            }
            catch { toReturn = false; }

            return toReturn;
        }

        private Dictionary<string, string> ParseTokens(string[] lines, bool loadMode = true, bool debug = false)
        {
            Dictionary<string, string> TmpDic = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                try
                {
                    string trim = line.Trim();

                    bool valid = !String.IsNullOrEmpty(trim) &&   // Not empty / null
                                 !trim.StartsWith("#") &&         // Isn't a comment
                                 trim.Contains("=");              // Contains '='

                    if (!loadMode && valid && trim.Count(x => x == '\"') < 2) { valid = false; } // Has < 2 of '"' in save mode = invalid line

                    if (valid) // Valid line, has key/value pair
                    {
                        string key = Regex.Replace(trim.Substring(0, trim.IndexOf('=')).Trim(), "[^a-zA-Z0-9_-]+", "", RegexOptions.Compiled); // "[^a-zA-Z_-]+"
                                                                                                                                               //string key = trim.Substring(0, trim.IndexOf('=')).Trim();
                                                                                                                                               //string key = trim.Substring(0, trim.IndexOf('=')).Trim().Replace(" ", "").Replace("\"", "");

                        if (!String.IsNullOrEmpty(key)) // Key not empty
                        {
                            if (!TmpDic.ContainsKey(key))
                            {
                                string tmpVal = trim.Substring(trim.IndexOf("=") + 1).Trim();

                                //  if (!String.IsNullOrEmpty(tmpVal) && tmpVal.Count(x => x == '\"') >= 2)
                                if (!String.IsNullOrEmpty(tmpVal))
                                {
                                    if (!loadMode && tmpVal.Count(x => x == '\"') < 2) { continue; }

                                    string value = loadMode ? tmpVal : tmpVal.Substring(1, tmpVal.LastIndexOf("\"") - 1);

                                    // Possible %reference% to a key 
                                    if (!loadMode && value.Contains("%")) // Check if in-file key referenced
                                    {
                                        try
                                        {
                                            int count = value.Count(x => x == '%');

                                            if (count >= 2) // Possible key referenced inside %%
                                            {
                                                int lastIndex = -1;
                                                string tmpValue = value;
                                                for (int i = 0; i < count; i++)
                                                {
                                                    int startIndex = value.IndexOf('%', lastIndex + 1);
                                                    int endIndex = value.IndexOf('%', startIndex + 1);

                                                    if (startIndex == -1 || endIndex == -1) { break; }

                                                    string tmpKey = value.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                                                    if (!String.IsNullOrEmpty(tmpKey))
                                                    {
                                                        string maybeKey = Regex.Replace(tmpKey, "[^a-zA-Z_-]+", "", RegexOptions.Compiled);

                                                        // Reference key found! Try to replace it in the value...
                                                        if (TmpDic.ContainsKey(maybeKey))
                                                        {
                                                            tmpValue = tmpValue.Replace($"%{tmpKey}%", TmpDic[maybeKey]);
                                                        }
                                                    }

                                                    lastIndex = endIndex;
                                                }
                                                value = tmpValue;
                                            }
                                        }
                                        catch { }
                                    }

                                    // Handle special escape sequences like '\n' => Environment.NewLine, '{version}' => Application version (when loading)
                                    if (loadMode)
                                    {
                                        value = value.Replace(@"\n", Environment.NewLine);
                                        value = value.Replace(@"{version}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                                    }

                                    TmpDic.Add(key, value);
                                    Debug.WriteLineIf(debug, $"  Parser > Key added: '{key}'");
                                    //Console.WriteLine($"{key}={value}");
                                }
                            }
                            else { Debug.WriteLineIf(debug, $"  Parser > Ignoring duplicate key entry '" + key + "'"); }
                        }
                    }
                }
                catch { return null; }
            }

            return TmpDic;
        }

        public void Unload()
        {
            _tokens = null;
            IsLayoutRTL = false;
            LocalizedName = "";
            EnglishName = "";
            Locale = "";
            Debug.WriteLine("=> Language.Unload(): Done");
        }

        public void Localize(Control container, bool isParent = true)
        {
            this.LocalizeControl(container, isParent);

            if (container.Controls.Count > 0)
            {
                foreach (Control childCtrl in container.Controls)
                {
                    this.LocalizeControl(childCtrl, false);

                    if (childCtrl.Controls.Count > 0) { this.Localize(childCtrl, false); }
                }
            }
        }

        public void LocalizeControl(Control ctrl, bool setRTL)
        {
            string ctrlTag = ctrl.Tag?.ToString().Trim();
            if (!String.IsNullOrEmpty(ctrlTag))
            {
                try
                {
                    if (ctrl is CheckBox)
                    {
                        try { ctrl.Text = _tokens[(ctrlTag + "_" + (ctrl as CheckBox).CheckState.ToString().ToLower())]; }
                        catch { ctrl.Text = _tokens[ctrlTag]; }
                    }
                    else { ctrl.Text = _tokens[ctrlTag]; }

                    if (setRTL) { ctrl.RightToLeft = setRTL ? RightToLeft.Yes : RightToLeft.No; }

                    ctrlTag = null;
                }
                catch { Debug.WriteLine("=> Language.LocalizeControl(" + ctrl.Name + "): Key '" + ctrlTag + "' not found in Language Tokens!"); }
            }
        }
    }
}
