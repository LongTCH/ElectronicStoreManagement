using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Modules.DataAccess.DTOs
{
    public class ReportMock : IComparable<ReportMock>
    {
        public int Year { get; set; }
        public int Sub { get; set; } = 0;
        public int Value { get; set; }

        public int CompareTo(ReportMock? other)
        {
            if (Year.Equals(other.Year)) return Sub.CompareTo(other.Sub);
            return Year.CompareTo(other.Year);
        }
        public override bool Equals(object? obj)
        {
            var other = obj as ReportMock;
            if (other == null) return false;
            if (Year.Equals(other.Year) && Sub.Equals(other.Sub)) return true;
            return false;
        }
    }
    public class RevenueDTO : IComparable<RevenueDTO>
    {
        public int Year { get; set; }
        public int Sub { get; set; }
        public decimal Value { get; set; }

        public int CompareTo(RevenueDTO? other)
        {
            if (Year.Equals(other.Year)) return Sub.CompareTo(other.Sub);
            return Year.CompareTo(other.Year);
        }
        public override bool Equals(object? obj)
        {
            var other = obj as RevenueDTO;
            if (other == null) return false;
            if (Year.Equals(other.Year) && Sub.Equals(other.Sub)) return true;
            return false;
        }
    }
}
