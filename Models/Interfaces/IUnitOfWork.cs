using System;

namespace Models.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    ILaptopRepository Laptops { get; }
    IMonitorRepository Monitors { get; }
    IPccpuRepository Pccpus { get; }
    IPcRepository Pcs { get; }
    IPcharddiskRepository Pcharddisks { get; }
    ISmartphoneRepository Smartphones { get; }
    IVgaRepository Vgas { get; }
    void Complete();
}
