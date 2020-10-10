using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.Interfaces
{
	public interface IDataPortal
	{
		string ConnectionString { get; }

		void SetConnectionString(string sqlServerInstanceName, string databaseName, string userName = "", string password = "");

		IDataPortalResult Persist__Customer(DataTable customerCustomerDataTable);

		IDataPortalResult Persist__Account(DataTable customerAccountDataTable);
	}
}
