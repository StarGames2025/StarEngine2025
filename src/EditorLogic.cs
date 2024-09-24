using System;
using System.IO;
using System.Windows.Forms;

namespace StarEngine2025
{
    public static class EditorLogic
    {
        private static readonly string projectsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarEngineProjects");

        static EditorLogic()
        {
            if (!Directory.Exists(projectsDirectory))
            {
                Directory.CreateDirectory(projectsDirectory);
            }
        }

        public static void SaveProject(string projectName, string projectData, IWin32Window owner)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Star Engine Project Files (*.sge)|*.sge|All Files (*.*)|*.*";
                saveFileDialog.Title = "Projekt speichern";
                saveFileDialog.FileName = projectName + ".sge";

                if (saveFileDialog.ShowDialog(owner) == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, projectData);
                        MessageBox.Show($"Projekt {projectName} wurde gespeichert als {Path.GetFileName(saveFileDialog.FileName)}.", "Info", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Speichern des Projekts: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static string LoadProject(string projectPath)
        {
            try
            {
                if (!File.Exists(projectPath))
                {
                    MessageBox.Show($"Das Projekt {projectPath} wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                string projectData = File.ReadAllText(projectPath);
                MessageBox.Show($"Projekt {projectPath} wurde geladen.", "Info", MessageBoxButtons.OK);
                return projectData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden des Projekts: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
