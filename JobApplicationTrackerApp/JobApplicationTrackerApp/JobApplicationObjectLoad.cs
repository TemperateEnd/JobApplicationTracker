using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTrackerApp
{
    [Table("JobApplicationsTable")]
    public class JobApplicationObjectLoad
    {
        [PrimaryKey, AutoIncrement]
        public int JobAppID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string ApplicationDate { get; set; }
        public string JobAppStatus { get; set; }
    }
}
