using ClosedXML.Excel;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.NotifyControlViewModels;

public class ProductDetailViewModel : ViewModelBase
{
    private readonly ProductDetailStore _currentProduct;
    public ICommand CloseCommand { get; }
    public List<CustomAtrribute>? Source { get; } = null;
    public ProductDetailViewModel(ProductDetailStore currentProduct,
        INavigationService closeModalNavigationService)
    {
        _currentProduct = currentProduct;
        CloseCommand = new RelayCommand<object>(_ => closeModalNavigationService.Navigate());
        var filePath = currentProduct.CurrentProduct!.DetailPath;
        if (File.Exists(filePath))
            Source = ReadExcelFile(filePath);
    }
    public string? ImagePath => _currentProduct.CurrentProduct?.ImagePath;
    public string? DetailPath => _currentProduct.CurrentProduct?.DetailPath;
    public string? Company => _currentProduct.CurrentProduct?.Company;
    public decimal? Price => _currentProduct.CurrentProduct?.Price;
    public decimal? SellPrice => _currentProduct.CurrentProduct?.SellPrice;
    public bool? DiscountShow => _currentProduct.CurrentProduct?.DiscountShow;
    private List<CustomAtrribute>? ReadExcelFile(string? filePath)
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
    public class CustomAtrribute
    {
        public CustomAtrribute(string? attribute, string? description)
        {
            Attribute = attribute;
            Description = description;
        }

        public string? Attribute { get; }
        public string? Description { get; }
    }
}
