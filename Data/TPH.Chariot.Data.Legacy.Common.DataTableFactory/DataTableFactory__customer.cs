using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace TPH.Chariot.Data.Legacy.Common.DataTableFactory
{
	public partial class DataTableFactory
	{
		public DataTable Customer(string tableName = "Customer")
        {
            DataTable dt = new DataTable(tableName);

            dt.Columns.Add(DataColumnFactory.PrimaryKeyDataColumn(columnName: "CustomerID"));
            dt.Columns.Add(DataColumnFactory.RowVersionDataColumn());
            dt.Columns.Add(DataColumnFactory.NVarCharCodeDataColumn(columnName: "Code"));
            dt.Columns.Add(DataColumnFactory.NVarCharEntityNameDataColumn(columnName: "Customer"));
            dt.Columns.Add(DataColumnFactory.BitDataColumn(columnName: "Active"));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["CustomerID"] };
            dt.Constraints.Add(new UniqueConstraint("UQ__Customer__Code", dt.Columns["Code"]));

            return dt;
        }

        public DataTable Account(string tableName = "Account")
        {
            DataTable dt = new DataTable(tableName);

            dt.Columns.Add(DataColumnFactory.PrimaryKeyDataColumn(columnName: "AccountID"));
            dt.Columns.Add(DataColumnFactory.RowVersionDataColumn());
            dt.Columns.Add(DataColumnFactory.ForeignKeyDataColumn("CustomerID"));
            dt.Columns.Add(DataColumnFactory.NVarCharCodeDataColumn(columnName: "Code"));
            dt.Columns.Add(DataColumnFactory.NVarCharEntityNameDataColumn(columnName: "Account"));
            dt.Columns.Add(DataColumnFactory.BitDataColumn(columnName: "Active"));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["AccountID"] };
            dt.Constraints.Add(new UniqueConstraint("UQ__Account__CustomerID__Code", new DataColumn[] { dt.Columns["CustomerID"], dt.Columns["Code"] }));

            return dt;
        }
    }
}
