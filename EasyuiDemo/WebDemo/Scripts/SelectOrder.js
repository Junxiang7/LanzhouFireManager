//百分比宽度
function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}

//编辑菜单
function EditData(val, row, index) {
    var str = "";
    //str += "&nbsp;&nbsp;<span style=\"color:green;cursor:pointer;\" class=\"Edit_Status\" RowId=\"" + index + "\">修改状态</span>";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Cus_Info\"  DataId=\"" + row.CusId + "\">客户详情</span>";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Log_Info\"  DataId=\"" + row.ProductId + "\">操作记录</span>";
    return str;
}


$(function () {
    //查询
    $("#Q_Query").click(function () {
        $('#DataList').datagrid('load', {
            ProductId: $('#Q_ProductId').val(),
            EncodingOrder: $('#Q_EncodingOrder').val(),
            EffectiveTime_s: $("#D_EffectiveTime_s").val(),
            EffectiveTime_e: $("#D_EffectiveTime_e").val(),
            OrderStatus: $("#Q_OrderStatus").val(),
            Type: 0,  //只查询当前状态
        });
    });

    $(document).on("click", ".Cus_Info", function () {
        var CusId = $(this).attr("DataId");
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/GetCusInfo",
            data: { CusId: CusId },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '暂无用户信息');
            },
            success: function (data) {
                if (data == null || data == "") {
                    $.messager.alert('Warning', '暂无用户信息');
                }
                else {
                    var html = CusHtml(data);
                    $("#CusInfo").html(html);
                    $("#CusInfo").show();
                    $("#CusInfo").dialog("open");
                }
            }
        });
    });

    var CusHtml = function (data) {
        var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
        html += "<tr><td>客户公司名称:" + data.CusCompany + "</td><td>联系人:" + data.CusLinkMan + "</td></tr>";
        html += "<tr><td>联系电话:" + data.CusLinkTel + "</td><td>联系手机:" + data.CusLinkPhone + "</td></tr>";
        html += "</table>";
        return html;
    }

    $(document).on("click", ".Log_Info", function () {
        var ProductId = $(this).attr("DataId");
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/GetLogInfo",
            data: { ProductId: ProductId },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            },
            success: function (data) {
                if (data == null || data == "" || data.length == 0) {
                    $.messager.alert('Warning', '暂无操作信息');
                }
                else {
                    var html = OrderLogHtml(data);
                    $("#LogInfo").html(html);
                    $("#LogInfo").show();
                    $("#LogInfo").dialog("open");
                }
            }
        });
    });

    var OrderLogHtml = function (data) {
        var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
        for (var i = 0; i < data.length; i++) {
            html += "<tr><td>操作时间:" + data[i].OperatingTime + "</td><td>操作人:" + data[i].Operator + "</td><td>操作内容:" + data[i].OperationalContent + "</td></tr>";
        }
        html += "</table>";
        return html;
    }


    $(document).on("click", ".Edit_Status", function () {
        var _index = $(this).attr("RowId");
        var rows = $('#DataList').datagrid('getRows')[_index];
        var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
        html += "<tr><td>订单状态:</td><td><select id='D_selectStatus'>";
        html += "<option value=\"1001\" >已经入库，等待充粉</option>";
        html += "<option value=\"1002\">已经充粉，等待组装</option>";
        html += "<option value=\"1003\">已经组装，等待贴标</option>";
        html += "<option value=\"1004\">已经贴标，等待出库</option>";
        html += "<option value=\"1005\">已经出库，操作完成</option>";
        html += "</select></td></tr>"; 
        html += "<tr><td colspan='2' align=\"center\"><input type='button' class='btnUpdate' value='确定修改' /><input type=\"hidden\" id=\"hid_id\" value=" + rows.OrderId + "><input type=\"hidden\" id=\"hid_ProductId\" value=" + rows.ProductId + "><input type=\"hidden\" id=\"hid_OrderStatus\" value=" + rows.OrderStatus + "> </td></tr></table>";
        $("#EditStatus").html(html);
        $("#D_selectStatus").val(rows.OrderStatus);
        $("#EditStatus").show();
        $("#EditStatus").dialog("open");
    });


    $(document).on("click", ".btnUpdate", function () {
        var OrderStatus = $("#D_selectStatus").val();
        var OrderId = $("#hid_id").val();
        var ProductId = $("#hid_ProductId").val();
        var oldStatus = $("#hid_OrderStatus").val();
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/UpdateStatus",
            data: { OrderStatus: OrderStatus, OrderId: OrderId, ProductId: ProductId, oldStatus: oldStatus },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '更新异常');
            },
            success: function (data) {
                if (data.Success < 1) {
                    $.messager.alert('Warning', '更新失败');
                }
                else
                {
                    document.location.reload();
                }
            }
        });
    });

})
