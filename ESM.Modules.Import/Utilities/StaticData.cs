using System.Collections.Generic;

namespace ESM.Modules.Import.Utilities
{
    public class StaticData
    {
        public static readonly IEnumerable<string> RoleList = new[] { "Quản trị viên", "Nhân viên bán hàng", "Nhân viên nhập liệu" };
        public enum RoleToNumber
        {
            ADMIN = 0, SELL = 1, TYPING = 2
        }
    }
}
