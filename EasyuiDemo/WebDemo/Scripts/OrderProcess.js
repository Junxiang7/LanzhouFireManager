//百分比宽度
function fixWidth(percent) {
    return (document.body.clientWidth - 5) * percent;
}



$(function () {
    var Input_arr = [];
    //第二工站
    $(document).on("click", "#btn_Second", function () {
        $("#SecondMsg").css("color", " ");
        $("#SecondMsg").html("");
        $("#txtSecond").val("");
        $("#Q_OrderStatus_Second").val("1002");
        $("#SecondInfo").show();
        $("#SecondInfo").dialog("open");
        setTimeout(function () {
            $("#txtSecond").focus();
        }, 1)
    });
    $("#btnSecond").click(function () {
        var ProductId = $("#txtSecond").val();
        var ProcessOrderStatus = $("#Q_OrderStatus_Second").val();
        //var SecondKgNum = $("#Q_SecondKgNum").val();
        if (ProductId == "" || ProductId == null || ProductId == undefined) {
            $("#SecondMsg").css("color", "red");
            $("#SecondMsg").html("录入ID不能为空");
            setTimeout(function () {
                $("#txtSecond").focus();
            }, 1)
            return false;
        }
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/OrderProcessInfo",
            data: { ProductId: ProductId, OrderStatus: 1002, ProcessOrderStatus: ProcessOrderStatus },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $("#SecondMsg").css("color", "red");
                $("#SecondMsg").html("操作异常");
                setTimeout(function () {
                    $("#txtSecond").focus();
                }, 1)
            },
            success: function (data) {
                if (data != "操作成功") {
                    $("#SecondMsg").css("color", "red");
                    $("#SecondMsg").html(data);
                }
                else {
                    if (ProcessOrderStatus == "1002") {
                        $("#SecondMsg").html("充粉成功:【ID：" + ProductId + "】");
                    }
                    if (ProcessOrderStatus == "1009") {
                        $("#SecondMsg").html("报废成功:【ID：" + ProductId + "】");
                    }

                    $("#SecondMsg").css("color", "green");
                    //$("#SecondMsg").html("");
                }
                $("#txtSecond").val("");
                setTimeout(function () {
                    $("#txtSecond").focus();
                }, 1)
            }
        });
    });

    //第三工站
    $(document).on("click", "#btn_Third", function () {
        $("#ThirdMsg").css("color", " ");
        $("#ThirdMsg").html("");
        $("#txtThird").val("");
        $("#Q_OrderStatus_Third").val("1003");
        $("#ThirdInfo").show();
        $("#ThirdInfo").dialog("open");
        setTimeout(function () {
            $("#txtThird").focus();
        }, 1)
    });
    $("#btnThird").click(function () {
        var ProductId = $("#txtThird").val();
        var ProcessOrderStatus = $("#Q_OrderStatus_Third").val();
        if (ProductId == "" || ProductId == null || ProductId == undefined) {
            $("#ThirdMsg").css("color", "red");
            $("#ThirdMsg").html("录入ID不能为空");
            setTimeout(function () {
                $("#txtThird").focus();
            }, 1)
            return false;
        }
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/OrderProcessInfo",
            data: { ProductId: ProductId, OrderStatus: 1003, ProcessOrderStatus: ProcessOrderStatus },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $("#ThirdMsg").css("color", "red");
                $("#ThirdMsg").html("操作异常");
                setTimeout(function () {
                    $("#txtThird").focus();
                }, 1)
            },
            success: function (data) {
                if (data != "操作成功") {
                    $("#ThirdMsg").css("color", "red");
                    $("#ThirdMsg").html(data);
                }
                else {
                    $("#ThirdMsg").css("color", "green");
                    if (ProcessOrderStatus == "1002") {
                        $("#ThirdMsg").html("组装成功:【ID：" + ProductId + "】");
                    }
                    if (ProcessOrderStatus == "1009") {
                        $("#ThirdMsg").html("报废成功:【ID：" + ProductId + "】");
                    }


                }
                $("#txtThird").val("");
                setTimeout(function () {
                    $("#txtThird").focus();
                }, 1)
            }
        });
    });

    //第四工站
    $(document).on("click", "#btn_Fourth", function () {
        $("#FourthMsg").css("color", " ");
        $("#FourthMsg").html("");
        $("#txtFourth").val("");

        $("#FourthInfo").show();
        $("#FourthInfo").dialog("open");
        setTimeout(function () {
            $("#txtFourth").focus();
        }, 1)
    });
    $("#btnFourth").click(function () {
        var ProductId = $("#txtFourth").val();
        if (ProductId == "" || ProductId == null || ProductId == undefined) {
            $("#FourthMsg").css("color", "red");
            $("#FourthMsg").html("录入ID不能为空");
            setTimeout(function () {
                $("#txtFourth").focus();
            }, 1)
            return false;
        }
        //ajax异步提交
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/OrderProcessInfo",
            data: { ProductId: ProductId, OrderStatus: "1004" },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $("#FourthMsg").css("color", "red");
                $("#FourthMsg").html("操作异常");
                setTimeout(function () {
                    $("#txtFourth").focus();
                }, 1)
            },
            success: function (data) {
                if (data.indexOf("操作成功") >= 0) {
                    $("#FourthMsg").css("color", "green");
                    $("#FourthMsg").html(data);
                }
                else {
                    $("#FourthMsg").css("color", "red");
                    $("#FourthMsg").html(data);
                }
                $("#txtFourth").val("");
                setTimeout(function () {
                    $("#txtFourth").focus();
                }, 1)

            }
        });
    });


    //商品入库
    $("#btn_Input").click(function () {
        //Input_arr = []
        //$.ajax({
        //    type: "POST",   //post提交方式默认是get
        //    url: "/OrderManager/GetScrapInfo",
        //    data: {},   //序列化
        //    cache: false,
        //    async: false,
        //    error: function (request) {      // 设置表单提交出错
        //        $.messager.alert('Warning', '操作出错');
        //    },
        //    success: function (data) {
        //        if (data == null || data == "" || data.length == 0) {
        //            $.messager.alert('Warning', '暂无操作信息');
        //        }
        //        else {
        //            for (var i = 0; i < data.length; i++) {
        //                $("#sel_Inputuser").append("<option value='" + data[i].CusId + "|" + data[i].CusCompany + "'>" + data[i].CusCompany + "</option>");
        //            }
        //            $("#InputInfo").show();
        //            $("#InputInfo").dialog("open");
        //            setTimeout(function () {
        //                $("#txt_InputNum").focus();
        //            }, 1)
        //        }
        //    }
        //});
        $("#Q_OrderStatus").val("1001")
        $("#Msg").css("color", "");
        $("#txt_InputNum").val("");
        $("#Msg").html("");
        $("#hid_CusId").val("");
        $("#hid_EncodingOrder").val("");
        $("#InputInfo").show();
        $("#InputInfo").dialog("open");
        setTimeout(function () {
            $("#txt_InputNum").focus();
        }, 1)

    });

    //批量入库
    $("#btn_BatchInput").click(function () {
        $('#btnBatchInput').removeAttr("disabled"); //移除disabled属性
        $("#BatchInfo").show();
        $("#BatchInfo").dialog("open");
        $("#txt_SInputNum").val("");
        $("#txt_EInputNum").val("");
        $("#sp_bTotal").html("");
        $("#BatchMsg").html("");
        $("#hid_bCusId").val("");
        $("#hid_bEncodingOrder").val("");
        $('#txt_bInputNum').val("");
    })

    $("#btnBatchInput").click(function () {
        var CusId = $("#hid_bCusId").val();
        var EncodingOrder = $("#hid_bEncodingOrder").val();
        var KgNum = $("#hid_bKgNum").val();
        var EncodeId = $("#hid_bEncodeId").val();
        var SProductId = $("#txt_SInputNum").val();
        if (SProductId == "" || SProductId == null || SProductId == undefined) {
            $("#BatchMsg").css("color", "red");
            $("#BatchMsg").html("录入开始ID不能为空");
            setTimeout(function () {
                $("#txt_SInputNum").focus();
            }, 1)
            return false;
        }
        var EProductId = $("#txt_EInputNum").val();
        if (EProductId == "" || EProductId == null || EProductId == undefined) {
            $("#BatchMsg").css("color", "red");
            $("#BatchMsg").html("录入结束ID不能为空");
            setTimeout(function () {
                $("#txt_EInputNum").focus();
            }, 1)
            return false;
        }
        //var index = layer.load();
        //ajax异步提交
        $('#btnBatchInput').attr('disabled', "true"); //添加disabled属性
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/ProductBatchInput",
            data: { SProductId: SProductId, EProductId: EProductId, CusId: CusId, EncodingOrder: EncodingOrder, KgNum: KgNum, EncodeId: EncodeId },   //序列化
            cache: false,
            async: true,
            error: function (request) {      // 设置表单提交出错
                $("#BatchMsg").css("color", "red");
                $("#BatchMsg").html("操作出错");
                setTimeout(function () {
                    $("#txt_SInputNum").focus();
                }, 1)
            },
            beforeSend: function (data) {
                layer.load();
            },
            complete: function () {
                $('#btnBatchInput').removeAttr("disabled"); //移除disabled属性
            },
            success: function (data) {
                layer.closeAll("loading");
                if (data != "操作成功") {
                    $("#BatchMsg").css("color", "red");
                    $("#BatchMsg").html(data);
                }
                else {
                    $("#BatchMsg").css("color", "green");
                    $("#BatchMsg").html("入库成功:【ID：" + SProductId + "----" + EProductId + "】");
                }
                $("#txt_SInputNum").val("");
                $("#txt_EInputNum").val("");

                $("#hid_bCusId").val("");
                $("#hid_bEncodingOrder").val("");
                $('#txt_bInputNum').val("");
                $("#sp_bTotal").html("");
                setTimeout(function () {
                    $("#txt_SInputNum").focus();
                }, 1)
            }
        });

    })

    EncodeTotal = function (obj) {
        var EncodingStart = $("#txt_SInputNum").val();
        var EncodingEnd = $("#txt_EInputNum").val();
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
            $("#sp_bTotal").html("区间ID有误");
            return false;
        }
        else if (Total > 25) {
            $("#sp_bTotal").html("ID数大于25个");
            return false;
        }
        $("#sp_bTotal").html("【" + Total + "】" + "个订单");

        if (EncodingStart.length == 13 && EncodingEnd.length == 13) {
            $.ajax({
                type: "POST",   //post提交方式默认是get
                url: "/OrderManager/BatchAutKeyUpCode",
                data: { EncodingStart: EncodingStart, EncodingEnd: EncodingEnd },   //序列化
                cache: false,
                async: false,
                error: function (request) {      // 设置表单提交出错
                    $("#BatchMsg").html("【操作出错】");
                },
                success: function (data) {
                    if (data == null || data == "" || data.length == 0) {
                        $("#BatchMsg").html("【操作出错】");
                    }
                    else {
                        if (data.Success == "True") {
                            $("#BatchMsg").css("color", "Green");
                            $("#BatchMsg").html("商户信息：" + data.data.CusCompany + "【订单编码:" + data.data.EncodingOrder + "】【公斤数：" + data.data.KgNum + "Kg 】");
                            $("#hid_bCusId").val(data.data.CusId);
                            $("#hid_bEncodingOrder").val(data.data.EncodingOrder);
                            $("#hid_bKgNum").val(data.data.KgNum)
                            $("#hid_bEncodeId").val(data.data.EncodeId);
                        }
                        else {
                            $("#BatchMsg").css("color", "red");
                            $("#hid_bCusId").val("");
                            $("#hid_bEncodingOrder").val("");
                            $('#txt_bInputNum').val("");
                            if (data.Message == null || data.Message == undefined || data.Message == "")
                            {
                                $("#BatchMsg").html("【暂无权限】");
                            }
                            else
                            {
                                $("#BatchMsg").html("【" + data.Message + "】");
                            }


                            setTimeout(function () {
                                $("#txt_SInputNum").focus();
                            }, 1)
                        }
                    }
                }
            });
        }
        else {
            $("#BatchMsg").css("color", "red");
            $("#BatchMsg").html("无效的ID长度");
        }
    }

    //var inputHtml = function (data) {
    //    var html = " <table class=\"contenttable\" style=\"width:500px;  margin:auto;\">";
    //    html += "<tr><td>编号</td><td><input type='text' id='txt_InputNum' name='txt_InputNum' /></td></tr>";
    //    html += "<tr><td>客户</td><td><select id='sel_Inputuser'>";
    //    for (var i = 0; i < data.length; i++) {
    //        html += "<option value='" + data[i].CusId + "|" + data[i].CusCompany + "' >" + data[i].CusCompany + "</option>";
    //    }
    //    html += "</select></td></tr>";
    //    html += "</table>";
    //    return html;
    //}

    //商品入库确认
    $("#btnInput").click(function () {
        var InputNum = $("#txt_InputNum").val();
        //if (InputNum != "") {
        //    Input_arr.push(InputNum);
        //}
        //if (Input_arr == undefined || Input_arr.length == 0) {
        //    alert("确保有录入的商品编号");
        //}
        //for (var i = 0; i < Input_arr.length; i++) {
        //    alert(Input_arr[i]);
        //}
        var CusId = $("#hid_CusId").val();
        var Status = $("#Q_OrderStatus").val();
        var EncodingOrder = $("#hid_EncodingOrder").val();
        var KgNum = $("#hid_KgNum").val();
        var EncodeId = $("#hid_EncodeId").val();
        if (InputNum == null || InputNum == "" || InputNum == undefined) {
            $("#Msg").html("录入ID不能为空");
            $("#Msg").css("color", "red");
            setTimeout(function () {
                $("#txt_InputNum").focus();
            }, 1)
            return false;
        }
        //var Inputuser = $("#sel_Inputuser").val();
        //CusId: CusId, Status: Status
        $.ajax({
            type: "POST",   //post提交方式默认是get
            url: "/OrderManager/ProductInput",
            data: { Status: Status, InputNum: InputNum, CusId: CusId, EncodingOrder: EncodingOrder, KgNum: KgNum, EncodeId: EncodeId },   //序列化
            cache: false,
            async: false,
            error: function (request) {      // 设置表单提交出错
                $("#Msg").css("color", "red");
                $("#Msg").html("操作出错");
                setTimeout(function () {
                    $("#txt_InputNum").focus();
                }, 1)
            },
            success: function (data) {
                if (data != "操作成功") {
                    $("#Msg").css("color", "red");
                    $("#Msg").html(data);
                }
                else {
                    $("#Msg").css("color", "green");
                    $("#Msg").html("入库成功:【ID：" + InputNum + "】");
                }
                $('#txt_InputNum').val("");

                $("#hid_CusId").val("");
                $("#hid_EncodingOrder").val("");
                setTimeout(function () {
                    $("#txt_InputNum").focus();
                }, 1)
            }
        });
    });

    $('#txt_InputNum').keyup(function () {
        var code = $(this).val();
        if (code.length > 13) {
            code = code.substr(0, 13);
            $(this).val(code);
        }
        if (code.length == 13) {
            $.ajax({
                type: "POST",   //post提交方式默认是get
                url: "/OrderManager/AutKeyUpCode",
                data: { code: code },   //序列化
                cache: false,
                async: false,
                error: function (request) {      // 设置表单提交出错
                    $.messager.alert('Warning', '操作出错');
                },
                success: function (data) {
                    if (data == null || data == "" || data.length == 0) {
                        $.messager.alert('Warning', '操作出错');
                    }
                    else {
                        if (data.Success == "True") {
                            $("#Msg").css("color", "");
                            $("#Msg").html("商户信息：" + data.data.CusCompany + "【订单编码:" + data.data.EncodingOrder + "】【公斤数：" + data.data.KgNum + "Kg 】");
                            $("#hid_CusId").val(data.data.CusId);
                            $("#hid_EncodingOrder").val(data.data.EncodingOrder);
                            $("#hid_KgNum").val(data.data.KgNum)
                            $("#hid_EncodeId").val(data.data.EncodeId);
                        }
                        else {
                            $("#Msg").css("color", "red");
                            $("#hid_CusId").val("");
                            $("#hid_EncodingOrder").val("");
                            $('#txt_InputNum').val("");
                            $("#Msg").html(code + ":【" + data.Message + "】");
                            setTimeout(function () {
                                $("#txt_InputNum").focus();
                            }, 1)
                        }
                    }
                }
            });
        }
        else {
            $("#Msg").css("color", "red");
            $("#Msg").html("无效的ID长度");
        }
    });

    //入库数据输入
    //$('#txt_InputNum').keyup(function () {
    //    var code = $(this).val();
    //    //alert(code);
    //    //如果输入超出产品识别号位数
    //    //if (code.length > 11) {
    //    //    code = code.substr(0, 11);
    //    //    $(this).val(code);
    //    //}
    //    if (code.length == 13) {
    //        if ($.inArray(code, Input_arr) >= 0) {
    //            $("#txt_InputNum").val("");
    //            alert("已存在该商品编号");
    //            setTimeout(function () {
    //                $("#txt_InputNum").focus();
    //            }, 1)
    //            return false;
    //        }
    //        if (Input_arr.length == 5) {
    //            $("#txt_InputNum").val("");
    //            alert("请线录入已扫描商品编号");
    //            return false;
    //        }
    //        Input_arr.push(code);
    //        $("#tab_Inputhtml").append("<tr><td>" + code + "</td></tr>");
    //        $("#txt_InputNum").val("");
    //        setTimeout(function () {
    //            $("#txt_InputNum").focus();
    //        }, 1)
    //    }
    //});

    $("#btn_ScrapOrder").click(function () {
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
                    setTimeout(function () {
                        $("#txt_ScrapNum").focus();
                    }, 1)
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

    //商品报废确认录入
    $("#btnScrap").click(function () {
        var InputNum = $("#txt_ScrapNum").val();


        var Inputuser = $("#sel_Scrapuser").val();
        //$.ajax({
        //    type: "POST",   //post提交方式默认是get
        //    url: "/OrderManager/ProductInput",
        //    data: { Status: "1006", InputNum: InputNum, Inputuser: Inputuser },   //序列化
        //    cache: false,
        //    async: false,
        //    error: function (request) {      // 设置表单提交出错
        //        $.messager.alert('Warning', '操作出错');
        //    },
        //    success: function (data) {
        //        if (data == "操作成功") {
        //            Input_arr = [];
        //            document.location.reload();
        //        }
        //        else {
        //            $.messager.alert('Warning', data);
        //        }
        //    }
        //});
    });
})
