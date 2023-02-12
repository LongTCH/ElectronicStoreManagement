using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class DataProvider
{
    private readonly ESMDbContext _context;
    public DataProvider(ESMDbContext dbContext)
    {
        _context = dbContext;
    }
    public Account? GetAcount(string username, string password)
        => _context.Accounts
            .Where(s => s.Username == username && s.PasswordHash == password)
            .FirstOrDefault();
}
