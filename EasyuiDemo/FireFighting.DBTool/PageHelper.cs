
using FireFighting.DBTool;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

public class PageHelper
    {
        #region 分页
        /// <summary>
        /// 方法: 获得分页数据, 带查询条件;
        /// </summary>
        /// <param name="sql">查询数据SQL语句</param>
        /// <param name="param">对应sql参数的条件参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="count">返回总查询数量</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPageTable(string sql, MySqlParameter[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            //定义记录条数变量;
            int num = 0;
            int num2 = 0;

            //定义SQL语句拼接字符串;
            string str = string.Empty;

            //定义StringBuilder;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            //判断当前页索引,如果是0,则初始化为1;
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

            //计算上一页总记录条数和当前页总记录条数值，第多少条记录;
            num = (pageIndex - 1) * pageSize;
            num2 = pageIndex * pageSize;

            //判断参数orderField是否为null或者'';
            if (!string.IsNullOrEmpty(orderField))
            {
                str = "ORDER BY " + orderField + " " + orderType;
            }
            else
            {
                str = "ORDER BY (SELECT 0)";
            }

            //stringbuilder拼装SQL语句, 分页功能;
            stringBuilder.Append(sql);

            //排序，LIMIT分页处理
            stringBuilder.Append(string.Concat(new object[]
			{
				str, " LIMIT ", num, ", ", num2
			}));

            //返回参数, 获得查询总记录数;
            count = System.Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM (" + sql + ") AS t", param));

            //执行查询, 参数1.命令类型SQL文本; 2.SQL查询语句; 3.查询条件参数;
            IDataReader reader = MySqlHelper.ExecuteDataReader(stringBuilder.ToString(), CommandType.Text, param);

            //返回查询datatable;
            return DbReader.ConverDateReaderToDataTable(reader);
        }

        /// <summary>
        /// 方法: 获得分页数据, 不带查询条件;
        /// </summary>
        /// <param name="sql">查询数据SQL语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="count">返回总查询数量</param>
        /// <returns>DataTable</returns>
        public static DataTable GetPageTable(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            //调用带查询条件的方法
            return PageHelper.GetPageTable(sql, null, orderField, orderType, pageIndex, pageSize, ref count);
        }

        /// <summary>
        /// 方法: 获得分页数据, 带查询条件, 返回List<>;
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="sql">查询数据SQL语句</param>
        /// <param name="param">对应sql参数的条件参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="count">返回总查询数量</param>
        /// <returns>List<></returns>
        public static System.Collections.Generic.List<T> GetPageList<T>(string sql, MySqlParameter[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            sql = sql.Replace("char(", " ").Replace("CHAR(", " "); 
            //定义记录条数变量;
            int num = 0;
            int num2 = 0;

            //定义SQL语句拼接字符串;
            string str = string.Empty;

            //定义StringBuilder;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            //判断当前页索引,如果是0,则初始化为1;
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

            //计算上一页总记录条数和当前页总记录条数;
            num = (pageIndex - 1) * pageSize;
            num2 = pageIndex * pageSize;

            //判断参数orderField是否为null或者'';
            if (!string.IsNullOrEmpty(orderField))
            {
                str = "ORDER BY " + orderField + " " + orderType;
            }
            else
            {
                str = "ORDER BY (SELECT 0)";
            }

            //stringbuilder拼装SQL语句, 分页功能;
            stringBuilder.Append(sql);

            //排序，LIMIT分页处理
            stringBuilder.Append(string.Concat(new object[]
			{
				str, " LIMIT ", num, ", ", pageSize
			}));

            count = System.Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, "Select Count(1) From (" + sql + ") As t", param));
            IDataReader dr = MySqlHelper.ExecuteDataReader(stringBuilder.ToString(), CommandType.Text, param);
            return DbReader.DataReaderToList<T>(dr);
        }

        /// <summary>
        /// 方法: 获得分页数据, 不带查询条件, 返回List<>;
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="sql">查询数据SQL语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="count">返回总查询数量</param>
        /// <returns>List<></returns>
        public static System.Collections.Generic.List<T> GetPageList<T>(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            //调用带查询条件的方法
            return PageHelper.GetPageList<T>(sql, null, orderField, orderType, pageIndex, pageSize, ref count);
        }

        #endregion
    }
