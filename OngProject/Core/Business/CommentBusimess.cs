using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public Task<bool> Update(Comment comment)
        {
            throw new NotImplementedException();
        }
        public Task Add(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
