(function ($) {
    $.fn.labelauty = function (tag, tag2) {
        
        //判断是否选中
        rdochecked(tag);

        //单选or多选
        if (tag2 == "rdo") {
            //单选
            $(".rdobox").click(function () {
                $(this).prev().prop("checked", "checked");
                rdochecked(tag);
            });
        } else {
            //多选
            $(".chkbox").click(function () {
                //
                if ($(this).prev().prop("checked") == true) {
                    $(this).prev().removeAttr("checked");
                }
                else {
                    $(this).prev().prop("checked", "checked");
                }
                rdochecked(tag);
            });
        }

        //判断是否选中
        function rdochecked(tag) {
            var tempA = '.' + tag;
            $(tempA).each(function (i) {
                var rdobox = $(tempA).eq(i).next();
                if ($(tempA).eq(i).prop("checked") == false) {
                    rdobox.removeClass("checked");
                    rdobox.addClass("unchecked");
                    rdobox.find(".check-image").css("background", "url(../img/input-unchecked.png)");
                }
                else {
                    rdobox.removeClass("unchecked");
                    rdobox.addClass("checked");
                    rdobox.find(".check-image").css("background", "url(../img/input-checked.png)");
                }
            });
        }
    }


    $.fn.labelauty2 = function (tag, tag2,tag3) {

        //判断是否选中
        rdochecked(tag);

        //单选or多选
        if (tag2 == "rdo") {
            //单选
            $('.' + tag3 + " .rdobox").click(function () {
                $(this).prev().prop("checked", "checked");
                rdochecked(tag);
            });
        }
        //判断是否选中
        function rdochecked(tag) {
            var tempA = '.' + tag3 + ' .' + tag;
            $(tempA).each(function (i) {
                var rdobox = $(tempA).eq(i).next();
                if ($(tempA).eq(i).prop("checked") == false) {
                    rdobox.removeClass("checked");
                    rdobox.addClass("unchecked");
                    rdobox.find(".check-image").css("background", "url(../img/input-unchecked.png)");
                }
                else {
                    rdobox.removeClass("unchecked");
                    rdobox.addClass("checked");
                    rdobox.find(".check-image").css("background", "url(../img/input-checked.png)");
                }
            });
        }
    }

}(jQuery));