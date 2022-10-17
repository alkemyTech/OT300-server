using OngProject.Core.Models;
using OngProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.ServicesMocks
{
    internal class MockEmail : IEmailService
    {
        public Task SendContactEmailAsync(string name, string email)
        {
            Console.WriteLine("email enviado");
            return Task.CompletedTask;
        }

        public Task SendEmailAsync(EmailModel emailModel)
        {
            Console.WriteLine("email enviado");
            return Task.CompletedTask;
        }

        public Task SendWelcomeEmailAsync(string name, string email)
        {
            Console.WriteLine("email enviado");
            return Task.CompletedTask;
        }
    }
}
