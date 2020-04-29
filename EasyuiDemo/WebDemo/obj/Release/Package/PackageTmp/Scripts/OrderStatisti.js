function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
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
            EncodingOrder: $('#Q_EncodingOrder').val(),
            EffectiveTime_s: $("#D_EffectiveTime_s").val(),
            EffectiveTime_e: $("#D_EffectiveTime_e").val(),
        });
    });
});