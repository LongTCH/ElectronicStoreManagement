using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbContexts;

public class EsmdatabaseDbContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public EsmdatabaseDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public EsmdatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<EsmdatabaseContext> options = new DbContextOptionsBuilder<EsmdatabaseContext>();

        _configureDbContext(options);

        return new EsmdatabaseContext(options.Options);
    }
}
