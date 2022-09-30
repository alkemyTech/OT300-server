﻿using System;
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
                PlainTextContent = emailModel.Content
            };

            msg.AddTo(new EmailAddress(emailModel.RecipientEmail, emailModel.RecipientName));

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        }
    }
}