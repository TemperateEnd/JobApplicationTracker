namespace JobApplicationTrackerApp;

[QueryProperty(nameof(JobAppID), "JobAppID")]
public partial class EditApplication : ContentPage
{
    private readonly DatabaseService DBService;

    public int JobAppID
    {
        get => jobAppID;
        set
        {
            jobAppID = value;
            Task.Run(async () => await LoadApplication());
            // still async, but you must await somewhere before using CurrentApplication
        }
    }
    private int jobAppID;
    public JobApplicationObjectSave CurrentApplication { get; set; }

    private readonly List<(string Name, int Value)> ApplicationStatuses = new()
    {
        ("Job Offer", 1),
        ("Ghosted", 2),
        ("Rejected", 3),
        ("Still awaiting response", 4),
        ("Interviewing", 5),
        ("Applied", 6)
    };

    public EditApplication(DatabaseService dbService)
    {
        InitializeComponent();
        DBService = dbService;
        StatusPicker.ItemsSource = ApplicationStatuses.Select(s => s.Name).ToList();
    }

    private async Task LoadApplication() {
        CurrentApplication = await DBService.GetJobApplication(jobAppID);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (CurrentApplication != null)
        {
            CompanyEntry.Text = CurrentApplication.CompanyName;
            PositionEntry.Text = CurrentApplication.Position;
            DateAppliedPicker.Date = DateTime.Parse(CurrentApplication.ApplicationDate);
            StatusPicker.SelectedIndex = CurrentApplication.JobAppStatusID-=1;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (StatusPicker.SelectedIndex >= 0) // ensure something is selected
        {
            var selectedStatus = ApplicationStatuses[StatusPicker.SelectedIndex];
            var updatedApplication = new JobApplicationObjectSave
            {
                JobAppID = jobAppID,
                CompanyName = CompanyEntry.Text,
                Position = PositionEntry.Text,
                ApplicationDate = DateAppliedPicker.Date.ToShortDateString(),
                JobAppStatusID = selectedStatus.Value
            };

            // Now call your DB update method
            await DBService.UpdateJobApplicationRecord(updatedApplication);
            await DisplayAlert("Success", "Application updated!", "OK");
            await Shell.Current.GoToAsync(".."); // go back
        }
        else
        {
            await DisplayAlert("Error", "Please select a status.", "OK");
        }

    }
}