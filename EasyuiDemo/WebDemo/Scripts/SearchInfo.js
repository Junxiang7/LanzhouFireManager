
$(function () {
    $("#query").click(function () {
        var EncodingOrder = $("#postid").val();
        if (EncodingOrder == null || EncodingOrder == "") {
            $("#tab_Data").html("查询信息为空");
            return false;
        }
        var regorder = new RegExp("^[0-9]*$");
        if (!regorder.test(EncodingOrder)) {
            return false;
        }
        var time = new Date().Format("yyyyMMddHHmmss");
        var Data = { EncodeOrder: EncodingOrder, Time: time };
        //var Data_str = JSON.stringify(Data);
        $("#sp_content").html("");
        $("#tab_Data").html("");
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/Search/GetFireInfo",
            data: Data,   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $("#sp_msg").html("订单编码：" + EncodingOrder);
                $("#tab_Data").html("未获取到查询信息");
            }, complete: function () {
                //$("#sp_msg").html("订单编码：" + EncodingOrder);
                //$("#tab_Data").html("等待中......");
            },
            success: function (data) {
                $("#sp_msg").html("订单编码：" + EncodingOrder);
                if (data != null && data.rows != "" && data.rows.length > 0) {
                    var html = "";
                    if (data.SearchType == "1") {
                        html = htmlData(data);
                        $("#sp_content").html(data.CusCompany + data.Msg);
                    }
                    else if (data.SearchType == "2") {
                        $("#sp_content").html(data.CusCompany + data.Msg);
                        html = HtmlOrderData(data);
                    }
                    else {
                        html = "无效的查询信息";
                    }
                    $("#tab_Data").html(html);
                }
                else {
                    $("#tab_Data").html(data.ErrorMsg);
                }
            }
        });

        function htmlData(data) {
            var html = "";
            html += "<thred><tr><th>公斤</th><th>总数</th><th>已修</th><th>报废</th><th>待修</th></tr></thead>";
            var m_EncodCount = 0;
            var m_ProductNum = 0;
            var m_ScrapNum = 0;
            var m_WaitRepair = 0;
            html+="<tbody>";
            for (var i = 0; i < data.rows.length; i++) {
                html += "<tr>";
                html += "<td>" + Nonull(data.rows[i].KgNum) + "</td>";
                html += "<td>" + Nonull(data.rows[i].EncodCount) + "</td>";
                html += "<td>" + Nonull(data.rows[i].ProductNum) + "</td>";
                html += "<td>" + Nonull(data.rows[i].ScrapNum) + "</td>";
                html += "<td>" + Nonull(data.rows[i].WaitRepair) + "</td>";
                html += "</tr>";
                m_EncodCount = m_EncodCount + parseInt(Nonull(data.rows[i].EncodCount));
                m_ProductNum = m_ProductNum + parseInt(Nonull(data.rows[i].ProductNum));
                m_ScrapNum = m_ScrapNum + parseInt(Nonull(data.rows[i].ScrapNum));
                m_WaitRepair = m_WaitRepair + parseInt(Nonull(data.rows[i].WaitRepair));
            }
            html += "</tbody>";
            html += "<tr><td colspan='5'>合计[总数：" + m_EncodCount + "][已修：" + m_ProductNum + "][报废：" + m_ScrapNum + "][待修：" + m_WaitRepair + "]</td></tr>";
            return html;
        }

        function HtmlOrderData(data) {
            var html = "";
            for (var i = 0; i < data.rows.length; i++) {
                html += "<tr>";
                html += "<td>" + Nonull(data.rows[i].CreateTime) + "</td>";
                html += "<td>" + Nonull(data.rows[i].KgNum) + "公斤干粉灭火器，状态为【 <span style='color:green;'>" + Nonull(data.rows[i].OrderStatusTest) + "</span>】</td>";
                html += "</tr>";
            }
            return html;
        }

    })
})

function Nonull(data) {
    if (data == null || data == "") {
        return "0";
    } else {
        return data;
    }
}

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