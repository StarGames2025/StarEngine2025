using System;

namespace StarEngine2025
{
    public static class TemplateLogic
    {
        public static void LoadTemplate(string templateName)
        {
            Console.WriteLine($"Vorlage '{templateName}' wurde geladen.");
        }

        public static void SaveTemplate(string templateName)
        {
            Console.WriteLine($"Vorlage '{templateName}' wurde gespeichert.");
        }
    }
}
