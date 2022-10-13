using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using SendGrid.Helpers.Mail;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System;


namespace OngProject.DataAccess.Seeds
{
    public static class CommentSeed
    {
        public static Comment[] GetData()
        {
            var comments = new[]
            {
                new Comment()
                {
                    UserId = 1,
                    Body = "Los programas educativos que posee la organizacion resultan ser muy recomendables.",
                    NewsId = 1
                },

                new Comment()
                {
                    UserId = 2,
                    Body = "Destaco positivo el trabajo que realizan los tutores en el acompaÃ±amiento a los alumnos en las becas de estimulo.",
                    NewsId = 2
                },

                new Comment()
                {
                    UserId = 3,
                    Body = "Dentro del taller de cuento, hubo una seleccion de textos literarios diversos acorde a la edad.",
                    NewsId = 1
                },

                new Comment()
                {
                    UserId = 5,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 2
                },

                new Comment()
                {
                    UserId = 4,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 3
                },

                new Comment()
                {
                    UserId = 6,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 3
                },

                new Comment()
                {
                    UserId = 7,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 4
                },

                new Comment()
                {
                    UserId = 8,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 4
                },

                new Comment()
                {
                    UserId = 9,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 5
                },

                new Comment()
                {
                    UserId = 10,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 5
                },

                new Comment()
                {
                    UserId = 9,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 6
                },

                new Comment()
                {
                    UserId = 8,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 6
                },

                new Comment()
                {
                    UserId = 7,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 7
                },

                new Comment()
                {
                    UserId = 6,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 7
                },

                new Comment()
                {
                    UserId = 5,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 8
                },

                new Comment()
                {
                    UserId = 4,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 8
                },

                new Comment()
                {
                    UserId = 3,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 9
                },

                new Comment()
                {
                    UserId = 2,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 9
                },

                new Comment()
                {
                    UserId = 1,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 10
                },

                new Comment()
                {
                    UserId = 2,
                    Body = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                    NewsId = 10
                }

            };
            var id = 1;
            foreach (var c in comments)
            {
                c.Id = id++;
                c.CreatedAt = DateTime.Now;
                c.IsDeleted = false;
                c.LastEditedAt = DateTime.Now;
                
            }

            return comments;
        }
    }
}
