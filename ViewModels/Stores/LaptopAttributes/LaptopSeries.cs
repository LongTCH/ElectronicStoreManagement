using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.LaptopAttributes;

public class LaptopSeries : IStore
{
    public string Name { get; set; }
    private bool isChecked;
    public bool IsChecked
    {
        get => isChecked;
        set
        {
            isChecked = value;
            CurrentStoreChanged?.Invoke();
        }
    }

    public event Action? CurrentStoreChanged;
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
    public override bool Equals(object? obj)
    {
        return Name == (obj as LaptopSeries)?.Name;
    }
}
