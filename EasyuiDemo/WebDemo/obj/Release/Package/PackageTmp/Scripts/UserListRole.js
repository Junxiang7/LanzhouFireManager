
var setting = {
    view: {
        selectedMulti: false
    },
    check: {
        enable: true
    },
    data: {
        key: {
            //将treeNode的ItemName属性当做节点名称
            name: "menuname"
        },
        simpleData: {
            //是否使用简单数据模式
            enable: true,
            //当前节点id属性  
            idKey: "menuid",
            //当前节点的父节点id属性 
            pIdKey: "fathermenuid",
            //用于修正根节点父节点数据，即pIdKey指定的属性值
            rootPId: 0
        }
    },
    callback: {
        beforeCheck: beforeCheck,
        onCheck: onCheck
    }
};
 
 
function beforeCheck(treeId, treeNode) {
 
    return (treeNode.doCheck !== false);
}
var arrayObj = new Array();
function onCheck(e, treeId, treeNode) {
    if (treeNode.check_Child_State > 0) {
        arrayObj.push(treeNode.menuid);
        for (var i = 0; i < treeNode.children.length; i++) {
            //	alert(treeNode.children[i].menuid + treeNode.children[i].menuname);
 
            arrayObj.push(treeNode.children[i].menuid);
        }
    }
    else {
        var fatherid;
        //选中则添加
        if (treeNode.checked == true) {
            //不存在数组中
            if (arrayObj.indexOf(treeNode.parentTId.split('_')[1]) == -1) {
                fatherid = treeNode.parentTId.split('_')[1];
                arrayObj.push(fatherid);
            }
            //var roleid = treeNode.parentTId.split('_')[1] + "," + treeNode.tId.split('_')[1];
            //alert(roleid);
            arrayObj.push(treeNode.menuid);
            //alert(treeNode.menuid + treeNode.menuname);
        }
            //去掉勾选则从数组中删除
        else {
            //判断一个父节点取消则相应的子节点全部取消
            if (treeNode.check_Child_State == 0) {
                //先清除掉父节点然后循环去掉字节点的选中ID
                RemoveRoleItem(treeNode.menuid);
                		
                for (var i = 0; i < treeNode.children.length; i++) {
                		
                    RemoveRoleItem(treeNode.children[i].menuid);
                			
                }
            }//清除选中的子节点及父节点
            else {
                //1:		换成arrayObj无效
                //var arr = [1, 10, 11, 12, 13, 14, 2, 3, 4, 5, 6, 7];
                //arr.splice($.inArray(4, arr), 1);
                //alert(arr);
                //第二种方法移除
                //arrayObj = $.grep(arrayObj, function (value) {
                //	return value != treeNode.menuid;
                //});
                //第三种方法移除
                RemoveRoleItem(treeNode.menuid);
                //父节点下面只有一个子节点处理(子节点取消,父节点保留)	后续再修改
                var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                var node = treeObj.getNodeByParam("menuid", treeNode.fathermenuid);
                node.checked = true;
                treeObj.updateNode(node);
            
            }
        }
    }
    //定义了sort的比较函数
    arrayObj = arrayObj.sort(function (a, b) {
        return a - b;
    });
    //	alert(arrayObj.toString());
    $("#hfRole").val('');
    $("#hfRole").val(arrayObj.toString());
}
//获取URL请求参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function RemoveRoleItem(name) {
    for (var i = 0; i < arrayObj.length; i++) {
        if (arrayObj[i] == name) {
            arrayObj.splice(i, 1);	//从下标为i的元素开始，连续删除1个元素
            i--;//因为删除下标为i的元素后，该位置又被新的元素所占据，所以要重新检测该位置
        }
    }
}

$(function () {
    $.post("UserRoleSet.aspx", function (json) {
        var treeObj = $.fn.zTree.init($("#treeDemo"), setting, json);
        //默认展开所有节点
        treeObj.expandAll(false);
 
        //如果地址有参数时则为修改选中状态
        if (getQueryString("upid") != null) {
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            arrayObj = $("#hfRole").val().split(',');
            //var mid = $("#hfRole").val().split(',');
            for (var i = 0; i < arrayObj.length; i++) {
                var node = treeObj.getNodeByParam("menuid", arrayObj[i]);
                node.checked = true;
                treeObj.updateNode(node);
 
            }
        }
 
    });
});
