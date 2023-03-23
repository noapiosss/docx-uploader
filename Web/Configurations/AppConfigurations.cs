namespace Web.Configurations
{
    public class AppConfiguration
    {
        public string BlobConnectionString { get; set; }
        public string LogicAppPostUrl { get; set; }
        public string FunctionTriggerPostUrl { get; set; }
    }
}