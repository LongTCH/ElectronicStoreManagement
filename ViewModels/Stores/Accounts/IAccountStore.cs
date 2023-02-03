using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.Accounts;

public interface IAccountStore
{
    Account? CurrentAccount { get; set; }
    event Action CurrentAccountChanged;
}
