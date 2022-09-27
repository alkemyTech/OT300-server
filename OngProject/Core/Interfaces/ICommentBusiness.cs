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
        Task Add(Comment comment);
        Task<bool> Update(Comment comment);
        Task<bool> Delete(int id);

        
    }
}
