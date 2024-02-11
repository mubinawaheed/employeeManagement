using Microsoft.AspNetCore.Identity;

namespace employeeManagement.Models
{
	public class ApplicationUser : IdentityUser
	{
        public string City { get; set; }
    }
}
