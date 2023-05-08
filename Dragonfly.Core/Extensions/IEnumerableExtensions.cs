using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dragonfly.Core
{
// ReSharper disable once InconsistentNaming
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            var each     = @this as IList<T> ?? @this.ToList();
            foreach (var t in each)
            {
                action(t);
            }
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName)
        {
            var tbl = ToDataTable(collection);
            tbl.TableName = tableName;
            return tbl;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            var dt = new DataTable();
            var t = typeof(T);
            var pia = t.GetProperties();
            //Create the columns in the DataTable
            foreach (var pi in pia)
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            //Populate the table
            foreach (var item in collection)
            {
                var dr = dt.NewRow();
                dr.BeginEdit();
                foreach (var pi in pia)
                {
                    dr[pi.Name] = pi.GetValue(item, null);
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}