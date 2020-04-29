using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.DAL
{
    public class SysToolsServer
    {
        public List<OperationEnt> GetOperatorInfoservice(ref PageParameterServerEnt PageParameter)
        {
            List<OperationEnt> operationli = new List<OperationEnt>();
            try
            {
                string sqlStr = "SELECT OperId,Operator,OperationContent,OperationTime,OperationName FROM `tbl_operationlog`  ";
                PageParameter.Orderby = "OperationTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                operationli = PageHelper.GetPageList<OperationEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (operationli.Count > 0)
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
                ErrorLog.InsertError("SysToolsServer", "GetOperatorInfoservice", ex.Message.ToString(), PageParameter.Where);
            }
            return operationli;
        }

        public List<ExternalsearchEnt> GetExternalsearchInfoservice(ref PageParameterServerEnt PageParameter)
        {
            List<ExternalsearchEnt> externalsearchli = new List<ExternalsearchEnt>();
            try
            {
                string sqlStr = "SELECT id,Ip,ConditionStr,Content,CreateTime FROM `tbl_externalsearch` ";
                PageParameter.Orderby = "CreateTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                externalsearchli = PageHelper.GetPageList<ExternalsearchEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (externalsearchli.Count > 0)
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
                ErrorLog.InsertError("SysToolsServer", "GetExternalsearchInfoservice", ex.Message.ToString(), PageParameter.Where);
            }
            return externalsearchli;
        }
    }
}
