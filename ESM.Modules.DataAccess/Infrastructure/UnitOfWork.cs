using ESM.Modules.DataAccess.Models;
using ESM.Modules.DataAccess.Repositories;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ESMDbContext _context;
        private bool _disposed = false;

        public IAccountRepository Accounts { get; }

        public ILaptopRepository Laptops { get; }

        public IMonitorRepository Monitors { get; }

        public IPccpuRepository Pccpus { get; }

        public IPcRepository Pcs { get; }

        public IPcharddiskRepository Pcharddisks { get; }

        public ISmartphoneRepository Smartphones { get; }

        public IVgaRepository Vgas { get; }

        public IBillRepository Bills { get; }

        public UnitOfWork()
        {
            //_context = context;
            Accounts = new AccountRepository(new ESMDbContext());
            Laptops = new LaptopRepository(new ESMDbContext());
            Monitors = new MonitorRepository(new ESMDbContext());
            Pcs = new PcRepository(new ESMDbContext());
            Pccpus = new PccpuRepository(new ESMDbContext());
            Pcharddisks = new PcharddiskRepository(new ESMDbContext());
            Vgas = new VgaRepository(new ESMDbContext());
            Smartphones = new SmartphoneRepository(new ESMDbContext());
            Bills = new BillRepository(new ESMDbContext());
        }

        public void Dispose()
        {
            
        }
        //public async Task<int> SaveChangesAsync()
        //{
        //    int res;
        //    try
        //    {
        //        res = await _context.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        res = -1;
        //    }
        //    return res;
        //}
        //#region Dispose
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }

        //        _disposed = true;
        //    }
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}


        //~UnitOfWork()
        //{
        //    Dispose(false);
        //}
        //#endregion
    }
}
