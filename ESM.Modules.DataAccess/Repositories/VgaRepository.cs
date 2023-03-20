using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IVgaRepository : IBaseRepository<VgaDTO> { }
public class VgaRepository : BaseRepository<VgaDTO>, IVgaRepository
{
    public VgaRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<VgaDTO>? GetAll()
    {
        return (from vga in _context.Vgas
                select new VgaDTO()
                {
                    Name = vga.Name,
                    Chip = vga.Chip,
                    Chipset = vga.Chipset,
                    Company = vga.Company,
                    DetailPath = @vga.DetailPath,
                    Discount = vga.Discount,
                    Gen = vga.Gen,
                    Id = vga.Id,
                    ImagePath = @vga.ImagePath,
                    Need = vga.Need,
                    AvatarPath = @vga.AvatarPath,
                    Series = vga.Series,
                    Unit = vga.Unit
                }).ToList();
    }
}
