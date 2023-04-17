using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface ILaptopRepository : IProductRepository<Laptop>
{
}
public class LaptopRepository : ProductRepository<Laptop>, ILaptopRepository
{
    public LaptopRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Laptop>?> GetAll()
    {
        return await _context.Laptops
                .Where(p => p.Remain > -1)
                .ToListAsync();
    }
    public override async Task<object?> Add(Laptop entity)
    {
        await _context.Laptops.AddAsync(entity);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Update(Laptop entity)
    {
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
        var p = await _context.Laptops.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.LAPTOP);
    }

    public override async Task<object?> AddList(IEnumerable<Laptop> list)
    {
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
        return await _context.Laptops.AnyAsync(x => x.Id == id);
    }
}
