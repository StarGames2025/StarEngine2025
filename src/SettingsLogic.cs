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
        private static readonly string settingsFilePath = PathHelper.RelativePathMaker("../UI/settings.json");
        private static readonly string themeDirectory = PathHelper.RelativePathMaker("../UI/Themes");
        private static readonly string fontsDirectory = PathHelper.RelativePathMaker("../source/Fonts");

        public static void ApplyStyle(Settings settingsConfig)
        {
            string themeName = settingsConfig.ThemeName;
            ThemeLoader(PathHelper.RelativePathMaker(themeDirectory + "/" + themeName + ".json"));

            //switch (themeName)
            //{
            //
            //}
        }
        
        private static void ThemeLoader(string themeFilePath)
        {
            //var json = File.ReadAllText(themeFilePath);
            //var theme = JsonConvert.DeserializeObject<Theme>(json);
            //return theme;
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

            ApplyStyle(MainForm.config);
        }

        public static void OpenSettings(MainForm mainForm, IWin32Window owner)
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

                Button saveButton = new Button { Text = "Speichern", Left = 110, Top = 100 };
                saveButton.Click += (s, ev) =>
                {
                    if (comboBoxTheme.SelectedItem != null && comboBoxFont.SelectedItem != null)
                    {
                        string theme = comboBoxTheme.SelectedItem.ToString();
                        string fontFamily = comboBoxFont.SelectedItem.ToString();
                        SaveSettings(theme, fontFamily);
                        ApplyStyle(MainForm.config);
                        MessageBox.Show("Einstellungen gespeichert.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        settingsForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bitte wähle ein Farbschema und eine Schriftart aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                Button applyButton = new Button { Text = "Apply", Left = 130, Top = 100 };
                applyButton.Click += (s, ev) => 
                {
                    if (comboBoxTheme.SelectedItem != null && comboBoxFont.SelectedItem != null)
                    {
                        string theme = comboBoxTheme.SelectedItem.ToString();
                        string fontFamily = comboBoxFont.SelectedItem.ToString();
                        SaveSettings(theme, fontFamily);
                        ApplyStyle(MainForm.config);
                        MessageBox.Show("Einstellungen Angewandt.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                settingsForm.Controls.Add(applyButton);

                settingsForm.ShowDialog(owner);
            }
        }

        private static void LoadFonts(ComboBox comboBoxFont)
        {
            string[] fonts =  Directory.GetFiles(fontsDirectory);
            if (fonts != null)
            {
                foreach (var font in fonts)
                {
                    comboBoxFont.Items.Add(font);
                }
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
    }
}
