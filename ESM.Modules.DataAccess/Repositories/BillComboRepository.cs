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
    }
}
