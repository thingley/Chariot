using System;
using System.Collections.Generic;
using System.Text;

namespace TPH.Chariot.Data.Legacy.Common.CommandFactory
{
	public sealed partial class CommandFactory
	{
		private ParameterFactory ParameterFactory { get; set; }

		public CommandFactory()
		{
			ParameterFactory = new ParameterFactory();
		}
	}
}
