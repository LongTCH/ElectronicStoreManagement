using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    string GetSuggestAccountId(string prefix);
    Task<IEnumerable<Account>> SearchAccounts(string keyword);
    Task<bool> ResetPassword(string ID, string newPasswordHash);
}
public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public override async Task<Account?> GetById(string id)
    {
        using var _context = new ESMDbContext();
        return await _context.Accounts.AsQueryable()
                .Where(ac => ac.Id == id)
                .FirstOrDefaultAsync();
    }
    public string GetSuggestAccountId(string prefix)
    {
        using var _context = new ESMDbContext();
        var MaxValue = _context.Accounts.AsQueryable()
                    .Where(a => a.Id.StartsWith(prefix))
                    .OrderBy(x => Convert.ToInt32(x.Id.Substring(5)))
                    .LastOrDefault();
        string result = (MaxValue != null) ? (Convert.ToInt32(MaxValue.Id[5..]) + 1).ToString() : "0";
        result = result.Insert(0, new('0', 4 - result.Length));
        return prefix + result;
    }
    public override async Task<object?> Update(Account accountDTO)
    {
        using var _context = new ESMDbContext();
        var account = (from ac in _context.Accounts
                       where ac.Id == accountDTO.Id
                       select ac).First();
        _context.Entry(account).CurrentValues.SetValues(accountDTO);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Add(Account accountDTO)
    {
        using var _context = new ESMDbContext();
        await _context.Accounts.AddAsync(accountDTO);
        await _context.SaveChangesAsync();
        return null;
    }
    public async Task<bool> ResetPassword(string ID, string newPasswordHash)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            var account = await (from a in _context.Accounts
                                 where a.Id == ID
                                 select a).FirstAsync();
            account.PasswordHash = newPasswordHash;
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public override async Task<IEnumerable<Account>?> GetAll()
    {
        using var _context = new ESMDbContext();
        return await _context.Accounts.AsNoTracking().Where(x => x.Phone != "0").ToListAsync();
    }
    public override async Task<object?> Delete(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Accounts.AsQueryable().SingleAsync(p => p.Id == id);
        p.Phone = "0";
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<bool> IsIdExist(string id)
    {
        using var _context = new ESMDbContext();
        return await _context.Accounts.AnyAsync(x => x.Id == id);
    }
    public async Task<IEnumerable<Account>> SearchAccounts(string keyword)
    {
        using var _context = new ESMDbContext();
        var list = await _context.Accounts
                    .AsNoTracking()
                    .ToListAsync();
        return list.Where(x => x.ToString().ToUpper().Contains($"{keyword.ToUpper()}"));
    }
}
