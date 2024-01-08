using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using CV_collection.Commands;
using CV_collection.Models;
using CV_collection.Views;
using System.IO;

namespace CV_collection.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string SavePath = "./Summaries.json";
        private bool _isJava;
        public bool IsJava
        {
            get
            {
                return _isJava;
            }
            set
            {
                _isJava = value;
                RaisePropertyChanged(nameof(IsJava));
            }
        }
        private bool _isCPlusPlus;
        public bool IsCPlusPlus
        {
            get { return _isCPlusPlus; }
            set
            {
                _isCPlusPlus = value;
                RaisePropertyChanged(nameof(IsCPlusPlus));
            }
        }
        private bool _isCSharp;
        public bool IsCSharp
        {
            get
            {
                return _isCSharp;
            }
            set
            {
                _isCSharp = value;
                RaisePropertyChanged(nameof(IsCSharp));
            }
        }
        private bool _isEnglish;
        public bool IsEnglish
        {
            get
            {
                return _isEnglish;
            }
            set
            {
                _isEnglish = value;
                RaisePropertyChanged(nameof(IsEnglish));
            }
        }

        private string? _currentName;
        public string? CurrentName
        {
            get
            {
                return _currentName;
            }
            set
            {
                _currentName = value;
                RaisePropertyChanged(nameof(CurrentName));
            }
        }
        private List<string>? _currentSkills;
        public List<string>? CurrentSkills
        {

            get
            {
                return _currentSkills;
            }
            set
            {
                _currentSkills = value;
                RaisePropertyChanged(nameof(CurrentSkills));
            }
        }
        private int _currentAge;
        public int CurrentAge
        {
            get
            {
                return _currentAge;
            }
            set
            {
                _currentAge = value;
                RaisePropertyChanged(nameof(CurrentAge));
            }
        }
        private string? _currentMartialStatus;
        public string? CurrentMartialStatus
        {
            get
            {
                return _currentMartialStatus;
            }
            set
            {
                _currentMartialStatus = value;
                RaisePropertyChanged(nameof(CurrentMartialStatus));
            }
        }
        private string? _currentAddress;
        public string? CurrentAddress
        {
            get
            {
                return _currentAddress;
            }
            set
            {
                _currentAddress = value;
                RaisePropertyChanged(nameof(CurrentAddress));
            }
        }

        private ObservableCollection<CV> _summaries = new ObservableCollection<CV>();
        public ObservableCollection<CV> Summaries
        {
            get
            {
                return _summaries;
            }
            set
            {
                _summaries = value;
                RaisePropertyChanged(nameof(Summaries));
            }
        }

        private ObservableCollection<string> _names = new ObservableCollection<string>();
        public ObservableCollection<string> Names
        {
            get { return _names; }
            set
            {
                _names = value;
                RaisePropertyChanged(nameof(Names));
            }
        }

        private string? _selectedSummaryItem;
        public string? SelectedSummaryItem
        {
            get
            {
                return _selectedSummaryItem;
            }
            set
            {
                _selectedSummaryItem = value;
                RaisePropertyChanged(nameof(SelectedSummaryItem));
            }
        }
        public MainViewModel()
        {
            CurrentAddress = "";
            CurrentAge = 0;
            CurrentMartialStatus = "";
            CurrentName = "";
            CurrentSkills = new List<string>();
            SelectedSummaryItem = "";
        }
        public MainViewModel(CV obj)
        {
            CurrentAddress = obj.Address;
            CurrentAge = obj.Age;
            CurrentMartialStatus = obj.MartialStatus;
            CurrentName = obj.Name;
            CurrentSkills = new List<string>(obj.Skills);
            SelectedSummaryItem = "";
            if (obj.Skills.Contains("English"))
            {
               IsEnglish = true;
            }
            if (obj.Skills.Contains("C++"))
            {
                IsCPlusPlus = true;
            }
            if (obj.Skills.Contains("C#"))
            {
                IsCSharp = true;
            }
            if (obj.Skills.Contains("Java"))
            {
                IsJava = true;
            }
        }

        private DelegateCommand? _ShowCommand;
        private DelegateCommand? _SaveCommand;
        private DelegateCommand? _ClearCommand;

        private void Save(object? parameters)
        {
            if (CurrentName != null)
            {
                Names.Add(CurrentName);
                Summaries.Add(new CV(CurrentName, CurrentSkills, CurrentAddress, CurrentAge, CurrentMartialStatus));

                string json = JsonSerializer.Serialize(Summaries);
                File.WriteAllText(SavePath, json);
            }
        }
        private bool  CanSave(object? parameters)
        {
            bool isUnique = true;
            for(int i=0; i < Names.Count; i++)
            {
                if (Names[i] == CurrentName) 
                {
                    isUnique = false;
                    break;
                }
            }
            CurrentSkills?.Clear();
            if (IsEnglish)
            {
                CurrentSkills?.Add("English");
            }
            if (IsCPlusPlus)
            {
                CurrentSkills?.Add("C++");
            }
            if (IsCSharp)
            {
                CurrentSkills?.Add("C#");
            }
            if (IsJava)
            {
                CurrentSkills?.Add("Java");
            }
            if (CurrentName == "" || CurrentAddress =="" || CurrentAge < 18 || CurrentMartialStatus == "" ||CurrentSkills?.Count < 1 || !isUnique)
            {
                return false;
            }
            return true;
        }
        public ICommand SaveCommand
        {
            get
            {
                if(_SaveCommand == null)
                {
                    _SaveCommand = new DelegateCommand(Save, CanSave);
                }
                return _SaveCommand;
            }
        }

        private void Clean(object? parameters)
        {
            CurrentName = "";
            CurrentSkills?.Clear();
            CurrentAddress = "";
            CurrentAge = 0;
            CurrentMartialStatus = "";
            IsCPlusPlus = false;
            IsCSharp = false;
            IsEnglish= false;
            IsJava = false;
        }

        private bool CanClean(object? prarameters)
        {
            return true;
        }
        public ICommand ClearCommand
        {
            get
            {
                if(_ClearCommand == null)
                {
                    _ClearCommand = new DelegateCommand(Clean, CanClean);
                }
                return _ClearCommand;
            }
        }

        private void Show(object? parameters)
        {
            CV selectedItem = new CV();
            for(int i=0; i< Summaries.Count; i++)
            {
                if(SelectedSummaryItem == Summaries[i].Name)
                {
                    selectedItem = Summaries[i];
                    break;
                }
            }

            ShowCVForm summaryView = new ShowCVForm();
            MainViewModel summaryModel = new MainViewModel(selectedItem);
            summaryView.DataContext = summaryModel;
            summaryView.Show();
        }
        private bool CanShow(object? prarameters)
        {
            if(SelectedSummaryItem!="" && SelectedSummaryItem != null)
            {
                return true;
            }
            return false;
        }
       public ICommand ShowCommand
        {
            get
            {
                if(_ShowCommand == null)
                {
                    _ShowCommand = new DelegateCommand(Show, CanShow);
                }
                return _ShowCommand;
            }
        }
        private void RaisePropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}