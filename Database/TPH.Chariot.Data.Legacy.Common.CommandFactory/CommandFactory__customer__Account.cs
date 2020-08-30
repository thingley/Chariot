using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.CommandFactory
{
    public sealed partial class CommandFactory
    {
        public SqlCommand customer__UP__Table__Account__Insert(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[customer].[UP__Table__Account__Insert]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pAccountID", sourceColumn: "AccountID", forInsert: true),
                ParameterFactory.RowVersionParameter(forInsert: true),
                ParameterFactory.ForeignKeyParameter(parameterName: "@pCustomerID", sourceColumn: "CustomerID"),
                ParameterFactory.NVarCharCodeParameter(),
                ParameterFactory.NVarCharEntityNameParameter(parameterName: "@pAccount", sourceColumn: "Account"),
                ParameterFactory.BitParameter(parameterName: "@pActive", sourceColumn: "Active"),
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }

        public SqlCommand customer__UP__Table__Account__Update(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[customer].[UP__Table__Account__Update]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pAccountID", sourceColumn: "AccountID", forInsert: false),
                ParameterFactory.RowVersionParameter(forInsert: false),
                ParameterFactory.ForeignKeyParameter(parameterName: "@pCustomerID", sourceColumn: "CustomerID"),
                ParameterFactory.NVarCharCodeParameter(),
                ParameterFactory.NVarCharEntityNameParameter(parameterName: "@pAccount", sourceColumn: "Account"),
                ParameterFactory.BitParameter(parameterName: "@pActive", sourceColumn: "Active"),
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }

        public SqlCommand customer__UP__Table__Account__Delete(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = @"[customer].[UP__Table__Account__Delete]",
                Transaction = transaction,
            };

            SqlParameter[] pa = new SqlParameter[]
            {
                ParameterFactory.ReturnValueParameter(),
                ParameterFactory.PrimaryKeyParameter(parameterName: "@pAccountID", sourceColumn: "AccountID", forInsert: false),
                ParameterFactory.RowVersionParameter(forInsert: false),
            };

            cmd.Parameters.AddRange(pa);

            return cmd;
        }
    }
}
