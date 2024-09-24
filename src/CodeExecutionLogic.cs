using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace StarEngine2025
{
    public static class CodeExecutionLogic
    {
        public static void ExecuteCode(string code, IWin32Window owner)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "TempCode.cs");
            File.WriteAllText(tempFilePath, code);

            var processInfo = new ProcessStartInfo
            {
                FileName = "mcs",
                Arguments = $"-out:TempCode.exe {tempFilePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                process.WaitForExit();

                string errorOutput = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(errorOutput))
                {
                    MessageBox.Show($"Fehler beim Kompilieren: {errorOutput}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            processInfo = new ProcessStartInfo
            {
                FileName = "mono",
                Arguments = "TempCode.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(errorOutput))
                {
                    MessageBox.Show($"Fehler beim Ausführen: {errorOutput}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Ausgabe:\n{output}", "Ergebnis", MessageBoxButtons.OK);
                }
            }
        }
    }
}
