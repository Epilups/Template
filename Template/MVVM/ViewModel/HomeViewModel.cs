using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Template.Core;
using Template.MVVM.Model;

namespace Template.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        private DateTime _currentDate;
        public ObservableCollection<DateTime> CalendarDays { get; set; }
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public string CurrentMonth => _currentDate.ToString("MMMM yyyy");

        public HomeViewModel()
        {

            _currentDate = DateTime.Now;
            InitializeCalendar();

            PreviousMonthCommand = new RelayCommand(PreviousMonth);
            NextMonthCommand = new RelayCommand(NextMonth);

        }

        private void InitializeCalendar()
        {

            CalendarDays = new ObservableCollection<DateTime>();

            DateTime firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            int daysInPreviousMonth = dayOfWeek;
            DateTime firstDayDisplayed = firstDayOfMonth.AddDays(-daysInPreviousMonth);
            int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

            for (int day = 0; day < daysInMonth + daysInPreviousMonth; day++)
            {
                CalendarDays.Add(firstDayDisplayed.AddDays(day));
       
            }
        }

        private void PreviousMonth(object obj)
        {
            _currentDate = _currentDate.AddMonths(-1);
            InitializeCalendar();
            OnPropertyChanged(nameof(CalendarDays));
            OnPropertyChanged(nameof(CurrentMonth));
        }

        private void NextMonth(object obj)
        {
            _currentDate = _currentDate.AddMonths(1);
            InitializeCalendar();
            OnPropertyChanged(nameof(CalendarDays));
            OnPropertyChanged(nameof(CurrentMonth));
        }

        
    }
}
