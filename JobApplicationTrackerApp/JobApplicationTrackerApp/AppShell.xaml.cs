namespace JobApplicationTrackerApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Only needed if you want to call Shell.Current.GoToAsync("AddApplication")
        Routing.RegisterRoute(nameof(AddApplication), typeof(AddApplication));
        Routing.RegisterRoute(nameof(EditApplication), typeof(EditApplication));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}