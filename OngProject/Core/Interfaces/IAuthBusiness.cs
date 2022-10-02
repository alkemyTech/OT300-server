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
        Task<string> Login(UserLoginDTO login);
        Task<string> Generate(UserTokenDTO userInput);
        Task<UserTokenDTO> Register(RegisterDTO register);

    }
}