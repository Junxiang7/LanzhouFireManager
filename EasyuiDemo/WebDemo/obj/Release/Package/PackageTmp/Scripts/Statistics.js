

//编辑菜单
//function EditData(val, row, index) {
//    var str = "";
//    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Cus_Info\"  DataId=\"" + row.CusId + "\">客户详情</span>";
//    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Log_Info\"  DataId=\"" + row.ProductId + "\">操作记录</span>";
//    return str;
//}

//百分比宽度
function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}




$(function () {
    //查询
    $("#Q_Query").click(function () {
        var CusTomer = $("#txt_CusTomer").val();
        var StartTime = $("#D_EffectiveTime_s").val();
        var EndTime = $("#D_EffectiveTime_e").val();
        var radio = $('input[name="radio"]:checked').val();
        var encodingOrder = $("#txt_encodingOrder").val();
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/GetStatisticsData",
            data: { CusTomer: CusTomer, StartTime: StartTime, EndTime: EndTime, radio: radio, encodingOrder: encodingOrder },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '暂无用户信息');
            },
            success: function (data) {
                var rows = [];

                for (var i = 0; i < data.rows.length; i++) {   //data是返回值的集合 
                    rows.push({               //把data数据对应的值压到rows对应数组中 
                        CusTomer: data.rows[i].CusTomer,
                        FirstWorkstation: data.rows[i].FirstWorkstation,
                        SecondWorkstation: data.rows[i].SecondWorkstation,
                        ThirdWorkstation: data.rows[i].ThirdWorkstation,
                        FourthWorkstation: data.rows[i].FourthWorkstation,
                        ScrapNum: data.rows[i].ScrapNum,
                        QueryTime: data.rows[i].QueryTime,
                        EncodingOrder: data.rows[i].EncodingOrder,
                        Kg_1: data.rows[i].Kg_1,
                        Kg_2: data.rows[i].Kg_2,
                        Kg_3: data.rows[i].Kg_3,
                        Kg_4: data.rows[i].Kg_4,
                        Kg_5: data.rows[i].Kg_5,
                        Kg_8: data.rows[i].Kg_8,
                        Kg_20: data.rows[i].Kg_20,
                        Kg_35: data.rows[i].Kg_35,
                        Kg_50: data.rows[i].Kg_50,
                        SumKG: data.rows[i].SumKG
                    });
                }
                if (radio == "0")  //每天查询
                {
                    $('#DataList').datagrid('showColumn', 'FirstWorkstation');
                    $('#DataList').datagrid('showColumn', 'SecondWorkstation');
                    $('#DataList').datagrid('showColumn', 'ThirdWorkstation');
                    $('#DataList').datagrid('showColumn', 'FourthWorkstation');
                    $('#DataList').datagrid('showColumn', 'ScrapNum');
                    $('#DataList').datagrid('showColumn', 'QueryTime');

                    $('#DataList').datagrid('hideColumn', 'CusTomer');
                    $('#DataList').datagrid('hideColumn', 'EncodingOrder');
                    $('#DataList').datagrid('hideColumn', 'Kg_1');
                    $('#DataList').datagrid('hideColumn', 'Kg_2');
                    $('#DataList').datagrid('hideColumn', 'Kg_3');
                    $('#DataList').datagrid('hideColumn', 'Kg_4');
                    $('#DataList').datagrid('hideColumn', 'Kg_5');
                    $('#DataList').datagrid('hideColumn', 'Kg_8');
                    $('#DataList').datagrid('hideColumn', 'Kg_20');
                    $('#DataList').datagrid('hideColumn', 'Kg_35');
                    $('#DataList').datagrid('hideColumn', 'Kg_50');
                    $('#DataList').datagrid('hideColumn', 'SumKG');
                    
                }
                else if (radio == "1")  //用户总计
                {
                    $('#DataList').datagrid('showColumn', 'FirstWorkstation');
                    $('#DataList').datagrid('showColumn', 'SecondWorkstation');
                    $('#DataList').datagrid('showColumn', 'ThirdWorkstation');
                    $('#DataList').datagrid('showColumn', 'FourthWorkstation');
                    $('#DataList').datagrid('showColumn', 'ScrapNum');
                    $('#DataList').datagrid('showColumn', 'CusTomer');
                    $('#DataList').datagrid('showColumn', 'EncodingOrder');
                    $('#DataList').datagrid('hideColumn', 'QueryTime');
                    $('#DataList').datagrid('hideColumn', 'Kg_1');
                    $('#DataList').datagrid('hideColumn', 'Kg_2');
                    $('#DataList').datagrid('hideColumn', 'Kg_3');
                    $('#DataList').datagrid('hideColumn', 'Kg_4');
                    $('#DataList').datagrid('hideColumn', 'Kg_5');
                    $('#DataList').datagrid('hideColumn', 'Kg_8');
                    $('#DataList').datagrid('hideColumn', 'Kg_20');
                    $('#DataList').datagrid('hideColumn', 'Kg_35');
                    $('#DataList').datagrid('hideColumn', 'Kg_50');
                    $('#DataList').datagrid('hideColumn', 'SumKG');
                }
                else if (radio == "2")  //公斤数总计
                {

                    $('#DataList').datagrid('hideColumn', 'FirstWorkstation');
                    $('#DataList').datagrid('hideColumn', 'SecondWorkstation');
                    $('#DataList').datagrid('hideColumn', 'ThirdWorkstation');
                    $('#DataList').datagrid('hideColumn', 'FourthWorkstation');
                    $('#DataList').datagrid('hideColumn', 'ScrapNum');
                    $('#DataList').datagrid('hideColumn', 'QueryTime');

                    $('#DataList').datagrid('showColumn', 'CusTomer');
                    $('#DataList').datagrid('showColumn', 'EncodingOrder');
                    $('#DataList').datagrid('showColumn', 'CusTomer');
                    $('#DataList').datagrid('showColumn', 'EncodingOrder');
                    $('#DataList').datagrid('showColumn', 'Kg_1');
                    $('#DataList').datagrid('showColumn', 'Kg_2');
                    $('#DataList').datagrid('showColumn', 'Kg_3');
                    $('#DataList').datagrid('showColumn', 'Kg_4');
                    $('#DataList').datagrid('showColumn', 'Kg_5');
                    $('#DataList').datagrid('showColumn', 'Kg_8');
                    $('#DataList').datagrid('showColumn', 'Kg_20');
                    $('#DataList').datagrid('showColumn', 'Kg_35');
                    $('#DataList').datagrid('showColumn', 'Kg_50');
                    $('#DataList').datagrid('showColumn', 'SumKG');
                }

                $('#DataList').datagrid({ data: rows }).datagrid('clientPaging');

            }
        });
    });

})

