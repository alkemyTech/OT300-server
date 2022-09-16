using OngProject.Entities;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace OngProject.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly OngDbContext context;
        public UserRepository(OngDbContext context) : base(context)
        {
            this.context = context;
        }


    }
}

