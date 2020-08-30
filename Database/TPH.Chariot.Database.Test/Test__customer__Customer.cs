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
			DataSet dataSet = new DataSet();
			DataTableFactory dataTableFactory = new DataTableFactory();

			DataTable customer__Customer = dataTableFactory.customer__Customer();
			dataSet.Tables.Add(customer__Customer);

			DataTable customer__Account = dataTableFactory.customer__Account();
			dataSet.Tables.Add(customer__Account);
			dataSet.Relations.Add(name: "FK__customer__Customer__customer__Account",
				parentColumn: customer__Customer.Columns["CustomerID"],
				childColumn: customer__Account.Columns["CustomerID"]);

			DataRow customer__CustomerDataRow = null;
			DataRow customer__AccountDataRow = null;

			for (int customer__CustomerIndex = 1; customer__CustomerIndex <= 100; customer__CustomerIndex++)
			{
				customer__CustomerDataRow = customer__Customer.NewRow();
				customer__CustomerDataRow["Code"] = $"{customer__CustomerIndex.ToString().PadLeft(4, '0')}";
				customer__CustomerDataRow["Customer"] = $"Customer {customer__CustomerIndex.ToString().PadLeft(4, '0')}";
				customer__CustomerDataRow["Active"] = 1;
				customer__Customer.Rows.Add(customer__CustomerDataRow);
			}

			IDataPortalResult result = DataPortal.Persist__customer__Customer(customer__Customer);

			Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
			Assert.Equal(100, result.RowsUpdated);

			foreach (DataRow parentDataRow in customer__Customer.Rows)
			{
				for (int customer_AccountIndex = 1; customer_AccountIndex <= 3; customer_AccountIndex++)
				{
					customer__AccountDataRow = customer__Account.NewRow();
					customer__AccountDataRow["CustomerID"] = parentDataRow["CustomerID"];
					customer__AccountDataRow["Code"] = $"{customer_AccountIndex.ToString().PadLeft(4, '0')}";
					customer__AccountDataRow["Account"] = $"Account {customer_AccountIndex.ToString().PadLeft(4, '0')}";
					customer__AccountDataRow["Active"] = 1;
					customer__Account.Rows.Add(customer__AccountDataRow);
				}
			}

			result = DataPortal.Persist__customer__Account(customer__Account);

			Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
			Assert.Equal(300, result.RowsUpdated);
		}
	}
}
