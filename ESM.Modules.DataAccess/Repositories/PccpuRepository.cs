using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPccpuRepository : IProductRepository<Pccpu> {
}
public class PccpuRepository : ProductRepository<Pccpu>, IPccpuRepository
{
    public override async Task<Pccpu?> GetById(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Pccpus.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p != null)
        {
            p.Discount = await GetDiscount(id);
            p.InMemory = true;
        }
        return p;
    }
    public override async Task<IEnumerable<Pccpu>?> GetAll()
    {
        using var _context = new ESMDbContext();
        var list = await _context.Pccpus.AsNoTracking()
                .Where(p => p.Remain > -1)
                .ToListAsync();
        foreach (var item in list)
        {
            item.InMemory = true;
            item.Discount = await GetDiscount(item.Id);
        }
        return list;
    }
    public override async Task<object?> Add(Pccpu entity)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            await _context.Pccpus.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public override async Task<object?> Update(Pccpu entity)
    {
        using var _context = new ESMDbContext();
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
        using var _context = new ESMDbContext();
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
        using var _context = new ESMDbContext();
        return await _context.Pccpus.AnyAsync(p => p.Id == id);
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.CPU);
    }
    public override async Task<object?> Delete(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Pccpus.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
}
