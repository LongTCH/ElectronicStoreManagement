using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Utilities;

namespace ViewModels.NotifyControlViewModels;

public class ProductDetailViewModel : ViewModelBase
{
    private readonly ProductDetailStore _currentProduct;
    public ICommand CloseCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }
    public List<CustomAtrribute>? DetailSource { get; } = null;
    public ProductDetailViewModel(ProductDetailStore currentProduct,
        INavigationService closeModalNavigationService)
    {
        _currentProduct = currentProduct;
        CloseCommand = new RelayCommand<object>(_ => closeModalNavigationService.Navigate());
        var filePath = currentProduct.CurrentProduct!.DetailPath;
        if (File.Exists(filePath))
            DetailSource = ReadExcelFile(filePath);
        var path = _currentProduct.CurrentProduct?.ImagePath;
        if (Directory.Exists(path))
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            imageFiles = new();
            foreach (string filename in files)
            {
                if (Regex.IsMatch(filename, ImageRegex.Get()))
                    imageFiles.Add(filename);
            }
            OnPropertyChanged(nameof(ImageSource));
            OnPropertyChanged(nameof(PreviousImage));
            OnPropertyChanged(nameof(NextImage));
        }
        NextCommand = new RelayCommand<object>(_ => nextCommand());
        PreviousCommand = new RelayCommand<object>(_ => previousCommand());
    }
    private List<string> imageFiles { get; }
    private int index = 0;
    public string? ImageSource => imageFiles.ElementAtOrDefault(index);
    public string? PreviousImage => imageFiles.ElementAtOrDefault((imageFiles.Count + index - 1) % imageFiles.Count);
    public string? NextImage => imageFiles.ElementAtOrDefault((index + 1) % imageFiles.Count);
    public string? DetailPath => _currentProduct.CurrentProduct?.DetailPath;
    public string? Name => _currentProduct.CurrentProduct?.Name;
    public string? Company => _currentProduct.CurrentProduct?.Company;
    public decimal? Price => _currentProduct.CurrentProduct?.Price;
    public decimal? SellPrice => _currentProduct.CurrentProduct?.SellPrice;
    public double? Discount => _currentProduct.CurrentProduct?.Discount;
    public bool? DiscountShow => _currentProduct.CurrentProduct?.DiscountShow;
    private void nextCommand()
    {
        index = (index + 1) % imageFiles.Count;
        OnPropertyChanged(nameof(ImageSource));
        OnPropertyChanged(nameof(PreviousImage));
        OnPropertyChanged(nameof(NextImage));
    }
    private void previousCommand()
    {
        index = (imageFiles.Count + index - 1) % imageFiles.Count;
        OnPropertyChanged(nameof(ImageSource)); 
        OnPropertyChanged(nameof(PreviousImage));
        OnPropertyChanged(nameof(NextImage));
    }
    private List<CustomAtrribute>? ReadExcelFile(string? filePath)
    {
        try
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                // Get the table range from the worksheet
                var range = worksheet.RangeUsed();

                // Convert the table range to a List of objects
                var rows = range.RowsUsed().ToList();
                var data = new List<CustomAtrribute>();
                foreach (var row in rows)
                {
                    var Attribute = row.Cell(1).Value.ToString();
                    var Description = row.Cell(2).Value.ToString();
                    if (Attribute.Trim() == "" && Description.Trim() == "") continue;
                    var item = new CustomAtrribute(Attribute, Description);
                    data.Add(item);
                }
                return data;
            }
        }
        catch { return null; }
    }
}
public record CustomAtrribute(string? Attribute, string? Description);