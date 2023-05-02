using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPcRepository : IProductRepository<Pc>
{
}
public class PcRepository : ProductRepository<Pc>, IPcRepository
{
    public PcRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<Pc?> GetById(string id)
    {
        var p = await _context.Pcs.FirstOrDefaultAsync(x => x.Id == id);
        if (p != null) p.Discount = await GetDiscount(id);
        return p;
    }
    public override async Task<IEnumerable<Pc>?> GetAll()
    {
        var list = await _context.Pcs.AsQueryable()
                .Where(pc => pc.Remain > -1)
                .ToListAsync();
        foreach (var item in list) item.Discount = await GetDiscount(item.Id);
        return list;
    }
    public override async Task<object?> Add(Pc entity)
    {
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
        var p = await _context.Pcs.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Update(Pc entity)
    {
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
        return await _context.Pcs.AnyAsync(x => x.Id == id);
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.PC);
    }
}
