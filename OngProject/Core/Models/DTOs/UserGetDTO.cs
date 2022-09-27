namespace OngProject.Core.Models.DTOs
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public int RoleId { get; set; }
    }
}
