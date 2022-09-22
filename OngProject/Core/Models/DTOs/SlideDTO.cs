using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    /// <summary>
    /// Slides Data Transfer Object
    /// </summary>
    public class SlideDTO
    {
        //public int Id { get; set; }
        public string ImageUrl { get; set; }
       // public string Text { get; set; }
        public int Order { get; set; }
      //  public int OrganizationId { get; set; }
    }
}
