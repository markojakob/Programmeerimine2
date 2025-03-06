using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Api;

namespace WpfApp1
{
    class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<Car> Lists { get; private set; }

        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<Car> ConfirmDelete { get; set; }

        private readonly IApiClient _apiClient;
        public MainWindowViewModel() : this(new ApiClient())
        {

        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            Lists = new ObservableCollection<Car>();

            NewCommand = new RelayCommand<Car>(
                // Execute
                list =>
                {
                    SelectedItem = new Car();
                }
            );

            SaveCommand = new RelayCommand<Car>(
                // Execute
                async list =>
                {
                    await _apiClient.Save(SelectedItem);
                    await Load();
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );

            DeleteCommand = new RelayCommand<Car>(
                // Execute
                async list =>
                {
                    if (ConfirmDelete != null)
                    {
                        var result = ConfirmDelete(SelectedItem);
                        if (!result)
                        {
                            return;
                        }
                    }

                    await _apiClient.Delete(SelectedItem.Id);
                    Lists.Remove(SelectedItem);
                    SelectedItem = null;
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );
        }

        public async Task Load()
        {
            Lists.Clear();

            var lists = await _apiClient.List();
            foreach (var list in lists)
            {
                Lists.Add(list);
            }
        }

        private Car _selectedItem;
        public Car SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }

}

