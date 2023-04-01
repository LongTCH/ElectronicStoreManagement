namespace ESM.Modules.DataAccess
{
    internal enum ProductType
    {
        LAPTOP, CPU, MONITOR, HARDDISK, VGA, SMARTPHONE, PC, COMBO
    }
    public enum ReportDuration
    { WEEK, MONTH, QUARTER, YEAR }
    internal static class StaticData
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
        internal static int GetWeekOfYear(DateTime dateTime)
        {
            return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dateTime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        internal static int GetQuarter(int month)
        {
            return (month - 1) / 3 + 1;
        }
    }
}
