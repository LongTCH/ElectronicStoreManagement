﻿namespace ESM.Modules.DataAccess.Models;

public partial class Monitor : ProductDTO
{
    private string? size;
    public string? Size
    {
        get => size;
        set
        {
            size = value;
            InMemory = false;
        }
    }
    private string? panel;
    public string? Panel
    {
        get => panel;
        set
        {
            panel = value;
            InMemory = false;
        }
    }
    private string? refreshRate;
    public string? RefreshRate
    {
        get => refreshRate;
        set
        {
            refreshRate = value;
            InMemory = false;
        }
    }
    private string? serires;
    public string? Series
    {
        get => serires;
        set
        {
            serires = value;
            InMemory = false;
        }
    }
    private string? need;
    public string? Need
    {
        get => need;
        set
        {
            need = value;
            InMemory = false;
        }
    }
}
