
using System;
using Newtonsoft.Json;

namespace GurruPCL.Models
{
	public class Form
	{
		[JsonProperty("organisationName")]
        public string OrganizationName { get; set; }

		[JsonProperty("parentOrganisationId")]
		public Guid? ParentOrganizationId 
		{ 
			get 
			{ 
				if( ParentOrganization != null)
					return ParentOrganization.Id;
				return null;
			} 
		}

		[JsonIgnore]
        public Organization ParentOrganization { get; set; }

		[JsonProperty("businessType")]
        public BusinessType BusinessType { get; set; }

		[JsonProperty("businessTypeId")]
		public Guid BusinessTypeId { get { return BusinessType.Id; } }

		[JsonProperty("businessPhone")]
        public string BusinessPhone { get; set; }

		[JsonProperty("emailAddress")]
        public string Email { get; set; }

#warning fix this!
		[JsonProperty("addressState")]
		public string AddressState { get { return "7"; } set { } }

		[JsonIgnore]
        public Contact Contact { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get { return Contact.FirstName; } }

		[JsonProperty("lastName")]
		public string LastName { get { return Contact.LastName; } }

		[JsonProperty("mobilePhone")]
		public string MobilePhone { get { return Contact.Phone; } }

		[JsonProperty("salesman")]
        public SalesPerson SalesPerson { get; set; }

		[JsonProperty("salesmanId")]
		public Guid SalesPersonId { get { return SalesPerson.Id;} }

		[JsonProperty("salesActivity")]
        public SalesActivity SalesActivity { get; set; }

		[JsonProperty("salesActivityId")]
		public Guid SalesActivityId { get { return SalesActivity.Id;} }

		[JsonProperty("source")]
		public Source FormSource { get; set; }
        
		[JsonProperty("description")]
		public string DetailOfOpportunity { get; set; }

		[JsonProperty("hasSave")]
		public bool HasSave { get; set; }

		public enum Source
		{
            None = 0,
            Website_Enquiry = 1,
			Phone = 2,
			Conference = 3,
			Cold_Call = 4,
			Resign = 5,
			Beevo = 6,
			Referral = 7,
			Follow_Up = 8
		}
    }
}
