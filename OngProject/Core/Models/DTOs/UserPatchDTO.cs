namespace OngProject.Core.Models.DTOs
{
	public class UserPatchDTO
	{
		#nullable enable
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Photo { get; set; }
		#nullable disable
	}
}
