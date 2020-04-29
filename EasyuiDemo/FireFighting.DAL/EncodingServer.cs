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
    public class EncodingServer
    {
        public List<EncodingEnt> SearchEncodingInfo(ref PageParameterServerEnt PageParameter)
        {
            List<EncodingEnt> Encodingli = new List<EncodingEnt>();
            try
            {
                string sqlStr = "select EncodeNum,KgNum,EncodingOrder,CusId,Id,CusCompany,CusLinkMan,EncodingSatrt,EncodingEnd,CreateTime from tbl_encodingdata ";
                PageParameter.Orderby = "CreateTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                Encodingli = PageHelper.GetPageList<EncodingEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (Encodingli.Count > 0)
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
                ErrorLog.InsertError("EncodingServer", "SearchEncodingInfo", ex.Message.ToString(), PageParameter.Where);
            }
            return Encodingli;
        }

        public int DeleteEncodingService(int Id)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_encodingdata set Del=1  where Id=" + Id;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "DeleteEncodingService", ex.Message.ToString(), "");
            }
            return result;
        }

        public int AddEncodingService(EncodingEntInfo encodingEntInfo)
        {
            MySqlTransaction tran = null;
            int Result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataConnManager.ConnectionString()))
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    for (int i = 0; i < encodingEntInfo.liEncoding.Count; i++)
                    {
                        using (MySqlCommand cmdA = connection.CreateCommand())
                        {
                            string SqlStr = "insert into tbl_encodingdata(CusCompany,CusLinkMan,EncodingSatrt,EncodingEnd,CreateTime,CusId,EncodingOrder,KgNum,EncodeNum)values(@CusCompany,@CusLinkMan,@EncodingSatrt,@EncodingEnd,NOW(),@CusId,@EncodingOrder,@KgNum,@EncodeNum)";
                            MySqlParameter[] Mpa = new MySqlParameter[]
                {
                                    new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusLinkMan",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@EncodingSatrt",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@EncodingEnd",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusId",MySqlDbType.Int32),
                                    new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@KgNum",MySqlDbType.Int32),
                                    new MySqlParameter("@EncodeNum",MySqlDbType.Int32),

                };
                            Mpa[0].Value = encodingEntInfo.liEncoding[i].CusCompany;
                            Mpa[1].Value = encodingEntInfo.liEncoding[i].CusLinkMan;
                            Mpa[2].Value = encodingEntInfo.liEncoding[i].EncodingSatrt;
                            Mpa[3].Value = encodingEntInfo.liEncoding[i].EncodingEnd;
                            Mpa[4].Value = encodingEntInfo.liEncoding[i].CusId;
                            Mpa[5].Value = encodingEntInfo.liEncoding[i].EncodingOrder;
                            Mpa[6].Value = encodingEntInfo.liEncoding[i].KgNum;
                            Mpa[7].Value = encodingEntInfo.liEncoding[i].EncodeNum;
                            //result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);
                            cmdA.CommandText = SqlStr.ToString();
                            cmdA.CommandType = CommandType.Text;
                            cmdA.Transaction = tran;
                            cmdA.Parameters.AddRange(Mpa);
                            Result = cmdA.ExecuteNonQuery();
                            if (Result <= 0)
                            {
                                tran.Rollback();
                                return Result;
                            }
                        }
                    }
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "AddEncodingService", ex.Message.ToString(), "");
            }
            return Result;
        }

        public int UpdateEncodingService(int Id, string EncodingSatrt, string EncodingEnd, int KgNum)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_encodingdata set KgNum=@KgNum,EncodingSatrt=@EncodingSatrt,EncodingEnd=@EncodingEnd where Id=@Id";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@Id",MySqlDbType.Int32),
                                    new MySqlParameter("@EncodingSatrt",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@EncodingEnd",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@KgNum",MySqlDbType.Int32),

    };
                Mpa[0].Value = Id;
                Mpa[1].Value = EncodingSatrt;
                Mpa[2].Value = EncodingEnd;
                Mpa[3].Value = KgNum;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "UpdateEncodingService", ex.Message.ToString(), "");
            }
            return result;
        }

        public List<EncodingEnt> GetEncodingDataService()
        {
            MySqlDataReader Mysqlreader = null;
            List<EncodingEnt> liEncoding = new List<EncodingEnt>();
            EncodingEnt encodingEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    //new MySqlParameter("@CusId",MySqlDbType.Int32)
                 };
                //sqlarray[0].Value = CusId;
                string SqlStr = string.Format("SELECT Id,KgNum,EncodingOrder,CusCompany,CusLinkMan,CusId,EncodingEnd,EncodingSatrt FROM tbl_encodingdata where Del=0 ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    encodingEnt = new EncodingEnt();
                    encodingEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    encodingEnt.CusLinkMan = Mysqlreader["CusLinkMan"].ToString();

                    encodingEnt.EncodingEnd = Mysqlreader["EncodingEnd"].ToString();
                    encodingEnt.EncodingSatrt = Mysqlreader["EncodingSatrt"].ToString();
                    encodingEnt.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    encodingEnt.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    encodingEnt.KgNum = TransformDataHelper.TransformToInt(Mysqlreader["KgNum"].ToString());
                    encodingEnt.Id = TransformDataHelper.TransformToInt(Mysqlreader["Id"].ToString());
                    liEncoding.Add(encodingEnt);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "GetEncodingDataService", ex.Message.ToString(), "");
                return liEncoding;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liEncoding;
        }
        public List<EncodingEnt> GetEncodingDataByIdService(int Id)
        {
            MySqlDataReader Mysqlreader = null;
            List<EncodingEnt> liEncoding = new List<EncodingEnt>();
            EncodingEnt encodingEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@Id",MySqlDbType.Int32)
                 };
                sqlarray[0].Value = Id;
                string SqlStr = string.Format("SELECT CusCompany,CusLinkMan,CusId,EncodingEnd,EncodingSatrt,CusId FROM tbl_encodingdata where Id!=@Id");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    encodingEnt = new EncodingEnt();
                    encodingEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    encodingEnt.CusLinkMan = Mysqlreader["CusLinkMan"].ToString();

                    encodingEnt.EncodingEnd = Mysqlreader["EncodingEnd"].ToString();
                    encodingEnt.EncodingSatrt = Mysqlreader["EncodingSatrt"].ToString();
                    encodingEnt.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    liEncoding.Add(encodingEnt);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "GetEncodingDataByIdService", ex.Message.ToString(), "");
                return liEncoding;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return liEncoding;
        }


        public EncodingEnt GetEncodingByOrderService(string EncodingOrder)
        {
            MySqlDataReader Mysqlreader = null;
            EncodingEnt encodingEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50)
                 };
                sqlarray[0].Value = EncodingOrder;
                string SqlStr = string.Format("SELECT EncodingOrder,CusCompany,CusLinkMan,CusId,EncodingEnd,EncodingSatrt,CusId FROM tbl_encodingdata where Del=0 and EncodingOrder=@EncodingOrder ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    encodingEnt = new EncodingEnt();
                    encodingEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    encodingEnt.CusLinkMan = Mysqlreader["CusLinkMan"].ToString();
                    encodingEnt.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    encodingEnt.EncodingEnd = Mysqlreader["EncodingEnd"].ToString();
                    encodingEnt.EncodingSatrt = Mysqlreader["EncodingSatrt"].ToString();
                    encodingEnt.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "GetEncodingByOrderService", ex.Message.ToString(), EncodingOrder);
                return encodingEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return encodingEnt;
        }


        public EncodingEnt GetEncodingByEncodeIdService(string EncodeId)
        {
            MySqlDataReader Mysqlreader = null;
            EncodingEnt encodingEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@Id",MySqlDbType.Int32)
                 };
                sqlarray[0].Value = EncodeId;
                string SqlStr = string.Format("SELECT KgNum,EncodingOrder,CusCompany,CusLinkMan,CusId,EncodingEnd,EncodingSatrt,CusId FROM tbl_encodingdata where Del=0 and Id=@Id ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    encodingEnt = new EncodingEnt();
                    encodingEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    encodingEnt.CusLinkMan = Mysqlreader["CusLinkMan"].ToString();
                    encodingEnt.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    encodingEnt.EncodingEnd = Mysqlreader["EncodingEnd"].ToString();
                    encodingEnt.EncodingSatrt = Mysqlreader["EncodingSatrt"].ToString();
                    encodingEnt.CusId = TransformDataHelper.TransformToInt(Mysqlreader["CusId"].ToString());
                    encodingEnt.KgNum = TransformDataHelper.TransformToInt(Mysqlreader["KgNum"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "GetEncodingByEncodeIdService", ex.Message.ToString(), EncodeId);
                return encodingEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return encodingEnt;
        }

        public SelectOrderEnt GetSelectOrderService(string EncodingOrder)
        {
            MySqlDataReader Mysqlreader = null;
            SelectOrderEnt selectOrderEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@EncodingOrder",MySqlDbType.VarChar,50)
                 };
                sqlarray[0].Value = EncodingOrder;
                string SqlStr = string.Format("SELECT COUNT(EncodingOrder) as OrderCount, EncodingOrder FROM `tbl_selectorder` WHERE Del=0 and EncodingOrder=@EncodingOrder ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    selectOrderEnt = new SelectOrderEnt();
                    selectOrderEnt.EncodingOrder = Mysqlreader["EncodingOrder"].ToString();
                    selectOrderEnt.OrderCount =TransformDataHelper.TransformToInt(Mysqlreader["OrderCount"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("EncodingServer", "GetSelectOrderService", ex.Message.ToString(), EncodingOrder);
                return selectOrderEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return selectOrderEnt;
        }
    }
}
