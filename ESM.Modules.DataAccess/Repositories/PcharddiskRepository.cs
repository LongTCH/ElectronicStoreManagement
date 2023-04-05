using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IPcharddiskRepository : IProductRepository<Pcharddisk>
{
}
public class PcharddiskRepository : ProductRepository<Pcharddisk>, IPcharddiskRepository
{
    public PcharddiskRepository(ESMDbContext context) : base(context)
    {
    }
    public override Pcharddisk? GetById(string id)
    {
        return _context.Pcharddisks.AsQueryable()
                .Where(p => p.Id == id)
                .FirstOrDefault();
    }
    public override object? Update(Pcharddisk entity)
    {
        var hd = _context.Pcharddisks.AsQueryable()
               .First(p => p.Id == entity.Id);
        hd.AvatarPath = entity.AvatarPath;
        hd.Company = entity.Company;
        hd.Connect = entity.Connect;
        hd.DetailPath = entity.DetailPath;
        hd.Discount = entity.Discount;
        hd.ImagePath = entity.ImagePath;
        hd.Name = entity.Name;
        hd.Price = entity.Price;
        hd.Remain = entity.Remain;
        hd.Unit = entity.Unit;
        hd.Storage = entity.Storage;
        hd.Series = entity.Series;
        return null;
    }
    public override IEnumerable<Pcharddisk>? GetAll()
    {
        return _context.Pcharddisks.AsQueryable()
                .Where(pcharddisk => pcharddisk.Remain > -1)
                .ToList();
    }
    public override object? Add(Pcharddisk entity)
    {
        _context.Pcharddisks.Add(entity);
        return null;
    }

    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.CPU);
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.HARDDISK);
    }
    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.HARDDISK, number);
    }
    public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueWeekDuration(startDate, endDate, ProductType.HARDDISK);
    }
}
