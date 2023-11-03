using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
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
using Template.MVVM.Model;
using Template.MVVM.ViewModel;

namespace Template.MVVM.View
{
    
    public partial class DiscoveryView : UserControl
    {
        public DiscoveryView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                var selectedDrug = e.AddedItems[0] as Drug;
                if (selectedDrug != null)
                {
                    ((TimeRegistrationViewModel)DataContext).SelectedDrug = selectedDrug;
                }
            }
        }

    }
}
