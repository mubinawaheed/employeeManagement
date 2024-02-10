using System.ComponentModel.DataAnnotations;

namespace employeeManagement.Utilities
{
	public class ValidEmailDomain : ValidationAttribute
	{
		private readonly string allowedDomain;
        public ValidEmailDomain(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;	
        }
        public override bool IsValid(object? value)
		{
			string[] domain = value.ToString().Split('@');
			return domain[1].ToUpper() == allowedDomain.ToUpper();

		}
	}
}
