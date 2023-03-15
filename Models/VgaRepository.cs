using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class VgaRepository : Repository<VgaDTO>, IVgaRepository
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
                    AvatarPath= @vga.AvatarPath,
                    Series = vga.Series,
                    Unit = vga.Unit
                }).ToList();
    }
}
