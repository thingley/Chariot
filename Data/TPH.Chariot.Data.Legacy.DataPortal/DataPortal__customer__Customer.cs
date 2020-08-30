using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Microsoft.Data.SqlClient;

using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Data.Legacy.DataPortal
{
	public partial class DataPortal
	{
		public IDataPortalResult Persist__customer__Customer(DataTable customerCustomerDataTable)
		{
			TransactedDatabaseOperation updateCustomers = PerformUpdate;

			return DoTransactedDatabaseOperations(new TransactedDatabaseOperation[] { updateCustomers });

			// Local function
			int PerformUpdate(SqlConnection connection, SqlTransaction transaction)
			{
				SqlDataAdapter da = new SqlDataAdapter()
				{
					ContinueUpdateOnError = false,
					InsertCommand = CommandFactory.customer__UP__Table__Customer__Insert(connection, transaction),
					UpdateCommand = CommandFactory.customer__UP__Table__Customer__Update(connection, transaction),
					DeleteCommand = CommandFactory.customer__UP__Table__Customer__Delete(connection, transaction),
				};

				DataTable changesOnly = customerCustomerDataTable.GetChanges();

				int rowsUpdated = da.Update(changesOnly);
				PurgeNewRows(customerCustomerDataTable);
				customerCustomerDataTable.Merge(changesOnly);
				customerCustomerDataTable.AcceptChanges();

				return rowsUpdated;
			}
		}
	}
}
