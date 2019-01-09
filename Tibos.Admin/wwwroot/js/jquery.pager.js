/*
* jQuery pager plugin
* Version 1.0 (12/22/2008)
* @requires jQuery v1.2.6 or later
*
* Example at: http://jonpauldavies.github.com/JQuery/Pager/PagerDemo.html
*
* Copyright (c) 2008-2009 Jon Paul Davies
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
* 
* Read the related blog post and contact the author at http://www.j-dee.com/2008/12/22/jquery-pager-plugin/
*
* This version is far from perfect and doesn't manage it's own state, therefore contributions are more than welcome!
*
* Usage: .pager({ pagenumber: 1, pagesize: 15, buttonClickCallback: PagerClickTest });
*
* Where pagenumber is the visible page number
*       pagesize is the total number of pages to display
*       buttonClickCallback is the method to fire when a pager button is clicked.
*
* buttonClickCallback signiture is PagerClickTest = function(pageclickednumber) 
* Where pageclickednumber is the number of the page clicked in the control.
*
* The included Pager.CSS file is a dependancy but can obviously tweaked to your wishes
* Tested in IE6 IE7 Firefox & Safari. Any browser strangeness, please report.
*/
;(function($) {

    $.fn.pager = function(options) {

        var opts = $.extend({}, $.fn.pager.defaults, options);

        return this.each(function() {

        // empty out the destination element and then render out the pager with the supplied options
            $(this).empty().append(renderpager(parseInt(options.pagenumber), parseInt(options.pagesize), parseInt(options.pagecount), options.buttonClickCallback));
            
            // specify correct cursor activity
            $('.pages li').mouseover(function() { document.body.style.cursor = "pointer"; }).mouseout(function() { document.body.style.cursor = "auto"; });
        });
    };

    // render and return the pager with the supplied options
    function renderpager(pagenumber, pagesize,pagecount, buttonClickCallback) {
        $pagerContainer = $('<div class="col-md-12"></div>');
        // setup $pager to hold render
        var $pager = $('<ul class="pagination small-pagination pull-right"></ul>');
        // add in the previous and next buttons
        var pagenum = Math.ceil(pagecount / pagesize);
        $pager.append(renderButton('<<', pagenumber, pagenum, buttonClickCallback)).append(renderButton('<', pagenumber, pagenum, buttonClickCallback));
        // pager currently only handles 10 viewable pages ( could be easily parameterized, maybe in next version ) so handle edge cases
        var startPoint = 1;
        var endPoint = pagenum;

        if (pagenumber > 4) {
            startPoint = pagenumber - 4;
            endPoint = pagenumber + 4;
        }
        if (endPoint > pagenum) {
            startPoint = pagenum - 8;
            endPoint = pagenum;
        }
        if (startPoint < 1) {
            startPoint = 1;
        }
        if (endPoint < 1) {
            endPoint = 1;
        }
        // loop thru visible pages and render buttons
        for (var page = startPoint; page <= endPoint; page++) {

            var $buttonContainer = $('<li></li>');
            var $currentButton = $('<a href="javascript:void(0);">' + (page) + '</a>');
            $buttonContainer.append($currentButton);

            page == pagenumber ? $buttonContainer.addClass('active') : $currentButton.click(function() { buttonClickCallback(this.firstChild.data); });
            $buttonContainer.appendTo($pager);
        }

        // render in the next and last buttons before returning the whole rendered control back.
        $pager.append(renderButton('>', pagenumber, pagenum, buttonClickCallback)).append(renderButton('>>', pagenumber, pagenum, buttonClickCallback));
        $pagerContainer.append($pager);
        return $pagerContainer;
    }

    // renders and returns a 'specialized' button, ie 'next', 'previous' etc. rather than a page number button
    function renderButton(buttonLabel, pagenumber, pagesize, buttonClickCallback) {
        var $buttonContainer = $('<li></li>');
        var $Button = $('<a href="javascript:void(0);">' + buttonLabel + '</a>');
        $buttonContainer.append($Button);
        var destPage = 1;
        // work out destination page for required button type
        switch (buttonLabel) {
            case "<<":
                destPage = 1;
                break;
            case "<":
                destPage = pagenumber - 1;
                break;
            case ">":
                destPage = pagenumber + 1;
                break;
            case ">>":
                destPage = pagesize;
                break;
        }

        // disable and 'grey' out buttons if not needed.
        if (buttonLabel == "<<" || buttonLabel == "<") {
            pagenumber <= 1 ? $buttonContainer.addClass('disabled') : $Button.click(function() { buttonClickCallback(destPage); });
        }
        else {
            pagenumber >= pagesize ? $buttonContainer.addClass('disabled') : $Button.click(function() { buttonClickCallback(destPage); });
        }

        return $buttonContainer;
    }

    // pager defaults. hardly worth bothering with in this case but used as placeholder for expansion in the next version
    $.fn.pager.defaults = {
        pagenumber: 1,
        pagesize: 1
    };

})(jQuery);





