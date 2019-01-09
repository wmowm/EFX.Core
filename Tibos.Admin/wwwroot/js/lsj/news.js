//请求地址(列表)
var RequestListUrl = "/News/GetNewsList";
//请求地址(单条记录)
var RequestUrl = "/News/GetNews";
//请求地址(删除记录)
var RequestDelUrl = "/News/DelNews";
//请求地址(修改)
var RequestEditUrl = "News/EditNews";
//请求控制器(添加)
var RequestAddUrl = "News/AddNews"

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
            //rowStyle: function (row, index) {
            //    //这里有5个取值代表5中颜色['active', 'success', 'info', 'warning', 'danger'];
            //    var strclass = "";
            //    if (row.is_lock == "√") {
            //        strclass = 'success';//还有一个active
            //    }
            //    else if (row.is_lock == "×") {
            //        strclass = 'danger';
            //    }
            //    else {
            //        return {};
            //    }
            //    return { classes: strclass }
            //},
            columns: [{
                checkbox: true
            },
            {
                field: 'img_url',
                title: '封面图片',
                width: 150,
                heith:120,
                valign: 'middle',
                align: 'center',
                formatter: img_urlFormatter

            },
            {
                field: 'news_type.title',
                title: '资讯类型',
                width:100
                
            },
            {
                field: 'title',//表字段
                title: '标题', //标题
                valign: 'middle',
                align: 'center'
            },
            
            {
                field: 'tags',
                title: '标签',
                valign: 'middle',
                align: 'center'
            },
            {
                field: 'author',
                title: '作者',
                valign: 'middle',
                align: 'center'
            },
            {
                field: 'sort_id',
                title: '排序',
                valign: 'middle',
                align: 'center'
            }
            ,
            {
                field: 'is_lock',
                title: '显示状态',
                valign: 'middle',
                align: 'center'
            },
            {
                field: 'is_red',
                title: '推荐状态',
                valign: 'middle',
                align: 'center',
                formatter: is_redFormatter
            },
            {
                field: 'start_time',
                title: '发布时间',
                valign: 'middle',
                align: 'center'
            },
            {
                field: 'manager.user_name',
                title: '发布人',
                valign: 'middle',
                align: 'center'
            }, {
                field: 'status',
                title: '审核状态',
                valign: 'middle',
                align: 'center',
                formatter: statusFormatter
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
    function img_urlFormatter(value, row, index) {
        if (row.img_url != "") {
            var res = "<a class='smailpic' rel='popover' style='width:100%; height:18px;border:0px;overflow:hidden;' data-content=''  onmouseenter=imgenter(this,'" + imagespath + row.img_url + "') onmouseleave=imgleave(this)>" +
                     "<img id='img" + row.id + "' style='border:0px;width:auto; height:auto;width:18px;heigh:18px' src='" + imagespath + row.img_url + "'>" +
                     "</a>";
            return res;
        } else
        {
            return "";
        }
    }
    function statusFormatter(value, row, index)
    {
        if (row.status == "1") {
            return "<i class=\"fa fa-circle text-navy\" title=\"成功\"></i>"

        } else if (row.status == "2") {
            return "<i class=\"fa fa-circle text-danger\" title=\"失败\"></i>"
        } else
        {
            return "<i class=\"fa fa-circle text-warning\" title=\"未审核\"></i>"
        }
    }
    function is_redFormatter(value, row, index)
    {
        var res = "";
        if(row.is_top=="1")
        {
            res += "<span class='label label-primary'>置顶</span>&nbsp;&nbsp;";
        }
        if (row.is_red == "1") {
            res += "<span class='label label-warning'>推荐</span>&nbsp;&nbsp;";
        }
        if (row.is_hot == "1") {
            res += "<span class='label label-danger'>热门</span>&nbsp;&nbsp;";
        }
        if (row.is_msg == "1") {
            res += "<span class='label label-info'>允许评论</span>&nbsp;&nbsp;";
        }
        return res;
    }

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            user_name: $("#user_name").val(),
            mobile: $("#mobile").val()
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
            editor.html("");
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
                $('.selectpicker').selectpicker('val', data.news_type.id);
                if (data.is_lock == "√") {
                    $("#myModal .rdobox:first").removeClass("unchecked");
                    $("#myModal .rdobox:first").addClass("checked");
                    $("#myModal .rdobox:first").children(".check-image").css("background", "url(../img/input-checked.png)");


                    $("#myModal .rdobox:last").removeClass("checked");
                    $("#myModal .rdobox:last").addClass("unchecked");
                    $("#myModal .rdobox:last").children(".check-image").css("background", "url(../img/input-unchecked.png)");


                } else {
                    $("#myModal .rdobox:last").removeClass("unchecked");
                    $("#myModal .rdobox:last").addClass("checked");
                    $("#myModal .rdobox:last").children(".check-image").css("background", "url(../img/input-checked.png)");

                    $("#myModal .rdobox:first").removeClass("checked");
                    $("#myModal .rdobox:first").addClass("unchecked");
                    $("#myModal .rdobox:first").children(".check-image").css("background", "url(../img/input-unchecked.png)");
                }

                $(".chkbox").each(function () {
                    var ac = $(this).children(".radiobox-content").html();
                    if (ac == "推荐")
                    {
                        if (data.is_red == "1") {
                            $(this).prev().prop("checked", "checked");
                            $(this).removeClass("unchecked");
                            $(this).addClass("checked");
                            $(this).children(".check-image").css("background", "url(../img/input-checked.png)");
                        } else
                        {
                            $(this).prev().prop("checked", "");
                            $(this).removeClass("checked");
                            $(this).addClass("unchecked");
                            $(this).children(".check-image").css("background", "url(../img/input-unchecked.png)");
                        }
                    } else if (ac == "置顶")
                    {
                        if (data.is_top == "1") {
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
                    } else if (ac == "热门") {
                        if (data.is_hot == "1") {
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

                    } else if (ac == "允许评论") {
                        if (data.is_msg == "1") {
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
                    }
                })

                $("#txt_title").val(data.title);
                if (data.img_url != "") {
                    $("#txt_img_url").val(data.img_url);
                    $("#imghead").attr("src", imagespath + data.img_url);
                } else
                {
                    $("#txt_img_url").val("news/null.jpg");
                    $("#imghead").attr("src", imagespath + "news/null.jpg");
                }
                $("#txt_sort_id").val(data.sort_id);
                $("#txt_click").val(data.click);
                $("#start_time").val(data.start_time);
                $("#txt_source").val(data.source);
                $("#txt_manager").val(data.manager.user_name);
                $("#txt_summary").val(data.summary);
                editor.html(data.content);
                $("#txtContent").val(data.content);
                $("#txt_seo_title").val(data.seo_title);
                $("#txt_seo_keywords").val(data.seo_keywords);
                $("#txt_seo_description").val(data.seo_description);
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
        $("#btn_exam").click(function ()
        {
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


function imgenter(t,src) {

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

function imgleave(t)
{
    var _this = t;
    setTimeout(function () {
        if (!$(".popover:hover").length) {
            $(_this).popover("hide")
        }
    }, 100);
}