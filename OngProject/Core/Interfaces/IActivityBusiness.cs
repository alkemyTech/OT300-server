using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivityBusiness 
    {
        IEnumerable<Activity> GetAll();
        Task<Activity> GetById(int id);
        Task Add(Activity activity);
        Task<bool> Update(Activity activity);
        Task<bool> Delete(int id);
    }
}
