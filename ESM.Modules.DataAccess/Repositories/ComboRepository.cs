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
    public interface IComboRepository : IBaseRepository<Combo>
    {
        Task<decimal> GetComboPrice(Combo combo);
        Task<IEnumerable<ProductDTO>> GetListProduct(Combo combo);
    }
    public class ComboRepository : BaseRepository<Combo>, IComboRepository
    {
        public ComboRepository(ESMDbContext context) : base(context)
        {
        }

        public override async Task<Combo?> GetById(string id)
        {
            return await _context.Combos.FirstOrDefaultAsync(x => x.Id == id);
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

            return res * (decimal)(1 - combo.Discount);
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
    }
}
