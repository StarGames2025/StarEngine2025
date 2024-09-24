using System;

namespace StarEngine2025
{
    public static class ProjectLogic
    {
        public static void CreateProject(string projectName)
        {
            Console.WriteLine($"Projekt '{projectName}' wurde erstellt.");
        }

        public static void ExportProject(string projectPath)
        {
            Console.WriteLine($"Projekt exportiert nach {projectPath}.");
        }
    }
}
