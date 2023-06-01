using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        Task<IEnumerable<Provider>> SearchProviders(string keyword);
    }
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        public override async Task<IEnumerable<Provider>?> GetAll()
        {
            using var _context = new ESMDbContext();
            return await _context.Providers.AsNoTracking().Where(x => x.Phone != "0").ToListAsync();
        }
        public override async Task<Provider?> GetById(string id)
        {
            using var _context = new ESMDbContext();
            return await _context.Providers.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == id && x.Phone != "0");
        }
        public override async Task<object?> Update(Provider entity)
        {
            using var _context = new ESMDbContext();
            bool res = true;
            try
            {
                var hd = await _context.Providers.AsQueryable()
                       .FirstAsync(p => p.Id == entity.Id);
                _context.Entry(hd).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }
        public override async Task<object?> Add(Provider entity)
        {
            using var _context = new ESMDbContext();
            bool res = true;
            try
            {
                await _context.Providers.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { res = false; }
            return res;
        }
        public override async Task<object?> Delete(string id)
        {
            using var _context = new ESMDbContext();
            var p = await _context.Providers.AsQueryable().SingleAsync(p => p.Id.ToString() == id);
            p.Phone = "0";
            await _context.SaveChangesAsync();
            return null;
        }
        public override async Task<string> GetSuggestID()
        {
            using var _context = new ESMDbContext();
            var id = (await _context.Providers.OrderBy(x=>x.Id).LastOrDefaultAsync())?.Id;
            if (id == null) return "1";
            return id.Value + 1 +"";
        }
        public override async Task<bool> IsIdExist(string id)
        {
            using var _context = new ESMDbContext();
            return await _context.Providers.AnyAsync(x => x.Id == int.Parse(id));
        }
        public async Task<IEnumerable<Provider>> SearchProviders(string keyword)
        {
            using var _context = new ESMDbContext();
            var list = await _context.Providers
                        .AsNoTracking()
                        .ToListAsync();
            return list.Where(x => x.ToString().ToUpper().Contains($"{keyword.ToUpper()}"));
        }
    }
}
