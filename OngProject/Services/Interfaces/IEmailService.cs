using System.Threading.Tasks;
using OngProject.Core.Models;

namespace OngProject.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel emailModel);
        Task SendWelcomeEmailAsync(string name, string email);
    }
}