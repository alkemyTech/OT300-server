using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContactsBusiness
    {
        IEnumerable<ContactDTO> GetAllContacts();
        Task AddContact(ContactDTO contact); 

    }
}
