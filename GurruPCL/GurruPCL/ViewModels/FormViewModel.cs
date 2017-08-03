
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GurruPCL.Models;
using GurruPCL.Models.Results;

namespace GurruPCL.ViewModels
{
    public class FormViewModel
    {
        static object locker = new object();

        private static FormViewModel instance;
        public static FormViewModel Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new FormViewModel();
                    return instance;
                }
            }
        }

		FormViewModel()
		{
			BusinessTypes = new List<BusinessType>();
            Organizations = new List<Organization>();
            SalesActivities = new List<SalesActivity>();
            SalesPersons = new List<SalesPerson>();
        }

		public List<BusinessType> BusinessTypes { get; set; }
		public List<Organization> Organizations { get; set; }
		public List<SalesActivity> SalesActivities { get; set; }
		public List<SalesPerson> SalesPersons { get; set; }

        public Form CurrentForm { get; set; }

        public async Task<BaseResult> GetInitializedAsync()
        {
            var tasks = new List<Task<BaseResult>>();

			tasks.Add(GetBusinessTypesAsync());
			tasks.Add(GetOrganizationAsync());
			tasks.Add(GetSalesPersonsAsync());
			tasks.Add(GetSalesActivitiesAsync());

            BaseResult[] results = await Task.WhenAll(tasks);

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].Status != System.Net.HttpStatusCode.OK)
                    return results[i];
            }

			CurrentForm = new Form()
			{
				SalesActivity = SalesActivities.FirstOrDefault(x => x.Name.Equals("New Customer"))
			};

            return new BaseResult();
        }

		private async Task<BaseResult> GetBusinessTypesAsync()
		{
            var result = await RequestSender.GetBusinessTypesAsync();

            if (result.Status == System.Net.HttpStatusCode.OK)
                BusinessTypes = result.Items;

            return result;
		}

        private async Task<BaseResult> GetOrganizationAsync()
        {
            var result = await RequestSender.GetOrganizationsAsync();

            if (result.Status == System.Net.HttpStatusCode.OK)
                Organizations = result.Items;

            return result;
        }

        private async Task<BaseResult> GetSalesPersonsAsync()
        {
            var result = await RequestSender.GetSalesPersonsAsync();

            if (result.Status == System.Net.HttpStatusCode.OK)
                SalesPersons = result.Items;

            return result;
        }

        private async Task<BaseResult> GetSalesActivitiesAsync()
        {
            var result = await RequestSender.GetSalesActivityAsync();

            if (result.Status == System.Net.HttpStatusCode.OK)
                SalesActivities = result.Items;

            return result;
        }

		public async Task<BaseResult> SaveCurrentFormAsync(bool qualify)
        {
			var res = await RequestSender.SaveFormAsync(CurrentForm, qualify);
            return res;
        }

    }
}
