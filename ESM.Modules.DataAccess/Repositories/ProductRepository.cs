using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public class ProductRepository<T> : BaseRepository<T> where T : class
    {
        protected async Task<string> GetSuggestID(ProductType type)
        {
            using var _context = new ESMDbContext();
            string? NewID = null;
            if (type == ProductType.LAPTOP) NewID = (await _context.Laptops.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.MONITOR) NewID = (await _context.Monitors.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.PC) NewID = (await _context.Pcs.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.CPU) NewID = (await _context.Pccpus.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.HARDDISK) NewID = (await _context.Pcharddisks.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.SMARTPHONE) NewID = (await _context.Smartphones.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.VGA) NewID = (await _context.Vgas.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;
            else if (type == ProductType.COMBO) NewID = (await _context.Combos.OrderBy(p => p.Id).LastOrDefaultAsync())?.Id;

            if (NewID == null) return DAStaticData.IdPrefix[type] + "0000000";
            int counter = Convert.ToInt32(NewID[2..]);
            ++counter;
            return DAStaticData.IdPrefix[type] + counter.ToString().PadLeft(7, '0');
        }
        protected async Task<double> GetDiscount(string id)
        {
            using var _context = new ESMDbContext();
            double res = 0;
            var list = await _context.Discounts
                .Where(x => ((DateTime)x.StartDate!).Date <= DateTime.Now.Date && ((DateTime)x.EndDate!) >= DateTime.Now)
                .Where(x => EF.Functions.Like(x.ProductIdlist!, $"%{id}%")).ToListAsync();
            foreach (var item in list) res += item.Discount1 ?? 0;
            return res;
        }
        public virtual async Task<IEnumerable<T>?> SearchProduct(string keyword)
        {
            using var _context = new ESMDbContext();
            IEnumerable<T>? list = null;
            if (typeof(T).Equals(typeof(Laptop)))
            {
                list = (IEnumerable<T>)(object)await _context.Laptops
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            else if (typeof(T).Equals(typeof(Models.Monitor)))
            {
                list = (IEnumerable<T>)(object)await _context.Monitors
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            else if (typeof(T).Equals(typeof(Pccpu)))
            {
                list = (IEnumerable<T>)(object)await _context.Pccpus
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            else if (typeof(T).Equals(typeof(Pcharddisk)))
            {
                list = (IEnumerable<T>)(object)await _context.Pcharddisks
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            else if (typeof(T).Equals(typeof(Smartphone)))
            {
                list = (IEnumerable<T>)(object)await _context.Smartphones
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            else if (typeof(T).Equals(typeof(Vga)))
            {
                list = (IEnumerable<T>)(object)await _context.Vgas
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            else if (typeof(T).Equals(typeof(Pc)))
            {
                list = (IEnumerable<T>)(object)await _context.Pcs
                    .Where(x => EF.Functions.Like(x.Name.ToUpper(), $"%{keyword.ToUpper()}%"))
                    .AsNoTracking().ToListAsync();
            }
            
            return list;
        }
    }
}
