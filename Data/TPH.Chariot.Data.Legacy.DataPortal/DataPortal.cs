using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Microsoft.Data.SqlClient;

using TPH.Chariot.Data.Legacy.Common.CommandFactory;
using TPH.Chariot.Data.Legacy.Common.Interfaces;

namespace TPH.Chariot.Data.Legacy.DataPortal
{
	public partial class DataPortal : IDataPortal
	{
        #region Constants
        private const string APPLICATION_ERROR_PREFIX = "ChariotException:";
		#endregion

		#region Delegates
		private delegate void NonTransactedDatabaseOperation(SqlConnection connection);
		private delegate int TransactedDatabaseOperation(SqlConnection connection, SqlTransaction transaction);
		#endregion

		#region Properties
		public string ConnectionString { get; set; }

        private CommandFactory CommandFactory { get; set; }
		#endregion

		#region Constructors
		public DataPortal()
        {
            CommandFactory = new CommandFactory();
        }

        public DataPortal(string sqlServerInstanceName, string databaseName, string userName = "", string password = "") : this()
        {
            SetConnectionString(sqlServerInstanceName: sqlServerInstanceName, databaseName: databaseName, userName: userName, password: password);
        }
        #endregion

        #region Public Methods
        public void SetConnectionString(string sqlServerInstanceName, string databaseName, string userName = "", string password = "")
		{
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder()
            {
                DataSource = sqlServerInstanceName,
                InitialCatalog = databaseName,
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

            ConnectionString = csb.ConnectionString;
        }

        #endregion

        #region Private Methods
        private IDataPortalResult DoNonTransactedDatabaseOperations(IEnumerable<NonTransactedDatabaseOperation> nonTransactedDatabaseOperations)
        {
            DataPortalResult dataPortalResult = new DataPortalResult();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    foreach (NonTransactedDatabaseOperation nonTransactedDatabaseOperation in nonTransactedDatabaseOperations)
                    {
                        nonTransactedDatabaseOperation.Invoke(connection);
                    }
                }
            }
            catch (SqlException ex)
            {
                WriteErrorLog(errorNumber: ex.Number,
                    errorProcedure: ex.Procedure,
                    errorMessage: ex.Message,
                    errorState: ex.State);

                dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
            }
            catch (Exception ex)
            {
                WriteErrorLog(errorMessage: ex.Message);

                dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
            }

            return dataPortalResult;
        }

        private IDataPortalResult DoTransactedDatabaseOperations(IEnumerable<TransactedDatabaseOperation> transactedDatabaseOperations)
        {
            DataPortalResult dataPortalResult = new DataPortalResult();
            SqlTransaction transaction = null;
            int rowsUpdated = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (TransactedDatabaseOperation transactedDatabaseOperation in transactedDatabaseOperations)
                    {
                        rowsUpdated += transactedDatabaseOperation.Invoke(connection, transaction);
                    }

                    transaction.Commit();
                    dataPortalResult.RowsUpdated = rowsUpdated;
                }
                catch (SqlException ex)
                {
                    if (transaction != null)
                    {
                        int tranCount = GetTranCount(connection);

                        if (tranCount >= 1)
                        {
                            transaction.Rollback();
                        }
                    }

                    WriteErrorLog(errorNumber: ex.Number,
                        errorProcedure: ex.Procedure,
                        errorMessage: ex.Message,
                        errorState: ex.State);

                    dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
                }
                catch (Exception ex)
                {
                    int tranCount = GetTranCount(connection);

                    if (tranCount >= 1)
                    {
                        transaction.Rollback();
                    }

                    WriteErrorLog(errorMessage: ex.Message);

                    dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
                }
            }

            return dataPortalResult;
        }

        private int GetTranCount(SqlConnection connection)
		{
            int result = -1;

            SqlCommand cmd = new SqlCommand("SELECT @@TRANCOUNT", connection);

            try { result = (int)cmd.ExecuteScalar(); }
            catch
            {
                // If there is an error we ignore it and leave the result as -1
            }   

            return result;
        }

        private void WriteErrorLog(long? errorNumber = null, string errorProcedure = null, string errorMessage = null, long? errorSeverity = null, long? errorState = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = CommandFactory.UP__Table__ErrorLog__Insert(connection: connection);

                cmd.Parameters["@pErrorLogID"].SqlValue = 0;
                cmd.Parameters["@pErrorNumber"].SqlValue = HandleFrameworkToDBNullConversion(errorNumber);
                cmd.Parameters["@pErrorProcedure"].SqlValue = HandleFrameworkToDBNullConversion(errorProcedure);
                cmd.Parameters["@pErrorMessage"].SqlValue = HandleFrameworkToDBNullConversion(errorMessage);
                cmd.Parameters["@pErrorSeverity"].SqlValue = HandleFrameworkToDBNullConversion(errorSeverity);
                cmd.Parameters["@pErrorState"].SqlValue = HandleFrameworkToDBNullConversion(errorState);

                connection.Open();
                cmd.BeginExecuteNonQuery();
            }
        }

        private object HandleFrameworkToDBNullConversion(object value)
        {
            return value ?? DBNull.Value;
        }

        private void PurgeNewRows(DataTable dataTable)
        {
            foreach (DataRow dr in dataTable.Select("", "", DataViewRowState.Added))
            {
                dataTable.Rows.Remove(dr);
            }
        }
		#endregion
	}
}
