using OngProject.Entities;
using System;

namespace OngProject.DataAccess.Seeds
{
    public static class MemberSeed
    {
        public static Member[] GetData()
        {

            var members = new[] {
                new Member()
                {
                    Id = 1,
                    Name = "María Irola",
                    Description = "Presidenta María estudió economía y se especializó en economía para el\r\ndesarrollo. Comenzó como voluntaria en la fundación y fue quien promovió el crecimiento y la organización de la institución acompañando la transformación de un simple comedor barrial al centro comunitario de atención integral que es hoy en día",
                    Image=""
                },
                new Member()
                {
                    Id = 2,
                    Name = "Marita Gomez",
                    Description = "Fundadora Marita estudió la carrera de nutrición y se especializó en nutrición infantil. Toda la vida fue voluntaria en distintos espacios en el barrio hasta que recidió abrir un comedor propio. Comenzó trabajando con 5 familias y culminó su trabajo transformando Somos Más en la organización que es hoy.",
                    Image=""
                },
                new Member()
                {
                    Id = 3,
                    Name = "Miriam Rodriguez",
                    Description = "Colaboradores | Terapista Ocupacional",
                    Image=""

                },
                new Member() { Id = 4, Name = "Cecilia Mendez", Description = "Colaboradores | Psicopedagoga" , Image=""},
                new Member() { Id = 5, Name = "Mario Fuentes:", Description = "Colaboradores | Psicólogo" ,Image=""},
                new Member() { Id = 6, Name = "Rodrigo Fuente", Description = "Colaboradores | Contador" ,Image=""},
                new Member() { Id = 7, Name = "Maria Garcia", Description = "Colaboradores | Profesora de Artes Dramáticas",Image="" },
                new Member() { Id = 8, Name = "Marco Fernandez", Description = "Colaboradores | Profesor de Educación Física",Image=""  },
            };

            foreach (var m in members)
            {
                //m.FacebookUrl = "";
                //m.InstagramUrl = "";
                m.CreatedAt = DateTime.Now;
                m.IsDeleted = false;
                m.LastEditedAt = DateTime.Now;
            }

            return members;

        }
    }
}
