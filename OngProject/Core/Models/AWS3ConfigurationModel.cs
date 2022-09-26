namespace OngProject.Core.Models
{
    public class AWS3ConfigurationModel
    {
        public const string AwsConfiguration  = "AwsConfiguration";

        public string AWSAccessKey { get; set; }

        public string AWSSecretKey { get; set; }

        public string AWSBuctketName { get; set; }
    }
}