using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Template.Core;
using Template.MVVM.Model;

namespace Template.MVVM.ViewModel
{
    public class TimeRegistrationViewModel : ObservableObject
    {
        private ObservableCollection<Drug>? _drugs;
        private DateTime? _displayedDateTime = null;
        private Drug? _selectedDrug;
        string path = "C:\\Users\\latvi\\source\\repos\\Template\\Template\\data.json";

        public ObservableCollection<Drug>? Drugs
        {
            get { return _drugs; }
            set
            {
                _drugs = value;
                OnPropertyChanged(nameof(Drugs));
            }
        }
        
        public string DisplayedDateTimes
        {
            get
            {
                if(SelectedDrug != null)
                {
                    var formattedDates = SelectedDrug.DateTimes
                        .Where(date => date.HasValue)
                        .Select(date => date.Value.ToString("dd-MM-yyyy h:mm tt"));

                    return string.Join("\n", formattedDates);
                }
                return string.Empty;
            }
            
        }
        
        public Drug? SelectedDrug
        {
            get { return _selectedDrug; }
            set
            {
                _selectedDrug = value;
                OnPropertyChanged(nameof(SelectedDrug));
                OnPropertyChanged(nameof(DisplayedDateTimes));

            }
        }

        public DateTime? DisplayedDateTime
        {
            get { return _displayedDateTime; }
            set
            {
                _displayedDateTime = value;
                OnPropertyChanged(nameof(DisplayedDateTime));
            }
        }

  
        public ICommand AddDateTimeCommand { get; set; }


        public TimeRegistrationViewModel()
        {
            Drugs = new ObservableCollection<Drug>();
            LoadDrugsFromJson(); //loads drug list when the viewmodel is created
            AddDateTimeCommand = new RelayCommand(AddToSchedule);
        }

        public void AddToSchedule(object obj)
        {

            if(SelectedDrug != null && DisplayedDateTime != null)
            {
                DateTime newDateTime = DisplayedDateTime.Value;

                //check for duplicates
                if (!SelectedDrug.DateTimes.Any(dt =>
                    dt.HasValue && Math.Abs((dt.Value - newDateTime).TotalMinutes) < 1))
                {
                    SelectedDrug.DateTimes.Add(newDateTime);
                    OnPropertyChanged(nameof(DisplayedDateTimes));
                    //save updated list
                    SaveDrugsToJson();
                }
                else
                {
                    MessageBox.Show("Duplicate or very close datetime detected, Please choose a different datetime");

                }
            }
            else
            {
                MessageBox.Show("Please select a drug and a valid date and time to add.");
            }
        }
        private void SaveDrugsToJson()
        {
            if (Drugs != null)
            {
                string json = JsonConvert.SerializeObject(Drugs, Formatting.Indented);
                File.WriteAllText(path, json);
            }
        }
        private void LoadDrugsFromJson()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Drugs = JsonConvert.DeserializeObject<ObservableCollection<Drug>>(json);
            }
        }

    }
}
