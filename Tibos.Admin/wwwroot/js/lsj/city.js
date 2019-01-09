
//请求地址(列表)
var RequestListUrl = "/City/GetCityList";
//请求参数(关联上级标识)
var RequestTopID = "id";
//请求地址(下级列表)
var RequestSubListUrl = "/City/GetCitySubList"
//请求地址(单条记录)
var RequestUrl = "/City/GetCity";
//请求地址(删除记录)
var RequestDelUrl = "/City/DelCity";
//请求控制器(修改)
var RequestEditUrl = "/City/EditCity";
//请求控制器(添加)
var RequestAddUrl = "/City/AddCity"

$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_departments').bootstrapTable({
            url: RequestListUrl,                 //请求后台的URL（*）
            method: 'post',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            checkboxHeader: true,               //列头是否显示全选
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: false,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "asc",                   //排序方式

            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）

            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            silent: true,  //刷新事件必须设置
            formatLoadingMessage: function () {
                return "<div class='sk-spinner sk-spinner-wave'>" +
                    "<div class='sk-rect1'></div>" +
                    "<div class='sk-rect2'></div>" +
                    "<div class='sk-rect3'></div>" +
                    "<div class='sk-rect4'></div>" +
                    "<div class='sk-rect5'></div>" +
                    "</div>";
            },
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: false,                //是否启用点击选中行

            height: 800,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表

            showExport: true,                     //是否显示导出
            exportDataType: "basic",              //basic', 'all', 'selected'.

            columns: [{
                checkbox: true
            }, {
                field: 'id',
                title: '编号',
            },
            {
                field: 'name',
                title: '名称',
                formatter: nameFormatter
            },
            {
            field: 'parent_id',
            title: '上级编号',
            }
            , {
                field: 'sort_id',
                title: '排序'
            }
             
            ,
            {
                field: 'status',
                title: '状态',
                formatter: statusFormatter
            }
            ]

        });

    };



    function nameFormatter(value, row, index) {

        var n = "";//层级的空格
        var f = "fa fa-folder";//展开收拢的样式
        //先判断节点层级
        if (row.layer != 1) {
            for (var i = 0; i < row.layer - 1; i++) {
                n += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            if (row.layer == 3) {
                f = "fa fa-file-text-o";
            }
            //return n + "<i class='" + f + "'txt='" + row.id + "'></i> " + row.name;
        }
        //再判断是否收拢

        if (row.opens) {
            f = "fa fa-folder-open";
        }
        return n + "<i class='" + f + "' onclick='openFile(this)' style='cursor:pointer' txt='" + row.id + "'></i> " + row.name;
    }

    function statusFormatter(value, row, index) {
        if (row.status == -1) {
            return "<i class=\"fa fa-circle text-danger\" title=\"禁用\"></i>"
        } else {
            return "<i class=\"fa fa-circle text-navy\" title=\"启用\"></i>"
        }
    }
    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        $("#btn_add").click(function () {
            $("#myModal").find(".form-control").val("");
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            $('#txt_parent_id').val(0)
            if (arrselections.length > 1) {
                var d = dialog({
                    fixed: true,
                    content: "只能选择一行进行新增",
                    padding: 30

                });
                d.show();
                //关闭提示模态框
                setTimeout(function () {
                    d.close().remove();
                }, 2000);
                return;
            }
            if (arrselections.length == 1) {
                if (arrselections[0].layer > 2) {
                    var d = dialog({
                        fixed: true,
                        content: "已经没有下级了!",
                        padding: 30

                    });
                    d.show();
                    //关闭提示模态框
                    setTimeout(function () {
                        d.close().remove();
                    }, 2000);
                    return;
                } else {
                    $('#txt_parent_id').val(arrselections[0].id);
                }
            }
            $("#myModalLabel").text("新增");
            $("#form0").attr("action", RequestAddUrl);
            $('#myModal').modal()
        });

        $("#btn_edit").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                var d = dialog({
                    fixed: true,
                    content: "只能选择一行进行编辑",
                    padding: 30

                });
                d.show();
                //关闭提示模态框
                setTimeout(function () {
                    d.close().remove();
                }, 2000);
                return;
            }
            if (arrselections.length <= 0) {
                var d = dialog({
                    fixed: true,
                    content: "请选择有效数据",
                    padding: 30

                });
                d.show();
                //关闭提示模态框
                setTimeout(function () {
                    d.close().remove();
                }, 2000);
                return;
            }
            $("#myModalLabel").text("编辑");
            $.post(RequestUrl, { id: arrselections[0].id }, function (data) {
                $("#id").val(data.id);
                $("#txt_name").val(data.name);
                $("#txt_sort_id").val(data.sort_id);
                $('#txt_parent_id').val(data.parent_id);
                if (data.status != -1) {
                    $(".rdobox:first").removeClass("unchecked");
                    $(".rdobox:first").addClass("checked");
                    $(".rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");


                    $(".rdobox:last").removeClass("checked");
                    $(".rdobox:last").addClass("unchecked");
                    $(".rdobox:last").children(".check-image").css("background", "url(../img/input-unchecked.png)");


                } else {
                    $(".rdobox:last").removeClass("unchecked");
                    $(".rdobox:last").addClass("checked");
                    $(".rdobox:last").children(".check-image").css("background", "url(../img/input-checked.png)");

                    $(".rdobox:first").removeClass("checked");
                    $(".rdobox:first").addClass("unchecked");
                    $(".rdobox:first").children(".check-image").css("background", "url(../img/input-unchecked.png)");
                }
            })

            $("#form0").attr("action", RequestEditUrl);
            $('#myModal').modal();
        });

        $("#btn_delete").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                var d = dialog({
                    fixed: true,
                    content: "请选择有效数据",
                    padding: 30

                });
                d.show();
                //关闭提示模态框
                setTimeout(function () {
                    d.close().remove();
                }, 2000);
                return;
            }


            var d = dialog({
                title: '系统提示',
                padding: 30,
                content: '确定要删除这些数据?',
                okValue: '确定',
                ok: function () {
                    this.title('提交中…');
                    var ids = "";
                    $(arrselections).each(function () {
                        ids += this.id + ",";
                    })
                    if (ids.length > 1)//去掉最后一个,
                    {
                        ids = ids.substring(0, ids.length - 1);
                    }

                    $.post(RequestDelUrl, { ids: ids }, function (data) {
                        var e = dialog({
                            padding: 30,
                            content: data.msg
                        });
                        //显示模态框
                        e.show();
                        //先关闭主窗体
                        $('#myModal').modal('hide')
                        //刷新数据
                        $('#tb_departments').bootstrapTable('refresh', { url: RequestListUrl });
                        //关闭提示模态框
                        setTimeout(function () {
                            e.close().remove();
                        }, 2000);
                    });
                },
                cancelValue: '取消',
                cancel: function () { }
            });
            d.show();



        });


        $("#btn_query").click(function () {
            $("#tb_departments").bootstrapTable('refresh');
        });
    };

    return oInit;
};
var subDate = new Array();
function Exists(arry, str) {
    var res = null;
    $.each(subDate, function (num, item) {
        if (item.id == str) {
            res = item.value;
        }
    }
)
    return res;
}
function SelData(arry, str, opens) {
    var res = null;
    $.each(arry, function (num, item) {
        if (item.id == str) {
            item.opens = opens;//展开还是收拢
            res = num + 1;
        }
    }
)
    return res;
}
//递归移除元素
function DelData(arry, str) {
    for (var i = 0; i < arry.length; i++) {
        if (arry[i] != null && arry[i].parent_ids != null) {
            var p_ids = arry[i].parent_ids.split(",");
            $.each(p_ids, function () {
                if (this == str) {
                    arry.splice(i, 1);
                    return DelData(arry, str);
                }
            })
        }
    }
    return arry;
}


