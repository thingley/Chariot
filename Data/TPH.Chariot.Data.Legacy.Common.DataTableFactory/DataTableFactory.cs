using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.DataTableFactory
{
	public sealed partial class DataTableFactory
	{
 		private DataColumnFactory DataColumnFactory { get; set; }

		public DataTableFactory()
		{
			DataColumnFactory = new DataColumnFactory();
		}
    }
}

