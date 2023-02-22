using System;

namespace ViewModels.Stores;

public class ProductAttributeStore: IStore
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
        return Name == ((ProductAttributeStore)obj)?.Name;
    }
}
