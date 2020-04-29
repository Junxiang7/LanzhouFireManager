using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class EncodingEntToModel
    {

        public EncodingInfoModel ListEncodingEntToModel(List<EncodingEnt> liEncodingEnt)
        {
            EncodingInfoModel encodingInfo = new Models.EncodingInfoModel();
            List<EncodingModel> liEncodingModel = new List<Models.EncodingModel>();
            try
            {
                if (liEncodingEnt != null && liEncodingEnt.Count > 0)
                {
                    for (int i = 0; i < liEncodingEnt.Count; i++)
                    {
                        EncodingModel encodingModel = new Models.EncodingModel();
                        encodingModel.Id = liEncodingEnt[i].Id;
                        encodingModel.CreateTime = liEncodingEnt[i].CreateTime;
                        encodingModel.CusCompany = liEncodingEnt[i].CusCompany;
                        encodingModel.CusLinkMan = liEncodingEnt[i].CusLinkMan;
                        encodingModel.EncodingSatrt = liEncodingEnt[i].EncodingSatrt;
                        encodingModel.EncodingEnd = liEncodingEnt[i].EncodingEnd;
                        encodingModel.CusId = liEncodingEnt[i].CusId;
                        encodingModel.EncodingOrder= liEncodingEnt[i].EncodingOrder;
                        encodingModel.KgNum = liEncodingEnt[i].KgNum;
                        encodingModel.EncodeNum = liEncodingEnt[i].EncodeNum;
                        liEncodingModel.Add(encodingModel);
                    }
                    encodingInfo.liEncoding = liEncodingModel;
                }
            }
            catch (Exception ex)
            {

            }
            return encodingInfo;
        }
    }
}