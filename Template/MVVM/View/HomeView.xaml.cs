using Microsoft.VisualBasic;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using Template.MVVM.Model;
using Template.MVVM.ViewModel;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Template.MVVM.View
{

    public partial class HomeView : UserControl
    {
        readonly string json = File.ReadAllText("C:\\Users\\latvi\\source\\repos\\Template\\Template\\data.json");
        public HomeView()
        {
            InitializeComponent();
        }

        private void CalendarDayClicked(object sender, MouseButtonEventArgs e)
        {
            var drugs = JsonConvert.DeserializeObject<List<Drug>>(json);

            if (sender is FrameworkElement element && element.DataContext is DateTime clickedDate)
            {
                var matchingDrugTimes = (
                    from drug in drugs
                    from dateTime in drug.DateTimes
                    where dateTime?.Date == clickedDate.Date
                    select $"{drug.Name}, {dateTime?.ToString("h:mm tt")}"
                    )
                    .ToList();

                if (matchingDrugTimes.Any())
                {
                    string message = $"Registered drugs for {clickedDate.ToString("dd-MM-yyyy")}:\n{string.Join("\n", matchingDrugTimes)}";
                    dataTextBlock.Text = message;
                }
                else
                {
                    dataTextBlock.Text = "No drugs registered for this day";
                }
            }
            
        }
    }
}
