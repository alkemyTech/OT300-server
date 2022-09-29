using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ContactBusiness: IContactsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddContact(ContactDTO values)
        {
            try
            {
                if (values.Email is null && values.Name is null)
                {
                    return;
                }

                Contact contact = new Contact()
                {
                    Name = values.Name,
                    Email = values.Email,
                    Phone = values.Phone,
                    Message = values.Message,
                };

                await _unitOfWork.ContactRepository.Add(contact);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }
        }

        public IEnumerable<ContactDTO> GetAllContacts()
        {
            var contacts = _unitOfWork.ContactRepository.GetAll();
            var dtos = new List<ContactDTO>();

            foreach (var contact in contacts)
            {
                dtos.Add(ContactsMapper.ContactsToContactsDTO(contact));
            }

            return dtos;
        }

        
    }
}
