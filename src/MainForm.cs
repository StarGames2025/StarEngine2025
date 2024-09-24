using System;
using System.Windows.Forms;

namespace StarEngine2025
{
    public class MainForm : Form
    {
        private TextBox textBox;
        private TextBox codeEditor;
        private TreeView projectTreeView;
        private ContextMenuStrip treeContextMenu;

        public MainForm()
        {
            Text = "StarEngine2025";
            Size = new System.Drawing.Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            var menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("Datei");
            fileMenu.DropDownItems.Add("Neues Projekt", null, NewProject);
            fileMenu.DropDownItems.Add("Projekt öffnen", null, OpenProject);
            fileMenu.DropDownItems.Add("Einstellungen", null, OpenSettings);
            fileMenu.DropDownItems.Add("Beenden", null, (s, e) => Application.Exit());
            menuStrip.Items.Add(fileMenu);
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            InitializeProjectTree();
            InitializeEditor();
            InitializeCodeEditor();
        }

        private void InitializeProjectTree()
        {
            projectTreeView = new TreeView
            {
                Dock = DockStyle.Left,
                Width = 250
            };
            Controls.Add(projectTreeView);

            treeContextMenu = new ContextMenuStrip();
            treeContextMenu.Items.Add("Neue Datei", null, NewFile);
            treeContextMenu.Items.Add("Löschen", null, DeleteFile);
            treeContextMenu.Items.Add("Umbenennen", null, RenameFile);
            projectTreeView.ContextMenuStrip = treeContextMenu;

            // Beispielprojektordner
            var rootNode = new TreeNode("Projekt");
            projectTreeView.Nodes.Add(rootNode);
        }

        private void InitializeEditor()
        {
            textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Top,
                Height = 200,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(textBox);
        }

        private void InitializeCodeEditor()
        {
            codeEditor = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                Font = new System.Drawing.Font("Consolas", 10)
            };
            Controls.Add(codeEditor);

            Button executeButton = new Button
            {
                Text = "Code Ausführen",
                Dock = DockStyle.Bottom
            };
            executeButton.Click += ExecuteButton_Click;
            Controls.Add(executeButton);
        }

        private void NewFile(object sender, EventArgs e)
        {
            if (projectTreeView.SelectedNode != null)
            {
                string fileName = Prompt.ShowDialog("Dateiname:", "Neue Datei");
                if (!string.IsNullOrEmpty(fileName))
                {
                    var newNode = new TreeNode(fileName);
                    projectTreeView.SelectedNode.Nodes.Add(newNode);
                    projectTreeView.SelectedNode.Expand();
                }
            }
        }

        private void DeleteFile(object sender, EventArgs e)
        {
            if (projectTreeView.SelectedNode != null && projectTreeView.SelectedNode.Parent != null)
            {
                projectTreeView.Nodes.Remove(projectTreeView.SelectedNode);
            }
        }

        private void RenameFile(object sender, EventArgs e)
        {
            if (projectTreeView.SelectedNode != null)
            {
                string newName = Prompt.ShowDialog("Neuer Name:", "Umbenennen", projectTreeView.SelectedNode.Text);
                if (!string.IsNullOrEmpty(newName))
                {
                    projectTreeView.SelectedNode.Text = newName;
                }
            }
        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            await CodeExecutionLogic.ExecuteCodeAsync(codeEditor.Text, this);
        }

        private void NewProject(object sender, EventArgs e)
        {
            string projectName = Prompt.ShowDialog("Projektname:", "Neues Projekt");
            if (!string.IsNullOrEmpty(projectName))
            {
                textBox.Text = "Neues Projekt erstellt: " + projectName;
                EditorLogic.SaveProject(projectName, textBox.Text, this);
            }
        }

        private void OpenProject(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Star Engine Project Files (*.sge)|*.sge|All Files (*.*)|*.*";
                openFileDialog.Title = "Projekt öffnen";

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string projectData = EditorLogic.LoadProject(openFileDialog.FileName);
                    if (projectData != null)
                    {
                        textBox.Text = projectData;
                    }
                }
            }
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            SettingsLogic.OpenSettings();
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form
            {
                Width = 400,
                Height = 150,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300, Text = defaultValue };
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
