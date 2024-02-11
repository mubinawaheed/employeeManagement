using System.ComponentModel.DataAnnotations;

namespace employeeManagement.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
