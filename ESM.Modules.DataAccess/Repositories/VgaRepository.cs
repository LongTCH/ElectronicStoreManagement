using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IVgaRepository : IProductRepository<Vga>{
}
public class VgaRepository : ProductRepository<Vga>, IVgaRepository
{
    public VgaRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<Vga?> GetById(string id)
    {
        var p = await _context.Vgas.FirstOrDefaultAsync(x => x.Id == id);
        if (p != null) p.Discount = await GetDiscount(id);
        return p;
    }
    public override async Task<IEnumerable<Vga>?> GetAll()
    {
        var list = await _context.Vgas.AsQueryable()
                .Where(vga => vga.Remain > -1)
               .ToListAsync();
        foreach (var item in list) item.Discount = await GetDiscount(item.Id);
        return list;
    }
    public override async Task<object?> Add(Vga entity)
    {
        bool res = true;
        try
        {
            await _context.Vgas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public override async Task<object?> Update(Vga entity)
    {
        bool res = true;
        try
        {
            var hd = await _context.Vgas.AsQueryable()
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
    public override async Task<bool> IsIdExist(string id)
    {
        return await _context.Vgas.AnyAsync(p => p.Id == id);
    }
    public override async Task<object?> AddList(IEnumerable<Vga> list)
    {
        bool res = true;
        try
        {
            await _context.Vgas.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.VGA);
    }
    public override async Task<object?> Delete(string id)
    {
        var p = await _context.Vgas.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
}
