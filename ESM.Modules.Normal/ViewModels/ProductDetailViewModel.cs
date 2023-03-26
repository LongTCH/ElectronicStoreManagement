using ClosedXML.Excel;
using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ESM.Modules.Normal.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ProductDetailViewModel : BindableBase, IModal, INavigationAware
    {
        private ProductDTO ProductDTO { get; set; }
        public ProductDetailViewModel(IModalService modalService)
        {
            CloseCommand = new(() => modalService.CloseModal());
        }
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        private List<string> imageList;
        public List<string> ImageList
        {
            get => imageList;
            set => SetProperty(ref imageList, value);
        }
        private List<CustomAtrribute> detailSource;
        public List<CustomAtrribute> DetailSource
        {
            get => detailSource;
            set => SetProperty(ref detailSource, value);
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string company;
        public string Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }
        private decimal price;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        private decimal sellPrice;
        public decimal SellPrice
        {
            get => sellPrice;
            set => SetProperty(ref sellPrice, value);
        }
        private double? discount;
        public double? Discount
        {
            get => discount;
            set => SetProperty(ref discount, value);
        }
        private bool discountShow;
        public bool DiscountShow
        {
            get => discountShow;
            set => SetProperty(ref discountShow, value);
        }
        public DelegateCommand CloseCommand { get; }

        private void ReadExcel()
        {
            IsBusy = true;
            var filePath = ProductDTO.DetailPath;
            if (File.Exists(filePath))
            {
                try
                {
                    using var workbook = new XLWorkbook(filePath);
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
                    DetailSource = data;
                }
                catch { DetailSource = null; }
            }
            IsBusy = false;
            var path = ProductDTO.ImagePath;
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
                ImageList = new();
                foreach (string filename in files)
                {
                    if (Regex.IsMatch(filename, StaticData.ImageRegex))
                        ImageList.Add(filename);
                }
            }
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProductDTO = navigationContext.Parameters["Product"] as ProductDTO;
            System.Threading.Tasks.Task task = new(ReadExcel);
            task.Start();
            Name = ProductDTO.Name;
            Price = ProductDTO.Price;
            SellPrice = ProductDTO.SellPrice;
            Discount = ProductDTO.Discount;
            DiscountShow = ProductDTO.DiscountShow;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
    public record CustomAtrribute(string? Attribute, string? Description);
}
