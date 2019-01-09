$.loading = function (bool, text) {
    var $loadingpage = parent.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", (parent.$('body').width() - $loadingtext.width()) / 2 + 50);
    $loadingtext.css("top", (parent.$('body').height() - $loadingtext.height()) / 2);
}

$.dialog = function (msg, type) {
    var content = '';
    if (type == 0) {
        content = '<button class="btn btn-success btn-circle btn-lg" type="button"><i class="fa fa-check"></i></button><span class="dialog-content">' + msg + '</span>'
    } else {
        content = '<button class="btn btn-danger btn-circle btn-lg" type="button"><i class="fa fa-times"></i></button><span class="dialog-content">' + msg + '</span>'
    }
    var d = dialog({
        fixed: true,
        content: content,
        padding: 20

    });
    d.show();
    //关闭提示模态框
    setTimeout(function () {
        d.close().remove();
    }, 2000);
}


function nameenter(t, src, id, mobile, wx, name, mode, sex) {
    var img = "../img/man.png";
    if (sex == 2) {
        img = "../img/woman.png";
    }
    var res = "<div align='center'>";
    res += "<img src='" + img + "' style='border-radius: 50%;width:48px;height:48px' />"
    res += "<p><i class='fa fa-phone'></i>" + mobile + "</p>";
    res += "<p><i class='fa fa-weixin'></i>" + wx + "</p>";
    if (mode != "null") {
        res += "<p>模式:" + mode + "</p>";
    }
    res += "<a onclick=\"showInfo('/Student/GetStudentInfo/" + id + "','" + name + "','" + id + "')\">查看更多</a>";
    res += "</div>";

    $(t).popover({
        trigger: 'manual',
        placement: 'top',
        title: '',
        html: 'true',
        content: res,
        animation: false
    })

    $(t).popover("show");
    $(t).siblings(".popover").on("mouseleave", function () {
        $(t).popover('hide');
    });
}

function nameleave(t) {
    var _this = t;
    setTimeout(function () {
        if (!$(".popover:hover").length) {
            $(_this).popover("hide")
        }
    }, 100);
}

function showInfo(u, name, id) {
    $(window.parent.document).find(".page-tabs-content .J_menuTab").removeClass("active");
    $(window.parent.document).find(".J_iframe").each(function () {
        $(this).css("display", "none");
    })

    var r = $(window.parent.document).find(".page-tabs-content");
    $(r).append("<a href='javascript:;' class='J_menuTab active' data-id='" + id + "s2'>" + name + "<i class='fa fa-times-circle'></i></a>")
    var m = $(window.parent.document).find("#content-main");
    $(m).append("<iframe class='J_iframe' name='iframe3' width='100%' height='100%' src='" + u + "' frameborder='0' data-id='" + id + "s2' style='display: inline;'></iframe>");
}


//js的全局配置
$.token = function ()
{
    return "4BE2830173AEB5D057505EB0E58DA2C1";
}
$.requestPath = function ()
{
    return "http://localhost:7075/";
}