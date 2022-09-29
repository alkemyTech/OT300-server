using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class ContactsMapper
    {

        public static ContactDTO ContactsToContactsDTO(Contact contact)
        {
            ContactDTO contactsDTO = new ContactDTO()
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Message = contact.Message
            };       
            return contactsDTO;           
        }

        public static Contact ToEntity(ContactDTO dto)
        {
            Contact contact = new Contact()
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Message = dto.Message
            };

            return contact;
        }

    }
}
