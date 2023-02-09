using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels.Commands;

namespace Views.Admins
{
    /// <summary>
    /// Interaction logic for CompanyNeedView.xaml
    /// </summary>
    public partial class CompanyNeedView : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Company> _list;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Company> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged(nameof(List));
            }
        }
        public CompanyNeedView()
        {
            InitializeComponent();
            List = new ObservableCollection<Company>()
            ;
            DataContext = this;
            AddNew = new RelayCommand<object>(addNew);
        }
        public ICommand AddNew { get; }
        private void addNew(object o)
        {
            List.Add(new Company(-1, ""));
        }
    }
    public class Company
    {
        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
