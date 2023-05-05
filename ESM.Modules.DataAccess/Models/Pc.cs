﻿namespace ESM.Modules.DataAccess.Models;

public partial class Pc : ProductDTO
{

    private string? cpu;
    public string? Cpu
    {
        get => cpu?.Trim();
        set
        {
            cpu = value;
            InMemory = false;
        }
    }
    private string? ram;
    public string? Ram
    {
        get => ram?.Trim();
        set
        {
            ram = value;
            InMemory = false;
        }
    }
    private string? serires;
    public string? Series
    {
        get => serires?.Trim();
        set
        {
            serires = value;
            InMemory = false;
        }
    }
    private string? need;
    public string? Need
    {
        get => need?.Trim();
        set
        {
            need = value;
            InMemory = false;
        }
    }
    public override void Copy(ProductDTO other)
    {
        var o = (Pc)other;
        Cpu = o.Cpu;
        Ram = o.Ram;
        Series = o.Series;
        Need = o.Need;
        RaisePropertyChanged(nameof(Cpu));
        RaisePropertyChanged(nameof(Series));
        RaisePropertyChanged(nameof(Ram));
        RaisePropertyChanged(nameof(Need));
        base.Copy(other);
    }
}
