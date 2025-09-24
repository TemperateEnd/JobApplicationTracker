namespace JobApplicationTrackerApp;

public partial class AddApplication : ContentPage
{
    private readonly DatabaseService DBService;
    private readonly List<(string Name, int Value)> ApplicationStatuses = new()
    {
        ("Job Offer", 1),
        ("Ghosted", 2),
        ("Rejected", 3),
        ("Still awaiting response", 4),
        ("Interviewing", 5),
        ("Applied", 6)
    };

    public AddApplication(DatabaseService dbService)
    {
        InitializeComponent();
        DBService = dbService;
        StatusPicker.ItemsSource = ApplicationStatuses.Select(s => s.Name).ToList(); 
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (StatusPicker.SelectedIndex >= 0) // ensure something is selected
        {
            var selectedStatus = ApplicationStatuses[StatusPicker.SelectedIndex];

            var application = new JobApplicationObjectSave
            {
                CompanyName = CompanyEntry.Text,
                Position = PositionEntry.Text,
                ApplicationDate = DateAppliedPicker.Date.ToShortDateString(),
                JobAppStatusID = selectedStatus.Value
            };

            await DBService.InsertJobApplicationRecord(application);
            await DisplayAlert("Success", "Application saved!", "OK");

            // Navigate back to the main list
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("Error", "Please select a status.", "OK");
        }
    }
}