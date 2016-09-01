using System;

namespace Logentries
{
    class Program
    {
        static void Main(string[] args)
        {
            var logentriesService =
                LogentriesService.GetLogsAsync(
                    new LogentriesUrlFactory(LogModel.LogOtherExample, LogSetModel.LogSetDefault),
                    DateTime.Now,
                    DateTime.Now.AddDays(1)).GetAwaiter().GetResult();

            if (logentriesService != null)
            {
                var fatalLogs = logentriesService.ConvertToLogentries("^[^\n]*( FATAL )");
                foreach (var fatalLog in fatalLogs)
                {
                        Console.WriteLine(fatalLog);
                }
            }
        }
    }
}
