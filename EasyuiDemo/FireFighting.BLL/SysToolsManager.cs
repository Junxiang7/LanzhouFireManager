using FireFighting.DAL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.BLL
{
    public class SysToolsManager
    {
        public List<OperationEnt> GetOperatorInfo(string Operator,string EffectiveTime_s,string EffectiveTime_e, ref PageParameterServerEnt PageParameter)
        {
            List<OperationEnt> Operationli = new List<OperationEnt>();
            SysToolsServer sysToolsServer = new SysToolsServer();
            try
            {
                StringBuilder SbStr = new StringBuilder();
                if (string.IsNullOrEmpty(EffectiveTime_s))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(EffectiveTime_e))
                {
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) > TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                EffectiveTime_s = EffectiveTime_s + " 00:00:000";
                EffectiveTime_e = EffectiveTime_e + " 23:59:999";
                SbStr.Append(" OperationTime >'" + EffectiveTime_s + "' and OperationTime<'" + EffectiveTime_e + "' ");
                if (!string.IsNullOrEmpty(Operator))
                {
                    SbStr.Append(" AND Operator='" + Operator + "' ");
                }
                string sqlwhere = SbStr.ToString();
                PageParameter.Where = sqlwhere;
                Operationli = sysToolsServer.GetOperatorInfoservice(ref PageParameter);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("SysToolsManager", "GetOperatorInfo", ex.Message.ToString(), Operator);
            }

            return Operationli;
        }

        public List<ExternalsearchEnt> GetExternalsearchInfo(string Ip, string EffectiveTime_s, string EffectiveTime_e, ref PageParameterServerEnt PageParameter)
        {
            List<ExternalsearchEnt> externalsearchli = new List<ExternalsearchEnt>();
            SysToolsServer sysToolsServer = new SysToolsServer();
            try
            {
                StringBuilder SbStr = new StringBuilder();
                if (string.IsNullOrEmpty(EffectiveTime_s))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(EffectiveTime_e))
                {
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) > TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                EffectiveTime_s = EffectiveTime_s + " 00:00:000";
                EffectiveTime_e = EffectiveTime_e + " 23:59:999";
                SbStr.Append(" CreateTime >'" + EffectiveTime_s + "' and CreateTime<'" + EffectiveTime_e + "' ");
                if (!string.IsNullOrEmpty(Ip))
                {
                    SbStr.Append(" AND Ip='" + Ip + "' ");
                }
                string sqlwhere = SbStr.ToString();
                PageParameter.Where = sqlwhere;
                externalsearchli = sysToolsServer.GetExternalsearchInfoservice(ref PageParameter);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("SysToolsManager", "GetExternalsearchInfo", ex.Message.ToString(), Ip);
            }

            return externalsearchli;
        }
    }
}
