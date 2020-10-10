using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Data.SqlClient;
using System.IO;

using TPH.Chariot.Data.Legacy.DataPortal;
using TPH.Chariot.Data.Legacy.Common.DataTableFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Database.Test
{
	public class TestBase
	{
		protected IDataPortal DataPortal { get; private set; }
		protected DataTableFactory DataTableFactory { get; private set; }
		protected string DatabaseBackupFolderPath { get; private set; }

		readonly string _databaseName;
		readonly string _connnectionString;

		public TestBase()
		{
			var config = new ConfigurationBuilder().AddJsonFile("TPH.Chariot.Database.Test.config.json").Build();
			string sqlServerInstanceName = config["SQLServerInstanceName"];
			_databaseName =  config["DatabaseName"];
			string userName = config["UserName"];
			string password = config["Password"];

			DataPortal = new DataPortal(sqlServerInstanceName: sqlServerInstanceName,
				databaseName: _databaseName,
				userName: userName,
				password: password);

			DataTableFactory = new DataTableFactory();
			DatabaseBackupFolderPath = config["DatabaseBackupFolderPath"];

			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder()
			{
				DataSource = sqlServerInstanceName,
				InitialCatalog = "master",
			};

			if (string.IsNullOrWhiteSpace(userName))
			{
				csb.IntegratedSecurity = true;
			}
			else
			{
				csb.UserID = userName;
				csb.Password = password;
			}

			_connnectionString = csb.ConnectionString;
		}

		protected void RunTestMethod(Action testMethod)
		{
			string backupFilePath = Path.Combine(DatabaseBackupFolderPath, _databaseName + ".bak");

			if (File.Exists(backupFilePath))
				RestoreBackup(backupFilePath: backupFilePath);
			else
				CreateBackup(backupFilePath: backupFilePath);

			testMethod.Invoke();
		}

		private void CreateBackup(string backupFilePath)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append($"ALTER DATABASE [{_databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
			sb.Append($"BACKUP DATABASE [{_databaseName}] TO  DISK = N'{backupFilePath}' WITH NOFORMAT, NOINIT,  NAME = N'{_databaseName} Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10;");
			sb.Append($"ALTER DATABASE[{_databaseName}] SET MULTI_USER;");

			RunSQL(sql: sb.ToString());
		}

		private void RestoreBackup(string backupFilePath)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append($"ALTER DATABASE [{_databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
			sb.Append($"RESTORE DATABASE [{_databaseName}] FROM  DISK = N'{backupFilePath}' WITH  FILE = 1,  NOUNLOAD,  STATS = 5;");
			sb.Append($"ALTER DATABASE[{_databaseName}] SET MULTI_USER;");

			RunSQL(sql: sb.ToString());
		}

		private void RunSQL(string sql)
		{
			using (SqlConnection connection = new SqlConnection(_connnectionString))
			{
				SqlCommand command = new SqlCommand()
				{
					Connection = connection,
					CommandText = sql,
					CommandType = System.Data.CommandType.Text,
				};

				connection.Open();
				command.ExecuteNonQuery();
			}
		}
	}
}
