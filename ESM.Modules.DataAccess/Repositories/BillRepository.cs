using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
    }
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(ESMDbContext context) : base(context)
        {
        }
        public override Bill? GetById(string id)
        {
            return _context.Bills.AsQueryable()
                .Where(b => b.Id == Convert.ToInt32(id))
                .Include(b => b.BillProducts)
                .FirstOrDefault();
        }
        public override void Add(Bill entity)
        {
            entity.Id = GetNewID();
            foreach (var item in entity.BillProducts)
                DecreaseRemain(item.ProductId, item.Number);
            _context.Bills.Add(entity);
        }
        private int GetNewID()
        {
            var lastbill = _context.Bills.OrderBy(b => b.Id).LastOrDefault();
            if (lastbill == null) return 0;
            return lastbill.Id + 1;
        }
        private void DecreaseRemain(string id, int number)
        {
            if (id.StartsWith(StaticData.IdPrefix[ProductType.LAPTOP]))
                _context.Laptops.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(StaticData.IdPrefix[ProductType.PC]))
                _context.Pcs.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(StaticData.IdPrefix[ProductType.MONITOR]))
                _context.Monitors.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(StaticData.IdPrefix[ProductType.CPU]))
                _context.Pccpus.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(StaticData.IdPrefix[ProductType.HARDDISK]))
                _context.Pcharddisks.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(StaticData.IdPrefix[ProductType.SMARTPHONE]))
                _context.Smartphones.Where(p => p.Id == id).First().Remain -= number;
            else if (id.StartsWith(StaticData.IdPrefix[ProductType.VGA]))
                _context.Vgas.Where(p => p.Id == id).First().Remain -= number;
        }
    }
}
