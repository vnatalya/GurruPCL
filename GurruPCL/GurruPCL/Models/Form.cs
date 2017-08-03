
namespace GurruPCL.Models
{
    public class Form
    {
        public string OrganizationName { get; set; }
        public Organization ParentOrganization { get; set; }
        public BusinessType BusinessType { get; set; }
        public string BusinessPhone { get; set; }
        public string Email { get; set; }
        public Contact Contact { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public SalesActivity SalesActivity { get; set; }
        public Source Source { get; set; }
        public string DetailOfOpportunity { get; set; }
    }
}
