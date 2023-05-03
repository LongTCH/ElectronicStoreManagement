using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface ISmartphoneRepository : IProductRepository<Smartphone> {
    }
    public class SmartphoneRepository : ProductRepository<Smartphone>,  ISmartphoneRepository
    {
        public SmartphoneRepository(ESMDbContext context) : base(context)
        {
        }
        public override async Task<Smartphone?> GetById(string id)
        {
            var p = await _context.Smartphones.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (p != null)
            {
                p.Discount = await GetDiscount(id);
                p.InMemory = true;
            }
            return p;
        }
        public override async Task<IEnumerable<Smartphone>?> GetAll()
        {
            var list = await _context.Smartphones.AsNoTracking()
                    .Where(smartphone => smartphone.Remain > -1)
                    .ToListAsync();
            foreach (var item in list)
            {
                item.InMemory = true;
                item.Discount = await GetDiscount(item.Id);
            }
            return list;
        }
        public override async Task<object?> Add(Smartphone entity)
        {
            bool res = true;
            try
            {
                await _context.Smartphones.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { res = false; }
            return res;
        }
        public override async Task<object?> Update(Smartphone entity)
        {
            bool res = true;
            try
            {
                var hd = await _context.Smartphones.AsQueryable()
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
            var p = await _context.Smartphones.SingleAsync(p => p.Id == id);
            p.Remain = -1;
            await _context.SaveChangesAsync();
            return null;
        }
        public override async Task<bool> IsIdExist(string id)
        {
            return await _context.Smartphones.AnyAsync(p => p.Id == id);
        }
        public override async Task<object?> AddList(IEnumerable<Smartphone> list)
        {
            bool res = true;
            try
            {
                await _context.Smartphones.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { res = false; }
            return res;
        }
        public async Task<string> GetSuggestID()
        {
            return await GetSuggestID(ProductType.SMARTPHONE);
        }
    }
}
