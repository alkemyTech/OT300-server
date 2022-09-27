using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        
        public AuthBusiness(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public string Login(UserLoginDTO login)
        {
            User currentUser = Authenticate(login);

            if (currentUser is not null)
            {
                string token = Generate(currentUser);
                return token;
            }

            return null;
        }

        public RegisterDTO Register(RegisterDTO register)
        {
            var encryptedPassword = EncryptPassword(register.Password);

            var userNew = new User
            {
                LastName = register.LastName,
                FirstName = register.FirstName,
                Email = register.Email,
                Password = encryptedPassword
            };

            _unitOfWork.UserRepository.Add(userNew);
            _unitOfWork.SaveChanges();


            return register;

        }

        private string Generate(User userInput)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("Identifier", userInput.Id.ToString()),
                new Claim(ClaimTypes.Email, userInput.Email),
                new Claim(ClaimTypes.Role, userInput.RoleId.ToString())
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims, 
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLoginDTO loginUser)
        {
            User foundUser = ValidateUserEmail(loginUser);

            if (foundUser is not null)
            {
                User currentUser = ComparePasswords(loginUser, foundUser);

                return currentUser ?? null;
            }   

            return null;
        }

        private User ComparePasswords(UserLoginDTO requestUser, User foundUser)
        {
            requestUser.Password = EncryptPassword(requestUser.Password);

            if (foundUser.Password.Equals(requestUser.Password))
                return foundUser;
            
            return null;
        }

        private User ValidateUserEmail(UserLoginDTO checkUserEmail)
        {
            string email = checkUserEmail.Email;
            User currentUser = _unitOfWork.UserRepository.GetAll().FirstOrDefault(user => email == user.Email);

            return currentUser;
        }

        private string EncryptPassword(string password)
        {
            string salt = "OTSampleSalt300";
            password += salt;
            byte[] encoded = Encoding.UTF8.GetBytes(password);
            string encrypted = Convert.ToBase64String(encoded);
            return encrypted;
        }
    }
}