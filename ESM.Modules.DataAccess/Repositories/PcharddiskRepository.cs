using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IPcharddiskRepository : IBaseRepository<PcharddiskDTO>, IProductRepository
{
    string GetSuggestID();
}
public class PcharddiskRepository : BaseRepository<PcharddiskDTO>, IPcharddiskRepository
{
    public PcharddiskRepository(ESMDbContext context) : base(context)
    {
    }
    public override PcharddiskDTO? GetById(string id)
    {
        return _context.Pcharddisks.AsQueryable()
                .Where(p => p.Id == id)
                .Select(p => new PcharddiskDTO()
                {
                    Id = p.Id,
                    AvatarPath = @p.AvatarPath,
                    Company = p.Company,
                    Connect = p.Connect,
                    DetailPath = @p.DetailPath,
                    Discount = p.Discount,
                    ImagePath = @p.ImagePath,
                    Name = p.Name,
                    Price = p.Price,
                    Remain = p.Remain,
                    Series = p.Series,
                    Storage = p.Storage,
                    Type = p.Type,
                    Unit = p.Unit
                }).FirstOrDefault();
    }
    public override void Update(PcharddiskDTO entity)
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
    }
    public override IEnumerable<PcharddiskDTO>? GetAll()
    {
        return _context.Pcharddisks.AsQueryable()
                .Where(pcharddisk => pcharddisk.Remain > -1)
                .Select(pcharddisk => new PcharddiskDTO()
                {
                    Name = pcharddisk.Name,
                    Company = pcharddisk.Company,
                    Connect = pcharddisk.Connect,
                    DetailPath = @pcharddisk.DetailPath,
                    Discount = pcharddisk.Discount,
                    Id = pcharddisk.Id,
                    ImagePath = @pcharddisk.ImagePath,
                    Price = pcharddisk.Price,
                    Remain = pcharddisk.Remain,
                    Series = pcharddisk.Series,
                    Storage = pcharddisk.Storage,
                    AvatarPath = @pcharddisk.AvatarPath,
                    Type = pcharddisk.Type,
                    Unit = pcharddisk.Unit
                }).ToList();
    }
    public override void Add(PcharddiskDTO entity)
    {
        Pcharddisk e = new()
        {
            Name = entity.Name,
            Company = entity.Company,
            Connect = entity.Connect,
            DetailPath = entity.DetailPath,
            Discount = entity.Discount,
            Id = entity.Id,
            ImagePath = entity.ImagePath,
            Price = entity.Price,
            Remain = entity.Remain,
            Series = entity.Series,
            Storage = entity.Storage,
            Type = entity.Type,
            AvatarPath = entity.AvatarPath,
            Unit = entity.Unit
        };
        _context.Pcharddisks.Add(e);
    }

    public string GetSuggestID()
    {
        var NewID = _context.Pcharddisks.AsQueryable().OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.HARDDISK] + "0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        var x = StaticData.IdPrefix[ProductType.HARDDISK];
        return StaticData.IdPrefix[ProductType.HARDDISK] + counter.ToString().PadLeft(7, '0');
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

}
