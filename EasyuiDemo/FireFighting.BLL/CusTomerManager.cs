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
    public class CusTomerManager
    {
        /// <summary>
        /// 客户查询
        /// </summary>
        /// <param name="CusCompany"></param>
        /// <param name="PageParameter"></param>
        /// <returns></returns>
        public List<CusTomerEnt> GetCusTomerInfo(string CusCompany, ref PageParameterServerEnt PageParameter)
        {
            List<CusTomerEnt> licusTomer = new List<CusTomerEnt>();
            try
            {
                CusTomerManagerServer cusTomerManager = new CusTomerManagerServer();
                StringBuilder SbStr = new StringBuilder();
                SbStr.Append(" Del=0 ");
                if (!string.IsNullOrEmpty(CusCompany))
                {
                    SbStr.Append(" AND CusCompany like '%" + CusCompany + "%' ");
                }
                string sqlwhere = SbStr.ToString();
                PageParameter.Where = sqlwhere;
                if (PageParameter.PageIndex < 1)
                {
                    PageParameter.PageIndex = 1;
                }
                if (PageParameter.PageSize < 10)
                {
                    PageParameter.PageSize = 10;
                }

                licusTomer = cusTomerManager.SearchCusTomerInfo(ref PageParameter);
            }
            catch(Exception ex)
            {
                ErrorLog.InsertError("CusTomerManager", "GetCusTomerInfo", ex.Message.ToString(), CusCompany);
            }

            return licusTomer;
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="CusId"></param>
        /// <param name="CusLinkMan"></param>
        /// <param name="CusLinkTel"></param>
        /// <param name="CusLinkPhone"></param>
        /// <returns></returns>
        public int UpdateCusTomer(int CusId, string CusLinkMan, string CusLinkTel, string CusLinkPhone)
        {
            CusTomerManagerServer cusTomerManager = new CusTomerManagerServer();
            return cusTomerManager.UpdateCusTomerService(CusId, CusLinkMan, CusLinkTel, CusLinkPhone);
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="CusId"></param>
        /// <returns></returns>
        public int DeleteCusTomer(int CusId)
        {
            CusTomerManagerServer cusTomerManager = new CusTomerManagerServer();
            return cusTomerManager.DeleteCusTomerService(CusId);
        }

        /// <summary>
        /// 根据公司名称查询客户信息
        /// </summary>
        /// <param name="CusCompany"></param>
        /// <returns></returns>
        public CusTomerEnt SearchCusTomerinfo(string CusCompany)
        {
            CusTomerManagerServer cusTomerManager = new CusTomerManagerServer();
            return cusTomerManager.SearchCusTomerService(CusCompany);
        }

        /// <summary>
        /// 添加客户信息
        /// </summary>
        /// <param name="cusTomerEnt"></param>
        /// <returns></returns>
        public int AddCusTomer(CusTomerEnt cusTomerEnt)
        {
            CusTomerManagerServer cusTomerManager = new CusTomerManagerServer();
            return cusTomerManager.AddCusTomerService(cusTomerEnt);
        }

        /// <summary>
        /// 根据ID查询客户信息
        /// </summary>
        /// <param name="CusId"></param>
        /// <returns></returns>
        public CusTomerEnt GetCusTomerById(int CusId)
        {
            CusTomerManagerServer cusTomerManager = new CusTomerManagerServer();
            return cusTomerManager.GetCusTomerByIdService(CusId);
        }
    }
}
