using System.Collections.ObjectModel;

namespace JobApplicationTrackerApp
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService DBService;

        public MainPage(DatabaseService dbService)
        {
            InitializeComponent();
            DBService = dbService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var items = await DBService.GetItemsFromViewAsync();
            ApplicationsCollection.ItemsSource = new ObservableCollection<JobApplicationObjectLoad>(items);
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            // navigate using Shell route
            await Shell.Current.GoToAsync(nameof(AddApplication));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is JobApplicationObjectLoad selected)
            {
                await Shell.Current.GoToAsync(nameof(EditApplication), false,
                    new Dictionary<string, object>
                    {
                        { "JobAppID", selected.JobAppID } // This value must be non-zero
                    });

                // Deselect the item so it can be tapped again
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
