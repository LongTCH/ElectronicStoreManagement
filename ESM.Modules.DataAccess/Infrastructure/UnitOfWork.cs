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
            Smartphones = new SmartphoneRepository(context);
            Bills = new BillRepository(context);
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
