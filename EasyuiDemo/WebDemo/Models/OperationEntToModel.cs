using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OperationEntToModel
    {
        public OperationInfoModel ListOperationEntToModel(List<OperationEnt> operationli)
        {
            OperationInfoModel operationInfoModel = new Models.OperationInfoModel();
            List<OperationModel> liOperation = new List<OperationModel>();
            try
            {
                if (operationli != null && operationli.Count > 0)
                {
                    for (int i = 0; i < operationli.Count; i++)
                    {
                        OperationModel operationModel = new OperationModel();
                        operationModel.OperationContent = operationli[i].OperationContent;
                        if (operationModel.OperationContent.Length > 25)
                        {
                            operationModel.OperationContent = operationModel.OperationContent.Substring(0, 24) + "...";
                        }
                        operationModel.HidOperationContent = operationli[i].OperationContent;
                        operationModel.OperationName = operationli[i].OperationName;
                        operationModel.OperationTime = operationli[i].OperationTime;
                        operationModel.Operator = operationli[i].Operator;
                        operationModel.OperId = operationli[i].OperId;
                        liOperation.Add(operationModel);
                    }

                }
                operationInfoModel.liOperation = liOperation;
            }
            catch (Exception ex)
            {

            }
            return operationInfoModel;
        }
    }
}