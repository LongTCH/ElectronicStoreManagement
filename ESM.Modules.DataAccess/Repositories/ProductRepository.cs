﻿using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace ESM.Modules.DataAccess.Repositories
{
    public class ProductRepository<T> : BaseRepository<T> where T : class
    {
        public ProductRepository(ESMDbContext context) : base(context)
        {
        }
        protected async Task<string> GetSuggestID(ProductType type)
        {
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
    }
}
