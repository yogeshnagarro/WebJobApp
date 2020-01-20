namespace WebJobApp.Model
{
    public interface IBackgroundJobParam
    {
        int BackgroundJobTypeID { get; set; }
        string BackgroundJobDataKey { get; set; }
        string BackgroundJobData { get; set; }
        int? JobPriority { get; set; }
    }

    public class BackgroundJobParam : IBackgroundJobParam
    {
        public int BackgroundJobTypeID { get; set; }
        public string BackgroundJobDataKey { get; set; }
        public string BackgroundJobData { get; set; }
        public int? JobPriority { get; set; }
    }
    public interface IBackgroundJobType
    {
        string  HandlerAssemblyName { get; set; }
        string HandlerClassName { get; set; }
    }

    public interface IBackgroundJobHandler
    {
        void Process(IBackgroundJobParam backgroundJobParam, long? backgroundJobID);
    }
}
