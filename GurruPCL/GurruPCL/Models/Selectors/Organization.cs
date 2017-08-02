using System;
namespace GurruPCL
{
	public class Organization
	{
		public Organization()
		{
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string BusinessPhone { get; set; }
		public string PrimaryEmail { get; set; }
		public string ABN { get; set; }
		public int ContactsCount { get; set; }
	}
}
