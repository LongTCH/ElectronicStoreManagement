using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;

public interface IAccountRepository : IBaseRepository<AccountDTO>
{
    string GetSuggestAccountId(string prefix);
    void ResetPassword(string ID, string newPasswordHash);
}
public class AccountRepository : BaseRepository<AccountDTO>, IAccountRepository
{
    public AccountRepository(ESMDbContext context) : base(context)
    {
    }
    public override AccountDTO? GetById(string id)
    {
        return _context.Accounts.AsQueryable()
                .Where(ac => ac.Id == id)
                .Select(ac => new AccountDTO()
                {
                    Id = ac.Id,
                    AvatarPath = @ac.AvatarPath,
                    Birthday = ac.Birthday,
                    City = ac.City,
                    District = ac.District,
                    EmailAddress = ac.EmailAddress,
                    FirstName = ac.FirstName,
                    LastName = ac.LastName,
                    PasswordHash = ac.PasswordHash,
                    Phone = ac.Phone,
                    Gender = ac.Gender,
                    Street = ac.Street,
                    SubDistrict = ac.SubDistrict
                }).FirstOrDefault();
    }
    public override bool Any(string id)
    {
        return _context.Accounts.Any(a => a.Id == id);
    }
    public string GetSuggestAccountId(string prefix)
    {
        var MaxValue = _context.Accounts.AsQueryable()
                    .Where(a => a.Id.StartsWith(prefix))
                    .OrderByDescending(x => Convert.ToInt32(x.Id.Substring(5)))
                    .FirstOrDefault();
        string result = (MaxValue != null) ? (Convert.ToInt32(MaxValue.Id[5..]) + 1).ToString() : "0";
        result = result.Insert(0, new('0', 4 - result.Length));
        return prefix + result;
    }
    public override void Update(AccountDTO accountDTO)
    {
        var account = (from ac in _context.Accounts
                       where ac.Id == accountDTO.Id
                       select ac).First();
        account.FirstName = accountDTO.FirstName;
        account.LastName = accountDTO.LastName;
        account.EmailAddress = accountDTO.EmailAddress;
        account.Phone = accountDTO.Phone;
        account.AvatarPath = accountDTO.AvatarPath;
        account.Birthday = accountDTO.Birthday;
        account.City = accountDTO.City;
        account.SubDistrict = accountDTO.SubDistrict;
        account.District = accountDTO.District;
        account.Street = accountDTO.Street;
        account.Gender = accountDTO.Gender;
    }
    public override void Add(AccountDTO accountDTO)
    {
        Account newAccount = new()
        {
            AvatarPath = accountDTO.AvatarPath,
            Birthday = accountDTO.Birthday,
            City = accountDTO.City,
            District = accountDTO.District,
            EmailAddress = accountDTO.EmailAddress,
            FirstName = accountDTO.FirstName,
            Id = accountDTO.Id,
            LastName = accountDTO.LastName,
            PasswordHash = accountDTO.PasswordHash,
            Phone = accountDTO.Phone,
            Gender = accountDTO.Gender,
            Street = accountDTO.Street,
            SubDistrict = accountDTO.SubDistrict,
        };
        _context.Accounts.Add(newAccount);
    }
    public void ResetPassword(string ID, string newPasswordHash)
    {
        var account = (from a in _context.Accounts
                       where a.Id == ID
                       select a).First();

        account.PasswordHash = newPasswordHash;
    }
}
