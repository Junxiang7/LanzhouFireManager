function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}
//用来格式化标签
//编辑菜单
function EditData(val, row, index) {
    var str = "";
    str += "&nbsp;&nbsp;<span style=\"color:red;cursor:pointer;\" class=\"Edit_Delete\" DataType=\"0\" RowId=\"" + index + "\">删除</span>";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Edit_Data\"  RowId=\"" + index + "\">查看</span>";
    return str;
}
$(function () {
    //重置
    $("#Q_Reset").click(function () {
        $("#Q_CusCompany").val("");
        $("#Q_EncodingOrder").val("");
        $('#DataList').datagrid('load', {
            CusCompany: "",
        });
    });
    //查询
    $("#Q_Query").click(function () {
        $('#DataList').datagrid('load', {
            CusCompany: $('#Q_CusCompany').val(),
            EncodingOrder: $('#Q_EncodingOrder').val(),
        });
    });

    $(document).on("click", ".Edit_Delete", function () {
        if (confirm("确定要删除吗？")) {
            var _index = $(this).attr("RowId");
            var rows = $('#DataList').datagrid('getRows')[_index];
            //        $('#sel_Inputuser').val(rows.CusCompany);
            //var Id = $(this).attr("DataId");
            //var _datatype = $(this).attr("DataType");

            var Id = rows.Id;
            var EncodingOrder = rows.EncodingOrder;
            //ajax异步提交
            $.ajax({
                type: "POST",   //post提交方式默认是get
                url: "/Encoding/AddOrEditUser",
                data: { Id: Id, UpType: 2, EncodingOrder: EncodingOrder },   //序列化
                cache: false,
                async: false,
                error: function (request) {      // 设置表单提交出错
                    $.messager.alert('Warning', '操作出错');
                },
                success: function (data) {
                    console.log(data);
                    if (data != "操作成功")
                    {
                        $.messager.alert('Warning', data);
                    }
                    else
                    {
                        document.location.reload();
                    }
                }
            });
        }
    });

    $(document).on("click", ".Edit_Data", function () {
        $("#sel_Inputuser").find("option").remove();
        var _index = $(this).attr("RowId");
        $("#BuyerAll").show();
        $("#BuyerAll").dialog("open");
        //取消所有权限
        //获取当前行对象

        var rows = $('#DataList').datagrid('getRows')[_index];



        var table = document.getElementById("EncodeTable");
        var tr = $(table).find("tr");
        if (tr.length > 2) {
            for (var i = 2; i < tr.length; i++) {
                $(tr).eq(i).remove();
            }
        }
        var m_rows = table.rows;
        var m_EncodingStart = $(m_rows).find("input[name='EncodingStart']");
        var m_EncodingEnd = $(m_rows).find("input[name='EncodingEnd']");
        var m_SelKgNum = $(m_rows).find("select[name='Q_SelKgNum']");


        m_EncodingStart.val(rows.EncodingSatrt);
        m_EncodingEnd.val(rows.EncodingEnd);
        m_SelKgNum.val(rows.KgNum);


        m_EncodingStart.attr("disabled", true);
        m_EncodingEnd.attr("disabled", true);
        m_SelKgNum.attr("disabled", true);

        $("#D_Id").val(rows.Id);
        //$("#D_EncodingSatrt").val(rows.EncodingSatrt);
        //$("#D_EncodingEnd").val(rows.EncodingEnd);
        $('#sel_Inputuser').val(rows.CusCompany);
        $("#D_EncodingOrder").val(rows.EncodingOrder);
        //$("#Q_SelKgNum").val(rows.KgNum);
        $("#D_EncodingOrder").attr("disabled", true);
        $("#btntradd").css("display", "none");  //block
        $("#Save_btn").css("display", "none");  //block

        //$('#sel_Inputuser').attr('ReadOnly', "readonly"); //添加disabled属性
        $("#sel_Inputuser").append("<option value='" + rows.CusId + "|" + rows.CusCompany + "|" + rows.CusLinkMan + "'>" + rows.CusCompany + "</option>");
    });

    $("#Add_Encoding").click(function () {
        $('#D_EncodingOrder').removeAttr("disabled"); //移除disabled属性


        var table = document.getElementById("EncodeTable");
        var m_rows = table.rows;

        var tr = $(table).find("tr");
        if (tr.length > 2) {
            for (var i = 2; i < tr.length; i++) {
                $(tr).eq(i).remove();
            }
        }

        var m_EncodingStart = $(m_rows).find("input[name='EncodingStart']");
        var m_EncodingEnd = $(m_rows).find("input[name='EncodingEnd']");
        var m_SelKgNum = $(m_rows).find("select[name='Q_SelKgNum']");
        m_SelKgNum.val("1");
        m_EncodingStart.val("");
        m_EncodingEnd.val("");
        m_EncodingStart.removeAttr("disabled");
        m_EncodingEnd.removeAttr("disabled");
        m_SelKgNum.removeAttr("disabled");

        $("#Save_btn").css("display", "block");  //block
        $("#btntradd").css("display", "block");  //block
        $("#sel_Inputuser").find("option").remove();
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/Encoding/GetScrapInfo",
            data: {},   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            },
            success: function (data) {
                if (data != null && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        $("#sel_Inputuser").append("<option value='" + data[i].CusId + "|" + data[i].CusCompany + "|" + data[i].CusLinkMan + "'>" + data[i].CusCompany + "</option>");
                    }
                }
                $("#D_Id").val("0");
                $("#D_EncodingSatrt").val("");
                //var m_EncodingOrder = new Date().Format("HHmmss");
                //m_EncodingOrder = "8" + Math.ceil(Math.random() * 9) + m_EncodingOrder;
                $("#D_EncodingEnd").val("");
                $("#D_EncodingOrder").val("");
                $("#BuyerAll").show();
                $("#BuyerAll").dialog("open");
                $("#Q_SelKgNum").val("1");
                //$("#D_EncodingOrder").attr("disabled", true);
            }
        });


    });
    $("#Save_btn").click(function () {

        //var EncodingSatrt = $("#D_EncodingSatrt").val();
        //var EncodingEnd = $("#D_EncodingEnd").val();
        var EncodingOrder = $("#D_EncodingOrder").val();
        //var KgNum = $("#Q_SelKgNum").val();
        if (EncodingOrder == null || EncodingOrder == "" || EncodingOrder == undefined || EncodingOrder.length != 8) {
            $.messager.alert('Warning', '订单编码只能为八位数字');
            return false;
        }
        var regorder = new RegExp("^[0-9]*$");  // ^[0-9A-Za-z]*$
        if (!regorder.test(EncodingOrder)) {
            $.messager.alert('Warning', '订单编码只能包含为数字');
            return false;
        }

        var EncodingData = "";

        var table = document.getElementById("EncodeTable");
        var rows = table.rows;
        for (var i = 1; i < rows.length; i++) {    //--循环所有的行
            var m_rows = rows[i];
            var EncodingStart = $(m_rows).find("input[name='EncodingStart']").val();
            var EncodingEnd = $(m_rows).find("input[name='EncodingEnd']").val();
            var SelKgNum = $(m_rows).find("select[name='Q_SelKgNum']").val();

            if (EncodingStart == null || EncodingStart == "" || EncodingStart == undefined) {
                $.messager.alert('Warning', '开始ID不能为空');
                return false;
            }
            if (EncodingStart.length != 13) {
                $.messager.alert('Warning', '开始ID必须为13位数字');
                return false;
            }
            if (EncodingEnd == null || EncodingEnd == "" || EncodingEnd == undefined) {
                $.messager.alert('Warning', '结束ID不能为空');
                return false;
            }
            if (EncodingEnd.length != 13) {
                $.messager.alert('Warning', '结束ID必须为13位数字');
                return false;
            }
            var reg = new RegExp("^[0-9]*$");
            if (!reg.test(EncodingStart) || !reg.test(EncodingEnd)) {
                $.messager.alert('Warning', 'ID只能包含为数字');
                return false;
            }

            EncodingData += EncodingStart + "^" + EncodingEnd + "^" + SelKgNum + "|";
        }
        var postdata = {
            Id: $("#D_Id").val(),
            EncodingSatrt: "",
            EncodingEnd: "",
            Inputuser: $("#sel_Inputuser").val(),
            EncodingOrder: EncodingOrder,
            UpType: 0,
            KgNum: "",
            EncodingData: EncodingData
        }
        $('#Save_btn').attr('disabled', "true"); //添加disabled属性
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/Encoding/AddOrEditUser",
            data: postdata,   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            }, complete: function () {
                $('#Save_btn').removeAttr("disabled"); //移除disabled属性
            },
            success: function (data) {
                if (data == "操作成功") {
                    document.location.reload();
                }
                else {
                    $.messager.alert('Warning', '操作出错:' + data);
                }
            }
        });

    });


    $("#btntradd").click(function () {
        var _len = $("#EncodeTable tr").length;
        if (_len == 10) {
            $.messager.alert('Warning', '已达到最大添加数');
            return false;
        }
        var table = $("#EncodeTable");
        var html = "";
        html += "<tr>";
        html += "<td><label>开始Id: </label><input type='text' name='EncodingStart' onblur='EncodeTotal(this)' style='height:23px' /></td>";
        html += "<td><label>结束Id: </label><input type='text' name='EncodingEnd' onblur='EncodeTotal(this)' style='height:23px' /></td>";
        html += "<td><label>公斤数：</label> <select name='Q_SelKgNum' style='width:100px;height:27px'>";
        html += "<option value='1' selected='selected'>1</option> <option value='2'>2</option><option value='3'>3</option><option value='4'>4</option>";
        html += "<option value='5'>5</option><option value='8'>8</option><option value='20'>20</option><option value='35'>35</option><option value='50'>50</option></select> KG <a href=\'#\' style='text-decoration:none;color:red' onclick=\'deltr(this)\'>删除</a></td><td style='color:green;font-weight:bold'><span name='sp_Total'></span></td>";
        html += "</tr>";
        table.append(html);
    });

    //删除<tr/>
    deltr = function (obj) {
        $(obj).parent().parent().remove();//删除当前行
    }

    EncodeTotal = function (obj) {
        var tr = $(obj).parent().parent()
        var EncodingStart = $(tr).find("input[name='EncodingStart']").val();
        var EncodingEnd = $(tr).find("input[name='EncodingEnd']").val();
        if (EncodingStart == "" || EncodingStart == undefined || EncodingStart == null) {
            return false;
        }
        if (EncodingEnd == "" || EncodingEnd == undefined || EncodingEnd == null) {
            return false;
        }
        var m_EncodingStart = parseFloat(EncodingStart);
        var m_EncodingEnd = parseFloat(EncodingEnd);
        if (isNaN(m_EncodingStart)) {
            return false;
        }
        if (isNaN(m_EncodingEnd)) {
            return false;
        }
        var Total = m_EncodingEnd - m_EncodingStart + 1;
        if (Total < 1) {
            $(tr).find("span").html("区间ID有误");
            //$(span).html(Total);
            return false;
        }
        $(tr).find("span").html("【" + Total + "】" + "个订单");
        //$(span).html(Total);
        //var ss = $(tr).find("input[name='measures']").val();//input

    }
});



Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}