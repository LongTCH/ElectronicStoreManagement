using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Linq;

namespace Models;

public class AccountRepository : Repository<AccountDTO>, IAccountRepository
{
    public AccountRepository(ESMDbContext context) : base(context)
    {
    }
    public override AccountDTO? Get(string id)
    {
        AccountDTO? account = (from ac in _context.Accounts
                               where ac.Id == id
                               select new AccountDTO()
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
        return account;
    }

    public string GetSuggestAccountIdCounter()
    {
        var MaxValue = _context.Accounts
                    .OrderByDescending(x => Convert.ToInt32(x.Id.Substring(5)))
                    .FirstOrDefault();
        string result = (MaxValue != null) ? (Convert.ToInt32(MaxValue.Id[5..]) + 1).ToString() : "0";
        result = result.Insert(0, new('0', 4 - result.Length));
        return result;
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
