using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces;

public interface IAccountRepository: IRepository<AccountDTO>
{
    string GetSuggestAccountIdCounter();
    void ResetPassword(string ID, string newPassword);
}
