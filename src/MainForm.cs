using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace StarEngine2025
{
    public class MainForm : Form
    {
        private TextBox codeTextBox;

        public MainForm()
        {
            Text = "StarEngine2025";
            Size = new System.Drawing.Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            string relativePath = "../source/icons/AppIcons/AppIcon.ico";
            string combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            string iconPath = Path.GetFullPath(combinedPath);
            //Console.WriteLine($"Icon-Pfad: {iconPath}");

            try
            {
                if (File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
                else
                {
                    MessageBox.Show("Die Icon-Datei wurde nicht gefunden: " + iconPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden des Icons: " + ex.Message);
            }



            var menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("Datei");
            fileMenu.DropDownItems.Add("Neues Projekt", null, NewProject);
            fileMenu.DropDownItems.Add("Projekt öffnen", null, OpenProject);
            fileMenu.DropDownItems.Add("Einstellungen", null, (s, e) => SettingsLogic.OpenSettings(this));
            fileMenu.DropDownItems.Add("Beenden", null, (s, e) => Application.Exit());
            menuStrip.Items.Add(fileMenu);
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            InitializeEditor();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarEngineSettings.txt")))
            {
                string[] settings = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarEngineSettings.txt"));
                if (settings.Length > 0)
                {
                    string theme = settings[0];
                    SettingsLogic.ApplyStyle(theme, this);
                    SettingsLogic.ApplyStyle(theme, codeTextBox);

                    if (settings.Length > 1)
                    {
                        string fontFamily = settings[1];
                        try
                        {
                            var font = new Font(fontFamily, 12);
                            this.Font = font;
                            codeTextBox.Font = font;
                        }
                        catch (Exception)
                        {
                            this.Font = SystemFonts.DefaultFont;
                            codeTextBox.Font = SystemFonts.DefaultFont;
                        }
                    }
                }
            }
        }

        private void InitializeEditor()
        {
            codeTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(codeTextBox);
        }

        private void NewProject(object sender, EventArgs e)
        {
            string projectName = Prompt.ShowDialog("Projektname:", "Neues Projekt");
            if (!string.IsNullOrEmpty(projectName))
            {
                ProjectLogic.CreateProject(projectName);
                codeTextBox.Text = "Neues Projekt erstellt: " + projectName;
            }
        }

        private void OpenProject(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Star Engine Project Files (*.sge)|*.sge|Alle Dateien (*.*)|*.*";
                openFileDialog.Title = "Projekt öffnen";

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string projectData = ProjectLogic.LoadProject(openFileDialog.FileName);
                    if (projectData != null)
                    {
                        codeTextBox.Text = projectData;
                    }
                }
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form
            {
                Width = 400,
                Height = 150,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
            Button confirmation = new Button() { Text = "OK", Left = 250, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.ShowDialog();
            return textBox.Text;
        }
    }
}
