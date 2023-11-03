using Template.Core;

namespace Template.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TimeRegistrationViewCommand { get; set; }
        public RelayCommand DrugRegistrationViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public TimeRegistrationViewModel TimeVM { get; set; }
        public DrugRegistrationViewModel DrugVM { get; set; }


        public object _currentView;


        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();

            }
        }
        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            TimeVM = new TimeRegistrationViewModel();
            DrugVM = new DrugRegistrationViewModel();

            CurrentView = HomeVM;

            //switching views

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            TimeRegistrationViewCommand = new RelayCommand(o =>
            {
                CurrentView = TimeVM;
            });

            DrugRegistrationViewCommand = new RelayCommand(o =>
            {
                CurrentView = DrugVM;
            });

        }
    }
}
