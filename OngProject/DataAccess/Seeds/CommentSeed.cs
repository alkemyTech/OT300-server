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
                    Description = "Los programas educativos que posee la organizacion resultan ser muy recomendables.",
                },

                new Comment()
                {
                    UserId = 2,
                    Description = "Destaco positivo el trabajo que realizan los tutores en el acompaÃ±amiento a los alumnos en las becas de estimulo.",
                },

                new Comment()
                {
                    UserId = 3,
                    Description = "Dentro del taller de cuento, hubo una seleccion de textos literarios diversos acorde a la edad.",
                },

                new Comment()
                {
                    UserId = 5,
                    Description = "Los recortes utilizados estan relacionados de acuerdo al contexto mas cercano de los alumnos.",
                }

            };
            var id = 1;
            foreach (var c in comments)
            {
                c.Id = id++;
                c.CreatedAt = DateTime.Now;
                c.IsDeleted = false;
                c.LastEditedAt = DateTime.Now;
                c.CreationDate = DateTime.Now;
                
            }

            return comments;
        }
    }
}
