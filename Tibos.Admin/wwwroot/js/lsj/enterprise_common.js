//请求地址(列表)
var RequestListUrl = "/Enterprise_Common/GetEnterprise_CommonList";
//请求地址(获取资源)
var RequestUrl = "/Enterprise_Common/GetEnterprise_Common";
//请求地址(删除记录)
var RequestDelUrl = "/Enterprise_Common/DelEnterprise_Common";
//请求地址(修改)
var RequestEditUrl = "Enterprise_Common/EditEnterprise_Common";
//请求控制器(添加)
var RequestAddUrl = "Enterprise_Common/AddEnterprise_Common"


var mycars = new Array();
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
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
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
            rowStyle: function (row, index) {
                //这里有5个取值代表5中颜色['active', 'success', 'info', 'warning', 'danger'];
                var strclass = "";
                if (mycars.indexOf(row.id) >= 0) {
                    strclass = 'success';//还有一个active
                }
                return { classes: strclass }
            },
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 600,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'name',//表字段
                title: '企业名称', //标题
                //width: 500,//宽度 
                //align: 'center',//垂直居中
                valign: 'middle',//水平居中
                //sortable: true,//是否可排序                
                //formatter: titleFormatter //初始化,自定义内容(厉害了,word哥,有了这个,想怎么搞就怎么搞了)

            },
             {
                 field: 'address',
                 title: '办公地址'
             },{
                field: 'job_count',
                title: '招聘人数',
            }
            ,{
                field: 'job_post',
                title: '招聘岗位',
            },{
                field: 'job_language',
                title: '需求语种',
            }, {
                field: 'txt_is_factory',
                title: '有无厂房',
                align: 'center',//垂直居中
                valign: 'middle',//水平居中
                formatter: is_factoryFormatter
            }, {
                field: 'is_home',
                title: '是否包住',
                align: 'center',//垂直居中
                valign: 'middle',//水平居中
                formatter: is_homeFormatter
            }, {
                field: 'status',
                title: '企业类型',
                align: 'center',//垂直居中
                valign: 'middle',//水平居中
                formatter: statusFormatter
            }
             ,
            {
                field: 'remark',
                title: '备注',
                width: 100,//宽度 
                align: 'center',//垂直居中
                valign: 'middle'//水平居中
            }, {
                title: '获取资源',
                align: 'center',//垂直居中
                formatter: recordFormatter
            }]

        });
    };



    function is_factoryFormatter(value, row, index) {
        var res = "";
        if (row.is_factory == "1") {
            res += "<span class='label label-primary'>有厂房</span>&nbsp;&nbsp;";
        }
        else
        {
            res += "<span class='label label-danger'>无厂房</span>&nbsp;&nbsp;";
        }
        return res;
    }
    function is_homeFormatter(value, row, index) {
        var res = "";
        if (row.is_home == "1") {
            res += "<span class='label label-primary'>包住</span>&nbsp;&nbsp;";
        }
        else
        {
            res += "<span class='label label-danger'>不包住</span>&nbsp;&nbsp;";
        }
        return res;
    }
    function statusFormatter(value, row, index) {
        var res = "";
        if (row.status == "5") {
            res += "<span class='label label-danger'>已用人企业</span>&nbsp;&nbsp;";
        }
        else if (row.status == "4") {
            res += "<span class='label label-info'>确定用人企业</span>&nbsp;&nbsp;";
        }
        else if (row.status == "3") {
            res += "<span class='label label-primary'>准客户企业</span>&nbsp;&nbsp;";
        }
        else if (row.status == "2") {
            res += "<span class='label label-warning'>待开发企业</span>&nbsp;&nbsp;";
        }
        else if (row.status == "1") {
            res += "<span class='label label-default'>无效企业</span>&nbsp;&nbsp;";
        }
        return res;
    }


    function enterpriseFormatter(value, row, index)
    {
        if (row.manager != null)
        {
            return row.manager.real_name;
        } else
        {
            return "";
        }
    }


    function titleFormatter(value, row, index) {
        var res = "<button id=\"\" type=\"button\" class=\"btn btn-blue\" onclick='btn_edit_info(" + row.id + ")' style=\"height:24px;padding: 0px 8px; */\"><span class=\"glyphicon glyphicon-plus\" aria-hidden=\"true\"></span>添加</button>";
        res += "<button id=\"\" type=\"button\" class=\"btn btn-primary\" onclick='btn_address(" + row.id + ")' style=\"height:24px;padding: 0px 8px;margin: 0px 8px; */\"><span class=\"fa fa-eye\" aria-hidden=\"true\"></span>查看</button>";
        return res;
        //if (row.parent_id != 0) {
        //    return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-map-o'></i> " + row.name;
        //} else {
        //    return "<i class='fa fa-folder-open-o'></i> " + row.name;
        //}
    }
    function recordFormatter(value, row, index) {
        //var res = "<button id='btn_edit' type='button' class='btn btn-warning'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span>编辑</button>";
        var res = "<button id=\"\" type=\"button\" class=\"btn btn-primary\" onclick='btn_record(" + row.id + ")' style=\"height:24px;padding: 0px 8px;margin: 0px 8px; */\"><span class=\"fa fa-hand-lizard-o\" aria-hidden=\"true\"></span>获取</button>";
        return res;
    }

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            name: $("#name").val(),
            endsalesman: $("#endsalesman").val(),
            is_factory: $("#is_factory").val(),
            is_home: $("#is_home").val(),
            address: $("#address").val(),
            language: $("#language").val()
        };
        return temp;
    };
    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        $("#btn_add").click(function () {

                var arrselections = $("#tb_departments").bootstrapTable('getSelections');
                $(".selectpicker").selectpicker("refresh");
                $('.selectpicker').selectpicker('val', 0);
                $("#myModalLabel").text("新增");
                $("#myModal").find(".form-control").val("");
                $("#form0").attr("action", RequestAddUrl);
                $('#myModal').modal();
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
            $("#myModal").find(".form-control").val("");
            $.post(RequestUrl, { id: arrselections[0].id }, function (data) {


                $(".chkbox").each(function () {
                    if (data.job_language.indexOf($(this).children(".radiobox-content").html()) >= 0) {
                        $(this).prev().prop("checked", "checked");
                        $(this).removeClass("unchecked");
                        $(this).addClass("checked");
                        $(this).children(".check-image").css("background", "url(../img/input-checked.png)");

                    } else {
                        $(this).prev().prop("checked", "");
                        $(this).removeClass("checked");
                        $(this).addClass("unchecked");
                        $(this).children(".check-image").css("background", "url(../img/input-unchecked.png)");
                    }
                })

                //先清空
                $("#myModal .rdobox").each(function () {
                    $(this).removeClass("checked");
                    $(this).addClass("unchecked");
                    $(this).children(".check-image").css("background", "url(../img/input-unchecked.png)");
                })
                $("#txt_is_factory").val(data.is_factory);
                if (data.is_factory != 1) {
                    $("#myModal .rdo .rdobox:first").removeClass("unchecked");
                    $("#myModal .rdo .rdobox:first").addClass("checked");
                    $("#myModal .rdo .rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");

                } else {
                    $("#myModal .rdo .rdobox").eq(1).removeClass("unchecked");
                    $("#myModal .rdo .rdobox").eq(1).addClass("checked");
                    $("#myModal .rdo .rdobox").eq(1).children(".check-image").css("background", "url(../img/input-checked.png)");
                }

                $("#txt_is_home").val(data.is_home);
                if (data.is_home != 1) {
                    $("#myModal .rdo1 .rdobox:first").removeClass("unchecked");
                    $("#myModal .rdo1 .rdobox:first").addClass("checked");
                    $("#myModal .rdo1 .rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");

                } else {
                    $("#myModal .rdo1 .rdobox").eq(1).removeClass("unchecked");
                    $("#myModal .rdo1 .rdobox").eq(1).addClass("checked");
                    $("#myModal .rdo1 .rdobox").eq(1).children(".check-image").css("background", "url(../img/input-checked.png)");
                }

                $("#id").val(data.id);
                $("#txt_name").val(data.name);
                $("#txt_dean").val(data.dean);
                $("#txt_job_name").val(data.job_name);
                $("#txt_mobile").val(data.mobile);
                $("#txt_job_count").val(data.job_count);
                $("#txt_job_post").val(data.job_post);
                $("#txt_job_language").val(data.job_language);
                $("#txt_people_count").val(data.people_count);
                $("#txt_amazon_count").val(data.amazon_count);
                $("#txt_product").val(data.product);
                $("#txt_duration").val(data.duration);
                $("#txt_address").val(data.address);
                $("#txt_remark").val(data.remark);
                if (data.manager != null) {
                    $('#txt_endsalesman').selectpicker('val', data.manager.id);
                }
                if (data.status != 0) {
                    $('#txt_enterprisestatus').selectpicker('val', data.status);
                }
                $("#form0").attr("action", RequestEditUrl);
            }, "json");
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

        $('#myModal').on('hide.bs.modal', function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            if (arrselections.length > 0) {
                mycars[mycars.length] = arrselections[0].id;
                $('#tb_departments').bootstrapTable('refresh');
            }
        })

    };

    return oInit;
};



function btn_edit_info(id) {
    var arrselections = $("#tb_departments").bootstrapTable('getSelections');
    $("#myModalLabel2").text("新增");
    $("#myModal2").find(".form-control").val("");
    $("#form1").attr("action", RequestAddUrl2);
    $("#id2").val(id);
    $('#myModal2').modal();
}

function btn_record(id) {
    var d = dialog({
        title: '系统提示',
        padding: 30,
        content: '确定要获取这些数据?',
        okValue: '确定',
        ok: function () {
            this.title('提交中…');
            $.post(RequestUrl, { id: id }, function (data) {
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
}

function btn_address(id) {
    $("#eid").val(id);
    $('#myModal4').modal();
    $("#tb_departments4").bootstrapTable('refresh');
}


