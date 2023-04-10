using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess
{
    public enum ProductType
    {
        LAPTOP, CPU, MONITOR, HARDDISK, VGA, SMARTPHONE, PC, COMBO
    }
    public enum ReportDuration
    { WEEK, MONTH, QUARTER, YEAR }
    public static class DAStaticData
    {
        public static readonly Dictionary<ProductType, string> IdPrefix = new()
        {
            {ProductType.LAPTOP,"01" },
            {ProductType.PC,"02" },
            {ProductType.MONITOR,"03" },
            {ProductType.HARDDISK,"04" },
            {ProductType.CPU,"05" },
            {ProductType.VGA,"06" },
            {ProductType.SMARTPHONE,"07" },
            {ProductType.COMBO,"99" },
        };
        public static void AddOrUpdate<T>(this DbContext context, T entity, Func<T, object> keySelector)
          where T : class
        {
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var key = keySelector(entity);
                var existingEntity = context.Set<T>().Find(key);

                if (existingEntity != null)
                {
                    context.Entry(existingEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    context.Set<T>().Add(entity);
                }
            }
        }
    }
}
