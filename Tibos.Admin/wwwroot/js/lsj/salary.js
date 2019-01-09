//请求地址(列表)
var RequestListUrl = "/Salary/GetSalary_infoList";
//请求地址(单条记录)
var RequestUrl = "/Salary/GetSalary_info";
//请求地址(删除记录)
var RequestDelUrl = "/Salary/DelSalary_info";
//请求地址(修改)
var RequestEditUrl = "Salary/EditSalary_info";
//请求控制器(添加)
var RequestAddUrl = "Salary/AddSalary_info"

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
                title: '学生名称', //标题
                //width: 500,//宽度 
                //align: 'center',//垂直居中
                valign: 'middle',//水平居中
                //sortable: true,//是否可排序                
                formatter: studentFormatter //初始化,自定义内容(厉害了,word哥,有了这个,想怎么搞就怎么搞了)

            }, {
                title: '企业名称', //标题
                align: 'center',//垂直居中
                valign: 'middle',
                formatter: enterpriseFormatter

            },
             {
                 title: '应出勤天数',
                 formatter: intendFormatter
             },
            {
                title: '实际出勤天数',
                formatter: intendFormatter
            }, {
                field: 'e_time',
                width: 200,//宽度 
                title: '企业转账时间'
            },{
                title: '将扣金额',
                width: 200,//宽度 
                align: 'center',//垂直居中
                valign: 'middle',
                formatter: awardFormatter
            }, {
                title: '企业实转金额',
                align: 'center',//垂直居中
                valign: 'middle',
                formatter: reality_moneyFormatter
            }, {
                field: 'agent_time',
                title: '代发时间',
                width: 250,//宽度 
                align: 'center',//垂直居中
                valign: 'middle'
            }, {
                field: 'agent_money',
                title: '代发底薪',
                align: 'center',//垂直居中
                valign: 'middle',
                formatter: agent_moneyFormatter
            }, {
                title: '房补金额',
                align: 'center',//垂直居中
                valign: 'middle',
                formatter: home_moneyFormatter
            },{
                title: '合计薪资',
                align: 'center',//垂直居中
                valign: 'middle',
                formatter: sum_moneyFormatter
            }, {
                field: 'info',
                title: '到帐信息',
                width: 200,//宽度 
                align: 'center',//垂直居中
                valign: 'middle',//水平居中
            }, {
                field: 'remark',
                title: '备注',
                width: 200,//宽度 
                align: 'center',//垂直居中
                valign: 'middle'//水平居中
            }]

        });
    };


    function studentFormatter(value, row, index) {
        if (row.student != null) {
            if (row.student.avatar != "") {
                var res = "<a class='smailpic' rel='popover' style='width:100%; height:18px;border:0px;overflow:hidden;' data-content=''  onmouseenter=nameenter(this,'" + imagespath + row.student.avatar + "','" + row.student.id + "','" + row.student.mobile + "','" + row.student.wx + "','" + row.student.real_name + "','" + row.student.mode + "','" + row.student.sex + "') onmouseleave=nameleave(this)>" +
                         row.student.real_name +
                         "</a>";
                return res;
            } else {
                return "";
            }
            return row.student.real_name;
        } else {
            return "";
        }
    }

    function enterpriseFormatter(value, row, index) {
        if (row.enterprise != null)
        {
            return row.enterprise.name;
        }
        return "";
    }

    function intendFormatter(value, row, index) {
        return row.intend.toFixed(1);
    }
    function realityFormatter(value, row, index) {
        return row.reality.toFixed(1);
    }
    function awardFormatter(value, row, index) {
        return row.award.toFixed(2);
    }
    function reality_moneyFormatter(value, row, index) {
        return row.reality_money.toFixed(2);
    }
    function home_moneyFormatter(value, row, index) {
        return row.home_money.toFixed(2);
    }
    function reality_moneyFormatter(value, row, index) {
        return row.reality_money.toFixed(2);
    }
    function sum_moneyFormatter(value, row, index) {
        return row.sum_money.toFixed(2);
    }
    function agent_moneyFormatter(value, row, index) {
        return row.agent_money.toFixed(2);
    }


    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            student: $("#student").val(),
            enterprise: $("#enterprise").val(),
            start_time: $("#start_time").val(),
            end_time: $("#end_time").val(),
            start_time2: $("#start_time2").val(),
            end_time2: $("#end_time2").val(),
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
                if (data.student != null) {
                    $('#txt_student_id').selectpicker('val', data.student.id);
                    $("#txt_student_id").val(data.student.id);
                }
                if (data.enterprise != null) {
                    $('#txt_enterprise_id').selectpicker('val', data.enterprise.id);
                    $("#txt_enterprise_id").val(data.enterprise.id);
                }

                $("#txt_intend").val(data.intend);
                $("#txt_reality").val(data.reality);
                $("#txt_e_time").val(data.e_time);
                $("#txt_award").val(data.award);
                $("#txt_reality_money").val(data.reality_money);
                $("#txt_agent_time").val(data.agent_time);
                $("#txt_agent_money").val(data.agent_money);
                $("#txt_home_money").val(data.home_money);
                $("#txt_remark").val(data.remark);
                $("#txt_sum_money").val(data.sum_money);
                $("#txt_info").val(data.info);
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

