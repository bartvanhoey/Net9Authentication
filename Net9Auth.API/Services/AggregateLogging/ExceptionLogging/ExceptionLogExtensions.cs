using Microsoft.EntityFrameworkCore;
using Net9Auth.API.Database;
using Net9Auth.API.Models.AggregatedLogging.ExceptionLogging;

namespace Net9Auth.API.Services.AggregateLogging.ExceptionLogging;

public static class ExceptionLogExtensions
{
    public static async Task<long> SetFrequency(this ExceptionLog exceptionLog, ApplicationDbContext db)
    {
        var exceptionLogs = await db.ExceptionLogs.Where(x => x.ExceptionType == exceptionLog.ExceptionType &&
                                                              x.ApplicationName == exceptionLog.ApplicationName &&
                                                              x.ClassName == exceptionLog.ClassName &&
                                                              x.MethodName == exceptionLog.MethodName).ToListAsync();
        if (exceptionLogs.Count <= 0) return 0;

        var frequency = exceptionLogs.Max(x => x.ExceptionFrequency) + 1;
        exceptionLog.ExceptionFrequency = frequency;
        return frequency;
    }

    public static async Task RemoveSameExceptionLogsKeepLastAsync(this ExceptionLog exceptionLog,
        ApplicationDbContext db, long frequency, int countKeepLast)
    {
        if (frequency > 20)
        {
            var sameExceptions = await db.ExceptionLogs.Where(x => x.ExceptionType == exceptionLog.ExceptionType &&
                                                                   x.ApplicationName == exceptionLog.ApplicationName &&
                                                                   x.ClassName == exceptionLog.ClassName &&
                                                                   x.MethodName == exceptionLog.MethodName)
                .OrderByDescending(x => x.InsertTime).Skip(countKeepLast).ToListAsync();

            db.ExceptionLogs.RemoveRange(sameExceptions);
            await db.SaveChangesAsync();
        }
    }

    public static async Task<List<ExceptionLog>> GetOfSameTypeSkip20Newest(this ExceptionLog exceptionLog,
        ApplicationDbContext db) =>
        await db.ExceptionLogs.Where(x => x.ExceptionType == exceptionLog.ExceptionType &&
                                          x.ApplicationName == exceptionLog.ApplicationName &&
                                          x.ClassName == exceptionLog.ClassName &&
                                          x.MethodName == exceptionLog.MethodName).OrderByDescending(x => x.InsertTime)
            .Skip(20).ToListAsync();
}