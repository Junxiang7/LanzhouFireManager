$(function () {
    //查询
    $("#btn_pwd").click(function () {
        $("#txt_oldpwd").val("");
        $("#txt_newpwd").val("");
        $("#txt_anewpwd").val("");
        $("#PwdInfo").show();
        $("#PwdInfo").dialog("open");
    });
    $("#btn_newpwd").click(function () {
        var oldpwd = $("#txt_oldpwd").val();
        var newpwd = $("#txt_newpwd").val();
        var anewpwd = $("#txt_anewpwd").val();
        if (oldpwd == null || oldpwd == "" || oldpwd == undefined) {
            $.messager.alert('Warning', '请输入原密码');
            return false;
        }
        if (newpwd == null || newpwd == "" || newpwd == undefined) {
            $.messager.alert('Warning', '请输入新密码');
            return false;
        }
        if (anewpwd == null || anewpwd == "" || anewpwd == undefined) {
            $.messager.alert('Warning', '请输入确认密码');
            return false;
        }
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/SysManager/AutPwdInfo",
            data: { oldpwd: oldpwd, newpwd: newpwd, anewpwd: anewpwd },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $.messager.alert('Warning', '操作出错');
            },
            success: function (data) {
                if (data == "修改成功") {
                    $("#PwdInfo").hide();
                    $("#PwdInfo").dialog("close");
                    
                }
                else {
                    $.messager.alert('Warning', data);
                }
                //if (data == null || data == "" || data.length == 0) {
                //    $.messager.alert('Warning', '操作出错');
                //}
                //else {
                //    if (data.Success == "True") {
                //        $("#Msg").html("商户信息：" + data.data.CusCompany);
                //        $("#hid_CusId").val(data.data.CusId);
                //    }
                //    else {
                //        $("#Msg").html(data.Message);
                //    }
                //}
            }
        });
    });
})