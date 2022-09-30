using System;
using System.IO;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OngProject.Services
{
    public class SendGridEmailService : IEmailService
    {
        public async Task SendEmailAsync(EmailModel emailModel)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var senderEmail = "Group300Alkemy@protonmail.com";
            var senderName = "Alkemy300";


            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, senderName),
                Subject = emailModel.Subject,
                HtmlContent = GenerateEmail(emailModel.Title, emailModel.Content)
            };

            msg.AddTo(new EmailAddress(emailModel.RecipientEmail, emailModel.RecipientName));

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        }

        private string GenerateEmail(string title, string content)
        {
            var email = Path.Combine(Directory.GetCurrentDirectory(), "Templates\\email_template.html");

            StreamReader sr = File.OpenText(email);

            string textReader = sr.ReadToEnd();

            textReader = textReader.Replace("#T&iacute;tulo#", title);
            textReader = textReader.Replace("#Content#", content);

            return textReader;
        }
    }
}