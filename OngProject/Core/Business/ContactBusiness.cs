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


        public Task<bool> CreateContact()
        {
            throw new System.NotImplementedException();
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
