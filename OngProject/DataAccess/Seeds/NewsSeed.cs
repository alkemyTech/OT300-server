using OngProject.Entities;
using System;


namespace OngProject.DataAccess.Seeds
{
    public static class NewsSeed
    {

        public static News[] GetData()
        {

            var news = new[]
            {
                //because Category don´t have a seed yet i put a categoryId ramdom.
                new News()
                {
                    Id = 1,
                    Name= "Juntos por el poder Colectivo",
                    Content= "Durante más de 15 años el constante trabajo por fortalecer el trabajo colectivo ha llevado a Somos Más por los rincones más hermosos e inesperados de Colombia. Nos ha permitido luchar por el derecho que tienen los niños, niñas y adolescentes a que su voz sea...",
                    IdCategory= 2                
                },
                new News()
                {
                    Id = 2,
                    Name = "¡Llega a Colombia la tercera edición del FITS!",
                    Content = "Wingu trae nuevamente a Bogotá el Festival de Innovación y Tecnología Social FITS, una jornada de capacitación para encontrar, articular y compartir todos los conocimientos a través de herramientas tecnológicas que ayudan a las organizaciones de la sociedad civil en...",
                    IdCategory = 1                    
                },
                new News(){
                    Id = 3,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 3                
                },
                new News(){
                    Id = 4,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 4
                },
                new News(){
                    Id = 5,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 5
                },
                new News(){
                    Id = 6,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 6
                },
                new News(){
                    Id = 7,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 7
                },
                new News(){
                    Id = 8,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 8
                },
                new News(){
                    Id = 9,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 9
                },
                new News(){
                    Id = 10,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 10
                },
                new News(){
                    Id = 11,
                    Name = "PROMUEVE LA GARANTÍA DE LOS DERECHOS DE LOS NIÑOS, NIÑAS Y ADOLESCENTES, ¡COLOMBIA ESCUCHA TU VOZ!",
                    Content = "Con el objetivo de desarrollar acciones de movilización e impacto social para la incidencia en planes de Gobierno, planes de desarrollo, y políticas públicas a favor de la niñez y la adolescencia en Colombia, durante el mes de enero del presente año se realiza la...",
                    IdCategory = 11
                }
            };

            var imagesNumber = 1;
            foreach (var n in news)
            {
                n.Image = $"/OT300/ong/news/img{imagesNumber}.jpg";
                n.CreatedAt = DateTime.Now;
                n.IsDeleted = false;
                n.LastEditedAt = DateTime.Now;
                imagesNumber++;
            };

            return news;
        }

    }
}
