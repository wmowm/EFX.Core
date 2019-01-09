//请求地址(列表)
var RequestListUrl = "/Schools/GetSchool_infoList";
//请求地址(单条记录)
var RequestUrl = "/Schools/GetSchool_info";
//请求地址(删除记录)
var RequestDelUrl = "/Schools/DelSchool_info";
//请求地址(修改)
var RequestEditUrl = "Schools/EditSchool_info";
//请求地址(修改2)
var RequestEditUrl2 = "Schools/EditSchool_info2";
//请求控制器(添加)
var RequestAddUrl = "Schools/AddSchool_info"
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
                title: '高校名称', //标题
                //width: 500,//宽度 
                //align: 'center',//垂直居中
                valign: 'middle',//水平居中
                //sortable: true,//是否可排序                
                //formatter: titleFormatter //初始化,自定义内容(厉害了,word哥,有了这个,想怎么搞就怎么搞了)

            },
             {
                 field: 'address',
                 title: '地址'
             },
            {
                field: 'student_count',
                title: '生源数量(人)',
                valign: 'middle',
                align: 'center'
            }
            ,
            {
                field: 'deliver_count',
                title: '输送人数(人)',
                valign: 'middle',
                align: 'center'
            },
            {
                field: 'rebate',
                title: '返点(万/RMB)'
            },
            {
                field: 'dean',
                title: '负责人'
            }, {
                field: 'mobile',
                title: '联系方式'
            },{
                field: 'is_sign',
                title: '是否签约',
                width: 200,//宽度 
                align: 'center',//垂直居中
                valign: 'middle',//水平居中
                formatter: is_signFormatter
            }, {
                field: 'is_awarding',
                title: '是否授牌',
                formatter: is_awardingFormatter
            },
            
            {
                title: '合作模式',
                field: 'mode',//垂直居中
            },
            {
                title: '业务员',
                align: 'center',//垂直居中
                formatter: managerFormatter
            }, {
                title: '访问记录', //标题
                //width: 500,//宽度 
                //align: 'center',//垂直居中
                valign: 'middle',//水平居中
                //sortable: true,//是否可排序                
                formatter: recordFormatter //初始化,自定义内容(厉害了,word哥,有了这个,想怎么搞就怎么搞了)

            }

            ]

        });
    };



    function titleFormatter(value, row, index) {
        //var res = "<button id='btn_edit' type='button' class='btn btn-warning'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span>编辑</button>";
        var res = "<button id=\"\" type=\"button\" class=\"btn btn-warning\" onclick='btn_edit_info(" + row.id + ")' style=\"height:24px;padding: 0px 8px; */\"><span class=\"glyphicon glyphicon-pencil\" aria-hidden=\"true\"></span>编辑</button>";
        return res;
        //if (row.parent_id != 0) {
        //    return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-map-o'></i> " + row.name;
        //} else {
        //    return "<i class='fa fa-folder-open-o'></i> " + row.name;
        //}
    }
    function recordFormatter(value, row, index) {
        //var res = "<button id='btn_edit' type='button' class='btn btn-warning'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span>编辑</button>";
        var res = "<button id=\"\" type=\"button\" class=\"btn btn-primary\" onclick='btn_record(" + row.id + ")' style=\"height:24px;padding: 0px 8px;margin: 0px 8px; */\"><span class=\"fa fa-eye\" aria-hidden=\"true\"></span>查看</button>";
        return res;
    }

    function managerFormatter(value, row, index)
    {
        if (row.manager != null) {
            return row.manager.real_name;
        } else {
            return "";
        }
    }

    function is_signFormatter(value, row, index) {
        var res = "";
        if (row.is_sign == "1") {
            res += "<span class='label label-primary'>已签约</span>&nbsp;&nbsp;";
        }
        else {
            res += "<span class='label label-danger'>未签约</span>&nbsp;&nbsp;";
        }
        return res;
    }


    function is_awardingFormatter(value, row, index) {
        var res = "";
        if (row.is_awarding == "1") {
            res += "<span class='label label-primary'>已授牌</span>&nbsp;&nbsp;";
        }
        else {
            res += "<span class='label label-danger'>未授牌</span>&nbsp;&nbsp;";
        }
        return res;
    }

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            name: $("#name").val(),
            mobile: $("#mobile").val(),
            province: $('#province option:selected').val(),
            city: $('#city option:selected').val()
           
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
                $("#id").val(data.id);
                $("#txt_name").val(data.name);
                $("#txt_address").val(data.address);
                $("#txt_faculty").val(data.faculty);
                $("#txt_dean").val(data.dean);
                $("#txt_mobile").val(data.mobile);
                $("#txt_wx").val(data.wx);
                $("#txt_qq").val(data.qq);
                $("#txt_taddress").val(data.taddress);
                ProviceBind();
                $("#Province").find("option[value='" + data.province_city.id + "']").attr("selected", true);
                CityB(data.province_city.id);
                $("#City").find("option[value='" + data.city_city.id + "']").attr("selected", true);
                $("#txt_remark").val(data.remark);


                $("#txt_student_count").val(data.student_count);
                //先清空
                $("#myModal .rdobox").each(function () {
                    $(this).removeClass("checked");
                    $(this).addClass("unchecked");
                    $(this).children(".check-image").css("background", "url(../img/input-unchecked.png)");
                })
                $("#txt_is_sign").val(data.is_sign);
                if (data.is_sign != 1) {
                    $("#myModal .rdo .rdobox:first").removeClass("unchecked");
                    $("#myModal .rdo .rdobox:first").addClass("checked");
                    $("#myModal .rdo .rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");

                } else {
                    $("#myModal .rdo .rdobox").eq(1).removeClass("unchecked");
                    $("#myModal .rdo .rdobox").eq(1).addClass("checked");
                    $("#myModal .rdo .rdobox").eq(1).children(".check-image").css("background", "url(../img/input-checked.png)");
                }

                $("#txt_is_awarding").val(data.is_awarding);
                if (data.is_awarding != 1) {
                    $("#myModal .rdo1 .rdobox:first").removeClass("unchecked");
                    $("#myModal .rdo1 .rdobox:first").addClass("checked");
                    $("#myModal .rdo1 .rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");

                } else {
                    $("#myModal .rdo1 .rdobox").eq(1).removeClass("unchecked");
                    $("#myModal .rdo1 .rdobox").eq(1).addClass("checked");
                    $("#myModal .rdo1 .rdobox").eq(1).children(".check-image").css("background", "url(../img/input-checked.png)");
                }


                $("#txt_deliver_count").val(data.deliver_count);
                $("#txt_rebate").val(data.rebate);
                $("#txt_mode").val(data.mode);
                if (data.manager != null) {
                    $('#txt_salesman').selectpicker('val', data.manager.id);
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



$(function () {
    //默认绑定省
    ProviceBind();
    proviceBind();
    //绑定事件
    $("#Province").change(function () {
        CityBind();
    })
    $("#province").change(function () {
        cityBind();
    })
})
function ProviceBind() {
    //清空下拉数据
    $("#Province").html("");
    var str = "<option value=0>==请选择===</option>";
    $.ajax({
        type: "POST",
        url: "/Student/GetAddress",
        data: { "parent_id": 0 },
        dataType: "JSON",
        async: false,
        success: function (data) {
            //从服务器获取数据进行绑定
            $.each(data, function (i, item) {
                str += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            //将数据添加到省份这个下拉框里面
            $("#Province").append(str);
        }
    });




}
function proviceBind() {
    //清空下拉数据
    $("#province").html("");
    var str = "<option value=0>==请选择===</option>";
    $.ajax({
        type: "POST",
        url: "/Student/GetAddress",
        data: { "parent_id": 0 },
        dataType: "JSON",
        async: false,
        success: function (data) {
            //从服务器获取数据进行绑定
            $.each(data, function (i, item) {
                str += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            //将数据添加到省份这个下拉框里面
            $("#province").append(str);
        }
    });
}


function CityBind() {
    var provice = $('#Province option:selected').val();
    //判断省份这个下拉框选中的值是否为空
    if (provice == 0 || provice == "==请选择===") {
        $("#City").html("");
        var str = "<option value=0>==请选择===</option>";
        $("#City").append(str);
        return;
    }
    CityB(provice);


}
function cityBind() {
    var provice = $('#province option:selected').val();
    //判断省份这个下拉框选中的值是否为空
    if (provice == "" || provice == "==请选择===") {
        $("#city").html("");
        var str = "<option value=0>==请选择===</option>";
        $("#city").append(str);
        return;
    }
    CityB2(provice);


}
function CityB(provice) {
    $("#City").html("");
    var str = "<option value=0>==请选择===</option>";
    $.ajax({
        type: "POST",
        url: "/Student/GetAddress",
        data: { "parent_id": provice },
        dataType: "JSON",
        async: false,
        success: function (data) {
            //从服务器获取数据进行绑定
            $.each(data, function (i, item) {
                str += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            //将数据添加到省份这个下拉框里面
            $("#City").append(str);
        },
        error: function () { alert("Error1"); }
    });
}
function CityB2(provice) {
    $("#city").html("");
    var str = "<option value=0>==请选择===</option>";
    $.ajax({
        type: "POST",
        url: "/Student/GetAddress",
        data: { "parent_id": provice },
        dataType: "JSON",
        async: false,
        success: function (data) {
            //从服务器获取数据进行绑定
            $.each(data, function (i, item) {
                str += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            //将数据添加到省份这个下拉框里面
            $("#city").append(str);
        },
        error: function () { alert("Error1"); }
    });
}



function btn_record(id)
{
    $("#school").val(id);
    $('#myModal3').modal();
    $("#tb_departments2").bootstrapTable('refresh');
}