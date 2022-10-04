using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OngProject.Core.Models;
using OngProject.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OngProject.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly EmailConfigModel _config;

        public SendGridEmailService(IOptions<EmailConfigModel> welcomeConfig)
        {
            _config = welcomeConfig.Value;
        }
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

        public async Task SendWelcomeEmailAsync(string recipientName, string recipientMail)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var senderEmail = "Group300Alkemy@protonmail.com";
            var senderName = "Alkemy300";

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, senderName),
                Subject = _config.WelcomeEmail.Subject,
                HtmlContent = GenerateEmail(_config.WelcomeEmail.Title, _config.WelcomeEmail.Content)
            };

            msg.AddTo(new EmailAddress(recipientMail, recipientName));

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