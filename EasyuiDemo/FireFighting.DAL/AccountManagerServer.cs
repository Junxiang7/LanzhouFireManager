
using FireFighting.NET;
using FireFighting.Tool;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.DAL
{
    public class AccountManagerServer
    {
        public List<AccountInfoEnt> SearchAccountManagerInfo(ref PageParameterServerEnt PageParameter)
        {
            List<AccountInfoEnt> accountInfoli = new List<AccountInfoEnt>();
            try
            {
                string sqlStr = "select  EndTime,StartTime,UnlockPwd,Id,Account,AccountName,AccountStatus,lastTime,lastIP,LoginCount from tbl_userinfo ";
                PageParameter.Orderby = "CreateTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                accountInfoli = PageHelper.GetPageList<AccountInfoEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (accountInfoli.Count > 0)
                {
                    PageParameter.TotalCount = TotalCount;

                    decimal temp = TransformDataHelper.TransformToDecimal(TotalCount.ToString()) / TransformDataHelper.TransformToDecimal(PageParameter.PageSize.ToString());
                    decimal tempzero = temp;
                    temp = Math.Round(temp, 0);
                    if (temp < tempzero)
                    {
                        temp = temp + 1M;
                    }
                    PageParameter.PageCount = (int)temp;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "SearchAccountManagerInfo",ex.Message.ToString(), PageParameter.Where);
            }
            return accountInfoli;
        }

        public AccountInfoEnt SearchAccountinfo(string Account)
        {
            MySqlDataReader Mysqlreader = null;
            AccountInfoEnt AccountInfoServer = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@Account",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = Account;
                string SqlStr = string.Format("SELECT Id,Account,AccountName,AccountStatus,lastTime,lastIp,LoginCount,UnlockPwd,PwdErrorCount,StartTime,EndTime FROM  `tbl_userinfo` where Account=@Account");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    AccountInfoServer = new AccountInfoEnt();
                    AccountInfoServer.Account = Mysqlreader["Account"].ToString();
                    AccountInfoServer.lastIP = Mysqlreader["lastIP"].ToString();
                    AccountInfoServer.lastTime = Mysqlreader["lastTime"].ToString();
                    AccountInfoServer.AccountName = Mysqlreader["AccountName"].ToString();
                    AccountInfoServer.AccountStatus = TransformDataHelper.TransformToInt(Mysqlreader["AccountStatus"].ToString());
                    AccountInfoServer.LoginCount = TransformDataHelper.TransformToInt(Mysqlreader["LoginCount"].ToString());
                    AccountInfoServer.PwdErrorCount = TransformDataHelper.TransformToInt(Mysqlreader["PwdErrorCount"].ToString());
                    AccountInfoServer.StartTime = Mysqlreader["StartTime"].ToString();
                    AccountInfoServer.EndTime = Mysqlreader["EndTime"].ToString();
                    AccountInfoServer.UnlockPwd = TransformDataHelper.TransformToInt(Mysqlreader["UnlockPwd"].ToString());
                    AccountInfoServer.Id = TransformDataHelper.TransformToInt(Mysqlreader["Id"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "SearchAccountinfo", ex.Message.ToString(), Account);
                return AccountInfoServer;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return AccountInfoServer;
        }

        public AccountInfoEnt SearchAccountByIdService(int Id)
        {
            MySqlDataReader Mysqlreader = null;
            AccountInfoEnt AccountInfoServer = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@Id",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = Id;
                string SqlStr = string.Format("SELECT Account,AccountName,AccountStatus,lastTime,lastIp,LoginCount,UnlockPwd,PwdErrorCount,StartTime,EndTime FROM  `tbl_userinfo` where Id=@Id");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    AccountInfoServer = new AccountInfoEnt();
                    AccountInfoServer.Account = Mysqlreader["Account"].ToString();
                    AccountInfoServer.lastIP = Mysqlreader["lastIP"].ToString();
                    AccountInfoServer.lastTime = Mysqlreader["lastTime"].ToString();
                    AccountInfoServer.AccountName = Mysqlreader["AccountName"].ToString();
                    AccountInfoServer.AccountStatus = TransformDataHelper.TransformToInt(Mysqlreader["AccountStatus"].ToString());
                    AccountInfoServer.LoginCount = TransformDataHelper.TransformToInt(Mysqlreader["LoginCount"].ToString());
                    AccountInfoServer.PwdErrorCount = TransformDataHelper.TransformToInt(Mysqlreader["PwdErrorCount"].ToString());
                    AccountInfoServer.StartTime = Mysqlreader["StartTime"].ToString();
                    AccountInfoServer.EndTime = Mysqlreader["EndTime"].ToString();
                    AccountInfoServer.UnlockPwd = TransformDataHelper.TransformToInt(Mysqlreader["UnlockPwd"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "SearchAccountByIdService", ex.Message.ToString(), "");
                return AccountInfoServer;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return AccountInfoServer;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int updateStatusServer(int Id, int Status)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_userinfo set AccountStatus=" + Status + " where id=" + Id;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "updateStatusServer", ex.Message.ToString(), "");
            }
            return result;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpUnlockPwdServer(int Id, int Status)
        {
            int result = 0;
            try
            {
                string sql = string.Empty;
                if (Status == 0)
                {
                    sql = " ,PwdErrorCount=5";
                }
                string SqlStr = "update tbl_userinfo set UnlockPwd=" + Status + " " + sql + " where id=" + Id;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "UpUnlockPwdServer", ex.Message.ToString(), "");
            }
            return result;
        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int CSHPWDServer(int Id, string pwd)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_userinfo set Password='" + pwd + "' where id=" + Id;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "CSHPWDServer", ex.Message.ToString(), "");
            }
            return result;
        }
        



        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <returns></returns>
        public int AddUserInfoService(AccountInfoEnt AccountInfo)
        {
            int result = 0;
            try
            {
                string SqlStr = "insert into tbl_userinfo(Account,AccountName,AccountStatus,Password,UnlockPwd,PwdErrorCount,CreateTime,StartTime,EndTime)values(@Account,@AccountName,@AccountStatus,@Password,@UnlockPwd,@PwdErrorCount,NOW(),@StartTime,@EndTime)";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@Account",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@AccountName",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@AccountStatus",MySqlDbType.Int32),
                                    new MySqlParameter("@Password",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@UnlockPwd",MySqlDbType.Int32),
                                    new MySqlParameter("@PwdErrorCount",MySqlDbType.Int32),
                                    new MySqlParameter("@StartTime",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@EndTime",MySqlDbType.VarChar,50),

    };
                Mpa[0].Value = AccountInfo.Account;
                Mpa[1].Value = AccountInfo.AccountName;
                Mpa[2].Value = AccountInfo.AccountStatus;
                Mpa[3].Value = AccountInfo.Password;
                Mpa[4].Value = AccountInfo.UnlockPwd;
                Mpa[5].Value = AccountInfo.PwdErrorCount;
                Mpa[6].Value = AccountInfo.StartTime;
                Mpa[7].Value = AccountInfo.EndTime;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "AddUserInfoService", ex.Message.ToString(), "");
            }
            return result;
        }

        public int UpdateUserInfoService(int Id, string AccountName, string EffectiveTime_s, string EffectiveTime_e)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_userinfo set AccountName=@AccountName,StartTime=@StartTime,EndTime=@EndTime where id=@id";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@id",MySqlDbType.Int32),
                                    new MySqlParameter("@AccountName",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@StartTime",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@EndTime",MySqlDbType.VarChar,50),

    };
                Mpa[0].Value = Id;
                Mpa[1].Value = AccountName;
                Mpa[2].Value = EffectiveTime_s;
                Mpa[3].Value = EffectiveTime_e;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManagerServer", "UpdateUserInfoService", ex.Message.ToString(), AccountName+"|"+ EffectiveTime_s+"|"+ EffectiveTime_e);
            }
            return result;
        }
    }
}
