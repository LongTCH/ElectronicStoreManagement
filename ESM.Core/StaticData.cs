using System.Collections.Generic;

namespace ESM.Core
{
    public class StaticData
    {
        public static readonly IEnumerable<string> GenderList = new[] { "Nam", "Nữ" };
        public static readonly string ImageRegex = @"\.(jpe?g|png|bmp|gif|tiff?|ico|webp)$";
        public static readonly string ImageFilter =
        "All Files (*.*)|*.*" +
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
        "|Icon (*.ico)|*.ico";
        public static readonly string ExcelFilter =
            "All Files (*.*)|*.*" +
            "|Excel Files|*.xls;*.xlsx;*.xlsm";
        public static readonly string EmailVerificationPrefix = "From : Electronic Store" + "<br>Your verification code : ";
    }
}
