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
    public class CusTomerManagerServer
    {
        public List<CusTomerEnt> SearchCusTomerInfo(ref PageParameterServerEnt PageParameter)
        {
            List<CusTomerEnt> cusTomerli = new List<CusTomerEnt>();
            try
            {
                string sqlStr = "select  CusId,CusCompany,CusLinkMan,CusLinkTel,CusLinkPhone,CreateTime from tbl_customer ";
                PageParameter.Orderby = "CreateTime";
                int TotalCount = 0;
                if (!string.IsNullOrEmpty(PageParameter.Where))
                {
                    PageParameter.Where = " where " + PageParameter.Where;
                }
                sqlStr = sqlStr + PageParameter.Where;
                cusTomerli = PageHelper.GetPageList<CusTomerEnt>(sqlStr, PageParameter.Orderby, "desc", PageParameter.PageIndex, PageParameter.PageSize, ref TotalCount);
                if (cusTomerli.Count > 0)
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
                ErrorLog.InsertError("CusTomerManagerServer", "SearchCusTomerInfo", ex.Message.ToString(), PageParameter.Where);
            }
            return cusTomerli;
        }

        public int UpdateCusTomerService(int CusId, string CusLinkMan, string CusLinkTel, string CusLinkPhone)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_customer set CusLinkMan=@CusLinkMan,CusLinkTel=@CusLinkTel,CusLinkPhone=@CusLinkPhone where CusId=@CusId";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@CusId",MySqlDbType.Int32),
                                    new MySqlParameter("@CusLinkMan",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusLinkTel",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusLinkPhone",MySqlDbType.VarChar,50),

    };
                Mpa[0].Value = CusId;
                Mpa[1].Value = CusLinkMan;
                Mpa[2].Value = CusLinkTel;
                Mpa[3].Value = CusLinkPhone;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("CusTomerManagerServer", "UpdateCusTomerService", ex.Message.ToString(), CusLinkMan + "|" + CusLinkTel + "|" + CusLinkPhone);
            }
            return result;
        }

        public int DeleteCusTomerService(int CusId)
        {
            int result = 0;
            try
            {
                string SqlStr = "update tbl_customer set DEL=1 where CusId=" + CusId;
                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("CusTomerManagerServer", "DeleteCusTomerService", ex.Message.ToString(), "");
            }
            return result;
        }

        public CusTomerEnt SearchCusTomerService(string CusCompany)
        {
            MySqlDataReader Mysqlreader = null;
            CusTomerEnt cusTomer = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@CusCompany",MySqlDbType.VarChar)
                 };
                sqlarray[0].Value = CusCompany;
                string SqlStr = string.Format("select  CusId,CusCompany,CusLinkMan,CusLinkTel,CusLinkPhone,CreateTime from tbl_customer where del=0 and CusCompany=@CusCompany");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    cusTomer = new CusTomerEnt();
                    cusTomer.CusCompany = Mysqlreader["CusCompany"].ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("CusTomerManagerServer", "SearchCusTomerService", ex.Message.ToString(), CusCompany);
                return cusTomer;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return cusTomer;
        }
        public int AddCusTomerService(CusTomerEnt cusTomerEnt)
        {
            int result = 0;
            try
            {
                string SqlStr = "insert into tbl_customer(CusCompany,CusLinkMan,CusLinkTel,CusLinkPhone,CreateTime,Del)values(@CusCompany,@CusLinkMan,@CusLinkTel,@CusLinkPhone,NOW(),0)";
                MySqlParameter[] Mpa = new MySqlParameter[]
    {
                                    new MySqlParameter("@CusCompany",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusLinkMan",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusLinkTel",MySqlDbType.VarChar,50),
                                    new MySqlParameter("@CusLinkPhone",MySqlDbType.VarChar,50),


    };
                Mpa[0].Value = cusTomerEnt.CusCompany;
                Mpa[1].Value = cusTomerEnt.CusLinkMan;
                Mpa[2].Value = cusTomerEnt.CusLinkTel;
                Mpa[3].Value = cusTomerEnt.CusLinkPhone;

                result = MySqlHelper.ExecuteNonQuery(SqlStr, CommandType.Text, Mpa);

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("CusTomerManagerServer", "AddCusTomerService", ex.Message.ToString(), "");
            }
            return result;
        }

        public CusTomerEnt GetCusTomerByIdService(int CusId)
        {
            MySqlDataReader Mysqlreader = null;
            CusTomerEnt cusTomerEnt = null;
            try
            {
                MySqlParameter[] sqlarray = new MySqlParameter[]
                 {
                    new MySqlParameter("@CusId",MySqlDbType.Int32)
                 };
                sqlarray[0].Value = CusId;
                string SqlStr = string.Format("SELECT CusCompany,CusLinkMan,CusId FROM tbl_customer where CusId=@CusId ");

                Mysqlreader = MySqlHelper.ExecuteDataReader(SqlStr, CommandType.Text, sqlarray);
                while (Mysqlreader.Read())
                {
                    cusTomerEnt = new CusTomerEnt();
                    cusTomerEnt.CusCompany = Mysqlreader["CusCompany"].ToString();
                    cusTomerEnt.CusLinkMan = Mysqlreader["CusLinkMan"].ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("CusTomerManagerServer", "GetCusTomerByIdService", ex.Message.ToString(), "");
                return cusTomerEnt;
            }
            finally
            {
                Mysqlreader.Close();
                Mysqlreader.Dispose();
            }
            return cusTomerEnt;
        }

    }
}
