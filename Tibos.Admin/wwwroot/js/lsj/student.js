//请求地址(列表)
var RequestListUrl = "/Student/GetStudentList";
//请求地址(单条记录)
var RequestUrl = "/Student/GetStudent";
//请求地址(删除记录)
var RequestDelUrl = "/Student/DelStudent";
//请求地址(修改)
var RequestEditUrl = "Student/EditStudent";
//请求控制器(添加)
var RequestAddUrl = "Student/AddStudent"

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
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 600,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            rowStyle: function (row, index) {
                //这里有5个取值代表5中颜色['active', 'success', 'info', 'warning', 'danger'];
                var strclass = "";
                if (mycars.indexOf(row.id) >= 0) {
                    strclass = 'success';//还有一个active
                }
                return { classes: strclass }
            },
            columns: [{
                checkbox: true
            },
            {
                field: 'avatar',
                title: '身份证图片',
                width: 50,
                valign: 'middle',
                align: 'center',
                formatter: img_urlFormatter

            },
            {
                field: 'real_name',
                title: '姓名',
                width: 100,
                formatter: nameFormatter
            },
            {
                field: 'card',//表字段
                title: '身份证号', //标题
                valign: 'middle',
                align: 'center'
            },

            {
                field: 'mobile',
                title: '手机号',
                valign: 'middle',
                align: 'center'
            }, {
                field: 'birthday',
                title: '出生日期',
                valign: 'middle',
                align: 'center'
            },{
                title: '所属高校',
                valign: 'middle',
                align: 'center',
                formatter: schoolFormatter
            },
            {
                field: 'nation',
                title: '民族',
                valign: 'middle',
                align: 'center'
            },
            {
                //field: 'province_city.name',
                title: '省',
                valign: 'middle',
                align: 'center',
                formatter: provinceFormatter
            }
            ,
            {
                //field: 'city_city.name',
                title: '市',
                valign: 'middle',
                align: 'center',
                formatter: cityFormatter
            },
            {
                //field: 'classes.name',
                title: '班级',
                valign: 'middle',
                align: 'center',
                formatter:classesFormatter
            },
            {
                //field: 'manager.real_name',
                title: '业务员',
                valign: 'middle',
                align: 'center',
                formatter:managerFormatter
            },
            {
                field: 'add_time',
                title: '入学日期',
                valign: 'middle',
                align: 'center'
            },
            {
                field: 'status',
                title: '状态',
                valign: 'middle',
                align: 'center',
                formatter:statusFormatter
            }

            ]

        });
    };


    function titleFormatter(value, row, index) {
        if (row.parent_id != 0) {
            return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-map-o'></i> " + row.title;
        } else {
            return "<i class='fa fa-folder-open-o'></i> " + row.title;
        }
    }
    function schoolFormatter(value, row, index) {
        var res = "";
        if (row.school != null)
        {
            res = row.school.name;
        }
        if (row.faculty != null)
        {
            res += "(" + row.faculty.name + ")";
        }
        return res;
    }
    function img_urlFormatter(value, row, index) {
        if (row.avatar != "") {
            var res = "<a class='smailpic' rel='popover' style='width:100%; height:18px;border:0px;overflow:hidden;' data-content=''  onmouseenter=imgenter(this,'" + imagespath + row.avatar + "') onmouseleave=imgleave(this)>" +
                     "<img id='img" + row.id + "' style='border:0px;width:auto; height:auto;width:18px;heigh:18px' src='" + imagespath + row.avatar + "'>" +
                     "</a>";
            return res;
        } else {
            return "";
        }
    }
    function nameFormatter(value, row, index) {
        if (row.avatar != "") {
            var res = "<a class='smailpic' rel='popover' style='width:100%; height:18px;border:0px;overflow:hidden;' data-content=''  onmouseenter=nameenter(this,'" + imagespath + row.avatar + "','" + row.id + "','" + row.mobile + "','" + row.wx + "','" + row.real_name + "','" + row.mode + "','" + row.sex + "') onmouseleave=nameleave(this)>" +
                     row.real_name +
                     "</a>";
            return res;
        } else {
            return "";
        }
    }
    function statusFormatter(value, row, index) {
        var res = "";
        if (row.status == 1) {
            res += "<span class='label label-danger'>未缴费</span>&nbsp;&nbsp;";
        }
        if (row.status == 2) {
            res += "<span class='label label-warning'>已缴费</span>&nbsp;&nbsp;";
        }
        if (row.status == 3) {
            res += "<span class='label label-primary'>学习期</span>&nbsp;&nbsp;";
        }
        if (row.status == 4) {
            res += "<span class='label label-info'>就业期</span>&nbsp;&nbsp;";
        }
        if (row.status == 5) {
            res += "<span class='label label-default'>其它</span>&nbsp;&nbsp;";
        }
        return res;
    }
    function classesFormatter(value, row, index)
    {
        if (row.classes == null) {
            return "-";
        } else
        {
            return row.classes.name;
        }
    }
    function managerFormatter(value, row, index) {
        if (row.manager == null) {
            return "-";
        } else {
            return row.manager.real_name;
        }
    }


    function provinceFormatter(value, row, index) {
        if (row.province_city == null) {
            return "-";
        } else {
            return row.province_city.name;
        }
    }

    function cityFormatter(value, row, index) {
        if (row.city_city == null) {
            return "-";
        } else {
            return row.city_city.name;
        }
    }

    //得到查询的参数
    oTableInit.queryParams = function (params) { 
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            real_name: $("#real_name").val(),
            mobile: $("#mobile").val(),
            status: $("#status").val(),
            classes: $("#classes").val(),
            province: $('#province option:selected').val(),
            city: $('#city option:selected').val(),
            start_time: $("#start_time").val(),
            end_time: $("#end_time").val(),
            start_birthday: $("#start_birthday").val(),
            end_birthday: $("#end_birthday").val(),
            salesman: $("#salesman").val()
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
            //初始化
            $("#myModal").find(".form-control").val("");
            $("#form0").attr("action", RequestAddUrl);
            $("#txtContent").val("");
            $("#txt_img_url").val("");
            $("#imghead").attr("src", "");

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
            $("#myModal").find(".form-control").val("");
            $.post(RequestUrl, { id: arrselections[0].id }, function (data) {
                $("#id").val(data.id);
                //先清空
                $("#myModal .rdobox").each(function(){
                    $(this).removeClass("checked");
                    $(this).addClass("unchecked");
                    $(this).children(".check-image").css("background", "url(../img/input-unchecked.png)");
                })
                $("#txt_status").val(data.status);
                if (data.status == 1) {
                    $("#myModal .rdobox:first").removeClass("unchecked");
                    $("#myModal .rdobox:first").addClass("checked");
                    $("#myModal .rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");

                } 
                if (data.status == 2) {
                    $("#myModal .rdobox").eq(1).removeClass("unchecked");
                    $("#myModal .rdobox").eq(1).addClass("checked");
                    $("#myModal .rdobox").eq(1).children(".check-image").css("background", "url(../img/input-checked.png)");

                } 
                if (data.status == 3) {
                    $("#myModal .rdobox").eq(2).removeClass("unchecked");
                    $("#myModal .rdobox").eq(2).addClass("checked");
                    $("#myModal .rdobox").eq(2).children(".check-image").css("background", "url(../img/input-checked.png)");

                } 
                if (data.status == 4) {
                    $("#myModal .rdobox").eq(3).removeClass("unchecked");
                    $("#myModal .rdobox").eq(3).addClass("checked");
                    $("#myModal .rdobox").eq(3).children(".check-image").css("background", "url(../img/input-checked.png)");

                } 
                if (data.status == 5) {
                    $("#myModal .rdobox").eq(4).removeClass("unchecked");
                    $("#myModal .rdobox").eq(4).addClass("checked");
                    $("#myModal .rdobox").eq(4).children(".check-image").css("background", "url(../img/input-checked.png)");

                } 

            if (data.avatar != "") {
                $("#txt_img_url").val(data.avatar);
                $("#imghead").attr("src", imagespath + data.avatar);
            } else {
                $("#txt_img_url").val("news/null.jpg");
                $("#imghead").attr("src", imagespath + "news/null.jpg");
            }

            $("#txt_real_name").val(data.real_name);
            $("#txt_card").val(data.card);
            $("#txt_mobile").val(data.mobile);
            $("#txt_birthday").val(data.birthday);
            $("#txt_nation").val(data.nation);
            $("#txt_wx").val(data.wx);
            $("#txt_mode").val(data.mode);
            ProviceBind();
            $("#Province").find("option[value='" + data.province_city.id + "']").attr("selected", true);
            CityB(data.province_city.id);
            $('#City').selectpicker('val', data.city_city.id);

            $("#txt_address").val(data.address);
            if (data.manager != null) {
                $('#txt_salesman').selectpicker('val', data.manager.id);
            }
            if (data.classes != null) {
                $('#txt_classes').selectpicker('val', data.classes.id);
            }
            if (data.school != null) {
                faculty(data.school.id);
                $('#txt_school').selectpicker('val', data.school.id);
            }
            if (data.faculty != null) {
                $("#txt_faculty").find("option[value='" + data.faculty.id + "']").attr("selected", true);
            }
            $("#txt_grade").val(data.grade);
            $("#txt_urgent").val(data.urgent);
            $("#txt_urgent_mobile").val(data.urgent_mobile);

            $("#add_time").val(data.add_time);
            $("#txt_remark").val(data.remark);
            $("#txt_will").val(data.will);
            if (data.sex != null) {
                $('#txt_sex').selectpicker('val', data.sex);
            }
            $("#txt_language_level").val(data.language_level);
            $("#txt_specialty").val(data.specialty);
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
        $("#btn_exam").click(function () {
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
            var ids = "";
            $(arrselections).each(function () {
                ids += this.id + ",";
            })
            if (ids.length > 1)//去掉最后一个,
            {
                ids = ids.substring(0, ids.length - 1);
            }
            $("#ids").val(ids);
            $('#myExam').modal({ backdrop: 'static', keyboard: false });
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

//图片上传预览    IE是用了滤镜。
function previewImage(file) {
    txt_img_url.value = file.value;
    var MAXWIDTH = 120;
    var MAXHEIGHT = 120;
    var div = document.getElementById('preview');
    if (file.files && file.files[0]) {
        //div.innerHTML = '<img id=imghead>';
        var img = document.getElementById('imghead');
        //img.onload = function () {
        //    var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
        //    img.width = rect.width;
        //    img.height = rect.height;
        //    img.style.marginLeft = rect.left+'px';
        //    img.style.marginTop = rect.top + 'px';
        //}
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);

    }
    else //兼容IE
    {
        var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
        file.select();
        var src = document.selection.createRange().text;
        div.innerHTML = '<img id=imghead>';
        var img = document.getElementById('imghead');
        img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
        var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
        status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
        div.innerHTML = "<div id=divhead style='width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;" + sFilter + src + "\"'></div>";
    }
}
function clacImgZoomParam(maxWidth, maxHeight, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > maxWidth || height > maxHeight) {
        rateWidth = width / maxWidth;
        rateHeight = height / maxHeight;

        if (rateWidth > rateHeight) {
            param.width = maxWidth;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = maxHeight;
        }
    }

    param.left = Math.round((maxWidth - param.width) / 2);
    param.top = Math.round((maxHeight - param.height) / 2);
    return param;
}


var editor;
$(function () {
    //富文本编辑器
    //初始化编辑器
    editor = KindEditor.create('.editor', {
        width: '100%',
        height: '350px',
        resizeType: 1,
        uploadJson: '/news/UploadImage',
        fileManagerJson: '/news/ProcessRequest',
        allowFileManager: true,
        afterBlur: function () { this.sync(); }
    });
    var editorMini = KindEditor.create('.editor-mini', {
        width: '100%',
        height: '250px',
        resizeType: 1,
        uploadJson: '/news/UploadImage',
        items: [
            'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
            'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
            'insertunorderedlist', '|', 'emoticons', 'image', 'link']
    });
});


function imgenter(t, src) {

    $(t).popover({
        trigger: 'manual',
        placement: 'right',
        title: '',
        html: 'true',
        content: "<img style='border:0px;width:auto; height:auto;width:120px;heigh:120px' src='" + src + "' >",
        animation: false
    })

    $(t).popover("show");
    $(t).siblings(".popover").on("mouseleave", function () {
        $(t).popover('hide');
    });
}

function imgleave(t) {
    var _this = t;
    setTimeout(function () {
        if (!$(".popover:hover").length) {
            $(_this).popover("hide")
        }
    }, 100);
}



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


    //绑定事件
    $("#txt_school").change(function () {
        var school = $('#txt_school option:selected').val();
        //判断省份这个下拉框选中的值是否为空
        if (school == 0) {
            $("#txt_faculty").html("");
            var str = "<option value=0>==请选择===</option>";
            $("#txt_faculty").append(str);
            return;
        }
        faculty(school);
    })

})
function ProviceBind() {
    //清空下拉数据
    $("#Province").html("");
    var str = "<option value=0>==请选择===</option>";
    $.ajax({
        type: "POST",
        url: "/Student/GetAddress",
        data: { "parent_id": 0},
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
function proviceBind()
{
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
    if (provice == "" || provice == "==请选择===") {
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

function CityB(provice)
{
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
            $('.selectpicker').selectpicker('refresh');
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


function faculty(school) {
    $("#txt_faculty").html("");
    var str = "<option value=0>==请选择===</option>";
    $.ajax({
        type: "POST",
        url: "/Student/GetFaculty",
        data: { "id": school },
        dataType: "JSON",
        async: false,
        success: function (data) {
            //从服务器获取数据进行绑定
            $.each(data, function (i, item) {
                str += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            $("#txt_faculty").append(str);
        },
        error: function () { alert("Error1"); }
    });
}