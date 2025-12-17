using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfTest.Common;
using WpfTest.DataAccess;
using WpfTest.DataAccess.DataEntity;

namespace WpfTest.ViewModels
{
    public class MainViewModel : NotifyBase
    {
        private string _settingValue;
        public string SettingValue
        {
            get { return _settingValue; }
            set
            {
                _settingValue = value;
                DoNotify();
            }
        }

        public ObservableCollection<int> num { get; set; } = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6, 7 };


        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
