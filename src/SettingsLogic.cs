using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace StarEngine2025
{
    public static class SettingsLogic
    {
        // Pfad für die Einstellungen
        private static readonly string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UI", "settings.json");
        private static readonly string stylesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UI", "Styles");
        private static readonly string fontsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "source", "Fonts");

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
                var settings = File.ReadAllLines(settingsFilePath);
                if (settings.Length > 0)
                {
                    comboBoxTheme.SelectedItem = settings[0];
                    if (settings.Length > 1)
                    {
                        comboBoxFont.SelectedItem = settings[1];
                    }
                }
                else
                {
                    MessageBox.Show("Einstellungen sind leer.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Einstellungsdatei nicht gefunden. Standardwerte werden verwendet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static void LoadFonts(ComboBox comboBox)
        {
            if (Directory.Exists(fontsDirectory))
            {
                foreach (var fontFile in Directory.GetFiles(fontsDirectory, "*.ttf"))
                {
                    comboBox.Items.Add(Path.GetFileNameWithoutExtension(fontFile));
                }
            }
        }

        private static void SaveSettings(string theme, string fontFamily)
        {
            // Speichern der Einstellungen im einfachen Textformat
            var settings = new string[]
            {
                theme,
                fontFamily
            };
            File.WriteAllLines(settingsFilePath, settings);
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
                }
            }
        }

        private static ThemeSettings ParseThemeSettings(string jsonData)
        {
            // Manuelle Analyse der JSON-Daten
            string[] parts = jsonData.Replace("{", "").Replace("}", "").Replace("\"", "").Split(',');
            var themeSettings = new ThemeSettings();
            foreach (var part in parts)
            {
                var keyValue = part.Split(':');
                if (keyValue.Length == 2)
                {
                    switch (keyValue[0])
                    {
                        case "BackgroundColor":
                            themeSettings.BackgroundColor = Array.ConvertAll(keyValue[1].Split('|'), int.Parse);
                            break;
                        case "TextColor":
                            themeSettings.TextColor = Array.ConvertAll(keyValue[1].Split('|'), int.Parse);
                            break;
                        case "FontFamily":
                            themeSettings.FontFamily = keyValue[1];
                            break;
                        case "FontSize":
                            themeSettings.FontSize = float.Parse(keyValue[1]);
                            break;
                        case "FontStyle":
                            themeSettings.FontStyle = keyValue[1];
                            break;
                    }
                }
            }
            return themeSettings;
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
