using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Windows;
using System.Windows.Input;
using Template.Core;
using Template.MVVM.Model;
using Wpf.Ui.Input;

namespace Template.MVVM.ViewModel
{
    public class DrugRegistrationViewModel : ObservableObject
    {
        private string path = "C:\\Users\\latvi\\source\\repos\\Template\\Template\\data.json";
        private Drug _drug = new Drug();

        public Drug Drug
        {
            get { return _drug; }
            set
            {
                _drug = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; set; }

        public DrugRegistrationViewModel()
        {
            SubmitCommand = new RelayCommand(Submit);
        }

        //the method passed to RelayCommand has to have 1 parameter of type object, and must be void
        public void Submit(object obj)
        {
            if(!string.IsNullOrWhiteSpace(Drug.Name) || !string.IsNullOrWhiteSpace(Drug.Type))
            {
                List<Drug> drugs;

                if (File.Exists(path))
                {
                    string existingJson = File.ReadAllText(path);
                    drugs = JsonConvert.DeserializeObject<List<Drug>>(existingJson) ?? new List<Drug>();
                }
                else
                {
                    drugs = new List<Drug>();
                }

                // Adds new drug to the list
                drugs.Add(_drug);

                // write the list back to the json file
                string json = JsonConvert.SerializeObject(drugs, Formatting.Indented);
                File.WriteAllText(path, json);

                //why does this work??
                Drug = new Drug();
            }
            else
            {
                MessageBox.Show("Please provide both Name and Type for your drug");
            }
        }  
    }
}
