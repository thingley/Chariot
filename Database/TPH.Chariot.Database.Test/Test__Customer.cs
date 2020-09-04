﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Xunit;

using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;
using TPH.Chariot.Data.Legacy.DataPortal;

namespace TPH.Chariot.Database.Test
{
	public class Test__Customer: TestBase
	{
		[Fact]
		public void CreateValidCustomers()
		{
			DataSet dataSet = new DataSet();
			DataTableFactory dataTableFactory = new DataTableFactory();

			DataTable customerDataTable = dataTableFactory.Customer();
			dataSet.Tables.Add(customerDataTable);

			DataTable accountDataTable = dataTableFactory.Account();
			dataSet.Tables.Add(accountDataTable);
			dataSet.Relations.Add(name: "FK__Customer__Account",
				parentColumn: customerDataTable.Columns["CustomerID"],
				childColumn: accountDataTable.Columns["CustomerID"]);

			DataRow customerDataRow = null;
			DataRow accountDataRow = null;

			for (int customerIndex = 1; customerIndex <= 100; customerIndex++)
			{
				customerDataRow = customerDataTable.NewRow();
				customerDataRow["Code"] = $"{customerIndex.ToString().PadLeft(4, '0')}";
				customerDataRow["Customer"] = $"Customer {customerIndex.ToString().PadLeft(4, '0')}";
				customerDataRow["Active"] = 1;
				customerDataTable.Rows.Add(customerDataRow);
			}

			IDataPortalResult result = DataPortal.Persist__Customer(customerDataTable);

			Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
			Assert.Equal(100, result.RowsUpdated);

			foreach (DataRow parentDataRow in customerDataTable.Rows)
			{
				for (int accountIndex = 1; accountIndex <= 3; accountIndex++)
				{
					accountDataRow = accountDataTable.NewRow();
					accountDataRow["CustomerID"] = parentDataRow["CustomerID"];
					accountDataRow["Code"] = $"{accountIndex.ToString().PadLeft(4, '0')}";
					accountDataRow["Account"] = $"Account {accountIndex.ToString().PadLeft(4, '0')}";
					accountDataRow["Active"] = 1;
					accountDataTable.Rows.Add(accountDataRow);
				}
			}

			result = DataPortal.Persist__Account(accountDataTable);

			Assert.True(result.OK, $"Database operation returned error: {result.FirstErrorMessage}");
			Assert.Equal(300, result.RowsUpdated);
		}
	}
}