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
		public IDataPortalResult Persist__customer__Account(DataTable customerAccountDataTable)
		{
			TransactedDatabaseOperation updateAccounts = PerformUpdate;

			return DoTransactedDatabaseOperations(new TransactedDatabaseOperation[] { updateAccounts });

			// Local function
			int PerformUpdate(SqlConnection connection, SqlTransaction transaction)
			{
				SqlDataAdapter da = new SqlDataAdapter()
				{
					ContinueUpdateOnError = false,
					InsertCommand = CommandFactory.customer__UP__Table__Account__Insert(connection, transaction),
					UpdateCommand = CommandFactory.customer__UP__Table__Account__Update(connection, transaction),
					DeleteCommand = CommandFactory.customer__UP__Table__Account__Delete(connection, transaction),
				};

				DataTable changesOnly = customerAccountDataTable.GetChanges();

				int rowsUpdated = da.Update(changesOnly);
				PurgeNewRows(customerAccountDataTable);
				customerAccountDataTable.Merge(changesOnly);
				customerAccountDataTable.AcceptChanges();

				return rowsUpdated;
			}
		}
	}
}
