using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTrackerApp
{ 
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection DatabaseConnection;

        public DatabaseService(string DBPath)
        {
            DatabaseConnection = new SQLiteAsyncConnection(DBPath);
        }

        public Task<List<JobApplicationObjectLoad>> GetItemsFromViewAsync()
        {
            return DatabaseConnection.QueryAsync<JobApplicationObjectLoad>("SELECT * FROM JobApplicationsView");
        }

        public async Task<JobApplicationObjectSave> GetJobApplication(int jobAppID)
        {
            var application = await DatabaseConnection.Table<JobApplicationObjectSave>()
                                               .Where(x => x.JobAppID == jobAppID)
                                               .FirstOrDefaultAsync();

            if (application == null)
                throw new InvalidOperationException($"No job application with ID {jobAppID} found.");

            return application;
        }

        public Task<int> InsertJobApplicationRecord(JobApplicationObjectSave NewApplication)
        {
            return DatabaseConnection.InsertAsync(NewApplication);
        }

        public Task<int> UpdateJobApplicationRecord(JobApplicationObjectSave ExistingApplication)
        {
            return DatabaseConnection.UpdateAsync(ExistingApplication);
        }
    }
}
