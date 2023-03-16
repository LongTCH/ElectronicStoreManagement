using System;
using System.IO;
using System.Windows.Forms;

namespace ViewModels.Commands;

public class FolderCommand
{
    public static string? Set()
    {
        FolderBrowserDialog folder = new()
        {
            Description = "Time to select a folder",
            UseDescriptionForTitle = true,
            SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        + Path.DirectorySeparatorChar,
            ShowNewFolderButton = true
        };
        if (folder.ShowDialog() == DialogResult.OK)
            return folder.SelectedPath;
        return null;
    }
}
