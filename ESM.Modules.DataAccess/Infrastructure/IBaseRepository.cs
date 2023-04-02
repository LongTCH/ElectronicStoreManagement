using ESM.Modules.DataAccess.DTOs;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(string id);
        IEnumerable<T>? GetAll();
        void Add(T entity);
        void Delete(string id);
        void Update(T entity);
        bool Any(string id);
        
    }
    public interface IProductRepository
    {
        IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate);
        IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate);
        IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate);
        IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate);
    }
}
