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

        public Task Add(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
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

        public Task<bool> Update(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}