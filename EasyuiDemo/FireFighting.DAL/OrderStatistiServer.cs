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
    public class OrderStatistiServer
    {
        public OrderStatistiInfoEnt GetOrderStatistiData(string EncodingOrder)
        {
            OrderStatistiInfoEnt orderStatistiInfoEnt = new OrderStatistiInfoEnt();
            List<OrderStatistiEnt> liOrderStatisti = new List<OrderStatistiEnt>();
            OrderStatistiEnt orderStatisti = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = EncodingOrder;
                string SqlStr = string.Format("SELECT  a.EncodCount,a.EncodingOrder,a.CusCompany,b.ProductNum ,a.KgNum  FROM ( (SELECT EncodingOrder, KgNum, CusCompany, COUNT(ProductId) AS ProductNum  FROM `tbl_fourthworkstation`  GROUP BY EncodingOrder, KgNum, CusId, CusCompany) b   Right JOIN(SELECT SUM(EncodeNum) AS EncodCount, EncodingOrder, KgNum, CusCompany FROM  `tbl_encodingdata` where Del=0 GROUP BY EncodingOrder, KgNum, CusCompany) a  ON a.EncodingOrder = b.EncodingOrder AND a.KgNum = b.KgNum ) where a.EncodingOrder=@EncodingOrder");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    orderStatisti = new OrderStatistiEnt();
                    orderStatisti.CusCompany = Mysqlreader["CusCompany"].ToString();
                    orderStatisti.EncodCount = Mysqlreader["EncodCount"].ToString();
                    orderStatisti.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    orderStatisti.ProductNum = Mysqlreader["ProductNum"].ToString();
                    orderStatisti.KgNum = Mysqlreader["KgNum"].ToString();
                    liOrderStatisti.Add(orderStatisti);
                }
                orderStatistiInfoEnt.liOrderStatisti = liOrderStatisti;
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderStatistiServer", "GetOrderStatistiData", ex.Message.ToString(), "");
                return orderStatistiInfoEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return orderStatistiInfoEnt;
        }


        public List<OrderStatistiEnt> SearchOrderStatistiData(string where, ref PageParameterServerEnt PageParameter)
        {
            List<OrderStatistiEnt> orderStatistiEnt = new List<OrderStatistiEnt>();
            try
            {
                string sqlStr = "SELECT  a.EncodCount,a.EncodingOrder,a.CusCompany,b.ProductNum ,a.KgNum  FROM ( (SELECT EncodingOrder, KgNum, CusCompany, COUNT(ProductId) AS ProductNum  FROM `tbl_fourthworkstation`  WHERE " + where + " GROUP BY EncodingOrder, KgNum, CusId, CusCompany) b   Right JOIN(SELECT SUM(EncodeNum) AS EncodCount, EncodingOrder, KgNum, CusCompany FROM  `tbl_encodingdata` where Del=0 GROUP BY EncodingOrder, KgNum, CusCompany) a  ON a.EncodingOrder = b.EncodingOrder AND a.KgNum = b.KgNum ) ";
                //PageParameter.Orderby = "CreateTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                orderStatistiEnt = PageHelper.GetPageList<OrderStatistiEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (orderStatistiEnt.Count > 0)
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
                ErrorLog.InsertError("AccountManagerServer", "SearchAccountManagerInfo", ex.Message.ToString(), PageParameter.Where);
            }
            return orderStatistiEnt;
        }

        public ScraporderInfoEnt GetScraporderData(string where)
        {
            ScraporderInfoEnt scraporderInfoEnt = new ScraporderInfoEnt();
            List<ScraporderEnt> liScraporderEnt = new List<ScraporderEnt>();
            ScraporderEnt scraporderEnt = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    //new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                //sqlarray[0].Value = ProductId;
                if (!string.IsNullOrEmpty(where))
                {
                    where = " where " + where;
                }
                string SqlStr = string.Format("SELECT EncodingOrder,COUNT(ProductId) AS ProductNum,KgNum  FROM `tbl_sysscraporder` " + where + " GROUP BY EncodingOrder,KgNum ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    scraporderEnt = new ScraporderEnt();
                    scraporderEnt.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    scraporderEnt.ScraporderNum = TransformDataHelper.TransformToInt(Mysqlreader["ProductNum"].ToString());
                    scraporderEnt.KgNum = Mysqlreader["KgNum"].ToString();
                    
                    liScraporderEnt.Add(scraporderEnt);
                }
                scraporderInfoEnt.liScraporder = liScraporderEnt;
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderStatistiServer", "GetScraporderData", ex.Message.ToString(), where);
                return scraporderInfoEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return scraporderInfoEnt;
        }

        public void InsertExternalService(ExternalSearch externalSearch)
        {
            try
            {
                string SqlStr = "insert into tbl_externalsearch(Ip,ConditionStr,Content,CreateTime)values(@Ip,@ConditionStr,@Content,NOW())";
                MySqlParameter[] Mpa = new MySqlParameter[]
                {
                                    new MySqlParameter("@Ip",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@ConditionStr",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@Content",MySqlDbType.VarChar,500),


                 };
                Mpa[0].Value = externalSearch.Ip;
                Mpa[1].Value = externalSearch.Condition;
                Mpa[2].Value = externalSearch.Content;

                MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderStatistiServer", "InsertExternalService", ex.Message.ToString(), "");
            }
        }

        public List<ExternalSearch> GetOrderExternal(string Ip, string Condition)
        {
            List<ExternalSearch> liexternalSearch = new List<ExternalSearch>();
            ExternalSearch externalSearch = null;
            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    //new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar)
                 };
                //sqlarray[0].Value = EncodingOrder;
                string where = " where CreateTime>'" + DateTime.Now.AddMinutes(-2) + "' ";
                if (!string.IsNullOrEmpty(Ip))
                {
                    where += "  and ip='" + Ip + "'";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Condition))
                    {
                        where += " and ConditionStr='" + Condition + "'";
                    }
                    else
                    {
                        return liexternalSearch;
                    }
                }

                string SqlStr = "SELECT Ip,ConditionStr,Content,CreateTime from tbl_ExternalSearch" + where;

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    externalSearch = new ExternalSearch();
                    externalSearch.Ip = Mysqlreader["Ip"].ToString();
                    externalSearch.Condition = Mysqlreader["ConditionStr"].ToString();
                    externalSearch.Content = Mysqlreader["Content"].ToString();
                    externalSearch.CreateTime = Mysqlreader["CreateTime"].ToString();
                    liexternalSearch.Add(externalSearch);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderStatistiServer", "GetOrderExternal", ex.Message.ToString(), Ip+"|"+ Condition);
                return liexternalSearch;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liexternalSearch;
        }

        public List<SelectOrderEnt> GetSearchOrderService(string ProductId)
        {
            List<SelectOrderEnt> liSelectOrderEnt = new List<SelectOrderEnt>();
            SelectOrderEnt selectOrderEnt = null;

            MySqlDataReader Mysqlreader = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@ProductId",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = ProductId;
                string SqlStr = string.Format("SELECT CusCompany,OrderStatusTest,KgNum,CreateTime FROM `tbl_selectorder` WHERE Del=0 and ProductId=@ProductId ORDER BY OrderId DESC ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrderEnt = new SelectOrderEnt();
                    selectOrderEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    selectOrderEnt.KgNum = TransformDataHelper.TransformToInt(Mysqlreader["KgNum"].ToString());
                    selectOrderEnt.OrderStatusTest = Mysqlreader["OrderStatusTest"].ToString();
                    selectOrderEnt.CreateTime = Mysqlreader["CreateTime"].ToString();
                    liSelectOrderEnt.Add(selectOrderEnt);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderStatistiServer", "GetSearchOrderService", ex.Message.ToString(), "");
                return liSelectOrderEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liSelectOrderEnt;
        }
    }
}
