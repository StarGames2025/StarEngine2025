using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StarEngine2025
{
    public static class SettingsLogic
    {
        private static readonly string settingsFilePath = relativePathmaker("../UI/settings.json");
        private static readonly string stylesDirectory = relativePathmaker("../UI/Styles");
        private static readonly string fontsDirectory = relativePathmaker("../source/Fonts");

        public static void OpenSettings(IWin32Window owner)
        {
            using (var settingsForm = new Form())
            {
                settingsForm.Text = "Einstellungen";
                settingsForm.Size = new System.Drawing.Size(300, 300);
                settingsForm.StartPosition = FormStartPosition.CenterParent;

                Label labelTheme = new Label { Text = "Farbschema:", Left = 10, Top = 20 };
                ComboBox comboBoxTheme = new ComboBox { Left = 120, Top = 20, Width = 150 };
                comboBoxTheme.Items.AddRange(new[] { "Light", "Dark" });
                comboBoxTheme.SelectedIndex = 0;

                Label labelFont = new Label { Text = "Schriftart:", Left = 10, Top = 60 };
                ComboBox comboBoxFont = new ComboBox { Left = 120, Top = 60, Width = 150 };
                LoadFonts(comboBoxFont);

                Button saveButton = new Button { Text = "Speichern", Left = 120, Top = 100 };
                saveButton.Click += (s, ev) =>
                {
                    if (comboBoxTheme.SelectedItem != null && comboBoxFont.SelectedItem != null)
                    {
                        string theme = comboBoxTheme.SelectedItem.ToString();
                        string fontFamily = comboBoxFont.SelectedItem.ToString();
                        SaveSettings(theme, fontFamily);
                        MessageBox.Show("Einstellungen gespeichert.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        settingsForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bitte wähle ein Farbschema und eine Schriftart aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                settingsForm.Controls.Add(labelTheme);
                settingsForm.Controls.Add(comboBoxTheme);
                settingsForm.Controls.Add(labelFont);
                settingsForm.Controls.Add(comboBoxFont);
                settingsForm.Controls.Add(saveButton);

                LoadSettings(comboBoxTheme, comboBoxFont);

                settingsForm.ShowDialog(owner);
            }
        }

        private static void LoadSettings(ComboBox comboBoxTheme, ComboBox comboBoxFont)
        {
            if (File.Exists(settingsFilePath))
            {
                var jsonData = File.ReadAllText(settingsFilePath);
                
                try
                {
                    jsonData = jsonData.Replace("{", "").Replace("}", "").Replace("\"", "");
                    var settings = jsonData.Split(',');

                    var settingsDict = new Dictionary<string, string>();

                    foreach (var setting in settings)
                    {
                        var keyValue = setting.Split(':');
                        if (keyValue.Length == 2)
                        {
                            string key = keyValue[0].Trim();
                            string value = keyValue[1].Trim();
                            settingsDict[key] = value;
                        }
                    }

                    if (settingsDict.TryGetValue("theme", out var theme))
                    {
                        comboBoxTheme.SelectedItem = theme;
                    }
                    if (settingsDict.TryGetValue("fontFamily", out var fontFamily))
                    {
                        comboBoxFont.SelectedItem = fontFamily;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Laden der Einstellungen: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Einstellungsdatei nicht gefunden. Standardwerte werden verwendet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private static void SaveSettings(string theme, string fontFamily)
        {
            var settings = new List<string>
            {
                "{",
                $"  \"theme\": \"{theme}\",",
                $"  \"fontFamily\": \"{fontFamily}\"",
                "}"
            };

            try
            {
                File.WriteAllLines(settingsFilePath, settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern der Einstellungen: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ApplyStyle(string theme, Control control)
        {
            string styleFilePath = Path.Combine(stylesDirectory, $"{theme}Mode.json");
            if (File.Exists(styleFilePath))
            {
                var jsonData = File.ReadAllText(styleFilePath);
                var themeSettings = ParseThemeSettings(jsonData);

                if (themeSettings != null)
                {
                    Color backgroundColor = Color.FromArgb(themeSettings.BackgroundColor[0], themeSettings.BackgroundColor[1], themeSettings.BackgroundColor[2]);
                    Color textColor = Color.FromArgb(themeSettings.TextColor[0], themeSettings.TextColor[1], themeSettings.TextColor[2]);
                    Font font = new Font(themeSettings.FontFamily, themeSettings.FontSize, (FontStyle)Enum.Parse(typeof(FontStyle), themeSettings.FontStyle));

                    control.BackColor = backgroundColor;
                    control.ForeColor = textColor;
                    control.Font = font;

                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = backgroundColor;
                        textBox.ForeColor = textColor;
                    }
                }
            }
        }


        private static ThemeSettings ParseThemeSettings(string jsonData)
        {
            var themeSettings = new ThemeSettings();
            var lines = jsonData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.Contains("BackgroundColor"))
                {
                    themeSettings.BackgroundColor = ParseColor(line);
                }
                else if (line.Contains("TextColor"))
                {
                    themeSettings.TextColor = ParseColor(line);
                }
                else if (line.Contains("FontFamily"))
                {
                    themeSettings.FontFamily = ExtractValue(line);
                }
                else if (line.Contains("FontSize"))
                {
                    themeSettings.FontSize = float.Parse(ExtractValue(line));
                }
                else if (line.Contains("FontStyle"))
                {
                    themeSettings.FontStyle = ExtractValue(line);
                }
            }
            return themeSettings;
        }

        private static int[] ParseColor(string line)
        {
            string[] parts = ExtractValue(line).Split('|');
            return Array.ConvertAll(parts, int.Parse);
        }

        private static string ExtractValue(string line)
        {
            return line.Split(':')[1].Trim().Trim('"');
        }

        private static string relativePathmaker(string relativePath)
        {
            string combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            return Path.GetFullPath(combinedPath);
        }

        private static void LoadFonts(ComboBox comboBoxFont)
        {
            using (InstalledFontCollection col = new InstalledFontCollection())
            {
                foreach (FontFamily fa in col.Families)
                {
                    comboBoxFont.Items.Add(fa.Name);
                }
            }

            if (Directory.Exists(fontsDirectory))
            {
                string[] fontFiles = Directory.GetFiles(fontsDirectory, "*.ttf");

                foreach (var fontFile in fontFiles)
                {
                    try
                    {
                        using (PrivateFontCollection pfc = new PrivateFontCollection())
                        {
                            pfc.AddFontFile(fontFile);
                            FontFamily fontFamily = pfc.Families[0];
                            comboBoxFont.Items.Add(fontFamily.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Laden der Schriftart '{fontFile}': {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Das Schriftartenverzeichnis existiert nicht.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    public class ThemeSettings
    {
        public int[] BackgroundColor { get; set; }
        public int[] TextColor { get; set; }
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
        public string FontStyle { get; set; }
    }
}
