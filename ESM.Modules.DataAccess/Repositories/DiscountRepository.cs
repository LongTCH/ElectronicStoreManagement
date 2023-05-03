using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<IEnumerable<ProductDTO>> GetListProduct(Discount discount);
    }
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        public override async Task<object?> Add(Discount entity)
        {
            using var _context = new ESMDbContext();
            bool res = false;
            try
            {
                await _context.Discounts.AddAsync(entity);
                await _context.SaveChangesAsync();
                res = true;
            }
            catch (Exception) { }
            return res;
        }
        public override async Task<Discount?> GetById(string id)
        {
            using var _context = new ESMDbContext();
            var res = await _context.Discounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
            return res;
        }
        public override async Task<IEnumerable<Discount>?> GetAll()
        {
            using var _context = new ESMDbContext();
            var list = await _context.Discounts.AsNoTracking().ToListAsync();
            List<Discount>? result = new();
            foreach (var item in list)
            {
                item.ListProducts = await GetListProduct(item);
                result.Add(item);
            }
            return result;
        }
        public override async Task<object?> Update(Discount entity)
        {
            using var _context = new ESMDbContext();
            bool res = true;
            try
            {
                var hd = await _context.Discounts.AsQueryable()
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
        public async Task<IEnumerable<ProductDTO>> GetListProduct(Discount discount)
        {
            List<ProductDTO> products = new();
            Task task = new(() =>
            {
                if (string.IsNullOrWhiteSpace(discount.ProductIdlist)) return;
                var list = discount.ProductIdlist.Split(' ');
                foreach (var item in list)
                {
                    products.Add(GetProduct(item));
                }
            });
            task.Start();
            await task;
            return products;
        }

    }
}
