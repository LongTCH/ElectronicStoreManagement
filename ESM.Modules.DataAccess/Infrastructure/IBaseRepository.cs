using ESM.Modules.DataAccess.DTOs;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(string id);
        IEnumerable<T>? GetAll();
        object? Add(T entity);
        object? Delete(string id);
        object? Update(T entity);
        bool Any(string id);
        
    }
    public interface IProductRepository<T> : IBaseRepository<T> where T: ProductDTO
    {
        string GetSuggestID();
        IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate);
        IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate);
        IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate);
        IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate);
        IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number);
        IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate);
    }
}
