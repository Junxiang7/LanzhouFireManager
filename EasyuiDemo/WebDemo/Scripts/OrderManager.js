//百分比宽度
function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}

//编辑菜单
function EditData(val, row, index) {
    var str = "";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Cus_Info\"  DataId=\"" + row.CusId + "\">客户详情</span>";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Log_Info\"  DataId=\"" + row.ProductId + "\">操作记录</span>";
    return str;
}


$(function () {
    //查询
    $("#Q_Query").click(function () {
        $('#DataList').datagrid('load', {
            ProductId: $('#Q_ProductId').val(),
            EffectiveTime_s: $("#D_EffectiveTime_s").val(),
            EffectiveTime_e: $("#D_EffectiveTime_e").val(),
            OrderStatus: $("#Q_OrderStatus").val(),
            EncodingOrder: $('#Q_EncodingOrder').val(),
            Type: 1, //查询所有状态
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

    //第一工站
    //$(document).on("click", ".WaitFirst", function () {
    //    var ProductId = $(this).attr("DataId");
    //    var OrderStatus = $(this).attr("OrderStatus");
    //    $("#hid_FirstProductId").val(ProductId);
    //    $("#hid_FirstOrderStatus").val(OrderStatus);

    //    $("#FirstInfo").show();
    //    $("#FirstInfo").dialog("open");

    //});
    //$("#btnFirst").click(function () {
    //    var ProductId = $("#hid_FirstProductId").val();
    //    var OrderStatus = $("#hid_FirstOrderStatus").val();
    //    //ajax异步提交
    //    $.ajax({
    //        type: "POST",   //post提交方式默认是get
    //        url: "/OrderManager/OrderProcess",
    //        data: { ProductId: ProductId, OrderStatus: OrderStatus },   //序列化
    //        cache: false,
    //        async: false,
    //        error: function (request) {      // 设置表单提交出错
    //            $.messager.alert('Warning', '操作出错');
    //        },
    //        success: function (data) {

    //        }
    //    });
    //});

    ////第二工站
    //$(document).on("click", ".WaitSecond", function () {
    //    var ProductId = $(this).attr("DataId");
    //    var OrderStatus = $(this).attr("OrderStatus");
    //    //ajax异步提交
    //    $.ajax({
    //        type: "POST",   //post提交方式默认是get
    //        url: "/OrderManager/OrderProcess",
    //        data: { ProductId: ProductId, OrderStatus: OrderStatus },   //序列化
    //        cache: false,
    //        async: false,
    //        error: function (request) {      // 设置表单提交出错
    //            $.messager.alert('Warning', '操作出错');
    //        },
    //        success: function (data) {
    //            if (data == null || data == "" || data.length == 0) {
    //                $.messager.alert('Warning', '暂无操作信息');
    //            }
    //            else {
    //                var html = OrderLogHtml(data);
    //                $("#LogInfo").html(html);
    //                $("#LogInfo").show();
    //                $("#LogInfo").dialog("open");
    //            }
    //        }
    //    });
    //});
    //$(document).on("click", ".WaitFirst", function () {
    //    var ProductId = $(this).attr("DataId");
    //    var OrderStatus = $(this).attr("OrderStatus");
    //    $("#hid_FirstProductId").val(ProductId);
    //    $("#hid_FirstOrderStatus").val(OrderStatus);

    //    $("#FirstInfo").show();
    //    $("#FirstInfo").dialog("open");

    //});
    //$("#btnFirst").click(function () {
    //    var ProductId = $("#hid_FirstProductId").val();
    //    var OrderStatus = $("#hid_FirstOrderStatus").val();
    //    //ajax异步提交
    //    $.ajax({
    //        type: "POST",   //post提交方式默认是get
    //        url: "/OrderManager/OrderProcess",
    //        data: { ProductId: ProductId, OrderStatus: OrderStatus },   //序列化
    //        cache: false,
    //        async: false,
    //        error: function (request) {      // 设置表单提交出错
    //            $.messager.alert('Warning', '操作出错');
    //        },
    //        success: function (data) {

    //        }
    //    });
    //});

    ////第三工站
    //$(document).on("click", ".WaitThird", function () {
    //    var ProductId = $(this).attr("DataId");
    //    var OrderStatus = $(this).attr("OrderStatus");
    //    //ajax异步提交
    //    $.ajax({
    //        type: "POST",   //post提交方式默认是get
    //        url: "/OrderManager/OrderProcess",
    //        data: { ProductId: ProductId, OrderStatus: OrderStatus },   //序列化
    //        cache: false,
    //        async: false,
    //        error: function (request) {      // 设置表单提交出错
    //            $.messager.alert('Warning', '操作出错');
    //        },
    //        success: function (data) {
    //            if (data == null || data == "" || data.length == 0) {
    //                $.messager.alert('Warning', '暂无操作信息');
    //            }
    //            else {
    //                var html = OrderLogHtml(data);
    //                $("#LogInfo").html(html);
    //                $("#LogInfo").show();
    //                $("#LogInfo").dialog("open");
    //            }
    //        }
    //    });
    //});
    //$(document).on("click", ".WaitFirst", function () {
    //    var ProductId = $(this).attr("DataId");
    //    var OrderStatus = $(this).attr("OrderStatus");
    //    $("#hid_FirstProductId").val(ProductId);
    //    $("#hid_FirstOrderStatus").val(OrderStatus);

    //    $("#FirstInfo").show();
    //    $("#FirstInfo").dialog("open");

    //});
    //$("#btnFirst").click(function () {
    //    var ProductId = $("#hid_FirstProductId").val();
    //    var OrderStatus = $("#hid_FirstOrderStatus").val();
    //    //ajax异步提交
    //    $.ajax({
    //        type: "POST",   //post提交方式默认是get
    //        url: "/OrderManager/OrderProcess",
    //        data: { ProductId: ProductId, OrderStatus: OrderStatus },   //序列化
    //        cache: false,
    //        async: false,
    //        error: function (request) {      // 设置表单提交出错
    //            $.messager.alert('Warning', '操作出错');
    //        },
    //        success: function (data) {

    //        }
    //    });
    //});

    $("#Q_ScrapOrder").click(function () {
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/GetScrapInfo",
            data: {},   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            },
            success: function (data) {
                if (data == null || data == "" || data.length == 0) {
                    $.messager.alert('Warning', '操作异常');
                }
                else {
                    var html = ScrapHtml(data);
                    $("#div_ScrapInfo").html(html);
                    $("#ScrapInfo").show();
                    $("#ScrapInfo").dialog("open");
                }
            }
        });
    });

    var ScrapHtml = function (data) {
        var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
        html += "<tr><td>编号</td><td><input type='text' id='txt_ScrapNum' /></td></tr>";
        html += "<tr><td>客户</td><td><select id='sel_Scrapuser'>";
        for (var i = 0; i < data.length; i++) {
            html += "<option value='" + data[i].CusId + "|" + data[i].CusCompany + "' >" + data[i].CusCompany + "</option>";
        }
        html += "</select></td></tr>";
        // html += "<tr><td colspan='2' align='center'><input type='button' value='录入' class=\"easyui-linkbutton\" style=\"width:80px\" id='btnScrap' /></td></tr>";
        html += "</table>";
        return html;

    }

    //商品入库
    $("#btn_Input").click(function () {
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/GetScrapInfo",
            data: {},   //序列化
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
                    var html = inputHtml(data);
                    $("#div_InputInfo").html(html);
                    $("#InputInfo").show();
                    $("#InputInfo").dialog("open");
                }
            }
        });
    });

    var inputHtml = function (data) {
        var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
        html += "<tr><td>编号</td><td><input type='text' id='txt_InputNum' /></td></tr>";
        html += "<tr><td>客户</td><td><select id='sel_Inputuser'>";
        for (var i = 0; i < data.length; i++) {
            html += "<option value='" + data[i].CusId + "|" + data[i].CusCompany + "' >" + data[i].CusCompany + "</option>";
        }
        html += "</select></td></tr>";
        //html += "<tr><td colspan='2' align='center'><input type='button' value='录入' style=\"width:80px\" id='btnInput' /></td></tr>";
        html += "</table>";
        return html;

    }

    //商品入库确认
    $("#btnInput").click(function () {
        var InputNum = $("#txt_InputNum").val();
        var Inputuser = $("#sel_Inputuser").val();
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/ProductInput",
            data: { Status: "1001", InputNum: InputNum, Inputuser: Inputuser },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            },
            success: function (data) {
                if (data == "操作成功") {
                    $.messager.alert('Warning', "商品入库成功");
                    document.location.reload();
                }
                else {
                    $.messager.alert('Warning', data);
                }
            }
        });
    });

    //商品报废确认录入
    $("#btnScrap").click(function () {
        var InputNum = $("#txt_ScrapNum").val();
        var Inputuser = $("#sel_Scrapuser").val();
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/ProductInput",
            data: { Status: "1006", InputNum: InputNum, Inputuser: Inputuser },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            },
            success: function (data) {
                if (data == "操作成功") {
                    document.location.reload();
                }
                else {
                    $.messager.alert('Warning', data);
                }
            }
        });
    });
})
