using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services;

public class NonQueryDataService<T> where T : DomainObject
{
    private readonly EsmdatabaseDbContextFactory _contextFactory;

    public NonQueryDataService(EsmdatabaseDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<T> Create(T entity)
    {
        using (EsmdatabaseContext context = _contextFactory.CreateDbContext())
        {
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }
    }

    public async Task<T> Update(int id, T entity)
    {
        using (EsmdatabaseContext context = _contextFactory.CreateDbContext())
        {
            entity.Id = id;

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            ;
            return entity;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using (EsmdatabaseContext context = _contextFactory.CreateDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
