
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FireFighting.DBTool
{
    public static class DbReader
    {
        public static DataTable ConverDateReaderToDataTable(IDataReader dataReader) 
        {
            DataTable dataTable = new DataTable();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                DataColumn myDateColum = new DataColumn();
                myDateColum.DataType = dataReader.GetFieldType(i);
                myDateColum.ColumnName = dataReader.GetName(i);
                dataTable.Columns.Add(myDateColum);
            }
            while (dataReader.Read())
            {
                DataRow myDateRow = dataTable.NewRow();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    myDateRow[i] = dataReader[i].ToString();
                }
                dataTable.Rows.Add(myDateRow);
                myDateRow = null;
            }
            dataReader.Close();
            return dataTable;
        }

        public static System.Collections.Generic.List<T> DataReaderToList<T>(IDataReader idr)
        {
            System.Collections.Generic.List<T> resultList;
            try
            {
                System.Collections.Generic.List<string> fieldList = new System.Collections.Generic.List<string>(idr.FieldCount);
                for (int i = 0; i < idr.FieldCount; i++)
                {
                    fieldList.Add(idr.GetName(i).ToLower());
                }

                System.Collections.Generic.List<T> list2 = new System.Collections.Generic.List<T>();
                while (idr.Read())
                {
                    T t = System.Activator.CreateInstance<T>();
                    System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetProperty);
                    for (int j = 0; j < properties.Length; j++)
                    {
                        System.Reflection.PropertyInfo propertyInfo = properties[j];
                        if (fieldList.Contains(propertyInfo.Name.ToLower()))
                        {
                            if (!string.IsNullOrEmpty(idr[propertyInfo.Name].ToString()))
                            {
                                //Log4NetHelper.WriteLog(propertyInfo.Name);
                                propertyInfo.SetValue(t, DbReader.HackType(idr[propertyInfo.Name], propertyInfo.PropertyType), null);
                            }
                        }
                    }
                    list2.Add(t);
                }
                resultList = list2;
            }
            finally
            {
                if (idr != null)
                {
                    idr.Close();
                    idr.Dispose();
                }
            }
            return resultList;
        }

        public static object HackType(object value, System.Type conversionType)
        {
            object result;
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(System.Nullable<>)))
            {
                if (value == null)
                {
                    result = null;
                    return result;
                }
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            result = System.Convert.ChangeType(value, conversionType);
            return result;
        }
    }
}
