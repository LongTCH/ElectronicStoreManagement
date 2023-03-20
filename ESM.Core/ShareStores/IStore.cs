using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Core.ShareStores
{
    public interface IStore
    {
        public event Action? CurrentStoreChanged;
    }
}
