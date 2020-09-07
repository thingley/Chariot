using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Xunit;

using TPH.Chariot.Data.Legacy.Common.DataTableExtensionMethods;
using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;
using TPH.Chariot.Data.Legacy.DataPortal;
using Xunit.Sdk;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace TPH.Chariot.Database.Test
{
	public class Test__Customer: TestBase
	{
		[Fact]
		public void CreateValidCustomers()
		{
			DataTable customerDataTable = DataTableFactory.Customer();

			customerDataTable.AddCustomerRow(code: "AAA");
			customerDataTable.AddCustomerRow(code: "BBB");

			RunTestMethod(() => {
				IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

				Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
				Assert.Equal(2, result.RowsUpdated);
			});
		}

		[Fact]
		public void CreateCustomersWithSameCode()
		{
			DataTable customerDataTable = DataTableFactory.Customer(includeConstraints: false);

			customerDataTable.AddCustomerRow(code: "AAA");
			customerDataTable.AddCustomerRow(code: "AAA");

			RunTestMethod(() => {
				IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

				Assert.False(result.OK);
			});
		}

		[Fact]
		public void CreateCustomersWithSameName()
		{
			DataTable customerDataTable = DataTableFactory.Customer(includeConstraints: false);

			customerDataTable.AddCustomerRow(code: "AAA", customer: "AAA");
			customerDataTable.AddCustomerRow(code: "BBB", customer: "AAA");

			RunTestMethod(() => {
				IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

				Assert.False(result.OK);
			});
		}
	}
}
