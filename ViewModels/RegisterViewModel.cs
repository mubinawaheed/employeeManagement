using System.ComponentModel.DataAnnotations;

namespace employeeManagement.ViewModels
{
	public class RegisterViewModel
	{
        [Required]
		[EmailAddress]
        public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage ="Password does not match")]
		public string? ConfirmPassword { get; set; }

	}
}
