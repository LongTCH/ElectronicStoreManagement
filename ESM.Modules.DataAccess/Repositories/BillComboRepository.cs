using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IBillComboRepository : IBaseRepository<BillCombo> {
        Task<bool> IsComboExistInBill(string comboId);
    }
    public class BillComboRepository : BaseRepository<BillCombo>, IBillComboRepository
    {
        public async Task<bool> IsComboExistInBill(string comboId)
        {
            using var _context = new ESMDbContext();
            return await _context.BillCombos.AnyAsync(x => x.ComboId == comboId);
        }
        public override async Task<object?> Add(BillCombo entity)
        {
            using var _context = new ESMDbContext();
            entity.Id = GetNewID();
            var list = (await _context.Combos.FirstAsync(x => x.Id == entity.ComboId)).ProductIdlist.Split(' ');
            foreach (var item in list)
                DecreaseRemain(item, entity.Number);
            await _context.BillCombos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        private int GetNewID()
        {
            using var _context = new ESMDbContext();
            var lastbill = _context.BillCombos.OrderBy(b => b.Id).LastOrDefault();
            if (lastbill == null) return 0;
            return lastbill.Id + 1;
        }
        private void DecreaseRemain(string id, int number)
        {
            using var _context = new ESMDbContext();
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
            _context.SaveChanges();
        }
    }
}
