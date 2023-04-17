using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPccpuRepository : IProductRepository<Pccpu> {
}
public class PccpuRepository : ProductRepository<Pccpu>, IPccpuRepository
{
    public PccpuRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Pccpu>?> GetAll()
    {
        return await _context.Pccpus.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToListAsync();
    }
    public override async Task<object?> Add(Pccpu entity)
    {
        await _context.Pccpus.AddAsync(entity);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Update(Pccpu entity)
    {
        bool res = true;
        try
        {
            var hd = await _context.Pccpus.AsQueryable()
                   .FirstAsync(p => p.Id == entity.Id);
            _context.Entry(hd).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    public override async Task<object?> AddList(IEnumerable<Pccpu> list)
    {
        bool res = true;
        try
        {
            await _context.Pccpus.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) { res = false; }
        return res;
    }
    public override async Task<bool> IsIdExist(string id)
    {
        return await _context.Pccpus.AnyAsync(p => p.Id == id);
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.CPU);
    }
}
