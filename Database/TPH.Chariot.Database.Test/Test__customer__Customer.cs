using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Xunit;

using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;
using TPH.Chariot.Data.Legacy.DataPortal;

namespace TPH.Chariot.Database.Test
{
	public class Test__customer__Customer: TestBase
	{
		[Fact]
		public void CreateValidCustomers()
		{
			DataTableFactory dataTableFactory = new DataTableFactory();
			DataTable customers = dataTableFactory.customer__Customer();

			customers.Rows.Add(customers.NewRow().ItemArray = new object[] { 0, 0, "AAA", "AAA Finance", 1});

			IDataPortalResult result = DataPortal.Persist__customer__Customer(customers);

			Assert.True(result.OK, "Database operation returned error.");
			Assert.Equal(1, result.RowsUpdated);
		}
	}
}
