using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface ILaptopRepository : IProductRepository<Laptop>
{
}
public class LaptopRepository : ProductRepository<Laptop>, ILaptopRepository
{
    public override async Task<Laptop?> GetById(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Laptops.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p != null)
        {
            p.Discount = await GetDiscount(id);
            p.InMemory = true;
        }
        return p;
    }
    public override async Task<IEnumerable<Laptop>?> GetAll()
    {
        using var _context = new ESMDbContext();
        var list = await _context.Laptops
                .Where(p => p.Remain > -1)
                .ToListAsync();
        foreach (var item in list)
        {
            item.InMemory = true;
            item.Discount = await GetDiscount(item.Id);
        }
        return list;
    }
    public override async Task<object?> Add(Laptop entity)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            await _context.Laptops.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public override async Task<object?> Update(Laptop entity)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            var hd = await _context.Laptops.AsQueryable()
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
    public override async Task<object?> Delete(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Laptops.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.LAPTOP);
    }

    public override async Task<object?> AddList(IEnumerable<Laptop> list)
    {
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            await _context.Laptops.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) { res = false; }
        return res;
    }
    public override async Task<bool> IsIdExist(string id)
    {
        using var _context = new ESMDbContext();
        return await _context.Laptops.AnyAsync(x => x.Id == id);
    }
}
