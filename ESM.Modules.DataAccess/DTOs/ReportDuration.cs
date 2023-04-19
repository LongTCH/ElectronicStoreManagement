using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Modules.DataAccess.DTOs
{
    public class ReportMock : IComparable<ReportMock>
    {
        public int Year { get; set; }
        public int Sub { get; set; } = 0;
        public decimal Value { get; set; }

        public int CompareTo(ReportMock? other)
        {
            if (Year.Equals(other.Year)) return Sub.CompareTo(other.Sub);
            return Year.CompareTo(other.Year);
        }
        public override int GetHashCode()
        {
            return (Year^10 + Sub).GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            var other = obj as ReportMock;
            if (other == null) return false;
            if (Year.Equals(other.Year) && Sub.Equals(other.Sub)) return true;
            return false;
        }
        public static ReportMock operator + (ReportMock a, ReportMock b)
        {
            return new ReportMock() { Year = a.Year, Sub = a.Sub, Value = a.Value + b.Value };
        }
    }
}
