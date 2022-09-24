using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OngProject.Core.Business;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IAuthBusiness
    {
        public string Login(UserLogin login);
    }
}