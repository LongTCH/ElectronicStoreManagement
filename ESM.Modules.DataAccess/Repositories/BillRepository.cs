using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        Task<bool> IsProductExistInBill(string productId);
    }
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(ESMDbContext context) : base(context)
        {
        }
        public override async Task<Bill?> GetById(string id)
        {
            return await _context.Bills.AsQueryable()
                .Where(b => b.Id == Convert.ToInt32(id))
                .Include(b => b.BillProducts)
                .FirstOrDefaultAsync();
        }
        public override async Task<object?> Add(Bill entity)
        {
            entity.Id = GetNewID();
            foreach (var item in entity.BillProducts)
                DecreaseRemain(item.ProductId, item.Number);
            await _context.Bills.AddAsync(entity);
            return entity.Id;
        }
        private int GetNewID()
        {
            var lastbill = _context.Bills.OrderBy(b => b.Id).LastOrDefault();
            if (lastbill == null) return 0;
            return lastbill.Id + 1;
        }
        private void DecreaseRemain(string id, int number)
        {
            if (id.StartsWith(DAStaticData.IdPrefix[ProductType.LAPTOP]))
                _context.Laptops.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.PC]))
                _context.Pcs.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.MONITOR]))
                _context.Monitors.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.CPU]))
                _context.Pccpus.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.HARDDISK]))
                _context.Pcharddisks.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.SMARTPHONE]))
                _context.Smartphones.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.VGA]))
                _context.Vgas.Where(p => p.Id == id).First().Remain -= number;
        }

        public async Task<bool> IsProductExistInBill(string productId)
        {
            return await _context.BillProducts.AnyAsync(x=> x.ProductId == productId);
        }
    }
}
