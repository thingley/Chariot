using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.CommandFactory
{
	internal class ParameterFactory
	{
        #region Internal Constants

        private const int MAXLEN__CODE = 20;
        private const int MAXLEN__LOOKUP = 20;
        private const int MAXLEN__ENTITY_NAME = 100;

        #endregion

        internal SqlParameter ReturnValueParameter()
        {
            return new SqlParameter()
            {
                ParameterName = "ReturnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue,
            };
        }

        internal SqlParameter PrimaryKeyParameter(string parameterName, string sourceColumn, bool forInsert)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.InputOutput,
                SourceColumn = sourceColumn,
                SourceVersion = (forInsert ? DataRowVersion.Current : DataRowVersion.Original),
            };
        }

        internal SqlParameter RowVersionParameter(bool forInsert)
		{
            return new SqlParameter()
            {
                ParameterName = "@pRV",
                SqlDbType = SqlDbType.Timestamp,
                Direction = ParameterDirection.InputOutput,
                SourceColumn = "RV",
                SourceVersion = (forInsert ? DataRowVersion.Current : DataRowVersion.Original),
            };
        }

        internal SqlParameter ForeignKeyParameter(string parameterName, string sourceColumn)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.InputOutput,
                SourceColumn = sourceColumn,
                SourceVersion = DataRowVersion.Current,
            };
        }

        internal SqlParameter NVarCharParameter(string parameterName, int size, string sourceColumn)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType.NVarChar,
                Size = size,
                Direction = ParameterDirection.InputOutput,
                SourceColumn = sourceColumn,
                SourceVersion = DataRowVersion.Current
            };
        }

        internal SqlParameter NVarCharCodeParameter()
		{
            return NVarCharParameter(parameterName: "@pCode", size: MAXLEN__CODE, sourceColumn: "Code");
		}

        internal SqlParameter NVarCharLookupParameter()
        {
            return NVarCharParameter(parameterName: "@pLookup", size: MAXLEN__LOOKUP, sourceColumn: "Lookup");
        }

        internal SqlParameter NVarCharEntityNameParameter(string parameterName, string sourceColumn)
        {
            return NVarCharParameter(parameterName: parameterName, size: MAXLEN__LOOKUP, sourceColumn: sourceColumn);
        }

        internal SqlParameter NVarCharMaxParameter(string parameterName, string sourceColumn)
        {
            return NVarCharParameter(parameterName: parameterName, size: -1, sourceColumn: sourceColumn);
        }

        internal SqlParameter BitParameter(string parameterName, string sourceColumn)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.InputOutput,
                SourceColumn = sourceColumn,
                SourceVersion = DataRowVersion.Current
            };
        }
    }
}
