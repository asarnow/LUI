using System.Windows.Forms;

namespace LUI
{
    public static class GuiUtil
    {
        public static string SimpleFileNameDialog(string Filter)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = Filter;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return "";
            }
        }

        public static string SimpleFolderNameDialog()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            else
            {
                return "";
            }
        }
    }
}
