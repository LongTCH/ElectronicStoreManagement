using System.Collections.Generic;

namespace ViewModels.Commands;

public class GetGenderListCommand
{
    private readonly List<string> _genderList = new() { "Nam", "Nữ" };
    public GetGenderListCommand() { }
    public List<string> Execute() => _genderList;
}
