using Microsoft.Win32;

namespace ViewModels.Commands;

public class ImagePathCommand
{
    public ImagePathCommand()
    {
    }
    public string? Set()
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "All Files (*.*)|*.*" +
        "|All Pictures (*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico)" +
            "|*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico" +
        "|Windows Enhanced Metafile (*.emf)|*.emf" +
        "|Windows Metafile (*.wmf)|*.wmf" +
        "|JPEG File Interchange Format (*.jpg;*.jpeg;*.jfif;*.jpe)|*.jpg;*.jpeg;*.jfif;*.jpe" +
        "|Portable Network Graphics (*.png)|*.png" +
        "|Bitmap Image File (*.bmp;*.dib;*.rle)|*.bmp;*.dib;*.rle" +
        "|Compressed Windows Enhanced Metafile (*.emz)|*.emz" +
        "|Compressed Windows MetaFile (*.wmz)|*.wmz" +
        "|Tag Image File Format (*.tif;*.tiff)|*.tif;*.tiff" +
        "|Scalable Vector Graphics (*.svg)|*.svg" +
        "|Icon (*.ico)|*.ico",
            Title = "Direct to your avartar"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }
        return null;
    }
}
