﻿using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace ESM.Modules.DataAccess.Models;

public partial class Provider : BindableBase, IEquatable<Provider>
{
    public int Id { get; set; }

    public string? ProviderName { get; set; }
    private string? phone;
    public string? Phone
    {
        get => phone;
        set
        {
            phone = value;
            ValidateProperty(value, nameof(Phone));
        }
    }

    public virtual ICollection<Import> Imports { get; } = new List<Import>();

    public bool Equals(Provider? other)
    {
        if (other is null) return false;
        return Id == other.Id;
    }
    public override string ToString()
    {
        return Id + " " + ProviderName;
    }
    public void Copy(Provider other)
    {
        Id = other.Id;
        ProviderName = other.ProviderName;
        Phone = other.Phone;
        RaisePropertyChanged(nameof(Id));
        RaisePropertyChanged(nameof(ProviderName));
        RaisePropertyChanged(nameof(Phone));
    }
    void ValidateProperty<TProp>(TProp value, string name)
    {
        Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        {
            MemberName = name
        });
    }
}