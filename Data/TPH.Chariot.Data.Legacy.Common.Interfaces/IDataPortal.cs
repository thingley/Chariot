﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.Interfaces
{
	public interface IDataPortal
	{
		string ConnectionString { set; }

		IDataPortalResult Persist__Customer(DataTable customerCustomerDataTable);

		IDataPortalResult Persist__Account(DataTable customerAccountDataTable);
	}
}
