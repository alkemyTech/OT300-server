using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class CommentMapper
    {
        public static CommentDTO CommentToCommentDTO(Comment comment)
        {
            CommentDTO commentDTO = new CommentDTO()
            {
                CreationDate = comment.CreationDate,
                Description = comment.Description,
                UserId = comment.UserId
                
            };
            return commentDTO;
        }
    }
}
