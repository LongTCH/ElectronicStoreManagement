using Models.DTOs;
using Models.Entities;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models;

public class DataProvider
{
    private readonly ESMDbContext _context;
    public DataProvider(ESMDbContext dbContext)
    {
        _context = dbContext;
    }
    public AccountDTO? GetAcount(string? id, string? password = null)
    {
        ScryptEncoder encoder = new ScryptEncoder();
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
                                   Sex = ac.Sex,
                                   Street = ac.Street,
                                   SubDistrict = ac.SubDistrict
                               }).FirstOrDefault();
        if (password != null)
        {
            if (account != null && encoder.Compare(password, account.PasswordHash))
                return account;
            else return null;
        }
        return account;
    }
    public IEnumerable<LaptopDTO>? GetLaptopList()
    {
        return from laptop in _context.Laptops
               select new LaptopDTO()
               {
                   Name = laptop.Name,
                   Image_Path = @laptop.ImagePath,
                   Price = laptop.Price
               };
    }
    public void ResetPassword(string ID, string newPassword)
    {
        var account = (from a in _context.Accounts
                       where a.Id == ID
                       select a).First();
        ScryptEncoder encoder = new ScryptEncoder();
        account.PasswordHash = encoder.Encode(newPassword);
        _context.SaveChanges();
    }
    public string GetSeuggestAccountIdCounter()
    {
        var list = (from account in _context.Accounts
                    select new { Counter = Convert.ToInt32(account.Id.Substring(5)) }).ToList();
        string result = (list.Max(x => x.Counter) + 1).ToString();
        result = result.Insert(0, new('0', 4 - result.Length));
        return result;
    }
    public void AddUser(AccountDTO accountDTO)
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
            Sex = accountDTO.Sex,
            Street = accountDTO.Street,
            SubDistrict = accountDTO.SubDistrict,
        };
        _context.Accounts.Add(newAccount);
        _context.SaveChanges();
    }
}
