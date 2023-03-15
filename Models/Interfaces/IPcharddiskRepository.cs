using Models.DTOs;

namespace Models.Interfaces;

public interface IPcharddiskRepository : IRepository<PcharddiskDTO>
{
    string GetLastID();
}
