using System;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace StarEngine2025
{
    public static class CodeExecutionLogic
    {
        public static async System.Threading.Tasks.Task ExecuteCodeAsync(string code, IWin32Window owner)
        {
            try
            {
                var result = await CSharpScript.EvaluateAsync(code);
                MessageBox.Show($"Ergebnis: {result}", "Code Ergebnis", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Ausführen des Codes: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
