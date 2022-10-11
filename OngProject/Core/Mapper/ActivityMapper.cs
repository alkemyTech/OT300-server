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
                
            };
            return activityDTO;
        }

        public static Activity UpdateDtoToActivity(this Activity activity, ActivityUpdateDTO activityUpdateDTO)
        {

            activity.Name = activityUpdateDTO.Name ?? string.Empty;
            activity.Content = activityUpdateDTO.Content ?? string.Empty;
      
            return activity;
        }
    }            
}

    

