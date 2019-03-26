namespace ARMI.Data
{
    public class Config
    {
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string ConnectionType { get; set; }
    }

    public class ConnectionStrings
    {
        public string SqlDatabase { get; set; }
        public string SqlLiteDatabase { get; set; }
    }

    public class Configuration
    {
        public Config Config { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
