using System;

using System.Data;
using Xunit;
using Xunit.Sdk;

using TPH.Chariot.Data.Legacy.Common.DataTableExtensionMethods;
using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Database.Test
{
	public class Test__Customer: TestBase
	{
		[Fact]
		public void CreateValidCustomers()
		{
			RunTestMethod(() => {
				DataTable customerDataTable = DataTableFactory.Customer();

				customerDataTable.AddCustomerRow(code: "AAA");
				customerDataTable.AddCustomerRow(code: "BBB");

				IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

				Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
				Assert.Equal(2, result.RowsUpdated);
			});
		}

		[Fact]
		public void CreateCustomersWithSameCode()
		{
			RunTestMethod(() => {
				DataTable customerDataTable = DataTableFactory.Customer(includeConstraints: false);

				customerDataTable.AddCustomerRow(code: "AAA");
				customerDataTable.AddCustomerRow(code: "AAA");

				IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

				Assert.False(result.OK);
			});
		}

		[Fact]
		public void CreateCustomersWithSameName()
		{
			RunTestMethod(() => {
				DataTable customerDataTable = DataTableFactory.Customer(includeConstraints: false);

				customerDataTable.AddCustomerRow(code: "AAA", customer: "AAA");
				customerDataTable.AddCustomerRow(code: "BBB", customer: "AAA");

				IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

				Assert.False(result.OK);
			});
		}
	}
}
