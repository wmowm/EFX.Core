//请求地址(列表)
var RequestListUrl = "/Resume/GetResumeList";
//请求地址(单条记录)
var RequestUrl = "/Resume/GetResume";
//请求地址(删除记录)
var RequestDelUrl = "/Resume/DelResume";
//请求地址(修改)
var RequestEditUrl = "Resume/EditResume";
//请求控制器(添加)
var RequestAddUrl = "Resume/AddResume"


//请求地址(添加2)
var RequestAddUrl2 = "Resume_record/AddResume_record";

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
            pageNumber:1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            
            pageList: [10, 25, 50, 180000],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            silent: true,  //刷新事件必须设置
            formatLoadingMessage: function () {
                return "<div class='sk-spinner sk-spinner-wave'>"+
                    "<div class='sk-rect1'></div>"+
                    "<div class='sk-rect2'></div>" +
                    "<div class='sk-rect3'></div>"+
                    "<div class='sk-rect4'></div>"+
                    "<div class='sk-rect5'></div>"+
                    "</div>";
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
                field: 'name',
                title: '简历名称'
            }, {
                field: 'user_name',
                title: '联系人'
            }, {
                field: 'dean',
                title: '岗位'
            }, {
                field: 'mobile',
                title: '工作年限'
            }, {
                field: 'language',
                title: '语种'
            }, {
                field: 'address',
                title: '所在地'
            },{
                field: 'terrace',
                title: '平台'
            }, {
                field: 'add_time',
                title: '添加时间'
            },{
            title: '访问记录',
            align: 'center',//垂直居中
            formatter: recordFormatter
            },
            {
                title: '预览',
                valign: 'middle',
                align: 'center',
                formatter: fileFormatter

            }]
       
        });
    };

    function fileFormatter(value, row, index) {
       
        return "<a onclick=\"showInfo('"+filepath + row.url+"','"+ row.name +"','999')\">查看</a>";
    }


    function recordFormatter(value, row, index) {
        var res = "<button id=\"\" type=\"button\" class=\"btn btn-blue\" onclick='btn_edit_info(" + row.id + ")' style=\"height:24px;padding: 0px 8px; */\"><span class=\"glyphicon glyphicon-plus\" aria-hidden=\"true\"></span>添加</button>";
            res += "<button id=\"\" type=\"button\" class=\"btn btn-primary\" onclick='btn_record(" + row.id + ")' style=\"height:24px;padding: 0px 8px;margin: 0px 8px; */\"><span class=\"fa fa-eye\" aria-hidden=\"true\"></span>查看</button>";
        return res;
    }

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            name: $("#name").val(),
            user_name: $("#user_name").val(),
            mobile: $("#mobile").val(),
            language: $("#language").val(),
            terrace: $("#terrace").val()
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
            $("#myModalLabel").text("新增");
            $("#myModal").find(".form-control").val("");
            $("#form0").attr("action", RequestAddUrl);
            //$('#myModal').modal()
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
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
                $("#txt_user_name").val(data.user_name);
                $("#txt_img_url").val(data.url);
                $("#txt_name").val(data.name);
                $("#txt_dean").val(data.dean);
                $("#txt_mobile").val(data.mobile);
                $("#txt_address").val(data.address);

                if (data.language != null) {
                    $("#chk1").find(".chkbox").each(function () {
                        if (data.language.indexOf($(this).children(".radiobox-content").html()) >= 0) {
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
                }
                if (data.terrace != null) {
                    $("#chk2").find(".chkbox").each(function () {
                        if (data.terrace.indexOf($(this).children(".radiobox-content").html()) >= 0) {
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
    };

    return oInit;
};



function btn_record(id) {
    $("#resume").val(id);
    $('#myModal3').modal();
    $("#tb_departments2").bootstrapTable('refresh');
}


function openBrowse() {
    var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (ie) {
        document.getElementById("fileImage").click();
        document.getElementById("txt_img_url").value = document.getElementById("fileImage").value;
    } else {
        var a = document.createEvent("MouseEvents");//FF的处理 
        a.initEvent("click", true, true);
        document.getElementById("fileImage").dispatchEvent(a);
    }
}

function previewImage(file) {
    txt_img_url.value = file.value;
}



function btn_edit_info(id) {
    var arrselections = $("#tb_departments").bootstrapTable('getSelections');
    $("#myModalLabel2").text("新增");
    $("#myModal2").find(".form-control").val("");
    $("#form1").attr("action", RequestAddUrl2);
    $('#txt_resume').val(id);
    $('#myModal2').modal();
}