using System;
using System.Collections.Generic;
using System.Text;

namespace TPH.Chariot.Data.Legacy.Common.Interfaces
{
	public interface IDataPortalResult
	{
		public bool OK { get; }

		public int RowsUpdated { get; }

		public IEnumerable<string> ErrorMessages { get; }

	}
}
