using System;
using Hangfire;

namespace AnimeMovie.API.Jobs
{
	public class HangfireJobScheduler
	{
        public static void ScheduleRecurringJobs()
        {
            RecurringJob.RemoveIfExists(nameof(Jobs.Recurring.UserLoginCheck));
            RecurringJob.AddOrUpdate<Jobs.Recurring.UserLoginCheck>(nameof(Jobs.Recurring.UserLoginCheck), jox => jox.Run(JobCancellationToken.Null), Cron.Daily(12,00), TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(Jobs.Recurring.UserRosetteCheck));
            RecurringJob.AddOrUpdate<Jobs.Recurring.UserRosetteCheck>(nameof(Jobs.Recurring.UserRosetteCheck), jox => jox.Run(JobCancellationToken.Null), Cron.Daily(12, 00), TimeZoneInfo.Local);

        }
    }
}

