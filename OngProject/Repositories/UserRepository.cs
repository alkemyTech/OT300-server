using global::OngProject.Entities;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace OngProject.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        private readonly OngDbContext context;
        public UserRepository(OngDbContext context) :base(context)
        {
            this.context = context;
        }


    }
}

