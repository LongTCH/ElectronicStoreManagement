using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    string GetSuggestAccountId(string prefix);
    void ResetPassword(string ID, string newPasswordHash);
}
public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(ESMDbContext context) : base(context)
    {
    }
    public override Account? GetById(string id)
    {
        return _context.Accounts.AsQueryable()
                .Where(ac => ac.Id == id)
                .FirstOrDefault();
    }
    public override bool Any(string id)
    {
        return _context.Accounts.Any(a => a.Id == id);
    }
    public string GetSuggestAccountId(string prefix)
    {
        var MaxValue = _context.Accounts.AsQueryable()
                    .Where(a => a.Id.StartsWith(prefix))
                    .OrderBy(x => Convert.ToInt32(x.Id.Substring(5)))
                    .LastOrDefault();
        string result = (MaxValue != null) ? (Convert.ToInt32(MaxValue.Id[5..]) + 1).ToString() : "0";
        result = result.Insert(0, new('0', 4 - result.Length));
        return prefix + result;
    }
    public override object? Update(Account accountDTO)
    {
        var account = (from ac in _context.Accounts
                       where ac.Id == accountDTO.Id
                       select ac).First();
        _context.Entry(account).CurrentValues.SetValues(accountDTO);
        return null;
    }
    public override object? Add(Account accountDTO)
    {
        _context.Accounts.Add(accountDTO);
        return null;
    }
    public void ResetPassword(string ID, string newPasswordHash)
    {
        var account = (from a in _context.Accounts
                       where a.Id == ID
                       select a).First();

        account.PasswordHash = newPasswordHash;
    }
}
