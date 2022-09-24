using System;
using System.Threading.Tasks;
using OngProject.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OngProject.Services
{
    public class SendGridEmailService : IEmailService
    {
        public async Task SendEmailAsync(string emailAddress)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("placeHolder@Email.com", "Alkemy Team 300"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>"
            };
            msg.AddTo(new EmailAddress(emailAddress, "Test User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}