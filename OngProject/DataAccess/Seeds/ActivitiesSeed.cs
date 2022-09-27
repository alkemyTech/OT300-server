using System;
using Microsoft.EntityFrameworkCore;
using OngProject.Entities;

namespace OngProject.DataAccess.Seeds
{
    public static class ActivitiesSeed
    {
        public static Activity[] GetData()
        {
            var data = new[] {new Activity()
                {
                    Id = 1, Name = "Encuentro para Innovadores y Emprendedores Sociales",
                    Content =
                        "La Embajada de Canadá en Bogotá, realizará el próximo 16 de noviembre, " +
                        "un evento en el cual se invita a emprendedores e innovadores sociales, a explorar experiencias significativas de innovación y " +
                        "emprendimiento social, así mismo se identificará dónde se necesita mayor apoyo para potenciarlas El encuentro se enfocará en 3 de " +
                        "los Objetivos de desarrollo sostenible a saber, " +
                        "erradicación de pobreza, educación de calidad y equidad de género, " +
                        "este enfoque proporcionará elementos para identificar las principales brechas en el apoyo para " +
                        "innovadores y emprendedores y la consecución de mayor impacto colectivo." +
                        "El Auditorio del Impact HUB Bogotá, acogerá este evento (con inscripción previa) desde las 8 am.",
                    Image = "",
                    CreatedAt = DateTime.UtcNow,
                },
                new Activity()
                {
                    Id = 2, Name = "Presentes en el Foro Educativo Distrital 2017", Image = "",
                    CreatedAt = DateTime.UtcNow,
                    Content =
                        "La Corporación Somos Más facilitó espacios de encuentro entre integrantes de la comunidad educativa de la ciudad de Bogotá, " +
                        "en el marco del Foro Educativo Distrital – FED-2017: Ciudad Educadora para el reencuentro la reconciliación y la paz, liderado " +
                        "por la Secretaría de Educación del Distrito el 3 y 4 de octubre de 2017 en el Colegio la Felicidad. Somos Más presentó e " +
                        "implementó una propuesta metodológica para el desarrollo de dos de las modalidades de encuentro y conversación propuestas para " +
                        "el Foro: “conexiones estratégicas” y “experimentaciones colaborativas”. Siguiendo el principio de la Co-Creación, la metodología " +
                        "propuesta se ajustó para cada uno de estos 24 espacios según las características e intereses de los encargados y panelistas y las " +
                        "indicaciones de la directora del Foro, en todos los espacios el objetivo fundamental fue promover la conexión, la participación y " +
                        "el diálogo entre los asistentes. Participaron más de 1.500 personas en los dos días en el Foro, Somos Más desplegó " +
                        "un equipo de 23 facilitadores, quienes moderaron y facilitaron 9 “Conexiones estratégicas” que son espacios con formato de panel " +
                        "en los cuales se promovió la participación de los asistentes, y 15 “experimentaciones colaborativas” cuyo objetivo fue promover " +
                        "la construcción colectiva a través de la implementación de una metodología inspirada en el Pensamiento de diseño."
                },
                new Activity()
                {
                    Id = 3, Name = "Somos Más apoya a la ART", Image = "",
                    CreatedAt = DateTime.UtcNow,
                    Content = "La Agencia de Renovación del Territorio – ART hace parte de la arquitectura institucional del gobierno de Colombia fue creada " +
                              "a finales de 2015 para transformar el sector rural colombiano y cerrar las brechas entre el campo y la ciudad. Busca gerenciar " +
                              "procesos para la transformación de los territorios priorizados mediante la articulación institucional y la participación efectiva " +
                              "en el marco de los Planes de Renovación del Territorio. El Programa de Desarrollo con Enfoque Territorial- PDET es un proceso de construcción y " +
                              "participación a 10 años, que va a reflejar la visión colectiva de los actores del territorio. Es por esto que la participación activa de las " +
                              "comunidades es fundamental, ya que el PDET busca reivindicar su valor protagónico en la promoción de su propio desarrollo. Las Corporación Somos Más apoyó a " +
                              "la Dirección de Estructuración de Proyectos de la ART en la facilitación de dos encuentros con los equipos territoriales de las subregiones para promover " +
                              "dinámicas efectivas de trabajo colaborativo y socializar la metodología PDET. El primer encuentro se desarrolló del 24 al 26 de agosto de 2017 con " +
                              "100 participantes entre coordinador regional, facilitador experto, gestor municipal, apoyo social o espejo y profesional de la dirección de intervención " +
                              "del territorio de 14 subregiones; el segundo se desarrolló en 13 y 14 de septiembre y participaron los coordinadores y gerentes de las 16 subregiones PDET."
                }
            };
            return data;
        }
    }
}