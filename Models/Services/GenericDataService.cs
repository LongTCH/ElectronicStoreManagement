using Microsoft.EntityFrameworkCore;
using Models.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services;

public class GenericDataService<T> : IDataService<T> where T : DomainObject
{
    private readonly EsmdatabaseDbContextFactory _contextFactory;
    private readonly NonQueryDataService<T> _nonQueryDataService;

    public GenericDataService(EsmdatabaseDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
    }

    public async Task<T> Create(T entity)
    {
        return await _nonQueryDataService.Create(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _nonQueryDataService.Delete(id);
    }

    public async Task<T> Get(int id)
    {
        using (EsmdatabaseContext context = _contextFactory.CreateDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        using (EsmdatabaseContext context = _contextFactory.CreateDbContext())
        {
            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;
        }
    }

    public async Task<T> Update(int id, T entity)
    {
        return await _nonQueryDataService.Update(id, entity);
    }
}
