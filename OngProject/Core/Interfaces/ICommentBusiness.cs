using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentBusiness
    {
        IEnumerable<CommentDTO> GetAll();
        Task<Comment> GetById(int id);
        Task<Comment>  Add(CommentAddDto commentAddDto);
        Task<CommentDTO> Update(string newContent, int commentId, int userId);
        Task<bool> Delete(int id);
        List<CommentAddDto> showListCommentDto(int id);
        Task<bool> DoesExist(int id);
    }
}