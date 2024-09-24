using System.Windows.Forms;

namespace StarEngine2025
{
    public static class ProjectTreeLogic
    {
        public static void AddFile(TreeView projectTreeView, string fileName)
        {
            var newNode = new TreeNode(fileName);
            projectTreeView.SelectedNode.Nodes.Add(newNode);
            projectTreeView.SelectedNode.Expand();
        }

        public static void DeleteFile(TreeView projectTreeView)
        {
            if (projectTreeView.SelectedNode != null && projectTreeView.SelectedNode.Parent != null)
            {
                projectTreeView.Nodes.Remove(projectTreeView.SelectedNode);
            }
        }

        public static void RenameFile(TreeView projectTreeView, string newName)
        {
            if (projectTreeView.SelectedNode != null)
            {
                projectTreeView.SelectedNode.Text = newName;
            }
        }
    }
}
