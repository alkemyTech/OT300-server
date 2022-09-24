using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set;}
    }
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        
        public AuthBusiness(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public string Login(UserLogin login)
        {
            User CurrentUser = Authenticate(login);

            if (CurrentUser is not null)
            {
                string Token = Generate(CurrentUser);
                return Token;
            }

            return null;
        }

        private string Generate(User userInput)
        {
            Claim[] Claims = new Claim[]
            {
                new Claim("Identifier", userInput.Id.ToString()),
                new Claim(ClaimTypes.Email, userInput.Email),
                new Claim(ClaimTypes.Role, userInput.RoleId.ToString())
            };

            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken Token = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: Claims, 
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        private User Authenticate(UserLogin loginUser)
        {
            User FoundUser = ValidateUserEmail(loginUser);

            if (FoundUser is not null)
            {
                User CurrentUser = ComparePasswords(loginUser, FoundUser);

                return CurrentUser ?? null;
            }   

            return null;
        }

        private User ComparePasswords(UserLogin requestUser, User foundUser)
        {
            requestUser.Password = EncryptPassword(requestUser.Password);

            if (foundUser.Password.Equals(requestUser.Password))
            {
                return foundUser;
            }

            return null;
        }

        private User ValidateUserEmail(UserLogin checkUserEmail)
        {
            string Email = checkUserEmail.Email;
            User CurrentUser = _unitOfWork.UserRepository.GetAll().FirstOrDefault(user => Email == user.Email);

            return CurrentUser;
        }

        private string EncryptPassword(string password)
        {
            string Salt = "OTSampleSalt300";
            password += Salt;
            byte[] PasswordBytes = Encoding.UTF8.GetBytes(password);
            string EncryptedPassword = Convert.ToBase64String(PasswordBytes);
            return EncryptedPassword;
        }
    }
}