namespace JobApplicationTrackerApp
{
    public partial class App : Application
    {
        public App(DatabaseService dbService)
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
