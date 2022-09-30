namespace OngProject.Core.Models
{
    public class EmailModel
    {
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}