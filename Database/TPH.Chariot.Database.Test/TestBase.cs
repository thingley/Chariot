using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Transactions;

using TPH.Chariot.Data.Legacy.DataPortal;
using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Database.Test
{
	public class TestBase
	{
		protected delegate void TestMethod();

		protected DataTableFactory DataTableFactory { get; private set; }
		protected IDataPortal DataPortal { get; private set; }

		public TestBase()
		{
			var config = new ConfigurationBuilder().AddJsonFile("TPH.Chariot.Database.Test.config.json").Build();
			string connectionString = config["ConnectionString"];

			DataTableFactory = new DataTableFactory();

			DataPortal = new DataPortal();
			DataPortal.ConnectionString = connectionString;
		}

		protected void RunTestMethod(TestMethod testMethod)
		{
			try
			{
				using (TransactionScope transactionScope = new TransactionScope())
				{
					testMethod.Invoke();
					throw new IgnorableTestException("Rollback transaction");   // Raise exception which causes the transaction to be rolled back
				}
			}
			catch (IgnorableTestException ex)
			{
				// Do nothing (fake exception)
			}
			catch (Exception ex)
			{
				// Re-throw exception for test code to handle/catch
				throw ex;
			}
		}
	}
}
