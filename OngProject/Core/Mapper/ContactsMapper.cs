using Newtonsoft.Json.Linq;
using OngProject.Core.Models;
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

        public static Contact ToEntity(this ContactDTO values)
        {
            Contact contact = new Contact()
            {
                Name = values.Name,
                Email = values.Email,
                Phone = values.Phone,
                Message = values.Message,
            };

            return contact;
        }

        public static EmailModel ContactToEmailModel(Contact contact)
        {
            EmailModel emailModel = new EmailModel()
            {
                RecipientEmail = contact.Email,
                RecipientName = contact.Name,
            };

            return emailModel;
        }

    }
}
