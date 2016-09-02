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

        public string Url => $"{Api}{this.LogSet}/{this.Log}/";
    }

    public class LogSetModel
    {
        public static LogSetModel LogSetDefault => new LogSetModel("LOGSETNAME");

        private readonly string value;

        private LogSetModel(string log)
        {
            this.value = log;
        }

        public override string ToString()
        {
            return this.value;
        }
    }

    public class LogModel
    {
        public static LogModel LogDefault => new LogModel("LOGNAME1");

        public static LogModel LogOtherExample => new LogModel("LOGNAME2");

        private readonly string value;

        private LogModel(string log)
        {
            this.value = log;
        }

        public override string ToString()
        {
            return this.value;
        }
    }
}
