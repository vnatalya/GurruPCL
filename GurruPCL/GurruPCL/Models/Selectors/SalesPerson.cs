using System;
namespace GurruPCL
{
	public class SalesPerson
	{
		public SalesPerson()
		{
		}
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Guid? BusinessTypeId { get; set; }
		public string Email { get; set; }
	}
}
