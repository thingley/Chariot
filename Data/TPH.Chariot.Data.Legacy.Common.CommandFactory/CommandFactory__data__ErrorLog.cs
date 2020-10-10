using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.CommandFactory
{
	public partial class CommandFactory
	{
        public SqlCommand UP__Table__ErrorLog__Insert(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[data].[UP__Table__ErrorLog__Insert]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pErrorLogID", sourceColumn: "ErrorLogID", forInsert: true),
                new SqlParameter(){ ParameterName = "@pErrorNumber", SqlDbType = SqlDbType.BigInt, SourceColumn = "@ErrorNumber" },
                ParameterFactory.NVarCharMaxParameter(parameterName: "@pErrorProcedure", "ErrorProcedure"),
                ParameterFactory.NVarCharMaxParameter(parameterName: "@pErrorMessage", "ErrorMessage"),
                new SqlParameter(){ ParameterName = "@pErrorSeverity", SqlDbType = SqlDbType.BigInt, SourceColumn = "@ErrorSeverity" },
                new SqlParameter(){ ParameterName = "@pErrorState", SqlDbType = SqlDbType.BigInt, SourceColumn = "@ErrorState" },
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }

    }
}
