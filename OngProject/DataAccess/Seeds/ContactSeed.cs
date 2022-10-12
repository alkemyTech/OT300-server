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
                },
                new Contact()
                {
                    Id = 3,
                    Email = "test3@somosmas.org",
                    Name = "Madrid, España",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 4,
                    Email = "test4@somosmas.org",
                    Name = "Córdoba, Argentina",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 5,
                    Email = "test5@somosmas.org",
                    Name = "Rosario, Argentina",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 6,
                    Email = "test6@somosmas.org",
                    Name = "Buenos Aires, Argentina",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 7,
                    Email = "test7@somosmas.org",
                    Name = "Bogotá, Colombia",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 8,
                    Email = "test8@somosmas.org",
                    Name = "Medellín, Colombia",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 9,
                    Email = "test9@somosmas.org",
                    Name = "Valencia, Velenzuela",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 10,
                    Email = "test10@somosmas.org",
                    Name = "Córdoba, Argentina",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 11,
                    Email = "test11@somosmas.org",
                    Name = "Rosario, Argentina",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                },
                new Contact()
                {
                    Id = 12,
                    Email = "test12@somosmas.org",
                    Name = "Buenos Aires, Argentina",
                    Phone = 918061515,
                    Message = "loremp ipsum",
                }
            };
            return contacts;
        }
    }
}