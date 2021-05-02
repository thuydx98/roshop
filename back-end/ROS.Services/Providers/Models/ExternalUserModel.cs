namespace ROS.Services.Providers.Models
{
	public class ExternalUserModel
	{
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
