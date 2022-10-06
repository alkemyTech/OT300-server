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
                CreatedAt = comment.CreatedAt,
                Body = comment.Body,
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
            };
            return comment;
        }
        public static CommentAddDto CommentToCommentAddDto(Comment comment)
        {
            CommentAddDto commentAdd = new CommentAddDto()
            {
                UserId = comment.UserId,
                NewsId = comment.NewsId,
                Body = comment.Body,
            };
            return commentAdd;
        }
    }
}
