using OngProject.Entities;

namespace OngProject.DataAccess.Seeds
{
    public static class ContactSeed
    {
        public static Contact[] GetData()
        {
            var contacts = new[]
            {
                new Contact()
                {
                    Id = 1,
                    Name = "Bogotá, Colombia",
                    Email = "europa@somosmas.org",
                    Message = "loremp ipsum"
                },
                new Contact()
                {
                    Id = 2,
                    Email = "europa@somosmas.org",
                    Name = "Madrid, España",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                }
            };
            return contacts;
        }
    }
}