function openFile(t) {
    //加载框
    var loading = dialog({
        content: '<i class="fa fa-spinner fa-spin"></i>玩命加载中...',
    });
    var id = $(t).attr("txt");
    //如果展开就收拢,如果收拢就展开
    if ($(t).attr("class") == "fa fa-folder") {
        //判断是否存在
        var index = Exists(subDate, id);
        var index = null; //这里每次都从后台拿
        var eacheDate = null;
        if (index == null) {
            loading.show();
            $.post("/City/GetCitySubList", { parent_id: id }, function (data) {
                loading.close();
                eacheDate = data;
                var sumDate = $('#tb_departments').bootstrapTable("getData");
                var parent_index = SelData(sumDate, id, true);


                for (var i = data.length; i > 0; i--) {
                    sumDate.splice(parent_index, 0, data[i - 1]);
                }
                $('#tb_departments').bootstrapTable('load', sumDate);
                subDate[$(subDate).length] = { id: id, value: data };
            })
        } else {
            //这个方法重复,是因为前面是异步的ajax
            eacheDate = index;
            var sumDate = $('#tb_departments').bootstrapTable("getData");

            var parent_index = SelData(sumDate, id, true);

            for (var i = eacheDate.length; i > 0; i--) {
                eacheDate[i - 1].opens = false;
                sumDate.splice(parent_index, 0, eacheDate[i - 1]);
            }
            $('#tb_departments').bootstrapTable('load', sumDate);
        }

    } else {
        var eacheDate = null;
        var index = Exists(subDate, id);
        if (index != null) {
            eacheDate = index;
            //移除html要从最后一级开始
            var sumDate = $('#tb_departments').bootstrapTable("getData");
            var parent_index = SelData(sumDate, id, false);
            //sumDate.splice(parent_index, eacheDate.length);
            sumDate = DelData(sumDate, id);
            $('#tb_departments').bootstrapTable('load', sumDate);
        }
    }

    $('#tb_departments').bootstrapTable('resetView');
}