using System;
using System.IO;
using System.Windows.Forms;

namespace StarEngine2025
{
    public static class ProjectLogic
    {

        public static string CreateProject(string selectedPath, string projectName)
        {
            string projectDirectory = Path.Combine(selectedPath, projectName);
            string projectFilePath = Path.Combine(projectDirectory, $"{projectName}.sge");

            if (Directory.Exists(projectDirectory))
            {
                MessageBox.Show("Ein Projekt mit diesem Namen existiert bereits.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                Directory.CreateDirectory(projectDirectory);
                
                File.WriteAllText(projectFilePath, "Neues Projekt: " + projectName);

                string programFilePath = Path.Combine(projectDirectory, "Program.cs");
                File.WriteAllText(programFilePath, @"using System;

namespace " + projectName + @"
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, StarEngine!"");
        }
    }
}");

                MessageBox.Show($"Projekt '{projectName}' wurde erstellt.", "Info", MessageBoxButtons.OK);

                return programFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen des Projekts: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public static string LoadProject(string projectPath)
        {
            try
            {
                if (!File.Exists(projectPath))
                {
                    MessageBox.Show($"Das Projekt '{projectPath}' wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                string projectDirectory = Path.GetDirectoryName(projectPath);
                string projectData = File.ReadAllText(projectPath);

                string programFilePath = Path.Combine(projectDirectory, "Program.cs");
                if (File.Exists(programFilePath))
                {
                    string programData = File.ReadAllText(programFilePath);
                    MessageBox.Show($"Projekt '{projectPath}' wurden geladen.", "Info", MessageBoxButtons.OK);
                    return programData;
                }
                else
                {
                    MessageBox.Show("Die Datei 'Program.cs' wurde nicht gefunden.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return projectData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden des Projekts: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
