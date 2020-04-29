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
    public class OrderManagerServer
    {
        public SelectOrderInfoEnt GetSelectOrderInfoservice(ref PageParameterServerEnt PageParameter)
        {
            SelectOrderInfoEnt selectOrderInfo = new SelectOrderInfoEnt();
            List<SelectOrderEnt> SelectOrderli = new List<SelectOrderEnt>();
            try
            {
                string sqlStr = "select  KgNum,EncodingOrder,OrderId,ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime from tbl_SelectOrder ";
                PageParameter.Orderby = "CreateTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                SelectOrderli = PageHelper.GetPageList<SelectOrderEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (SelectOrderli.Count > 0)
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
                    selectOrderInfo.liSelectOrder = SelectOrderli;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetSelectOrderInfoservice", ex.Message.ToString(), PageParameter.Where);
            }
            return selectOrderInfo;
        }

        public CusTomerEnt GetCusTomerService(int CusId)
        {
            MySqlDataReader Mysqlreader = null;
            CusTomerEnt cusTomerEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@CusId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = CusId;
                string SqlStr = string.Format("SELECT CusId,CusCompany,CusLinkMan,CusLinkTel,CusLinkPhone FROM `tbl_customer` where CusId=@CusId");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    cusTomerEnt = new CusTomerEnt();
                    cusTomerEnt.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    cusTomerEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    cusTomerEnt.CusLinkMan = Mysqlreader["CusLinkMan"].ToString();
                    cusTomerEnt.CusLinkTel = Mysqlreader["CusLinkTel"].ToString();
                    cusTomerEnt.CusLinkPhone = Mysqlreader["CusLinkPhone"].ToString();

                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetCusTomerService", ex.Message.ToString(), "");
                return cusTomerEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return cusTomerEnt;
        }

        public OrderLogInfoEnt GetOrderLogService(string ProductId)
        {
            OrderLogInfoEnt orderLogInfoEnt = new OrderLogInfoEnt();
            List<OrderLogEnt> liOrderLogEnt = new List<OrderLogEnt>();
            OrderLogEnt orderLogEnt = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT ProductId,Operator,OperatingTime,OperationalContent FROM `tbl_orderlog` where ProductId=@ProductId");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    orderLogEnt = new OrderLogEnt();
                    orderLogEnt.ProductId = Mysqlreader["ProductId"].ToString();
                    orderLogEnt.Operator = Mysqlreader["Operator"].ToString();
                    orderLogEnt.OperatingTime = TransformDataHelper.TransformToDateTime(Mysqlreader["OperatingTime"].ToString());
                    orderLogEnt.OperationalContent = Mysqlreader["OperationalContent"].ToString();
                    liOrderLogEnt.Add(orderLogEnt);
                }
                orderLogInfoEnt.liOrderLog = liOrderLogEnt;
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetOrderLogService", ex.Message.ToString(), ProductId);
                return orderLogInfoEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return orderLogInfoEnt;
        }

        public SelectOrderEnt GetselectOrder(string ProductId)
        {
            SelectOrderEnt selectOrder = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT EncodingOrder,CusCompany,CusId,OrderStatus,OrderStatusTest FROM `tbl_selectorder` where ProductId=@ProductId and DEL=0");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrder = new SelectOrderEnt();
                    selectOrder.CusCompany = Mysqlreader["CusCompany"].ToString();
                    selectOrder.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    selectOrder.OrderStatus = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatus"].ToString());
                    selectOrder.OrderStatusTest = Mysqlreader["OrderStatusTest"].ToString();
                    selectOrder.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetselectOrder", ex.Message.ToString(), ProductId);
                return selectOrder;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return selectOrder;
        }

        public SelectOrderEnt GetSecondOrder(string ProductId)
        {
            SelectOrderEnt selectOrder = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT CusCompany,CusId,OrderStatus,OrderStatusTest FROM `tbl_SecondWorkstation` where ProductId=@ProductId and Del=0");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrder = new SelectOrderEnt();
                    selectOrder.CusCompany = Mysqlreader["CusCompany"].ToString();
                    selectOrder.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    selectOrder.OrderStatus = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatus"].ToString());
                    selectOrder.OrderStatusTest = Mysqlreader["OrderStatusTest"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetSecondOrder", ex.Message.ToString(), ProductId);
                return selectOrder;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return selectOrder;
        }
        public SelectOrderEnt GetThirdOrder(string ProductId)
        {
            SelectOrderEnt selectOrder = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT CusCompany,CusId,OrderStatus,OrderStatusTest FROM `tbl_ThirdWorkstation` where ProductId=@ProductId and Del=0");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrder = new SelectOrderEnt();
                    selectOrder.CusCompany = Mysqlreader["CusCompany"].ToString();
                    selectOrder.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    selectOrder.OrderStatus = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatus"].ToString());
                    selectOrder.OrderStatusTest = Mysqlreader["OrderStatusTest"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetThirdOrder", ex.Message.ToString(), ProductId);
                return selectOrder;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return selectOrder;
        }
        public SelectOrderEnt GetFourthOrder(string ProductId)
        {
            SelectOrderEnt selectOrder = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT CusCompany,CusId,OrderStatus,OrderStatusTest FROM `tbl_fourthworkstation` where ProductId=@ProductId and Del=0");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrder = new SelectOrderEnt();
                    selectOrder.CusCompany = Mysqlreader["CusCompany"].ToString();
                    selectOrder.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    selectOrder.OrderStatus = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatus"].ToString());
                    selectOrder.OrderStatusTest = Mysqlreader["OrderStatusTest"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetFourthOrder", ex.Message.ToString(), ProductId);
                return selectOrder;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return selectOrder;
        }

        public List<SelectOrderEnt> GetSelectOrderList(string ProductId)
        {
            List<SelectOrderEnt> liSelectOrder = new List<SelectOrderEnt>();
            SelectOrderEnt selectOrder = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT KgNum,ProductId,hide,EncodingOrder,CusCompany,CusId,OrderStatus,OrderStatusTest FROM `tbl_selectorder` where ProductId=@ProductId and Del=0");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrder = new SelectOrderEnt();
                    selectOrder.CusCompany = Mysqlreader["CusCompany"].ToString();
                    selectOrder.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    selectOrder.OrderStatus = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatus"].ToString());
                    selectOrder.OrderStatusTest = Mysqlreader["OrderStatusTest"].ToString();
                    selectOrder.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    selectOrder.hide = TransformDataHelper.TransformToInt(Mysqlreader["hide"].ToString());
                    selectOrder.ProductId = Mysqlreader["ProductId"].ToString();
                    selectOrder.KgNum = TransformDataHelper.TransformToInt(Mysqlreader["KgNum"].ToString());
                    liSelectOrder.Add(selectOrder);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetSelectOrderList", ex.Message.ToString(), ProductId);
                return liSelectOrder;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liSelectOrder;
        }

        public int UpdateStatusService(int OrderStatus, int OrderId, string Account, string ProductId, string OperationalContent, string OrderStatusText)
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
                        str.Append(@"update tbl_SelectOrder set OrderStatus=@OrderStatus,OrderStatusTest=@OrderStatusText where OrderId=@OrderId");
                        MySqlParameter[] Mpr = new MySqlParameter[]
                            {
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusText",MySqlDbType.VarChar,50),
                            };
                        Mpr[0].Value = OrderStatus;
                        Mpr[1].Value = OrderId;
                        Mpr[2].Value = OrderStatusText;
                        cmdA.CommandText = str.ToString();
                        cmdA.CommandType = CommandType.Text;
                        cmdA.Transaction = tran;
                        cmdA.Parameters.AddRange(Mpr);
                        Result = cmdA.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                        }
                    }
                    #endregion
                    #region 修改交易
                    using (MySqlCommand cmdB = connection.CreateCommand())
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(@"insert into tbl_orderlog(ProductId,Operator,OperatingTime,OperationalContent)values(@ProductId,@Operator,NOW(),@OperationalContent)");
                        MySqlParameter[] Mpt = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@Operator",MySqlDbType.VarChar,50),
                                new MySqlParameter("@OperationalContent",MySqlDbType.VarChar,50),
                            };
                        Mpt[0].Value = ProductId;
                        Mpt[1].Value = Account;
                        Mpt[2].Value = OperationalContent;
                        cmdB.CommandText = st.ToString();
                        cmdB.CommandType = CommandType.Text;
                        cmdB.Transaction = tran;
                        cmdB.Parameters.AddRange(Mpt);
                        Result = cmdB.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                        }
                    }
                    #endregion
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "UpdateStatusService", ex.Message.ToString(), Account + "|" + ProductId);
                tran.Rollback();
            }
            return Result;
        }

        public int ProductInputService(SelectOrderEnt SelectOrderEnt, string Account)
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
                    #region   入库
                    using (MySqlCommand cmdA = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append(@"insert into tbl_FirstWorkstation(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum) ");
                        MySqlParameter[] Mpr = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                            };
                        Mpr[0].Value = SelectOrderEnt.ProductId;
                        Mpr[1].Value = SelectOrderEnt.CusCompany;
                        Mpr[2].Value = SelectOrderEnt.CusId;
                        Mpr[3].Value = SelectOrderEnt.OrderStatus;
                        Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                        Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                        Mpr[6].Value = SelectOrderEnt.KgNum;
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
                    #region 订单交易
                    using (MySqlCommand cmdC = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append(@"insert into tbl_selectorder(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                        MySqlParameter[] Mpr = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                            };
                        Mpr[0].Value = SelectOrderEnt.ProductId;
                        Mpr[1].Value = SelectOrderEnt.CusCompany;
                        Mpr[2].Value = SelectOrderEnt.CusId;
                        Mpr[3].Value = SelectOrderEnt.OrderStatus;
                        Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                        Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                        Mpr[6].Value = SelectOrderEnt.KgNum;
                        cmdC.CommandText = str.ToString();
                        cmdC.CommandType = CommandType.Text;
                        cmdC.Transaction = tran;
                        cmdC.Parameters.AddRange(Mpr);
                        Result = cmdC.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                            return Result;
                        }
                    }
                    #endregion
                    #endregion
                    if (SelectOrderEnt.OrderStatus == 1006)
                    {
                        #region  报废
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_SysScrapOrder(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
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
                    }
                    if (SelectOrderEnt.OrderStatus == 1007)
                    {
                        #region  第二工站
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_secondworkstation(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
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
                    }
                    if (SelectOrderEnt.OrderStatus == 1008)
                    {
                        #region  贴标
                        using (MySqlCommand cmdT = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_thirdworkstation(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
                            cmdT.CommandText = str.ToString();
                            cmdT.CommandType = CommandType.Text;
                            cmdT.Transaction = tran;
                            cmdT.Parameters.AddRange(Mpr);
                            Result = cmdT.ExecuteNonQuery();
                            if (Result <= 0)
                            {
                                tran.Rollback();
                                return Result;
                            }
                        }
                        #endregion
                    }
                    #region 订单操作日志
                    using (MySqlCommand cmdB = connection.CreateCommand())
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(@"insert into tbl_orderlog(ProductId,Operator,OperatingTime,OperationalContent)values(@ProductId,@Operator,NOW(),@OperationalContent)");
                        MySqlParameter[] Mpt = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@Operator",MySqlDbType.VarChar,50),
                                new MySqlParameter("@OperationalContent",MySqlDbType.VarChar,50),
                            };
                        Mpt[0].Value = SelectOrderEnt.ProductId;
                        Mpt[1].Value = Account;
                        if (SelectOrderEnt.OrderStatus == 1001)
                        {
                            Mpt[2].Value = "商品入库充粉";
                        }
                        else if (SelectOrderEnt.OrderStatus == 1006)
                        {
                            Mpt[2].Value = "商品报废";
                        }
                        else if (SelectOrderEnt.OrderStatus == 1007)
                        {
                            Mpt[2].Value = "商品维修";
                        }
                        else if (SelectOrderEnt.OrderStatus == 1008)
                        {
                            Mpt[2].Value = "商品贴标";
                        }
                        else
                        {
                            Mpt[2].Value = "异常操作";
                        }
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

                    #region 用户操作日志
                    using (MySqlCommand cmdG = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("insert into tbl_OperationLog(Operator,OperationContent,OperationTime,OperationType,OperationName)values(@Operator,@OperationContent,NOW(),@OperationType,@OperationName)");
                        MySqlParameter[] Mpa = new MySqlParameter[]
                        {
                                    new MySqlParameter("@Operator",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@OperationContent",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@OperationType",MySqlDbType.Int32),
                                    new MySqlParameter("@OperationName",MySqlDbType.VarChar,50),

                         };
                        Mpa[0].Value = Account;
                        Mpa[1].Value = "对ID" + SelectOrderEnt.ProductId + "订单 进行" + ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                        Mpa[2].Value = 0;
                        Mpa[3].Value = "第一工站入库操作";
                        cmdG.CommandText = str.ToString();
                        cmdG.CommandType = CommandType.Text;
                        cmdG.Transaction = tran;
                        cmdG.Parameters.AddRange(Mpa);
                        Result = cmdG.ExecuteNonQuery();
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
                ErrorLog.InsertError("OrderManagerServer", "ProductInputService", ex.Message.ToString(), Account);
                tran.Rollback();
            }
            return Result;
        }

        public int OrderProcessService(SelectOrderEnt SelectOrderEnt, string Account, int OldOrderStatus)
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
                    if (SelectOrderEnt.OrderStatus == 1002)
                    {
                        #region   充粉
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_SecondWorkstation(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum) ");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
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
                    }
                    else if (SelectOrderEnt.OrderStatus == 1003)
                    {
                        #region  组装充气
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_ThirdWorkstation(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
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
                    }
                    else if (SelectOrderEnt.OrderStatus == 1004)
                    {
                        #region  贴标完成
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_fourthworkstation(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
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
                    }
                    else if (SelectOrderEnt.OrderStatus == 1009)
                    {
                        #region  报废
                        using (MySqlCommand cmdK = connection.CreateCommand())
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append(@"insert into tbl_SysScrapOrder(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                            MySqlParameter[] Mpr = new MySqlParameter[]
                                {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                };
                            Mpr[0].Value = SelectOrderEnt.ProductId;
                            Mpr[1].Value = SelectOrderEnt.CusCompany;
                            Mpr[2].Value = SelectOrderEnt.CusId;
                            Mpr[3].Value = SelectOrderEnt.OrderStatus;
                            Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                            Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                            Mpr[6].Value = SelectOrderEnt.KgNum;
                            cmdK.CommandText = str.ToString();
                            cmdK.CommandType = CommandType.Text;
                            cmdK.Transaction = tran;
                            cmdK.Parameters.AddRange(Mpr);
                            Result = cmdK.ExecuteNonQuery();
                            if (Result <= 0)
                            {
                                tran.Rollback();
                            }
                        }
                        #endregion
                    }
                    #region 操作日志
                    using (MySqlCommand cmdB = connection.CreateCommand())
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(@"insert into tbl_orderlog(ProductId,Operator,OperatingTime,OperationalContent)values(@ProductId,@Operator,NOW(),@OperationalContent)");
                        MySqlParameter[] Mpt = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@Operator",MySqlDbType.VarChar,50),
                                new MySqlParameter("@OperationalContent",MySqlDbType.VarChar,50),
                            };
                        Mpt[0].Value = SelectOrderEnt.ProductId;
                        Mpt[1].Value = Account;
                        if (SelectOrderEnt.OrderStatus == 1002)
                        {
                            Mpt[2].Value = "已经操作充粉 (" + SelectOrderEnt.KgNum + ")KG";
                        }
                        else if (SelectOrderEnt.OrderStatus == 1003)
                        {
                            Mpt[2].Value = "组装充气";
                        }
                        else if (SelectOrderEnt.OrderStatus == 1004)
                        {
                            Mpt[2].Value = "贴标出库";
                        }
                        else if (SelectOrderEnt.OrderStatus == 1009)
                        {
                            Mpt[2].Value = "商品人工报废";
                        }
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


                    #region 用户操作日志
                    using (MySqlCommand cmdG = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("insert into tbl_OperationLog(Operator,OperationContent,OperationTime,OperationType,OperationName)values(@Operator,@OperationContent,NOW(),@OperationType,@OperationName)");
                        MySqlParameter[] Mpa = new MySqlParameter[]
                        {
                                    new MySqlParameter("@Operator",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@OperationContent",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@OperationType",MySqlDbType.Int32),
                                    new MySqlParameter("@OperationName",MySqlDbType.VarChar,50),

                         };
                        Mpa[0].Value = Account;
                        Mpa[1].Value = "对ID" + SelectOrderEnt.ProductId + "订单 进行" + ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                        Mpa[2].Value = 0;
                        Mpa[3].Value = "各工站操作";
                        cmdG.CommandText = str.ToString();
                        cmdG.CommandType = CommandType.Text;
                        cmdG.Transaction = tran;
                        cmdG.Parameters.AddRange(Mpa);
                        Result = cmdG.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                        }
                    }
                    #endregion

                    #region 订单交易
                    using (MySqlCommand cmdC = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append(@"Update tbl_selectorder set hide=1  where ProductId=@ProductId  and OrderStatus=@OldOrderStatus");
                        MySqlParameter[] Mpr = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@OldOrderStatus",MySqlDbType.Int32),
                            };
                        Mpr[0].Value = SelectOrderEnt.ProductId;
                        Mpr[1].Value = OldOrderStatus;
                        cmdC.CommandText = str.ToString();
                        cmdC.CommandType = CommandType.Text;
                        cmdC.Transaction = tran;
                        cmdC.Parameters.AddRange(Mpr);
                        Result = cmdC.ExecuteNonQuery();
                        if (Result <= 0)
                        {
                            tran.Rollback();
                            return Result;
                        }
                    }
                    #endregion

                    #region 新增交易记录
                    using (MySqlCommand cmdZ = connection.CreateCommand())
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append(@"insert into tbl_selectorder(ProductId,CusCompany,CusId,OrderStatus,OrderStatusTest,CreateTime,EncodingOrder,KgNum)values(@ProductId,@CusCompany,@CusId,@OrderStatus,@OrderStatusTest,Now(),@EncodingOrder,@KgNum)");
                        MySqlParameter[] Mpr = new MySqlParameter[]
                            {
                                new MySqlParameter("@ProductId",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                new MySqlParameter("@CusId",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatus",MySqlDbType.Int32),
                                new MySqlParameter("@OrderStatusTest",MySqlDbType.VarChar,50),
                                new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                new MySqlParameter("@KgNum",MySqlDbType.Int32),
                            };
                        Mpr[0].Value = SelectOrderEnt.ProductId;
                        Mpr[1].Value = SelectOrderEnt.CusCompany;
                        Mpr[2].Value = SelectOrderEnt.CusId;
                        Mpr[3].Value = SelectOrderEnt.OrderStatus;
                        Mpr[4].Value = ((StatusEnumEnt)SelectOrderEnt.OrderStatus).ToString();
                        Mpr[5].Value = SelectOrderEnt.EncodingOrder;
                        Mpr[6].Value = SelectOrderEnt.KgNum;
                        cmdZ.CommandText = str.ToString();
                        cmdZ.CommandType = CommandType.Text;
                        cmdZ.Transaction = tran;
                        cmdZ.Parameters.AddRange(Mpr);
                        Result = cmdZ.ExecuteNonQuery();
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
                ErrorLog.InsertError("OrderManagerServer", "OrderProcessService", ex.Message.ToString(), Account);
                tran.Rollback();
            }
            return Result;
        }

        public Dictionary<string, List<StatisticsDayEnt>> GetStatisticsService(ref PageParameterServerEnt PageParameter)
        {
            Dictionary<string, List<StatisticsDayEnt>> dicStatisticsDay = new Dictionary<string, List<StatisticsDayEnt>>();

            List<StatisticsDayEnt> liStatisticsDay_F = new List<StatisticsDayEnt>();
            MySqlDataReader Mysqlreader = null;
            try
            {
                string Strsql = string.Empty;

                MySqlParameter[] sqlarray = new MySqlParameter[]
                {
                 };
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    Strsql = " where " + PageParameter.Where;
                }

                #region 1001

                string SqlStr = string.Format("SELECT SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus,CusCompany FROM `tbl_firstworkstation` " + Strsql + " GROUP BY OrderStatus,CusCompany");
                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    liStatisticsDay_F.Add(StatisticsDay);

                }
                dicStatisticsDay.Add("1001", liStatisticsDay_F);
                #endregion
                #region 1002
                liStatisticsDay_F = new List<StatisticsDayEnt>();
                SqlStr = string.Format("SELECT SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus,CusCompany FROM `tbl_secondworkstation` " + Strsql + " GROUP BY OrderStatus,CusCompany");
                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    liStatisticsDay_F.Add(StatisticsDay);

                }
                dicStatisticsDay.Add("1002", liStatisticsDay_F);
                #endregion
                #region 1003
                liStatisticsDay_F = new List<StatisticsDayEnt>();
                SqlStr = string.Format("SELECT SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus,CusCompany FROM `tbl_thirdworkstation` " + Strsql + " GROUP BY OrderStatus,CusCompany");
                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    liStatisticsDay_F.Add(StatisticsDay);

                }
                dicStatisticsDay.Add("1003", liStatisticsDay_F);
                #endregion
                #region 1004
                liStatisticsDay_F = new List<StatisticsDayEnt>();
                SqlStr = string.Format("SELECT SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus,CusCompany FROM `tbl_fourthworkstation` " + Strsql + " GROUP BY OrderStatus,CusCompany");
                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    liStatisticsDay_F.Add(StatisticsDay);

                }
                dicStatisticsDay.Add("1004", liStatisticsDay_F);
                #endregion
                #region 1006
                liStatisticsDay_F = new List<StatisticsDayEnt>();
                SqlStr = string.Format("SELECT SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus,CusCompany FROM `tbl_sysscraporder` " + Strsql + " GROUP BY OrderStatus,CusCompany");
                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    liStatisticsDay_F.Add(StatisticsDay);

                }
                dicStatisticsDay.Add("1006", liStatisticsDay_F);
                #endregion
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetStatisticsService", ex.Message.ToString(), "");
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return dicStatisticsDay;
        }


        public List<StatisticsDayEnt> GetStatisticsInfoService(ref PageParameterServerEnt PageParameter)
        {
            List<StatisticsDayEnt> liStatisticsDay = new List<StatisticsDayEnt>();
            MySqlDataReader Mysqlreader = null;
            try
            {
                string Strsql = string.Empty;
                MySqlParameter[] sqlarray = new MySqlParameter[]
                {

                 };
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    Strsql = " where " + PageParameter.Where;
                }
                string SqlStr = string.Format(" SELECT COUNT(OrderStatus) AS OrderStatusCount,OrderStatus,CusId,EncodingOrder,CusCompany FROM `tbl_selectorder` " + Strsql + " GROUP BY OrderStatus,CusId,EncodingOrder,CusCompany");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    StatisticsDay.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    StatisticsDay.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    liStatisticsDay.Add(StatisticsDay);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetStatisticsInfoService", ex.Message.ToString(), PageParameter.Where);

            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liStatisticsDay;
        }

        public List<StatisticsDayEnt> GetStatisticsByKgService(ref PageParameterServerEnt PageParameter)
        {
            List<StatisticsDayEnt> liStatisticsDay = new List<StatisticsDayEnt>();
            MySqlDataReader Mysqlreader = null;
            try
            {
                string Strsql = string.Empty;
                MySqlParameter[] sqlarray = new MySqlParameter[]
                {

                 };
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    Strsql = " where " + PageParameter.Where + " and hide=0 AND OrderStatus<>1001 AND OrderStatus<>1007";
                }
                string SqlStr = string.Format(" SELECT CusId,EncodingOrder,CusCompany,COUNT(*) AS KgNumCount,KgNum FROM `tbl_selectorder` " + Strsql + "   GROUP BY CusId,EncodingOrder,CusCompany,KgNum");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.KgNumCount = TransformDataHelper.TransformToInt(Mysqlreader["KgNumCount"].ToString());
                    StatisticsDay.CusCompany = Mysqlreader["CusCompany"].ToString();
                    StatisticsDay.KgNum = TransformDataHelper.TransformToInt(Mysqlreader["KgNum"].ToString());
                    StatisticsDay.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    StatisticsDay.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    liStatisticsDay.Add(StatisticsDay);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetStatisticsInfoService", ex.Message.ToString(), PageParameter.Where);

            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liStatisticsDay;
        }

        public List<StatisticsDayEnt> GetStatisticsDayService(ref PageParameterServerEnt PageParameter)
        {
            List<StatisticsDayEnt> liStatisticsDay = new List<StatisticsDayEnt>();
            MySqlDataReader Mysqlreader = null;
            try
            {
                string Strsql = string.Empty;
                MySqlParameter[] sqlarray = new MySqlParameter[]
                {

                 };
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    Strsql = " where " + PageParameter.Where;
                }
                string SqlStr = string.Format(" SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus FROM `tbl_selectorder` " + Strsql + " GROUP BY OrderStatus");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    liStatisticsDay.Add(StatisticsDay);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetStatisticsDayService", ex.Message.ToString(), PageParameter.Where);
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liStatisticsDay;
        }


        public List<StatisticsDayEnt> GetStatisticsDayInfoService(ref PageParameterServerEnt PageParameter)
        {
            List<StatisticsDayEnt> liStatisticsDay = new List<StatisticsDayEnt>();
            MySqlDataReader Mysqlreader = null;
            try
            {
                string Strsql = string.Empty;
                MySqlParameter[] sqlarray = new MySqlParameter[]
                {

                 };
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    Strsql = " where " + PageParameter.Where;
                }
                string SqlStr = string.Format(" SELECT COUNT(OrderStatus) as OrderStatusCount,OrderStatus FROM `tbl_selectorder` " + Strsql + " GROUP BY OrderStatus");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    StatisticsDayEnt StatisticsDay = new StatisticsDayEnt();
                    StatisticsDay.OrderStatusCount = TransformDataHelper.TransformToInt(Mysqlreader["OrderStatusCount"].ToString());
                    StatisticsDay.OrderStatus = Mysqlreader["OrderStatus"].ToString();
                    liStatisticsDay.Add(StatisticsDay);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerServer", "GetStatisticsDayInfoService", ex.Message.ToString(), PageParameter.Where);
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liStatisticsDay;
        }
    }
}
