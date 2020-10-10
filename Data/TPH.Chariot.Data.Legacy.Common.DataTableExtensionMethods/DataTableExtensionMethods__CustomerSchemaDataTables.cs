using System;
using System.Collections.Generic;
using System.Text;

using System.Data.Common;
using System.Data;
using System.Runtime.CompilerServices;

namespace TPH.Chariot.Data.Legacy.Common.DataTableExtensionMethods
{
	public static partial class DataTableExtensionMethods
	{
		public static DataRow AddCustomerRow(this DataTable customerDataTable, string code, string customer = null, bool active = true)
		{
			if (customerDataTable.Namespace != "Customer")
				throw new ArgumentException("AddCustomerRow() called against non-Customer DataTable!");

			DataRow dr = customerDataTable.NewRow();
			dr["Code"] = code;
			dr["Customer"] = customer ?? Guid.NewGuid().ToString();
			dr["Active"] = active;
			customerDataTable.Rows.Add(dr);

			return dr;
		}

		public static DataRow AddAccountRow(this DataTable accountDataTable, long customerID, string code, string account = null, bool active = true)
		{
			if (accountDataTable.Namespace != "Account")
				throw new ArgumentException("AddAccountRow() called against non-Account DataTable!");

			DataRow dr = accountDataTable.NewRow();
			dr["CustomerID"] = customerID;
			dr["Code"] = code;
			dr["Account"] = account ?? Guid.NewGuid().ToString();
			dr["Active"] = active;
			accountDataTable.Rows.Add(dr);

			return dr;
		}
	}
}
