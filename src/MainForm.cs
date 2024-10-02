using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StarEngine2025
{
    public class Settings
    {
        public int[] backgroundColor { get; set; }
        public int[] textColor { get; set; }
        public int[] borderColor { get; set; }
        public int fontSize { get; set; }
        public string fontStyle { get; set; }
    }

    public class MainForm : Form
    {
        public static settings Default => new settings
        {
            backgroundColor = new int[] { 255, 255, 255 }, 
            textColor = new int[] { 0, 0, 0 },
            borderColor = new int[] { 245, 245, 220 },
            fontSize = 12,
            fontStyle = "Regular"
       };
        private TextBox codeTextBox;
        private Panel editorPanel;
        private MenuStrip menuStrip;

        public MainForm()
        {
            Text = "StarEngine2025";
            Size = new System.Drawing.Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            string relativePath = "../source/icons/AppIcons/AppIcon.ico";
            string combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            string iconPath = Path.GetFullPath(combinedPath);

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

            InitializeMenu();
            InitializeEditor();
            LoadSettings();
        }

        private void LoadSettings()
        {
            string settingsPath = pathHelper.RelativePathMaker("../UI/settings.json");

            if (!File.Exists(settingsPath))
            {
                MessageBox.Show("Einstellungsdatei nicht gefunden: " + settingsPath);
                return;
            }

            string jsonBody = File.ReadAllText(settingsPath);
            var settings = ParseJsonToDictionary(jsonBody);
            

            if (settings != null && settings.Count > 0)
            {
                if (settings.TryGetValue("theme", out string theme))
                {
                    try
                    {
                        SettingsLogic.ApplyStyle(theme, settingsConfig);

                        var codeTextBox = this.codeTextBox;
                        if (codeTextBox != null)
                        {
                            codetextBox.BackColor = settingsConfig.backColor;
                            codeTextBox.ForeColor = settingsConfig.foreColor;
                        }

                        var menuStrip = this.menuStrip;
                        if (menuStrip != null)
                        {
                            menuStrip.BackColor = settingsConfig.backColor;
                            menuStrip.ForeColor = settingsConfig.foreColor;
                        }
                        
                        var editorPanel = this.editorPanel;
                        if (editorPanel != null)
                        {
                            editorPanel.BackColor = settingsConfig.borderColor;
                            editorPanel.ForeColor = settingsConfig.foreColor;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"theme: {theme}");
                        Console.WriteLine($"jsonBody: {jsonBody}");
                        Console.WriteLine($"settings: {settings}");
                        MessageBox.Show("Fehler beim Anwenden des Themes: " + e.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }          
            }
        }

        private void InitializeMenu()
        {
            menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("Datei");

            fileMenu.DropDownItems.Add("Neues Projekt", null, NewProject);
            fileMenu.DropDownItems.Add("Projekt öffnen", null, OpenProject);
            fileMenu.DropDownItems.Add("Einstellungen", null, (s, e) => SettingsLogic.OpenSettings(this, this));
            fileMenu.DropDownItems.Add("Beenden", null, (s, e) => Application.Exit());

            menuStrip.Items.Add(fileMenu);

            menuStrip.ForeColor = Color.FromArgb(0,0,0);

            Controls.Add(menuStrip);

            MainMenuStrip = menuStrip;
        }

        private void InitializeEditor()
        {
            editorPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(5, 25 , 5, 5),
                BackColor = Color.FromArgb(245, 245, 220)
            };

            codeTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                WordWrap = false,
                Font = new Font("Consolas", 12)
            };

            editorPanel.Controls.Add(codeTextBox);
            
            Controls.Add(editorPanel);
        }

        private void NewProject(object sender, EventArgs e)
        {
            string projectName = Prompt.ShowDialog("Projektname:", "Neues Projekt");

            if (!string.IsNullOrEmpty(projectName))
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Wählen Sie einen Speicherort für das Projekt";
                    if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string selectedPath = folderBrowserDialog.SelectedPath;
                        
                        string programFilePath = ProjectLogic.CreateProject(selectedPath, projectName);

                        if (!string.IsNullOrEmpty(programFilePath) && File.Exists(programFilePath))
                        {
                            string programData = File.ReadAllText(programFilePath);
                            codeTextBox.Text = programData;
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Erstellen des Projekts oder Program.cs wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void OpenProject(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Star Engine Project Files (*.sge)|*.sge";
                openFileDialog.Title = "Projekt öffnen";

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string projectData = ProjectLogic.LoadProject(openFileDialog.FileName);
                    if (projectData != null)
                    {
                        codeTextBox.Text = projectData;
                        codeTextBox.SelectionStart = 0;
                        codeTextBox.ScrollToCaret();

                        codeTextBox.Focus();
                        codeTextBox.SelectionLength = 0;
                        codeTextBox.SelectionStart = 0;
                        codeTextBox.ScrollToCaret();
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
