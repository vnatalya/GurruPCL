
using System.Collections.Generic;
using System.Threading.Tasks;
using GurruPCL.Models;

namespace GurruPCL.ViewModels
{
    public class FormViewModel
    {
        static object locker;

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
		}

		public List<BusinessType> BusinessTypes { get; set; }
		public List<Organization> Organizations { get; set; }
		public List<SalesActivity> SalesActivitys { get; set; }
		public List<SalesPerson> SalesPersons { get; set; }

		public async void GetInitializedAsync()
        {
			var tasks = new List<Task<BaseResult>>();
			tasks.Add(GetBusinessType());
			tasks.Add(GetBusinessType());
			tasks.Add(GetBusinessType());
			tasks.Add(GetBusinessType());
        }

		private async Task<BaseResult> GetBusinessType()
		{
			return new BaseResult();
		}

    }
}
