using ESM.Modules.DataAccess.Models;
using ESM.Modules.DataAccess.Repositories;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        public IAccountRepository Accounts { get; }

        public ILaptopRepository Laptops { get; }

        public IMonitorRepository Monitors { get; }

        public IPccpuRepository Pccpus { get; }

        public IPcRepository Pcs { get; }

        public IPcharddiskRepository Pcharddisks { get; }

        public ISmartphoneRepository Smartphones { get; }

        public IVgaRepository Vgas { get; }

        public IBillRepository Bills { get; }
        public IComboRepository Combos { get; }

        public IBillComboRepository BillCombos { get; }

        public IImportRepository Imports { get; }

        public IReportRepository Reports { get; }
        public IDiscountRepository Discounts { get; }
        public IProviderRepository Providers { get; }

        public UnitOfWork()
        {
            Accounts = new AccountRepository();
            Laptops = new LaptopRepository();
            Monitors = new MonitorRepository();
            Pcs = new PcRepository();
            Pccpus = new PccpuRepository();
            Pcharddisks = new PcharddiskRepository();
            Vgas = new VgaRepository();
            Smartphones = new SmartphoneRepository();
            Bills = new BillRepository();
            Combos = new ComboRepository();
            BillCombos = new BillComboRepository();
            Imports = new ImportRepository();
            Reports = new ReportRepository();
            Discounts = new DiscountRepository();
            Providers = new ProviderRepository();
        }
    }
}
