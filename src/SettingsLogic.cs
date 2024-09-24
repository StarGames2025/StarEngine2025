using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;

namespace StarEngine2025
{
    public static class SettingsLogic
    {
        private static readonly string settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarEngineSettings.txt");
        private static readonly string stylesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UI", "Styles");
        private static readonly string fontsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "source", "Fonts");

        public static void OpenSettings(IWin32Window owner)
        {
            using (var settingsForm = new Form())
            {
                settingsForm.Text = "Einstellungen";
                settingsForm.Size = new System.Drawing.Size(300, 300);
                settingsForm.StartPosition = FormStartPosition.CenterParent;

                // Farbschema Auswahl
                Label labelTheme = new Label { Text = "Farbschema:", Left = 10, Top = 20 };
                ComboBox comboBoxTheme = new ComboBox { Left = 120, Top = 20, Width = 150 };
                comboBoxTheme.Items.AddRange(new[] { "Hell", "Dunkel" });
                comboBoxTheme.SelectedIndex = 0; // Standardmäßig auf "Hell"

                // Schriftart Auswahl
                Label labelFont = new Label { Text = "Schriftart:", Left = 10, Top = 60 };
                ComboBox comboBoxFont = new ComboBox { Left = 120, Top = 60, Width = 150 };
                LoadFonts(comboBoxFont); // Schriftarten laden

                Button saveButton = new Button { Text = "Speichern", Left = 120, Top = 100 };
                saveButton.Click += (s, ev) =>
                {
                    string theme = comboBoxTheme.SelectedItem.ToString();
                    string fontFamily = comboBoxFont.SelectedItem.ToString();
                    File.WriteAllLines(settingsFilePath, new[] { theme, fontFamily });
                    MessageBox.Show("Einstellungen gespeichert.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    settingsForm.Close();
                };

                settingsForm.Controls.Add(labelTheme);
                settingsForm.Controls.Add(comboBoxTheme);
                settingsForm.Controls.Add(labelFont);
                settingsForm.Controls.Add(comboBoxFont);
                settingsForm.Controls.Add(saveButton);

                LoadSettings(comboBoxTheme, comboBoxFont); // Einstellungen laden

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

        public static void ApplyStyle(string theme, Control control)
        {
            string styleFilePath = Path.Combine(stylesDirectory, $"{theme}Mode.txt");
            if (File.Exists(styleFilePath))
            {
                var lines = File.ReadAllLines(styleFilePath);
                Color backgroundColor = Color.White;
                Color textColor = Color.Black;
                Font font = SystemFonts.DefaultFont;

                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length != 2) continue;

                    switch (parts[0].Trim())
                    {
                        case "BackgroundColor":
                            var bgColorValues = parts[1].Trim().Split(',');
                            backgroundColor = Color.FromArgb(int.Parse(bgColorValues[0]), int.Parse(bgColorValues[1]), int.Parse(bgColorValues[2]));
                            break;
                        case "TextColor":
                            var textColorValues = parts[1].Trim().Split(',');
                            textColor = Color.FromArgb(int.Parse(textColorValues[0]), int.Parse(textColorValues[1]), int.Parse(textColorValues[2]));
                            break;
                        case "FontFamily":
                            font = new Font(parts[1].Trim(), font.Size, font.Style);
                            break;
                        case "FontSize":
                            font = new Font(font.FontFamily, float.Parse(parts[1].Trim()), font.Style);
                            break;
                        case "FontStyle":
                            font = new Font(font.FontFamily, font.Size, (FontStyle)Enum.Parse(typeof(FontStyle), parts[1].Trim()));
                            break;
                    }
                }

                control.BackColor = backgroundColor;
                control.ForeColor = textColor;
                control.Font = font;
            }
        }
    }
}
