using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.DataTableFactory
{
	internal class DataColumnFactory
	{
        #region Internal Constants

        private const int MAXLEN__CODE = 20;
        private const int MAXLEN__LOOKUP = 20;
        private const int MAXLEN__ENTITY_NAME = 100;

		#endregion

		#region Internal Interface

		internal DataColumn PrimaryKeyDataColumn(string columnName)
        {
            return new DataColumn()
            {
                ColumnName = columnName,
                DataType = typeof(Int64),
                AutoIncrement = true,
                AutoIncrementSeed = -1,
                AutoIncrementStep = -1,
            };
        }

        internal DataColumn ForeignKeyDataColumn(string columnName)
        {
            return new DataColumn()
            {
                ColumnName = columnName,
                DataType = typeof(Int64),
                DefaultValue = null,
            };
        }

        internal DataColumn RowVersionDataColumn(string columnName = "RV")
        {
            return new DataColumn()
            {
                ColumnName = columnName,
                DataType = typeof(byte[]),
                DefaultValue = DBNull.Value,
            };
        }

        internal DataColumn NVarCharDataColumn(string columnName, int maxLength)
        {
            return new DataColumn()
            {
                ColumnName = columnName,
                DataType = typeof(string),
                MaxLength = maxLength,
                DefaultValue = string.Empty,
            };
        }

        internal DataColumn NVarCharCodeDataColumn(string columnName)
        {
            return NVarCharDataColumn(columnName: columnName, maxLength: MAXLEN__CODE);
        }

        internal DataColumn NVarCharLookupDataColumn(string columnName)
        {
            return NVarCharDataColumn(columnName: columnName, maxLength: MAXLEN__LOOKUP);
        }

        internal DataColumn NVarCharEntityNameDataColumn(string columnName)
        {
            return NVarCharDataColumn(columnName: columnName, maxLength: MAXLEN__ENTITY_NAME);
        }

        internal DataColumn NVarCharMaxDataColumn(string columnName)
        {
            return NVarCharDataColumn(columnName: columnName, maxLength: -1);
        }

        internal DataColumn BitDataColumn(string columnName)
        {
            return new DataColumn()
            {
                ColumnName = columnName,
                DataType = typeof(bool),
                DefaultValue = false,
            };
        }

        #endregion
    }
}
