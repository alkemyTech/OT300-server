using Microsoft.EntityFrameworkCore;

namespace OngProject.DataAccess
{
    public class OngDbContext : DbContext
    {
        public OngDbContext(DbContextOptions<OngDbContext> options): base(options)
        {

        }
    }
}