namespace OngProject.Core.Models
{
    public class EmailConfigModel
    {
        public const string WelcomeEmailConfig = "EmailConfigModel";
        public const string AddContactEmailConfig = "EmailConfigModel";

        public WelcomeEmail WelcomeEmail { get; set; }
        public AddContactEmail AddContactEmail { get; set; }

    }
}