using System.ComponentModel.DataAnnotations;

namespace employeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage ="Invalid name length")]    
        public string? Name { get; set; } //adding ? to indicate it can be null

        [Required]
        public Dept? Department { get; set; }

		[Required]
        [Display(Name="Office Email")]
		public string? Email { get; set; }

        public string? ProfileImage { get; set; }


    }
}
