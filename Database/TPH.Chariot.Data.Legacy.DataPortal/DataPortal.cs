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
                dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
            }
            catch (Exception ex)
            {
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
                        transaction.Rollback();
                    }

                    dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    dataPortalResult.Initialise(new string[] { ex.Message.Replace(APPLICATION_ERROR_PREFIX, string.Empty) });
                }
            }

            return dataPortalResult;
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
