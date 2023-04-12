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
        public BillComboRepository(ESMDbContext context) : base(context)
        {
        }

        public async Task<bool> IsComboExistInBill(string comboId)
        {
            // avoid concurrent issue
            using var context = new ESMDbContext();
            return await context.BillCombos.AnyAsync(x => x.ComboId == comboId);
        }
        public override async Task<object?> Add(BillCombo entity)
        {
            bool res = false;
            try
            {
                await _context.BillCombos.AddAsync(entity);
                await _context.SaveChangesAsync();
                res = true;
            } catch (Exception)
            {

            }
            return res;
        }
    }
}
