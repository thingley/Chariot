using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Xunit;

using TPH.Chariot.Data.Legacy.Common.DataTableExtensionMethods;
using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;
using TPH.Chariot.Data.Legacy.DataPortal;


namespace TPH.Chariot.Database.Test
{
	public class Test__CreateDatabase : TestBase
	{
		[Fact]
		public void CreateDatabase()
		{
			DataTableFactory dataTableFactory = new DataTableFactory();
			IDataPortalResult result = null;

			#region Create Customers

			DataTable customerDataTable = dataTableFactory.Customer();

			for (int customerIndex = 1; customerIndex <= 100; customerIndex++)
			{
				customerDataTable.AddCustomerRow(code: $"{customerIndex.ToString().PadLeft(4, '0')}");
			}

			result = DataPortal.Persist__Customer(customerDataTable);

			Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
			Assert.Equal(100, result.RowsUpdated);

			#endregion

			#region Create Accounts

			DataTable accountDataTable = dataTableFactory.Account();

			foreach (DataRow parentDataRow in customerDataTable.Rows)
			{
				for (int accountIndex = 1; accountIndex <= 3; accountIndex++)
				{
					accountDataTable.AddAccountRow(customerID: (long)parentDataRow["CustomerID"], code: $"{accountIndex.ToString().PadLeft(4, '0')}");
				}
			}

			result = DataPortal.Persist__Account(accountDataTable);

			Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
			Assert.Equal(300, result.RowsUpdated);

			#endregion
		}
	}
}
