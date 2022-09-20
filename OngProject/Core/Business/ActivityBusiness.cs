using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class ActivityBusiness : IActivityBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Activity Insert(Activity request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Activity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> Update(Activity user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activity user)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> Add(Activity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}