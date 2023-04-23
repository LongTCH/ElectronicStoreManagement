using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IProductRepository<Models.Monitor>
{
    Task<object?> AddList(IEnumerable<Models.Monitor> list);
}
public class MonitorRepository : ProductRepository<Models.Monitor>, IMonitorRepository
{
    public MonitorRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Models.Monitor>?> GetAll()
    {
        return await _context.Monitors.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToListAsync();
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.MONITOR);
    }
    public override async Task<object?> Add(Models.Monitor entity)
    {
        bool res = true;
        try
        {
            await _context.Monitors.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception) { res = false; }
        return res;
    }
    public override async Task<object?> Update(Models.Monitor entity)
    {
        bool res = true;
        try
        {
            var hd = await _context.Monitors.AsQueryable()
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
    public override async Task<object?> AddList(IEnumerable<Models.Monitor> list)
    {
        bool res = true;
        try
        {
            await _context.Monitors.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) { res = false; }
        return res;
    }
    public override async Task<bool> IsIdExist(string id)
    {
        return await _context.Monitors.AnyAsync(p => p.Id == id);
    }
    public override async Task<object?> Delete(string id)
    {
        var p = await _context.Monitors.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
}
