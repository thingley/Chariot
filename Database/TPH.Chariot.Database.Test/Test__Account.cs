using System;

using System.Data;
using Xunit;
using Xunit.Sdk;

using TPH.Chariot.Data.Legacy.Common.DataTableExtensionMethods;
using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Database.Test
{
	public class Test__Account : TestBase
	{
		[Fact]
		public void CreateValidAccounts()
		{
			RunTestMethod(() => {
				// Add customer records
				IDataPortalResult result = null;
				DataTable customerDataTable = DataTableFactory.Customer();

				customerDataTable.AddCustomerRow(code: "AAA");
				customerDataTable.AddCustomerRow(code: "BBB");

				result = DataPortal.Persist__Customer(customerDataTable);

				// Add account records for each customer
				DataTable accountDataTable = DataTableFactory.Account();

				long customerID = (long)customerDataTable.Rows[0]["CustomerID"];
				accountDataTable.AddAccountRow(customerID: customerID, code: "AAA");
				accountDataTable.AddAccountRow(customerID: customerID, code: "BBB");

				customerID = (long)customerDataTable.Rows[1]["CustomerID"];
				accountDataTable.AddAccountRow(customerID: customerID, code: "AAA");
				accountDataTable.AddAccountRow(customerID: customerID, code: "BBB");

				result = DataPortal.Persist__Account(accountDataTable);

				// Assertions
				Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
				Assert.Equal(4, result.RowsUpdated);
			});
		}

		[Fact]
		public void CreateAccountsWithSameCustomerIDAndCode()
		{
			RunTestMethod(() => {
				// Add customer record
				IDataPortalResult result = null;
				DataTable customerDataTable = DataTableFactory.Customer();

				customerDataTable.AddCustomerRow(code: "AAA");

				result = DataPortal.Persist__Customer(customerDataTable);

				// Add account records for customer with matching Code
				DataTable accountDataTable = DataTableFactory.Account(includeConstraints: false);

				long customerID = (long)customerDataTable.Rows[0]["CustomerID"];
				accountDataTable.AddAccountRow(customerID: customerID, code: "AAA");
				accountDataTable.AddAccountRow(customerID: customerID, code: "AAA");

				result = DataPortal.Persist__Account(accountDataTable);

				Assert.False(result.OK);
			});
		}

		[Fact]
		public void CreateAccountsWithSameCustomerIDAndName()
		{
			RunTestMethod(() => {
				// Add customer record
				IDataPortalResult result = null;
				DataTable customerDataTable = DataTableFactory.Customer();

				customerDataTable.AddCustomerRow(code: "AAA");

				result = DataPortal.Persist__Customer(customerDataTable);

				// Add account records for customer with matching name
				DataTable accountDataTable = DataTableFactory.Account(includeConstraints: false);

				long customerID = (long)customerDataTable.Rows[0]["CustomerID"];
				accountDataTable.AddAccountRow(customerID: customerID, code: "AAA", account: "AAA");
				accountDataTable.AddAccountRow(customerID: customerID, code: "BBB", account: "AAA");

				result = DataPortal.Persist__Account(accountDataTable);

				Assert.False(result.OK);
			});
		}
	}
}
