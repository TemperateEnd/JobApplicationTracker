using SQLite;

namespace JobApplicationTrackerApp
{
    [Table("JobApplicationsTable")]
    public class JobApplicationObjectSave
    {
        [PrimaryKey, AutoIncrement]
        public int JobAppID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string ApplicationDate { get; set; }
        public int JobAppStatusID { get; set; }
    }
}
