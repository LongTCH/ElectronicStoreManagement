﻿using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IImportRepository : IBaseRepository<Import>
    {
        Task<IEnumerable<Import>?> GetAll(DateTime start, DateTime end);
    }
    public class ImportRepository : BaseRepository<Import>, IImportRepository
    {
        public override async Task<Import?> GetById(string id)
        {
            using var _context = new ESMDbContext();
            return await _context.Imports.AsQueryable()
                .Where(b => b.Id == Convert.ToInt32(id))
                .Include(b => b.ImportProducts)
                .FirstOrDefaultAsync();
        }
        public override async Task<object?> Add(Import entity)
        {
            using var _context = new ESMDbContext();
            entity.Id = GetNewID();
            foreach (var item in entity.ImportProducts)
                IncreaseRemain(item.ProductId, item.Number);
            await _context.Imports.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        private int GetNewID()
        {
            using var _context = new ESMDbContext();
            var lastbill = _context.Imports.OrderBy(b => b.Id).LastOrDefault();
            if (lastbill == null) return 0;
            return lastbill.Id + 1;
        }
        private void IncreaseRemain(string id, int number)
        {
            using var _context = new ESMDbContext();
            if (id.StartsWith(DAStaticData.IdPrefix[ProductType.LAPTOP]))
                _context.Laptops.Where(p => p.Id == id).First().Remain += number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.PC]))
                _context.Pcs.Where(p => p.Id == id).First().Remain += number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.MONITOR]))
                _context.Monitors.Where(p => p.Id == id).First().Remain += number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.CPU]))
                _context.Pccpus.Where(p => p.Id == id).First().Remain += number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.HARDDISK]))
                _context.Pcharddisks.Where(p => p.Id == id).First().Remain += number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.SMARTPHONE]))
                _context.Smartphones.Where(p => p.Id == id).First().Remain += number;
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.VGA]))
                _context.Vgas.Where(p => p.Id == id).First().Remain += number;
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Import>?> GetAll(DateTime start, DateTime end)
        {
            using var _context = new ESMDbContext();
            return await _context.Imports
                .Include(x => x.ImportProducts)
                .Include(x => x.Provider)
                .Where(x => x.ImportDate.Date >= start.Date && x.ImportDate.Date <= end.Date)
                .AsNoTracking().ToListAsync();
        }
    }
}
