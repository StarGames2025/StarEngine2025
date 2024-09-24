using System;
using System.IO;
using System.Windows.Forms;

namespace StarEngine2025
{
    public static class ProjectLogic
    {
        private static readonly string projectsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarEngineProjects");

        static ProjectLogic()
        {
            if (!Directory.Exists(projectsDirectory))
            {
                Directory.CreateDirectory(projectsDirectory);
            }
        }

        public static void CreateProject(string projectName, IWin32Window owner)
        {
            string projectFilePath = Path.Combine(projectsDirectory, $"{projectName}.sge");

            if (File.Exists(projectFilePath))
            {
                MessageBox.Show("Ein Projekt mit diesem Namen existiert bereits.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                File.WriteAllText(projectFilePath, "Neues Projekt: " + projectName);
                MessageBox.Show($"Projekt '{projectName}' wurde erstellt.", "Info", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen des Projekts: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string LoadProject(string projectPath, IWin32Window owner)
        {
            try
            {
                if (!File.Exists(projectPath))
                {
                    MessageBox.Show($"Das Projekt '{projectPath}' wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                string projectData = File.ReadAllText(projectPath);
                MessageBox.Show($"Projekt '{projectPath}' wurde geladen.", "Info", MessageBoxButtons.OK);
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
