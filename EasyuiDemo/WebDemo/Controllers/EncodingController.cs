using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class EncodingController : BaseController
    {
        OperateUserInfo oppo = null;
        // GET: Encoding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EncodingInput()
        {
            return View();
        }

        /// <summary>
        /// 编码录入查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="CusCompany"></param>
        /// <param name="EncodingOrder"></param>
        /// <returns></returns>
        public JsonResult UserListJson(int page = 1, int rows = 10, string CusCompany = "", string EncodingOrder = "")
        {
            PageParameterServerEnt PageParameter = new PageParameterServerEnt(); //服务器分页实例化
            PageParameter.PageSize = rows;
            if (page <= 0)
            {
                PageParameter.PageIndex = 1;
            }
            else
            {
                PageParameter.PageIndex = page;
            }
            if (rows <= 10)
            {
                PageParameter.PageSize = 10;
            }
            else
            {
                PageParameter.PageSize = rows;
            }
            CusCompany = SqlFilter.SuperFilter(CusCompany);
            EncodingOrder = SqlFilter.SuperFilter(EncodingOrder);
            EncodingManager encodingManager = new EncodingManager();
            EncodingEntToModel EncodingETM = new EncodingEntToModel();
            List<EncodingEnt> Encodingli = encodingManager.GetEncodingInfo(CusCompany, EncodingOrder, ref PageParameter);
            EncodingInfoModel encodingInfoModel = new Models.EncodingInfoModel();
            encodingInfoModel = EncodingETM.ListEncodingEntToModel(Encodingli);
            var jsondata = new { rows = encodingInfoModel.liEncoding, total = PageParameter.TotalCount };
            return Json(jsondata);
        }

        /// <summary>
        /// 增加或者修改账号
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <param name="LinkName"></param>
        /// <param name="LinkPhone"></param>
        /// <param name="EffectiveTime"></param>    
        /// <param name="Autlist">权限</param>
        /// <returns></returns>
        public string AddOrEditUser(int Id, string EncodingSatrt, string EncodingEnd, string Inputuser, int UpType, string EncodingOrder, string KgNum, string EncodingData)
        {
            string Message = string.Empty;
            string OperationContent = "";
            try
            {
                int result = 0;
                EncodingManager encodingManager = new EncodingManager();
                if (UpType == 2)  //代表
                {
                    #region 删除
                    if (Id > 0)
                    {
                        OperationContent = "删除编码录入 Id:" + Id + "|编码：" + EncodingOrder;

                        SelectOrderEnt selectOrderEnt = null;
                        if (!string.IsNullOrEmpty(EncodingOrder))
                        {
                            selectOrderEnt = encodingManager.GetSelectOrder(EncodingOrder);
                        }
                        else
                        {
                            return "删除信息有误";
                        }
                        if (selectOrderEnt != null && selectOrderEnt.OrderCount > 0)
                        {
                            return "已经有录入的订单编码，无法删除";
                        }
                        else
                        {
                            result = encodingManager.DeleteEncoding(Id);
                        }
                    }
                    else
                    {
                        return "删除信息有误";
                    }
                    #endregion
                }
                else  //添加
                {
                    string[] count = Inputuser.Split('|');
                    if (count.Length != 3)
                    {
                        return "保存信息有误,请选择公司名称";
                    }
                    int CusId = TransformDataHelper.TransformToInt(Inputuser.Split('|')[0]);
                    string CusCompany = Inputuser.Split('|')[1];
                    string CusLinkMan = Inputuser.Split('|')[2];


                    if (Id != 0)
                    {
                        #region 修改
                        //EncodingSatrt = SqlFilter.SuperFilter(EncodingSatrt.Trim());
                        //EncodingEnd = SqlFilter.SuperFilter(EncodingEnd.Trim());
                        string[] m_EncodingData = EncodingData.TrimEnd('|').Split('|');
                        if (m_EncodingData.Length == 1)
                        {
                            string[] EncodingInfo = m_EncodingData[0].Split('^');
                            if (EncodingInfo.Length != 3)
                            {
                                return "存储的范围有误";
                            }
                            string m_EncodingS = EncodingInfo[0];
                            string m_EncodingE = EncodingInfo[1];
                            string m_KgN = EncodingInfo[2];
                            long m_EncodingSatrt = TransformDataHelper.TransformToLong(m_EncodingS);
                            long m_EncodingEnd = TransformDataHelper.TransformToLong(m_EncodingE);
                            int m_KgNum = TransformDataHelper.TransformToInt(m_KgN);
                            if (m_KgNum <= 0)
                            {
                                return "录入的公斤数有误";
                            }
                            if (m_EncodingSatrt <= 0 || m_EncodingEnd <= 0 || m_EncodingSatrt >= m_EncodingEnd)
                            {
                                return "开始Id或者结束Id有误";
                            }
                            List<EncodingEnt> liEncoding = encodingManager.GetEncodingDataById(Id);
                            if (liEncoding.Count > 0)
                            {
                                bool Aut = AutEncodingData(liEncoding, m_EncodingS, m_EncodingE);
                                if (!Aut)
                                {
                                    return "已经存在当前范围";
                                }
                            }
                            result = encodingManager.UpdateEncoding(Id, EncodingSatrt, EncodingEnd, m_KgNum);
                            OperationContent = "修改 Id:" + Id;
                        }
                        #endregion
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(EncodingOrder))
                        {
                            return "编码不能为空";
                        }
                        EncodingOrder = SqlFilter.SuperFilter(EncodingOrder.Trim());
                        if (EncodingOrder.Length != 8)
                        {
                            return "编码长度为八位";
                        }
                        #region 添加
                        EncodingEnt encodingEntorder = encodingManager.GetEncodingByOrder(EncodingOrder);
                        if (encodingEntorder != null)
                        {
                            return "该编码已经存在";
                        }
                        List<EncodingEnt> liEncoding = encodingManager.GetEncodingData();
                        string[] m_EncodingData = EncodingData.TrimEnd('|').Split('|');
                        if (m_EncodingData.Length > 0)
                        {
                            List<EncodingEnt> liEncodeEx = new List<EncodingEnt>();
                            EncodingEntInfo encodingEntInfo = new EncodingEntInfo();
                            List<EncodingEnt> liEncodingEnt = new List<EncodingEnt>();
                            string m_EncodingS = "";
                            string m_EncodingE = "";
                            string m_KgN = "";
                            for (int i = 0; i < m_EncodingData.Length; i++)
                            {
                                if (liEncoding.Count > 0)
                                {
                                    string[] EncodingInfo = m_EncodingData[i].Split('^');
                                    if (EncodingInfo.Length != 3)
                                    {
                                        return "存储的范围有误";
                                    }
                                    m_EncodingS = EncodingInfo[0];
                                    m_EncodingE = EncodingInfo[1];
                                    m_KgN = EncodingInfo[2];
                                    long m_EncodingSatrt = TransformDataHelper.TransformToLong(m_EncodingS);
                                    long m_EncodingEnd = TransformDataHelper.TransformToLong(m_EncodingE);
                                    int m_KgNum = TransformDataHelper.TransformToInt(m_KgN);
                                    if (m_KgNum <= 0)
                                    {
                                        return "录入的公斤数有误";
                                    }
                                    if (m_EncodingSatrt <= 0 || m_EncodingEnd <= 0 || m_EncodingSatrt > m_EncodingEnd)
                                    {
                                        return "开始Id或者结束Id有误";
                                    }

                                    if (m_EncodingS.Length != 13 || m_EncodingE.Length != 13)
                                    {
                                        return "开始Id或者结束Id长度有误";
                                    }

                                    bool Aut = AutEncodingData(liEncoding, m_EncodingS, m_EncodingE);
                                    if (!Aut)
                                    {
                                        return "已经存在当前范围";
                                    }
                                }
                                bool AutEx = AutEncodingData(liEncodeEx, m_EncodingS, m_EncodingE);
                                if (!AutEx)
                                {
                                    return "添加的范围存在重复";
                                }
                                EncodingEnt encodingEx = new EncodingEnt();
                                encodingEx.EncodingSatrt = m_EncodingS;
                                encodingEx.EncodingEnd = m_EncodingE;
                                liEncodeEx.Add(encodingEx);
                                //EncodingEnt encodingEnt = null; //encodingManager.SearchCusTomerinfo(CusCompany);  //需要判断是否存在
                                try
                                {
                                    EncodingEnt encodingEnt = new EncodingEnt();
                                    encodingEnt.CusCompany = CusCompany;
                                    encodingEnt.CusLinkMan = CusLinkMan;
                                    encodingEnt.EncodingSatrt = m_EncodingS.Trim();
                                    encodingEnt.EncodingEnd = m_EncodingE.Trim();
                                    encodingEnt.CusId = CusId;
                                    encodingEnt.EncodingOrder = SqlFilter.SuperFilter(EncodingOrder.Trim());
                                    encodingEnt.KgNum = TransformDataHelper.TransformToInt(m_KgN);
                                    encodingEnt.EncodeNum = TransformDataHelper.TransformToInt((TransformDataHelper.TransformToLong(m_EncodingE) - TransformDataHelper.TransformToLong(m_EncodingS)).ToString()) + 1;
                                    liEncodingEnt.Add(encodingEnt);
                                }
                                catch (Exception ex)
                                {
                                    Message = "新增信息异常";
                                }
                            }
                            encodingEntInfo.liEncoding = liEncodingEnt;
                            result = encodingManager.AddEncoding(encodingEntInfo);
                            OperationContent = "添加编码导入 CusCompany:" + CusCompany + "|CusLinkMan:" + CusLinkMan + "|EncodingOrder:" + EncodingOrder + "|EncodingData:" + EncodingData;
                        }
                        else
                        {
                            Message = "添加数据格式有误";
                        }
                        #endregion
                    }
                }
                if (result > 0)
                {
                    oppo = new OperateUserInfo(LoginInfo);
                    ErrorLog.InsertOperation(oppo.Account, OperationContent, 0, "编码管理");
                    Message = "操作成功";
                }
                else
                {
                    Message = "操作失败，联系管理员";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingController", "AddOrEditUser", ex.Message.ToString(), "");
            }
            return Message;
        }

        /// <summary>
        /// 编码验证
        /// </summary>
        /// <param name="liEncoding"></param>
        /// <param name="EncodingSatrt"></param>
        /// <param name="EncodingEnd"></param>
        /// <returns></returns>
        public bool AutEncodingData(List<EncodingEnt> liEncoding, string EncodingSatrt, string EncodingEnd)
        {
            bool Aut = true;
            try
            {
                if (liEncoding.Count == 0)
                {
                    return true;
                }
                long m_EncodingSatrt = TransformDataHelper.TransformToLong(EncodingSatrt);
                long m_EncodingEnd = TransformDataHelper.TransformToLong(EncodingEnd);
                if (m_EncodingSatrt > m_EncodingEnd)
                {
                    Aut = false;
                    return Aut;
                }
                for (int i = 0; i < liEncoding.Count; i++)
                {
                    long a_EncodingSatrt = TransformDataHelper.TransformToLong(liEncoding[i].EncodingSatrt);
                    long a_EncodingEnd = TransformDataHelper.TransformToLong(liEncoding[i].EncodingEnd);
                    if ((a_EncodingSatrt <= m_EncodingSatrt && m_EncodingSatrt <= a_EncodingEnd) || (a_EncodingSatrt <= m_EncodingEnd && m_EncodingEnd <= a_EncodingEnd))
                    {
                        Aut = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingController", "AutEncodingData", ex.Message.ToString(), "");
                Aut = false;
            }
            return Aut;
        }

        public JsonResult GetScrapInfo()
        {
            CusTomerManager cusTomerManager = new CusTomerManager();
            PageParameterServerEnt PageParameter = new PageParameterServerEnt();
            PageParameter.PageSize = 100;
            List<CusTomerEnt> liCusTomerEnt = cusTomerManager.GetCusTomerInfo("", ref PageParameter);
            return Json(liCusTomerEnt);
        }
    }
}