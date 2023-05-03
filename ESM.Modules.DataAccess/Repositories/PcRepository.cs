using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPcRepository : IProductRepository<Pc>
{
}
public class PcRepository : ProductRepository<Pc>, IPcRepository
{
    public override async Task<Pc?> GetById(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Pcs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p != null)
        {
            p.Discount = await GetDiscount(id);
            p.InMemory = true;
        }
        return p;
    }
    public override async Task<IEnumerable<Pc>?> GetAll()
    {
        using var _context = new ESMDbContext();
        var list = await _context.Pcs.AsNoTracking()
                .Where(pc => pc.Remain > -1)
                .ToListAsync();
        foreach (var item in list)
        {
            item.InMemory = true;
            item.Discount = await GetDiscount(item.Id);
        }
        return list;
    }
    public override async Task<object?> Add(Pc entity)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            await _context.Pcs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public override async Task<object?> Delete(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Pcs.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Update(Pc entity)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            var hd = await _context.Pcs.AsQueryable()
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
    public override async Task<object?> AddList(IEnumerable<Pc> list)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            await _context.Pcs.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) { res = false; }
        return res;
    }
    public override async Task<bool> IsIdExist(string id)
    {
        using var _context = new ESMDbContext();
        return await _context.Pcs.AnyAsync(x => x.Id == id);
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.PC);
    }
}
