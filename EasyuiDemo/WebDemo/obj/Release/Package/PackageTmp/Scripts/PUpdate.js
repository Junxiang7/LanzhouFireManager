var psum = 0;

$(function () {

    read();

    $("#savepower").click(function () {
        save();
    });

    $("#backgo").click(function () {
        history.go(-1);
    });
});

//初始化数据
var read = function () {
    var zTree = null;
    var p = $("#ParmKey").val();
    var index = null;

    $.AjaxPostRetJson(BaseUrl + "/AuthorityRole/PowerRead", { power: p }, function () {
        index = layer.load(0, { time: 10 * 1000 });
    }, false, function (rel) {
        layer.close(index);
        if (rel.Result) {
            var roleId;
            if (rel.UserName != null && rel.UserName != undefined) {
                var u = rel.UserName.split(',');
                roleId = u[0];
                $("#_userName").html(u[1]);
            }
            if (rel.Data != null && rel.Data.length > 0) {
                var array = new Array();
                for (var i = 0; i < rel.Data.length; i++) {
                    var check = false;
                    if (rel.Data[i].RoleID == roleId) {
                        array.push("<input type='radio' checked='checked' name='roleradio' value='" + rel.Data[i].RoleID + "' title='" + rel.Data[i].RoleName + "' onclick=roleclick(\'" + rel.Data[i].RoleID + "\') />" + rel.Data[i].RoleName + "<br/>");
                    } else {
                        array.push("<input type='radio' name='roleradio' value='" + rel.Data[i].RoleID + "' title='" + rel.Data[i].RoleName + "' onclick=roleclick(\'" + rel.Data[i].RoleID + "\') />" + rel.Data[i].RoleName + "<br/>");
                    }
                }
                var hl = array.join("");
                $("#RoleGroup").append(hl);
                zTree = rel.Data1;
                psum = parseInt(rel.PowerSum);

                var setting = {
                    treeId: "pZtree",
                    check: {
                        enable: false,
                        chkboxType: { "Y": "ps", "N": "ps" }
                    },
                    data: {
                        simpleData: {
                            enable: true
                        }
                    },
                    view: {
                        showLine: false
                    }
                };
                if (zTree != null) {
                    var zTreeNode = zNode(zTree);
                    var ztreeObj = $("#zPowerTree");
                    $.fn.zTree.init(ztreeObj, setting, zTreeNode);
                }

            } else {
                nullshuju();
            }
        } else {
            tishidecontens(rel.Msg);
        }

    })
    return zTree;
}
//封装zTree 
var zNode = function (data) {
    var zarray = new Array();
    for (var i = 0; i < data.length; i++) {

        zarray.push({ id: data[i].TId, name: data[i].Name, pId: data[i].ParentTId, open: false, icon: "../Content/zTreeStyle/img/diy/8.png" });

    }

    return zarray;
}

//角色点击事件
var roleclick = function (rid) {
    var treeObj = $.fn.zTree.getZTreeObj("zPowerTree");
    treeObj.checkAllNodes(false);
    var index = null;

    $.AjaxPostRetJson(BaseUrl + "/AuthorityRole/GetPowerByRoleID", { RoleID: rid }, function () {
        index = layer.load(0, { time: 10 * 1000 });
    }, false, function (rel) {

        layer.close(index);
        if (rel.Result) {

            var setting = {
                treeId: "pZtree",
                check: {
                    enable: false,
                    chkboxType: { "Y": "ps", "N": "ps" }
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                view: {
                    showLine: false
                }
            };

            var zTreeNode = zNode(rel.Data);
            var ztreeObj = $("#zPowerTree");

            $.fn.zTree.init(ztreeObj, setting, zTreeNode);
        } else {
            tishidecontens(rel.Msg);
        }
    })
}

//保存
var save = function () {
    var oldInfo = $("#ParmKey").val().split(',');

    var roleid = $("[name='roleradio']:checked").val();
    var roleName = $("[name='roleradio']:checked").attr("title");
    if (roleid == null || roleid == undefined) {
        tishidecontens("请先选择角色信息。");
        return;
    }
    if ($("#ParmKey").val() == "" || $("#ParmKey").val() == undefined) {
        tishidecontens("信息丢失请返回上一页,重新进入。");
        return;
    }


    var oldRoleId = oldInfo[1];
    var rarId = oldInfo[0];
    var walletkey = oldInfo[3];
    var walletAccount = oldInfo[4];
    var treeObj = $.fn.zTree.getZTreeObj("zPowerTree");
    var nodes = treeObj.getNodes();
    var selectNode = treeObj.transformToArray(nodes);
    var idSum = 0;
    var ids = "";

    for (var i = 0; i < selectNode.length; i++) {

        var node = selectNode[i];
        ids += node.id + ",";
        idSum += parseInt(node.id);
    }

    if (oldRoleId == roleid) {
        tishidecontens("角色没有任何修改本次提交不会保存。");
        return;
    }


    $.AjaxPostRetJson(BaseUrl + "/AuthorityRole/Save", { RarID: rarId, NewRoleId: roleid, OldRoleId: oldRoleId, Pids: ids, OldPSum: psum, RoleName: roleName, walletkey: walletkey, walletAccount: walletAccount }, function () {
        index = layer.load(0, { time: 10 * 1000 });
    }, false, function (rel) {

        layer.close(index);
        if (rel.Result) {
            layer.confirm("保存成功。是否返回用户管理？", {
                btn: ['确定', '取消'] //按钮
            }, function () {
                window.location.href = "/AuthorityRole/PowerInfo";
            }, function () {

            });
        } else {
            tishidecontens(rel.Msg);
        }
    })

}

