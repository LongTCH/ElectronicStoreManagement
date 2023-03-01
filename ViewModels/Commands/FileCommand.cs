using Microsoft.Win32;
using ViewModels.Filter;

namespace ViewModels.Commands;
public enum FileType { Image, Excel}
public class FileCommand
{
    public FileCommand()
    {
    }
    public string? Set(FileType? fileType = null)
    {
        string filter;
        if (fileType == FileType.Image) filter = new MyFilter().GetImageFilter();
        else if (fileType == FileType.Excel) filter = new MyFilter().GetExcelFilter();
        else filter = "All Files (*.*)|*.*";
        OpenFileDialog openFileDialog = new()
        {
            Filter = filter,
            Title = "Direct to your avartar"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }
        return null;
    }
}
