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

    public class EncodingManager
    {
        /// <summary>
        /// 编码录入查询
        /// </summary>
        /// <param name="CusCompany"></param>
        /// <param name="EncodingOrder"></param>
        /// <param name="PageParameter"></param>
        /// <returns></returns>
        public List<EncodingEnt> GetEncodingInfo(string CusCompany, string EncodingOrder, ref PageParameterServerEnt PageParameter)
        {
            List<EncodingEnt> liEncodin = new List<EncodingEnt>();
            EncodingServer encodingServer = new EncodingServer();
            try
            {
                StringBuilder SbStr = new StringBuilder();
                SbStr.Append(" Del=0 ");
                if (!string.IsNullOrEmpty(CusCompany))
                {
                    SbStr.Append(" AND CusCompany like '%" + CusCompany + "%' ");
                }
                if (!string.IsNullOrEmpty(EncodingOrder))
                {
                    SbStr.Append(" AND EncodingOrder='" + EncodingOrder + " '");
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

                liEncodin = encodingServer.SearchEncodingInfo(ref PageParameter);
            }
            catch(Exception ex)
            {
                ErrorLog.InsertError("EncodingManager", "GetEncodingInfo", ex.Message.ToString(), CusCompany);
            }

            return liEncodin;
        }

        /// <summary>
        /// 删除编码录入信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteEncoding(int Id)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.DeleteEncodingService(Id);
        }

        /// <summary>
        /// 添加编码录入信息
        /// </summary>
        /// <param name="encodingEntInfo"></param>
        /// <returns></returns>
        public int AddEncoding(EncodingEntInfo encodingEntInfo)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.AddEncodingService(encodingEntInfo);
        }

        /// <summary>
        /// 修改编码信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="EncodingSatrt"></param>
        /// <param name="EncodingEnd"></param>
        /// <param name="KgNum"></param>
        /// <returns></returns>
        public int UpdateEncoding(int Id, string EncodingSatrt, string EncodingEnd,int KgNum)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.UpdateEncodingService(Id, EncodingSatrt, EncodingEnd, KgNum);
        }

        /// <summary>
        /// 获取所有编码录入信息
        /// </summary>
        /// <returns></returns>
        public List<EncodingEnt> GetEncodingData()
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.GetEncodingDataService();
        }

        /// <summary>
        /// 根据ID获取编码信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<EncodingEnt> GetEncodingDataById(int Id)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.GetEncodingDataByIdService(Id);
        }

        /// <summary>
        /// 根据编码获得信息
        /// </summary>
        /// <param name="EncodingOrder"></param>
        /// <returns></returns>
        public EncodingEnt GetEncodingByOrder(string EncodingOrder)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.GetEncodingByOrderService(EncodingOrder);
        }

        /// <summary>
        /// 根据ID获取编码信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EncodingEnt GetEncodingByEncodeId(string EncodeId)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.GetEncodingByEncodeIdService(EncodeId);
        }

        /// <summary>
        /// 根据编码获得信息
        /// </summary>
        /// <param name="EncodingOrder"></param>
        /// <returns></returns>
        public SelectOrderEnt GetSelectOrder(string EncodingOrder)
        {
            EncodingServer encodingServer = new EncodingServer();
            return encodingServer.GetSelectOrderService(EncodingOrder);
        }



    }
}
