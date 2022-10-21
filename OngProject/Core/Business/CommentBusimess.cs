using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CommentBusiness : ICommentBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<CommentDTO> GetAll()
        {
            var comment = _unitOfWork.CommentRepository.GetAll();
            var commentDto = new List<CommentDTO>();

            foreach (var comments in comment)
            {
                commentDto.Add(CommentMapper.CommentToCommentDTO(comments));
            }

            return commentDto;
        }


        public Task<Comment> GetById(int id)
        {
            return _unitOfWork.CommentRepository.GetById(id);
        }

        public async Task<CommentDTO> Update(string newContent, int commentId, int userId)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(commentId);

            var user = await _unitOfWork.UserRepository.GetById(userId);

            if ((comment == null || comment.UserId != userId) && user.RoleId != 1)
            {
                return null;
            }

            comment.Body = newContent;

            var updated = await _unitOfWork.CommentRepository.Update(comment);

            await _unitOfWork.SaveChangesAsync();

            return CommentMapper.CommentToCommentDTO(comment);
        }
        public async Task<Comment> Add(CommentAddDto commentAddDto)
        {
            Comment comment = new Comment();
            comment = CommentMapper.DTOToComment(commentAddDto);
            var user = await _unitOfWork.UserRepository.GetById(comment.UserId);

            var news = await _unitOfWork.NewsRepository.GetById(comment.NewsId);

            if (user != null && news != null)
            {
                await _unitOfWork.CommentRepository.Add(comment);
                _unitOfWork.SaveChanges();
                return comment;

            }
            return null;
        }
        public List<CommentAddDto> showListCommentDto(int id)
        {
            List<CommentAddDto> listaFiltrada = new List<CommentAddDto>();
            var lista = _unitOfWork.CommentRepository.GetAll();


                foreach (var item in lista)
                {
                    if (item.NewsId == id)
                    {
                        listaFiltrada.Add(CommentMapper.CommentToCommentAddDto(item));
                    }
                }

            return listaFiltrada;
        }

        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.CommentRepository.EntityExist(id);
        }

        public async Task<bool> Delete(int id)
        {
            await _unitOfWork.CommentRepository.Delete(id);
            return true;
        }
    }
     
}