using OngProject.Entities;
using System;

namespace OngProject.DataAccess.Seeds
{
    public static class OrganizationSeed
    {
        public static Organization[] GetData()
        {
            var Org = new[]
            {
                new Organization()
                {
                    Id = 1,
                    Name = "ONG Somos Más",
                    Img = "",
                    Adress = "",
                    PhoneNumber = 0,
                    Email = "",
                    FacebookUrl = "",
                    InstagramUrl = "",
                    LinkedInUrl = "",                    
                    WelcomeText = "",
                    AboutUsText = "",
                    IsDeleted = false,
                    LastEditedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                }
            };

            return Org;
        }
    }
}
