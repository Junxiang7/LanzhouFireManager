function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}
//用来格式化标签
//编辑菜单
function EditData(val, row, index) {
    var str = "";
    str += "&nbsp;&nbsp;<span style=\"color:green;cursor:pointer;\" class=\"Edit_Delete\" DataType=\"0\" DataId=\"" + row.CusId + "\">删除</span>";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Edit_Data\"  RowId=\"" + index + "\">编辑</span>";
    return str;
}
$(function () {
    //重置
    $("#Q_Reset").click(function () {
        $("#Q_CusCompany").val("");
        $('#DataList').datagrid('load', {
            CusCompany: "",
        });
    });
    //查询
    $("#Q_Query").click(function () {
        $('#DataList').datagrid('load', {
            CusCompany: $('#Q_CusCompany').val(),
        });
    });

    $(document).on("click", ".Edit_Delete", function () {
        if (confirm("确定要删除吗？")) {
            var CusId = $(this).attr("DataId");
            var _datatype = $(this).attr("DataType");
            //ajax异步提交
            $.ajax({
                type: "POST",   //post提交方式默认是get
                url: "/Customer/AddOrEditUser",
                data: { CusId: CusId, UpType: 2 },   //序列化
                cache: false,
                async: false,
                error: function (request) {      // 设置表单提交出错
                    $.messager.alert('Warning', '操作出错');
                },
                success: function (data) {
                    $.messager.alert('Warning', data);
                    document.location.reload();
                }
            });
        }
    });

    $(document).on("click", ".Edit_Data", function () {

        var _index = $(this).attr("RowId");
        $("#BuyerAll").show();
        $("#BuyerAll").dialog("open");
        //取消所有权限
        //获取当前行对象
        var rows = $('#DataList').datagrid('getRows')[_index];
        $("#D_CusId").val(rows.CusId);
        $("#D_CusCompany").val(rows.CusCompany);
        $("#D_CusLinkMan").val(rows.CusLinkMan);
        $("#D_CusLinkTel").val(rows.CusLinkTel);
        $("#D_CusLinkPhone").val(rows.CusLinkPhone);
        $("#D_CusCompany").attr("disabled", true);  //添加disabled属性
    });
    $("#Add_User").click(function () {
        $('#Save_btn').removeAttr("disabled"); //移除disabled属性
        $('#D_CusCompany').removeAttr("disabled"); //移除disabled属性
        $("#D_CusId").val("0");
        $("#D_CusCompany").val("");
        $("#D_CusLinkMan").val("");
        $("#D_CusLinkTel").val("");
        $("#D_CusLinkPhone").val("");
        $("#BuyerAll").show();
        $("#BuyerAll").dialog("open");
    });
    $("#Save_btn").click(function () {
        var CusLinkMan = $("#D_CusLinkMan").val();
        var CusLinkTel = $("#D_CusLinkTel").val();
        var CusLinkPhone = $("#D_CusLinkPhone").val();

        if (CusLinkMan == null || CusLinkMan == "" || CusLinkMan == undefined) {
            $.messager.alert('Warning', '联系人不能为空');
            return false;
        }
        var postdata = {
            CusId: $("#D_CusId").val(),
            CusCompany: $("#D_CusCompany").val(),
            CusLinkMan: CusLinkMan,
            CusLinkTel: CusLinkTel,
            CusLinkPhone: CusLinkPhone,
            UpType: 0,
        }
        $('#Save_btn').attr('disabled', "true"); //添加disabled属性
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/Customer/AddOrEditUser",
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
});