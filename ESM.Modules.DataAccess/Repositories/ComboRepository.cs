using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ComboRepository(ESMDbContext context) : base(context)
        {
        }
        public override async Task<object?> AddList(IEnumerable<Combo> list)
        {
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
            return await _context.Combos.FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task<IEnumerable<Combo>?> GetAll()
        {
            var list = await _context.Combos.ToListAsync();
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
            return await _context.Combos.AnyAsync(x => x.Id == id);
        }
        public async Task<decimal> GetComboPrice(Combo combo)
        {
            decimal res = 0;
            Task task = new(() =>
            {
                string[] list = combo.ProductIdlist.Split(' ');
                foreach (var item in list)
                {
                    res += GetProduct(item).SellPrice;
                }
            });
            task.Start();
            await task;

            return res;
        }
        public override async Task<object?> Delete(string id)
        {
            var res = false;
            try
            {
                var item = await _context.Combos.FirstAsync(x => x.Id == id);
                _context.Combos.Remove(item);
                await _context.SaveChangesAsync();
                res = true;
            }
            catch { }
            return res;

        }
        private ProductDTO GetProduct(string id)
        {
            if (id.StartsWith(DAStaticData.IdPrefix[ProductType.LAPTOP]))
            {
                return _context.Laptops.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.MONITOR]))
            {
                return _context.Monitors.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.HARDDISK]))
            {
                return _context.Pcharddisks.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.CPU]))
            {
                return _context.Pccpus.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.SMARTPHONE]))
            {
                return _context.Smartphones.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.VGA]))
            {
                return _context.Vgas.First(x => x.Id == id);
            }
            return _context.Pcs.First(x => x.Id == id);
        }

        public async Task<IEnumerable<ProductDTO>> GetListProduct(Combo combo)
        {
            List<ProductDTO> products = new();
            Task task = new(() =>
            {
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
        public string GetSuggestID()
        {
            return GetSuggestID(ProductType.COMBO);
        }
    }
}
