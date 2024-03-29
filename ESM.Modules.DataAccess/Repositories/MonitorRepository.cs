﻿using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IProductRepository<Models.Monitor>
{
    
}
public class MonitorRepository : ProductRepository<Models.Monitor>, IMonitorRepository
{
    public override async Task<Models.Monitor?> GetById(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Monitors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p != null)
        {
            p.Discount = await GetDiscount(id);
        }
        return p;
    }
    public override async Task<IEnumerable<Models.Monitor>?> GetAll()
    {
        using var _context = new ESMDbContext();
        var list = await _context.Monitors.AsNoTracking()
                .Where(p => p.Remain > -1)
                .ToListAsync();
        foreach (var item in list)
        {
            
            item.Discount = await GetDiscount(item.Id);
        }
        return list;
    }
    public async Task<string> GetSuggestID()
    {
        return await GetSuggestID(ProductType.MONITOR);
    }
    public override async Task<object?> Add(Models.Monitor entity)
    {
        using var _context = new ESMDbContext();
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
        using var _context = new ESMDbContext();
        bool res = true;
        try
        {
            var hd = await _context.Monitors.AsNoTracking()
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
        using var _context = new ESMDbContext();
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
        using var _context = new ESMDbContext();
        return await _context.Monitors.AnyAsync(p => p.Id == id);
    }
    public override async Task<object?> Delete(string id)
    {
        using var _context = new ESMDbContext();
        var p = await _context.Monitors.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
}
