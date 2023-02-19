using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commands;

public class GetConditionsCommand
{
    private readonly List<string> conditions = new() { "None", "Giá tăng dần", "Giá giảm dần", "Bán chạy" };

    public GetConditionsCommand()
    {
    }
    public List<string> Execute() => conditions;
}
