//请求地址(列表)
var RequestListUrl = "/Exam_mark/GetExam_markList";
//请求地址(单条记录)
var RequestUrl = "/Exam_mark/GetExam_mark";
//请求地址(删除记录)
var RequestDelUrl = "/Exam_mark/DelExam_mark";
//请求地址(修改)
var RequestEditUrl = "Exam_mark/EditExam_mark";
//请求控制器(添加)
var RequestAddUrl = "Exam_mark/AddExam_mark"
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
            },
             {
                 title: '学生',
                 align: 'center',//垂直居中
                 valign: 'middle',//水平居中
                 formatter: studentFormatter
             },
             {
                 title: '科目名称',
                 formatter: examFormatter
             },
            {
                title: '成绩',
                align: 'center',//垂直居中
                valign: 'middle',//水平居中
                formatter: markFormatter
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
    function examFormatter(value, row, index) {
        if (row.exam_type != null) {
            return row.exam_type.name;
        } else {
            return "";
        }
    }
    function markFormatter(value, row, index) {

        var res = "";
        if (row.mark < 60)
        {
            res += "<span class='label label-danger'>" + row.mark + "</span>&nbsp;&nbsp;";
        } else if (row.mark < 80)
        {
            res += "<span class='label label-info'>" + row.mark + "</span>&nbsp;&nbsp;";
        } else
        {
            res += "<span class='label label-primary'>"+ row.mark +"</span>&nbsp;&nbsp;";
        }
        return res;
    }
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            student: $("#student").val(),
            exam_type: $("#exam_type").val(),
            start_mark: $("#start_mark").val(),
            end_mark: $("#end_mark").val()
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
                    $('#txt_student').selectpicker('val', data.student.id);
                    $("#txt_student").val(data.student.id);
                }
                if (data.exam_type != null) {
                    $('#txt_exam_type').selectpicker('val', data.exam_type.id);
                    $("#txt_exam_type").val(data.exam_type.id);
                }
                $("#txt_mark").val(data.mark);
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
