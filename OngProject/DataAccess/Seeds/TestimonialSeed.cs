
using OngProject.Entities;
using System;

namespace OngProject.DataAccess.Seeds
{
    public static class TestimonialSeed
    {
        public static Testimonial[] GetData()
        {

            var test = new[] {
                new Testimonial()
                {
                    Id = 1,
                    Name = "CONFLICTOS EN ÁFRICA EN 2022: CAUSAS Y PAÍSES MÁS AFECTADOS",
                    Content = "Los innumerables conflictos que se están viviendo actualmente en África nos lleva a hablar de una «triple crisis» que afecta a los sectores alimentarios, energéticos y financieros. Como consecuencia de este drama humano muchos...",
                },
                new Testimonial()
                {
                    Id = 2,
                    Name = "LA POBLACIÓN NÓMADA SE ENFRENTA A LA SEQUÍA EN SOMALIA",
                    Content = "Más de seis millones de personas se han visto afectadas en todo el país, y 800 000 personas se han visto obligadas a huir de sus hogares e instalarse en campos de desplazados internos en zonas urbanas. El número de desplazamientos está aumentando porque no ha llovido lo suficiente en el país durante la temporada de lluvias de Gu (primavera), y...",
                },
                new Testimonial()
                {
                    Id = 3,
                    Name = "CÓMO \"PLANTAR HIERBA\" SALVÓ A UN PUEBLO DEL HAMBRE",
                    Content = "La introducción de cultivos como el arroz en las zonas inundadas de Sudán del Sur ha despertado la esperanza...",
                }
            };

            var i = 1;
            foreach (var m in test)
            {
                m.Image = $"/s3/ong/testimonie/img{1}.jpg";
                m.CreatedAt = DateTime.Now;
                m.IsDeleted = false;
                // m.LastEditedAt = DateTime.Now;
                i++;
            }

            return test;

        }
    }
}
