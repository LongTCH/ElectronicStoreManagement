using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IComboRepository : IProductRepository<Combo>
    {
        Task<decimal> GetComboPrice(Combo combo);
        Task<IEnumerable<ProductDTO>> GetListProduct(Combo combo);
        Task<int> GetRemain(Combo combo);
    }
    public class ComboRepository : ProductRepository<Combo>, IComboRepository
    {
        public override async Task<object?> Add(Combo entity)
        {
            using var _context = new ESMDbContext();
            bool res = true;
            try
            {
                await _context.Combos.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { res = false; }
            return res;
        }
        public override async Task<object?> AddList(IEnumerable<Combo> list)
        {
            using var _context = new ESMDbContext();
            bool res = true;
            try
            {
                await _context.Combos.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { res = false; }
            return res;
        }
        public override async Task<Combo?> GetById(string id)
        {
            using var _context = new ESMDbContext();
            var res = await _context.Combos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
            {
                var validItem = await GetRemain(res);
                if (validItem > -1)
                {
                    res.Remain = validItem;
                    res.ListProducts = await GetListProduct(res);
                    res.Price = await GetComboPrice(res);
                }
                else res = null;
            }
            return res;
        }
        public override async Task<IEnumerable<Combo>?> GetAll()
        {
            using var _context = new ESMDbContext();
            var list = await _context.Combos.AsNoTracking().Where(x => x.Status == true).ToListAsync();
            List<Combo>? result = new();
            foreach (var item in list)
            {
                var validItem = await GetRemain(item);
                if (validItem > -1)
                {
                    item.Remain = validItem;
                    item.ListProducts = await GetListProduct(item);
                    item.Price = await GetComboPrice(item);
                    result.Add(item);
                }
            }
            return result;
        }
        public override async Task<object?> Update(Combo entity)
        {
            using var _context = new ESMDbContext();
            bool res = true;
            try
            {
                var hd = await _context.Combos.AsQueryable()
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
            using var _context = new ESMDbContext();
            return await _context.Combos.AnyAsync(x => x.Id == id);
        }
        public async Task<decimal> GetComboPrice(Combo combo)
        {
            decimal res = 0;
            await Task.Run(() =>
            {
                string[] list = combo.ProductIdlist.Split(' ');
                foreach (var item in list)
                {
                    res += GetProduct(item).Price;
                }
            });
            return res;
        }
        public override async Task<object?> Delete(string id)
        {
            using var _context = new ESMDbContext();
            var res = false;
            try
            {
                var item = await _context.Combos.FirstAsync(x => x.Id == id);
                item.Status = false;
                await _context.SaveChangesAsync();
                res = true;
            }
            catch { }
            return res;

        }
        public async Task<IEnumerable<ProductDTO>> GetListProduct(Combo combo)
        {
            List<ProductDTO> products = new();
            Task task = new(() =>
            {
                if (string.IsNullOrWhiteSpace(combo.ProductIdlist)) return;
                var list = combo.ProductIdlist.Split(' ');
                foreach (var item in list)
                {
                    products.Add(GetProduct(item));
                }
            });
            task.Start();
            await task;
            return products;
        }
        public async Task<int> GetRemain(Combo combo)
        {
            int res = int.MaxValue;
            Task task = new(() =>
            {
                string[] list = combo.ProductIdlist.Split(' ');
                foreach (var item in list)
                {
                    res = Math.Min(res, GetProduct(item).Remain);
                }
            });
            task.Start();
            await task;

            return res;
        }
        public async Task<string> GetSuggestID()
        {
            return await GetSuggestID(ProductType.COMBO);
        }
        public override async Task<IEnumerable<Combo>?> SearchProduct(string keyword)
        {
            using var _context = new ESMDbContext();
            var list = await _context.Combos
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            List<Combo>? result = new();
            foreach (var item in list)
            {
                var validItem = await GetRemain(item);
                if (validItem > -1)
                {
                    item.Remain = validItem;
                    item.ListProducts = await GetListProduct(item);
                    item.Price = await GetComboPrice(item);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
