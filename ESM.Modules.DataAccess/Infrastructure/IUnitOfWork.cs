﻿using ESM.Modules.DataAccess.Repositories;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        ILaptopRepository Laptops { get; }
        IMonitorRepository Monitors { get; }
        IPccpuRepository Pccpus { get; }
        IPcRepository Pcs { get; }
        IPcharddiskRepository Pcharddisks { get; }
        ISmartphoneRepository Smartphones { get; }
        IVgaRepository Vgas { get; }
        IBillRepository Bills { get; }
        IComboRepository Combos { get; }
        IBillComboRepository BillCombos { get; }
        IImportRepository Imports { get; }
        IReportRepository Reports { get; }
        IDiscountRepository Discounts { get; }
        IProviderRepository Providers { get; }
    }
}
