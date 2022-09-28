using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IAuthBusiness
    {
        string Login(UserLoginDTO login);
        UserGetDTO Register(RegisterDTO register);

    }
}