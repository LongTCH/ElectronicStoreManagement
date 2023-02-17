using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores;

public class ProductDetailStore
{
    public IProductDTO? CurrentProduct { get; set; }
}
