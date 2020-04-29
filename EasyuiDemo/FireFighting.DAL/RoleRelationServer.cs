
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
    public class RoleRelationServer
    {
        public RetResultEnt GetRoleAccountInfo(RoleAccountRelationServerEnt role, int PageSize, int CurrentPage)
        {
            RetResultEnt rel = new RetResultEnt();
            rel.Result = false;
            try
            {
                if (role == null)
                {
                    rel.Msg = "查询条件不可为空。";
                    return rel;
                }
                string MySql = @"SELECT * FROM `tbl_userinfo` w  LEFT JOIN tbl_RoleAccountRelation r ON r.`Account`=w.`Account` ";
                StringBuilder myStr = new StringBuilder();
                List<MySqlParameter> myList = new List<MySqlParameter>();
                MySqlParameter MyParm = null;
                if (!string.IsNullOrEmpty(role.Account))
                {
                    myStr.Append(" w.Account like '%" + role.Account + "%' AND");

                }
                if (myStr.ToString().Length == 0)
                {
                    MySql = @"SELECT * FROM `tbl_userinfo` w  LEFT  JOIN tbl_RoleAccountRelation r ON r.`Account`=w.`Account`  ORDER BY RarID DESC LIMIT ?CurrentPage,?PageSize;";

                }
                else
                {
                    string where = myStr.ToString().Substring(0, myStr.ToString().LastIndexOf("AND"));
                    MySql = @"SELECT * FROM `tbl_userinfo` w  LEFT  JOIN tbl_RoleAccountRelation r ON r.`Account`=w.`Account` WHERE " + where + "  ORDER BY RarID DESC LIMIT ?CurrentPage,?PageSize;";
                }

                int colRow = (CurrentPage - 1) * PageSize;
                MyParm = new MySqlParameter("?CurrentPage", MySqlDbType.Int32);
                MyParm.Value = colRow;
                myList.Add(MyParm);
                MyParm = new MySqlParameter("?PageSize", MySqlDbType.Int32);
                MyParm.Value = PageSize;
                myList.Add(MyParm);

                DataTable dt = MySqlHelper.ExecuteDataTable(MySql, myList.ToArray());
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<RoleAccountRelationServerEnt> roleList = new List<RoleAccountRelationServerEnt>();
                    RoleAccountRelationServerEnt rEnt = null;
                    foreach (DataRow r in dt.Rows)
                    {
                        rEnt = new RoleAccountRelationServerEnt();
                        if (r["RarID"].ToString() != "")
                        {
                            rEnt.RarID = int.Parse(r["RarID"].ToString());
                        }

                        rEnt.KeyId = r["KeyId"].ToString();
                        rEnt.Account = r["Account"].ToString();
                        if (r["AddTime"].ToString() != "")
                        {
                            rEnt.AddTime = DateTime.Parse(r["AddTime"].ToString());
                        }
                        if (r["IsEnable"].ToString() != "")
                        {
                            rEnt.IsEnable = int.Parse(r["IsEnable"].ToString());

                        }
                        if (r["IsDelete"].ToString() != "")
                        {
                            rEnt.IsDelete = int.Parse(r["IsDelete"].ToString());
                        }
                        rEnt.RoleId = r["RoleId"].ToString();
                        rEnt.RoleName = r["RoleName"].ToString();
                        rEnt.AddAccount = r["AddAccount"].ToString();
                        roleList.Add(rEnt);
                    }
                    rel.Result = true;
                    rel.RoleAccount = roleList;
                    string myCount = string.Empty;
                    if (myStr.ToString().Length == 0)
                    {
                        myCount = "SELECT Count(*) FROM `tbl_userinfo` w  LEFT  JOIN tbl_RoleAccountRelation r ON r.`Account`=w.`Account`";
                    }
                    else
                    {
                        string where = myStr.ToString().Substring(0, myStr.ToString().LastIndexOf("AND"));
                        myCount = "SELECT count(*) FROM `tbl_userinfo` w  LEFT  JOIN tbl_RoleAccountRelation r ON r.`Account`=w.`Account` WHERE " + where + "";
                    }
                    object obj = MySqlHelper.ExecuteScalar(myCount);
                    if (obj != null)
                    {
                        double pageCount = double.Parse(obj.ToString()) / PageSize;
                        double d = Math.Ceiling(pageCount);
                        rel.PageCount = int.Parse(d.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleRelationServer", "GetRoleAccountInfo", ex.Message.ToString(), "");
            }
            return rel;
        }

        /// <summary>
        /// 得到角色权限
        /// </summary>
        /// <param name="RoleId">角色权限</param>
        /// <param name="NodeType">权限节点类型</param>
        /// <param name="PNodeID">父节点ID</param>
        /// <returns></returns>
        public RetResultEnt GetRolePower(int? RoleId, int? NodeType, int? PNodeID, int IsForeground)
        {

            RetResultEnt rel = new RetResultEnt();
            rel.Result = false;
            try
            {
                string MySql = string.Empty;
                StringBuilder myStr = new StringBuilder();
                List<MySqlParameter> myList = new List<MySqlParameter>();
                MySqlParameter MyParm = null;
                RoleId = RoleId == -1 ? null : RoleId;
                if (RoleId != null)
                {
                    myStr.Append(" r.RoleId=?RoleId AND");
                    MyParm = new MySqlParameter("?RoleId", MySqlDbType.Int32);
                    MyParm.Value = RoleId;
                    myList.Add(MyParm);
                }
                if (NodeType != null)
                {

                    myStr.Append(" p.NodeType=?NodeType AND");
                    MyParm = new MySqlParameter("?NodeType", MySqlDbType.Int32);
                    MyParm.Value = NodeType;
                    myList.Add(MyParm);
                }
                if (PNodeID != null)
                {
                    myStr.Append(" p.PNodeID=?PNodeID AND");
                    MyParm = new MySqlParameter("?PNodeID", MySqlDbType.Int32);
                    MyParm.Value = PNodeID;
                    myList.Add(MyParm);

                }
                if (IsForeground != 3)
                {
                    myStr.Append(" p.IsForeground=?IsForeground AND");
                    MyParm = new MySqlParameter("?IsForeground", MySqlDbType.Int32);
                    MyParm.Value = IsForeground;
                    myList.Add(MyParm);
                }
                string where = "1=1";
                if (myList.Count != 0)
                {
                    where = myStr.ToString().Substring(0, myStr.ToString().LastIndexOf("AND"));

                }
                MySql = @"SELECT r.RoleId,r.RoleName,p.PowerID,p.NodeName,p.NodeType,p.PNodeID,p.IsForeground,p.AccessPath,p.EnterUrl,p.NodeCode FROM tbl_RoleInfo r
                            INNER JOIN tbl_RolePowerRelation  b
                            ON r.RoleId = b.RoleId
                            INNER JOIN tbl_PowerInfo p
                            ON p.PowerId=b.PowerId ";
                MySql += " where " + where;
                MySql += " order by p.A2";

                DataTable dt = MySqlHelper.ExecuteDataTable(MySql, myList.ToArray());

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<RolePowerInfoServerEnt> rps = new List<RolePowerInfoServerEnt>();
                    RolePowerInfoServerEnt rp = null;
                    foreach (DataRow item in dt.Rows)
                    {
                        rp = new RolePowerInfoServerEnt();
                        rp.RoleID = int.Parse(item["RoleId"].ToString());
                        rp.PowerID = int.Parse(item["PowerID"].ToString());
                        rp.RoleName = item["RoleName"].ToString();
                        rp.NodeName = item["NodeName"].ToString();
                        rp.NodeType = Convert.ToInt32(item["NodeType"]);
                        rp.PNodeID = Convert.ToInt32(item["PNodeID"]);
                        rp.AccessPath = item["AccessPath"].ToString();
                        rp.EnterUrl = item["EnterUrl"].ToString();
                        rp.NodeCode = item["NodeCode"].ToString();
                        rp.IsForeground = TransformDataHelper.TransformToInt(item["IsForeground"].ToString());


                        rps.Add(rp);
                    }

                    rel.Result = true;
                    rel.RolePower = rps;

                }


            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleRelationServer", "GetRolePower", ex.Message.ToString(), "");
            }


            return rel;
        }

        public SysRetResultEnt GetSysPowerService(int Id)
        {
            SysRetResultEnt sysRetResult = new SysRetResultEnt();
            List<MySqlParameter> myList = new List<MySqlParameter>();
            try
            {
                string Strwhere = string.Empty;
                if (Id > 0)
                {
                    Strwhere = " WHERE AccountId = " + Id + "";
                }

                string MySql = @"SELECT p.PowerId,p.NodeName,p.NodeNameEN,p.PNodeID,p.AccessPath,p.IsShow,p.IsDefutle FROM `tbl_syspoweraccountrelation` a  
LEFT JOIN `tbl_syspowerinfo` p ON a.PowerId=p.PowerID " + Strwhere;
                DataTable dt = MySqlHelper.ExecuteDataTable(MySql, myList.ToArray());
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<SysPowerInfoEnt> liSysPowerInfo = new List<SysPowerInfoEnt>();
                    foreach (DataRow r in dt.Rows)
                    {
                        SysPowerInfoEnt sysPowerInfo = new NET.SysPowerInfoEnt();
                        sysPowerInfo.AccessPath = r["AccessPath"].ToString();
                        sysPowerInfo.NodeName = r["NodeName"].ToString();
                        sysPowerInfo.NodeNameEN = r["NodeNameEN"].ToString();
                        sysPowerInfo.PNodeID = TransformDataHelper.TransformToInt(r["PNodeID"].ToString());
                        sysPowerInfo.IsShow = TransformDataHelper.TransformTobool(r["IsShow"].ToString());
                        sysPowerInfo.IsDefutle = TransformDataHelper.TransformTobool(r["IsDefutle"].ToString());
                        sysPowerInfo.PowerID = TransformDataHelper.TransformToInt(r["PowerID"].ToString());
                        liSysPowerInfo.Add(sysPowerInfo);
                    }
                    sysRetResult.liSysPowerInfo = liSysPowerInfo;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleRelationServer", "GetSysPowerService", ex.Message.ToString(), "");
            }
            return sysRetResult;
        }

        public SysRetResultEnt GetAllSysPowerService()
        {
            SysRetResultEnt sysRetResult = new SysRetResultEnt();
            List<MySqlParameter> myList = new List<MySqlParameter>();
            try
            {

                string MySql = @"select PowerID,NodeName,NodeNameEN,PNodeID,AccessPath,IsShow,IsDefutle from tbl_syspowerinfo";
                DataTable dt = MySqlHelper.ExecuteDataTable(MySql, myList.ToArray());
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<SysPowerInfoEnt> liSysPowerInfo = new List<SysPowerInfoEnt>();
                    foreach (DataRow r in dt.Rows)
                    {
                        SysPowerInfoEnt sysPowerInfo = new NET.SysPowerInfoEnt();
                        sysPowerInfo.AccessPath = r["AccessPath"].ToString();
                        sysPowerInfo.NodeName = r["NodeName"].ToString();
                        sysPowerInfo.NodeNameEN = r["NodeNameEN"].ToString();
                        sysPowerInfo.PNodeID = TransformDataHelper.TransformToInt(r["PNodeID"].ToString());
                        sysPowerInfo.IsShow = TransformDataHelper.TransformTobool(r["IsShow"].ToString());
                        sysPowerInfo.IsDefutle = TransformDataHelper.TransformTobool(r["IsDefutle"].ToString());
                        sysPowerInfo.PowerID = TransformDataHelper.TransformToInt(r["PowerID"].ToString());
                        liSysPowerInfo.Add(sysPowerInfo);
                    }
                    sysRetResult.liSysPowerInfo = liSysPowerInfo;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleRelationServer", "GetAllSysPowerService", ex.Message.ToString(), "");
            }
            return sysRetResult;
        }

        public List<SysPowerByAccount> GetSysPowerByAccountService(int Id)
        {
            List<SysPowerByAccount> LiSysPowerByAccount = new List<SysPowerByAccount>();
            List<MySqlParameter> myList = new List<MySqlParameter>();
            try
            {

                string MySql = @"select AccountId,PowerId tbl_syspoweraccountrelation where Id=" + Id;
                DataTable dt = MySqlHelper.ExecuteDataTable(MySql, myList.ToArray());
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SysPowerByAccount sysPowerByAccount = new NET.SysPowerByAccount();
                        sysPowerByAccount.AccountId = TransformDataHelper.TransformToInt(r["AccountId"].ToString());
                        sysPowerByAccount.PowerId = TransformDataHelper.TransformToInt(r["PowerId"].ToString());
                        LiSysPowerByAccount.Add(sysPowerByAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleRelationServer", "GetSysPowerByAccountService", ex.Message.ToString(), "");
            }
            return LiSysPowerByAccount;
        }

        public int UpdateRoleInfoService(int Id, string pids, int Type)
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
                    if (Type == 1)  //修改
                    {
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"Delete from tbl_syspoweraccountrelation where AccountId=@AccountId");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@AccountId",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = Id;
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
                    }
                    #endregion
                    #region 修改交易
                    string[] array = pids.Split(',');
                    for (int i = 0; i < array.Length; i++)
                    {
                        using (MySqlCommand cmdB = connection.CreateCommand())
                        {
                            StringBuilder st = new StringBuilder();
                            st.Append(@"insert into tbl_syspoweraccountrelation(AccountId,PowerId)values(@AccountId,@PowerId)");
                            MySqlParameter[] Mpt = new MySqlParameter[]
                                {
                                new MySqlParameter("@AccountId",MySqlDbType.Int32),
                                new MySqlParameter("@PowerId",MySqlDbType.Int32),
                                };
                            int PowerId = TransformDataHelper.TransformToInt(array[i]);
                            Mpt[0].Value = Id;
                            Mpt[1].Value = PowerId;
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
                    }
                    #endregion
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleRelationServer", "UpdateRoleInfoService", ex.Message.ToString(), "");
                tran.Rollback();
                Result = 0;
                return Result;
            }
            return Result;
        }
    }
}
