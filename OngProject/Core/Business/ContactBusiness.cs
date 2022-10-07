using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using OngProject.Services;
using OngProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ContactBusiness: IContactsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public ContactBusiness(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task AddContact(ContactDTO values)
        {
            try
            {
                if (values.Email is null && values.Name is null)
                {
                    return;
                }

                Contact contact = ContactsMapper.ToEntity(values);

                await _unitOfWork.ContactRepository.Add(contact);
                await _unitOfWork.SaveChangesAsync();
				await _emailService.SendContactEmailAsync(contact.Name, contact.Email);

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
