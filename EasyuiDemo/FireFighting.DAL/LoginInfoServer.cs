
using FireFighting.DBTool;
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
    public class LoginInfoServer
    {
        public AccountInfoEnt SearchAccountserver(string Account)
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
                string SqlStr = string.Format("SELECT AccountName,Id,Account,AccountStatus,StartTime,EndTime,UnlockPwd,PASSWORD,lastIP,lastTime FROM `tbl_userinfo` where Account=@Account");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    AccountInfoServer = new AccountInfoEnt();
                    AccountInfoServer.AccountName = Mysqlreader["AccountName"].ToString();
                    AccountInfoServer.Account = Mysqlreader["Account"].ToString();
                    AccountInfoServer.lastIP = Mysqlreader["lastIP"].ToString();
                    AccountInfoServer.lastTime = Mysqlreader["lastTime"].ToString();
                    AccountInfoServer.AccountStatus = TransformDataHelper.TransformToInt(Mysqlreader["AccountStatus"].ToString());
                    AccountInfoServer.Password = Mysqlreader["PASSWORD"].ToString();

                    AccountInfoServer.StartTime = Mysqlreader["StartTime"].ToString();
                    AccountInfoServer.EndTime = Mysqlreader["EndTime"].ToString();
                    AccountInfoServer.UnlockPwd = TransformDataHelper.TransformToInt(Mysqlreader["UnlockPwd"].ToString());
                    AccountInfoServer.Id = TransformDataHelper.TransformToInt(Mysqlreader["Id"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoServer", "SearchAccountserver", ex.Message.ToString(), Account);
                return AccountInfoServer;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return AccountInfoServer;
        }

        public AccountInfoEnt SearchAccountByidserver(int Id)
        {
            AccountInfoEnt AccountInfo = new AccountInfoEnt();
            MySqlDataReader Mysqlreader = null;
            AccountInfoEnt AccountInfoServer = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@Id",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = Id;
                string SqlStr = string.Format("SELECT Account,AccountStatus,StartTime,EndTime,UnlockPwd,PASSWORD,lastIP,lastTime FROM `tbl_userinfo` where id=@Id");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    AccountInfoServer = new AccountInfoEnt();
                    AccountInfoServer.Account = Mysqlreader["Account"].ToString();
                    AccountInfoServer.lastIP = Mysqlreader["lastIP"].ToString();
                    AccountInfoServer.lastTime = Mysqlreader["lastTime"].ToString();
                    AccountInfoServer.AccountStatus = TransformDataHelper.TransformToInt(Mysqlreader["AccountStatus"].ToString());
                    AccountInfoServer.Password = Mysqlreader["PASSWORD"].ToString();

                    AccountInfoServer.StartTime = Mysqlreader["StartTime"].ToString();
                    AccountInfoServer.EndTime = Mysqlreader["EndTime"].ToString();
                    AccountInfoServer.UnlockPwd = TransformDataHelper.TransformToInt(Mysqlreader["UnlockPwd"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoServer", "SearchAccountByidserver", ex.Message.ToString(), "");
                return AccountInfoServer;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return AccountInfoServer;
        }

        public int LogLoginInfoService(string account, string Ip, string Message)
        {
            MySqlTransaction tran = null;
            string _SqlCmd = string.Empty;
            int Result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataConnManager.ConnectionString()))
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    #region 
                    using (MySqlCommand cmdA = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append(@"insert into LoginLog(LoginAccount,LoginIp,LoginTime,LoginMessage)values(@LoginAccount,@LoginIp,NOW(),@LoginMessage)");
                        MySqlParameter[] Mpr = new MySqlParameter[]
                            {
                                new MySqlParameter("@LoginAccount",MySqlDbType.VarChar,50),
                                new MySqlParameter("@LoginIp",MySqlDbType.VarChar,50),
                                new MySqlParameter("@LoginMessage",MySqlDbType.VarChar,50),
                            };
                        Mpr[0].Value = account;
                        Mpr[1].Value = Ip;
                        Mpr[2].Value = Message;
                        cmdA.CommandText = str.ToString();
                        cmdA.CommandType = CommandType.Text;
                        cmdA.Transaction = tran;
                        cmdA.Parameters.AddRange(Mpr);
                        Result = cmdA.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                            return Result;
                        }
                    }
                    #endregion
                    #region 修改交易
                    using (MySqlCommand cmdB = connection.CreateCommand())
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(@"update tbl_userinfo 
                                        set lastTime = NOW(),LoginCount  = LoginCount+1 ,lastIP  = @lastIP 
                                        WHERE Account=@Account");
                        MySqlParameter[] Mpt = new MySqlParameter[]
                            {
                                new MySqlParameter("@lastIP",MySqlDbType.VarChar,50),
                                new MySqlParameter("@Account",MySqlDbType.VarChar,50),
                            };
                        Mpt[0].Value = Ip;
                        Mpt[1].Value = account;
                        cmdB.CommandText = st.ToString();
                        cmdB.CommandType = CommandType.Text;
                        cmdB.Transaction = tran;
                        cmdB.Parameters.AddRange(Mpt);
                        Result = cmdB.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                            return Result;
                        }
                    }
                    #endregion
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoServer", "LogLoginInfoService", ex.Message.ToString(), account + "|" + Message);
                tran.Rollback();
                return Result;
            }
            return Result;
        }

        public void UnLockPwdService(string account)
        {
            try
            {
                string SqlStr = "update tbl_userinfo set PwdErrorCount=PwdErrorCount-1 where Account='" + account + "'";
                MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoServer", "UnLockPwdService", ex.Message.ToString(), account );
            }
        }
    }
}
