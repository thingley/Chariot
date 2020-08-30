using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Data.Legacy.DataPortal
{
	public class DataPortalResult : IDataPortalResult
	{
		public bool OK { get; private set; }

		public int RowsUpdated { get; internal set; }

		public IEnumerable<string> ErrorMessages { get; private set; }

		public string FirstErrorMessage { get { return ErrorMessages?.First() ?? string.Empty; } }

		internal DataPortalResult()
		{
			OK = true;
			RowsUpdated = 0;
		}

		internal void Initialise(IEnumerable<string> errorMessages)
		{
			OK = false;
			ErrorMessages = errorMessages;
		}
	}
}
