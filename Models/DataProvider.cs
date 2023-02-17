using Models.DTOs;
using Models.Entities;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models;

public class DataProvider
{
    private readonly ESMDbContext _context;
    public DataProvider(ESMDbContext dbContext)
    {
        _context = dbContext;
    }
    public void ChangeUser(AccountDTO accountDTO)
    {
        var account = (from ac in _context.Accounts
                       where ac.Id == accountDTO.Id
                       select ac).First();
        account.FirstName = accountDTO.FirstName;
        account.LastName = accountDTO.LastName;
        account.EmailAddress = accountDTO.EmailAddress;
        account.Phone = accountDTO.Phone;
        account.AvatarPath = accountDTO.AvatarPath;
        account.Birthday = accountDTO.Birthday;
        account.City = accountDTO.City;
        account.SubDistrict = accountDTO.SubDistrict;
        account.District = accountDTO.District;
        account.Street = accountDTO.Street;
        account.Sex = accountDTO.Sex;
        _context.SaveChanges();
    }
    public AccountDTO? GetAcount(string? id, string? password = null)
    {
        ScryptEncoder encoder = new ScryptEncoder();
        AccountDTO? account = (from ac in _context.Accounts
                               where ac.Id == id
                               select new AccountDTO()
                               {
                                   Id = ac.Id,
                                   AvatarPath = @ac.AvatarPath,
                                   Birthday = ac.Birthday,
                                   City = ac.City,
                                   District = ac.District,
                                   EmailAddress = ac.EmailAddress,
                                   FirstName = ac.FirstName,
                                   LastName = ac.LastName,
                                   PasswordHash = ac.PasswordHash,
                                   Phone = ac.Phone,
                                   Sex = ac.Sex,
                                   Street = ac.Street,
                                   SubDistrict = ac.SubDistrict
                               }).FirstOrDefault();
        if (password != null)
        {
            if (account != null && encoder.Compare(password, account.PasswordHash))
                return account;
            else return null;
        }
        return account;
    }
    public IEnumerable<LaptopDTO>? GetLaptopList()
    {
        return (from laptop in _context.Laptops
                select new LaptopDTO()
                {
                    Name = laptop.Name,
                    ImagePath = @laptop.ImagePath,
                    Price = laptop.Price,
                    Company = laptop.Company,
                    Cpu = laptop.Cpu,
                    DetailPath = @laptop.DetailPath,
                    Discount = laptop.Discount,
                    Graphic = laptop.Graphic,
                    Id = laptop.Id,
                    Need = laptop.Need,
                    Ram = laptop.Ram,
                    Remain = laptop.Remain,
                    Series = laptop.Series,
                    Storage = laptop.Storage
                }).ToList();
    }
    public IEnumerable<MonitorDTO> GetMonitorList()
    {
        return (from monitor in _context.Monitors
                select new MonitorDTO()
                {
                    Name = monitor.Name,
                    ImagePath = @monitor.ImagePath,
                    Price = monitor.Price,
                    Company = monitor.Company,
                    DetailPath = @monitor.DetailPath,
                    Discount = monitor.Discount,
                    Id = monitor.Id,
                    Need = monitor.Need,
                    Panel = monitor.Panel,
                    RefreshRate = monitor.RefreshRate,
                    Remain = monitor.Remain,
                    Series = monitor.Series,
                    Size = monitor.Size
                }).ToList();
    }
    public IEnumerable<PccpuDTO> GetPccpuList()
    {
        return (from pccpu in _context.Pccpus
                select new PccpuDTO()
                {
                    Name = pccpu.Name,
                    ImagePath = @pccpu.ImagePath,
                    Price = pccpu.Price,
                    Company = pccpu.Company,
                    DetailPath = @pccpu.DetailPath,
                    Discount = pccpu.Discount,
                    Id = pccpu.Id,
                    Need = pccpu.Need,
                    Remain = pccpu.Remain,
                    Series = pccpu.Series,
                    Socket = pccpu.Socket
                }).ToList();
    }
    public IEnumerable<PcDTO> GetPcList()
    {
        return (from pc in _context.Pcs
                select new PcDTO()
                {
                    Name = pc.Name,
                    Company = pc.Company,
                    Cpu = pc.Cpu,
                    DetailPath = @pc.DetailPath,
                    Discount = pc.Discount,
                    Id = pc.Id,
                    ImagePath = @pc.ImagePath,
                    Price = pc.Price,
                    Need = pc.Need,
                    Ram = pc.Ram,
                    Series = pc.Series,
                    Remain = pc.Remain
                }).ToList();
    }
    public IEnumerable<PcharddiskDTO> GetPcharddiskList()
    {
        return (from pcharddisk in _context.Pcharddisks
                select new PcharddiskDTO()
                {
                    Name = pcharddisk.Name,
                    Company = pcharddisk.Company,
                    Connect = pcharddisk.Connect,
                    DetailPath = @pcharddisk.DetailPath,
                    Discount = pcharddisk.Discount,
                    Id = pcharddisk.Id,
                    ImagePath = @pcharddisk.ImagePath,
                    Price = pcharddisk.Price,
                    Remain = pcharddisk.Remain,
                    Series = pcharddisk.Series,
                    Storage = pcharddisk.Storage,
                    Type = pcharddisk.Type
                }).ToList();
    }
    public IEnumerable<SmartphoneDTO> GetSmartphoneList()
    {
        return (from smartphone in _context.Smartphones
                select new SmartphoneDTO()
                {
                    Name = smartphone.Name,
                    Company = smartphone.Company,
                    Storage = smartphone.Storage,
                    Cpu = smartphone.Cpu,
                    DetailPath = @smartphone.DetailPath,
                    Discount = smartphone.Discount,
                    Id = smartphone.Id,
                    ImagePath = @smartphone.ImagePath,
                    Price = smartphone.Price,
                    Ram = smartphone.Ram,
                    Remain = smartphone.Remain,
                    Series = smartphone.Series
                }).ToList();
    }
    public IEnumerable<VgaDTO> GetVgaList()
    {
        return (from vga in _context.Vgas
                select new VgaDTO()
                {
                    Name = vga.Name,
                    Chip = vga.Chip,
                    Chipset = vga.Chipset,
                    Company = vga.Company,
                    DetailPath = @vga.DetailPath,
                    Discount = vga.Discount,
                    Gen = vga.Gen,
                    Id = vga.Id,
                    ImagePath = @vga.ImagePath,
                    Need = vga.Need,
                    Series = vga.Series
                }).ToList();
    }
    public void ResetPassword(string ID, string newPassword)
    {
        var account = (from a in _context.Accounts
                       where a.Id == ID
                       select a).First();
        ScryptEncoder encoder = new ScryptEncoder();
        account.PasswordHash = encoder.Encode(newPassword);
        _context.SaveChanges();
    }
    public string GetSeuggestAccountIdCounter()
    {
        var list = (from account in _context.Accounts
                    select new { Counter = Convert.ToInt32(account.Id.Substring(5)) }).ToList();
        string result = (list.Max(x => x.Counter) + 1).ToString();
        result = result.Insert(0, new('0', 4 - result.Length));
        return result;
    }
    public void AddUser(AccountDTO accountDTO)
    {
        Account newAccount = new()
        {
            AvatarPath = accountDTO.AvatarPath,
            Birthday = accountDTO.Birthday,
            City = accountDTO.City,
            District = accountDTO.District,
            EmailAddress = accountDTO.EmailAddress,
            FirstName = accountDTO.FirstName,
            Id = accountDTO.Id,
            LastName = accountDTO.LastName,
            PasswordHash = accountDTO.PasswordHash,
            Phone = accountDTO.Phone,
            Sex = accountDTO.Sex,
            Street = accountDTO.Street,
            SubDistrict = accountDTO.SubDistrict,
        };
        _context.Accounts.Add(newAccount);
        _context.SaveChanges();
    }
}
