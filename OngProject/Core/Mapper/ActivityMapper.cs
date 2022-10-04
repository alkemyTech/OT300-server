using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public static class ActivityMapper
    {
        public static ActivityDTO ActivityToActivityDTO(Activity activity)
        {
            ActivityDTO activityDTO = new ActivityDTO()
            {
                Content = activity.Content,
                Name = activity.Name,
                //ImageFile = activity.Image
            };
            return activityDTO;
        }
    }
}
