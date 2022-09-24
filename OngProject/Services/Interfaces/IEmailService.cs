using System.Threading.Tasks;

namespace OngProject.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailAddress);
    }
}