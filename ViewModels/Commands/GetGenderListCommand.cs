using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commands;

public class GetGenderListCommand
{
    private readonly List<string> _genderList = new() { "Nam", "Nữ" };
    public GetGenderListCommand() { }
    public List<string> Execute() => _genderList;
}
