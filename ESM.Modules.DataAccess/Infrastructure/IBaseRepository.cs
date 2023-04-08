using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(string id);
        Task<IEnumerable<T>?> GetAll();
        Task<object?> Add(T entity); 
        Task<object?> AddList(IEnumerable<T> list);
        Task<object?> Delete(string id);
        Task<object?> Update(T entity);
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
