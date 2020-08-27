using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using TPH.Chariot.Data.Legacy.DataPortal;
using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Database.Test
{
	public class TestBase
	{
		protected IDataPortal DataPortal { get; private set; }

		public TestBase()
		{
			var config = new ConfigurationBuilder().AddJsonFile("TPH.Chariot.Database.Test.config.json").Build();
			string connectionString = config["ConnectionString"];

			DataPortal = new DataPortal();
			DataPortal.ConnectionString = connectionString;
		}
	}
}
