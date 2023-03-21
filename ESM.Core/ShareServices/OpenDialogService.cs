using System;
using System.IO;
using System.Windows.Forms;

namespace ESM.Core.ShareServices
{
    public enum FileType { Image, Excel };
    public interface IOpenDialogService
    {
        public string? FolderDialog();
        public string? FileDialog(FileType? fileType);
    }
    public class OpenDialogService : IOpenDialogService
    {
        public string? FolderDialog()
        {
            FolderBrowserDialog folder = new()
            {
                Description = "Select a folder",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };
            if (folder.ShowDialog() == DialogResult.OK)
                return folder.SelectedPath;
            return null;
        }
        public string? FileDialog(FileType? fileType = null)
        {
            string filter;
            if (fileType == FileType.Image) filter = StaticData.ImageFilter;
            else if (fileType == FileType.Excel) filter = StaticData.ExcelFilter;
            else filter = "All Files (*.*)|*.*";
            OpenFileDialog openFileDialog = new()
            {
                Filter = filter,
                Title = "Direct to your avartar"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
    }
}
