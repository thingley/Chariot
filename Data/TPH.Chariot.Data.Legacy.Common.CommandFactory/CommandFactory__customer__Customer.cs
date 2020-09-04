using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.CommandFactory
{
	public sealed partial class CommandFactory
	{
        public SqlCommand UP__Table__Customer__Insert(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[customer].[UP__Table__Customer__Insert]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pCustomerID", sourceColumn: "CustomerID", forInsert: true),
                ParameterFactory.RowVersionParameter(forInsert: true),
                ParameterFactory.NVarCharCodeParameter(),
                ParameterFactory.NVarCharEntityNameParameter(parameterName: "@pCustomer", sourceColumn: "Customer"),
                ParameterFactory.BitParameter(parameterName: "@pActive", sourceColumn: "Active"),
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }

        public SqlCommand UP__Table__Customer__Update(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[customer].[UP__Table__Customer__Update]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pCustomerID", sourceColumn: "CustomerID", forInsert: false),
                ParameterFactory.RowVersionParameter(forInsert: false),
                ParameterFactory.NVarCharCodeParameter(),
                ParameterFactory.NVarCharEntityNameParameter(parameterName: "@pCustomer", sourceColumn: "Customer"),
                ParameterFactory.BitParameter(parameterName: "@pActive", sourceColumn: "Active"),
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }

        public SqlCommand UP__Table__Customer__Delete(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[customer].[UP__Table__Customer__Delete]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pCustomerID", sourceColumn: "CustomerID", forInsert: false),
                ParameterFactory.RowVersionParameter(forInsert: false),
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }
    }
}
