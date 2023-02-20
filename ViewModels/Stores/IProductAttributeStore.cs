using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores;

public interface IProductAttributeStore : IStore
{
    public string Name { get; set; }
}
