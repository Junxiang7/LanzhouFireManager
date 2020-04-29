using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FireFighting.Tool
{
    public static class ErrorLog
    {
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="Method"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="Parameter"></param>
        public static void InsertError(string ClassName, string Method, string ErrorMsg, string Parameter)
        {
            try
            {
                string SqlStr = "insert into tbl_errorlog(ClassName,Method,ErrorMsg,Parameter,CreateTime)values(@ClassName,@Method,@ErrorMsg,@Parameter,NOW())";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@ClassName",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@Method",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@ErrorMsg",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@Parameter",MySqlDbType.VarChar,50),

    };
                Mpa[0].Value = ClassName;
                Mpa[1].Value = Method;
                Mpa[2].Value = ErrorMsg;
                Mpa[3].Value = Parameter;
                MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="Method"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="Parameter"></param>
        public static void InsertOperation(string Operator, string OperationContent, int OperationType, string OperationName)
        {
            try
            {
                string SqlStr = "insert into tbl_OperationLog(Operator,OperationContent,OperationTime,OperationType,OperationName)values(@Operator,@OperationContent,NOW(),@OperationType,@OperationName)";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@Operator",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@OperationContent",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@OperationType",MySqlDbType.Int32),
                                    new MySqlParameter("@OperationName",MySqlDbType.VarChar,50),

    };
                Mpa[0].Value = Operator;
                Mpa[1].Value = OperationContent;
                Mpa[2].Value = OperationType;
                Mpa[3].Value = OperationName;
                MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
