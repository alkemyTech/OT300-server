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
        public static Comment DTOToComment(CommentAddDto commentAddDto)
        {
            Comment comment = new Comment()
            {
                UserId = commentAddDto.UserId,
                NewsId = commentAddDto.NewsId,
                Body = commentAddDto.Body,
                Description = ""
            };
            return comment;
        }
    }
}
