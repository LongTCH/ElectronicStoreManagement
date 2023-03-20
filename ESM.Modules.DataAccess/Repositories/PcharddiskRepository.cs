using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IPcharddiskRepository : IBaseRepository<PcharddiskDTO>
{
    string GetLastID();
}
public class PcharddiskRepository : BaseRepository<PcharddiskDTO>, IPcharddiskRepository
{
    public PcharddiskRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PcharddiskDTO>? GetAll()
    {
        return (from pcharddisk in _context.Pcharddisks
                select new PcharddiskDTO()
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

    public string GetLastID()
    {
        string? NewID = _context.Pcharddisks.OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return "040000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return "04" + counter.ToString().PadLeft(7, '0');
    }
}
