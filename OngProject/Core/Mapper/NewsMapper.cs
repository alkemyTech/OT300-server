using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Mapper
{
    public static class NewsMapper
    {
        public static News ToEntity(this NewsPostDTO news)
        {
            var entity = new News()
            {
                //id missing
                Name = news.Name,
                Content = news.Content,
                IdCategory = news.IdCategory
            };
            return entity;
        }

        public static News ToEntity(this NewsDTO news)
        {
            var entity = new News()
            {
                //id missing
                Name = news.Name,
                Content = news.Content,
                Image = news.Image,
                IdCategory = news.IdCategory
            };
            return entity;
        }

        public static NewsFullDTO ToFullDTO(this News news)
        {
            var dto = new NewsFullDTO()
            {
                Id = news.Id,
                Name = news.Name,
                Content = news.Content,
                Image = news.Image,
                IdCategory = news.IdCategory
            };
            return dto;
        }

        public static List<NewsDTO> ToDTO(this IList<News> entities)
        {
            var listDTOs = new List<NewsDTO>();
            foreach (var item in entities)
            {
                listDTOs.Add(item.ToFullDTO());
                
            }
            return listDTOs;
        }
    }
}
