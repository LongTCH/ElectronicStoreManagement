using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly ESMDbContext _context;

    public UnitOfWork(ESMDbContext context)
    {
        _context = context;
        Accounts = new AccountRepository(context);
        Laptops = new LaptopRepository(context);
        Monitors = new MonitorRepository(context);
        Pcs = new PcRepository(context);
        Pccpus = new PccpuRepository(context);
        Pcharddisks = new PcharddiskRepository(context);
        Vgas = new VgaRepository(context);

    }

    public IAccountRepository Accounts { get; private set; }

    public ILaptopRepository Laptops { get; private set; }

    public IMonitorRepository Monitors { get; private set; }

    public IPccpuRepository Pccpus { get; private set; }

    public IPcRepository Pcs { get; private set; }

    public IPcharddiskRepository Pcharddisks { get; private set; }

    public ISmartphoneRepository Smartphones { get; private set; }

    public IVgaRepository Vgas { get; private set; }

    public void Complete()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
