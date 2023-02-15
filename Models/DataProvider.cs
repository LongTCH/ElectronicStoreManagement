using Models.DTOs;
using Scrypt;
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
    public Account? GetAcount(string id, string password)
    {
        ScryptEncoder encoder = new ScryptEncoder();
        var list = _context.Accounts.AsEnumerable();
        foreach(var account in list)
        {
            if (account.Id == id && encoder.Compare(password, account.PasswordHash))
                return account;
        }
        return null;
    }
    public Account? GetAcountByID(string id)
    {
        var list = _context.Accounts.AsEnumerable();
        foreach (var account in list)
        {
            if (account.Id == id)
                return account;
        }
        return null;
    }
    public IEnumerable<LaptopDTO> GetLaptopList()
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
}
