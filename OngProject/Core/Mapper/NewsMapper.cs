using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Runtime.CompilerServices;

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

        public static News NewsUpdate(this News news, NewsPutDTO newsPutDTO)
        {
            {
                news.Name = newsPutDTO.Name ?? news.Name;
                news.Content = newsPutDTO.Content ?? news.Content;
                news.IdCategory = newsPutDTO.IdCategory == 0 ? news.IdCategory : newsPutDTO.IdCategory;
            }
            return news;
        }
    }
}
