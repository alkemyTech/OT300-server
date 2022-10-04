using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivityBusiness 
    {
        IEnumerable<Activity> GetAll();
        Task<Activity> GetById(int id);
        Task<ActivityDTO> Add(ActivityDTO activityDTO);
        Task<bool> Update(Activity activity);
        Task<bool> Delete(int id);
    }
}
