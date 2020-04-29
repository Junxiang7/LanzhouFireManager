//百分比宽度
function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}

//编辑菜单
function EditData(val, row, index) {
    var str = "";
    str += "&nbsp;&nbsp;<span style=\"color:blue;cursor:pointer;\" class=\"Log_Info\"  RowId=\"" + index + "\">记录查看</span>";
    return str;
}


$(function () {
    //查询
    $("#Q_Query").click(function () {
        $('#DataList').datagrid('load', {
            Operator: $('#Q_Operator').val(),
            EffectiveTime_s: $("#D_EffectiveTime_s").val(),
            EffectiveTime_e: $("#D_EffectiveTime_e").val(),
        });
    });

    $(document).on("click", ".Log_Info", function () {
        var _index = $(this).attr("RowId");
        var rows = $('#DataList').datagrid('getRows')[_index];
        var m_HidOperationContent = rows.HidOperationContent
        var html = OrderLogHtml(m_HidOperationContent);
        $("#LogInfo").html(html);
        $("#LogInfo").show();
        $("#LogInfo").dialog("open");
    });

    var OrderLogHtml = function (data) {
        var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
        html += "<tr><td>操作内容:" + data + "</td></tr>";
        html += "</table>";
        return html;
    }

})
