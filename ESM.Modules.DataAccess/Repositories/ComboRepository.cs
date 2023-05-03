using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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
        public override async Task<object?> Add(Combo entity)
        {
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
            var res = await _context.Combos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (res != null) res.Discount = await GetDiscount(res.Id);
            return res;
        }
        public override async Task<IEnumerable<Combo>?> GetAll()
        {
            var list = await _context.Combos.AsNoTracking().ToListAsync();
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
                    res += GetProduct(item).Price;
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

    }
}
