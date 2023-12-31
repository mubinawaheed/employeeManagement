using employeeManagement.Models;

namespace employeeManagement.ViewModels
{
	public class HomeDetailsViewModel
	{
		//declared as non nullable
		public Employee Employee { get; set; }
        public string PageTitle { get; set; }

        public HomeDetailsViewModel()
		{
			// Assign a non-null value to the Employee property. This fixes the above warning
			Employee = new Employee(); // or obtain a non-null instance from elsewhere
			PageTitle = "Default";
		}

	}
}
