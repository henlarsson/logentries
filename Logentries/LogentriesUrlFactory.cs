namespace Logentries
{
    public class LogentriesUrlFactory
    {
        private static string AccountKey => "Your account key";

        private static readonly string Api = $"https://pull.logentries.com/{AccountKey}/hosts/";

        public LogModel Log { get; set; }
        public LogSetModel LogSet { get; set; }

        public LogentriesUrlFactory(LogModel log, LogSetModel logSet)
        {
            this.Log = log;
            this.LogSet = logSet;
        }

        public string Url => $"{Api}{this.LogSet.Value}/{this.Log.Value}/";
    }

    public class LogSetModel
    {
        public static LogSetModel LogSetDefault => new LogSetModel("LOGSETNAME");

        public readonly string Value;

        private LogSetModel(string log)
        {
            this.Value = log;
        }
    }

    public class LogModel
    {
        public static LogModel LogDefault => new LogModel("LOGNAME1");

        public static LogModel LogOtherExample => new LogModel("LOGNAME2");

        public readonly string Value;

        private LogModel(string log)
        {
            this.Value = log;
        }
    }
}
