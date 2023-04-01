namespace ESM.Modules.DataAccess
{
    internal enum ProductType
    {
        LAPTOP, CPU, MONITOR, HARDDISK, VGA, SMARTPHONE, PC, COMBO
    }
    public enum ReportDuration
    { WEEK, MONTH, QUARTER, YEAR }
    internal class StaticData
    {
        internal static readonly Dictionary<ProductType, string> IdPrefix = new()
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
    }
}
