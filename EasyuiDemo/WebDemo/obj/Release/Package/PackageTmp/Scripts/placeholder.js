var zhm_placeholder = function () {
    supportPlaceholder = 'placeholder' in document.createElement('input');
    if (!supportPlaceholder) {
        //placeholder();
        //console.log(placeholder());
        var Oinput = jQuery('[placeholder]');
        $.each(Oinput, function (i, e) {
            var thatVal = $(e).attr('placeholder');
            var html = "<span class='oSpan' style='position:absolute;top:5px;left:15px;text-align:left;z-index:2;color:#888'>" + thatVal + "</span>";
            $(e).parent().css({"position": "relative"});
            $(e).parent().find("input").css({"position":"relative","z-index":"3", "background-color": "transparent","color":"#555"});
            if ($(e).val() == $(e).attr('placeholder')) {
                $(e).val('');
                $(e).parent().append(html);
                $(e).parent().find("input").css({ "background": "transparent" });
            }
            if ($(e).val() == "") {
                $(e).parent().append(html);
                $(e).parent().find("input").css({ "background": "transparent" });
            }
            $(e).on("focus", function () {
                $(this).parent().find(".oSpan").remove();
                $(e).parent().find("input").css({ "background": "#fff" });
            });
            $(e).on("blur", function () {
                if ($(e).val() == "") {
                    $(e).parent().append(html);
                    $(e).parent().find("input").css({ "background-color": "transparent" });
                    if ($(e).hasClass("hasDatepicker")) {
                        if ($(e).parent().next().find(".hasDatepicker").length>0) {
                            $(e).parent().next().find(".oSpan").remove();
                            $(e).parent().find("input").css({ "background-color": "#fff" });
                        }
                        $(this).parent().find(".oSpan").remove();
                        $(e).parent().find("input").css({ "background-color": "#fff" });
                    }
                }
            });
            $(e).on("click", function () {
                $(this).parent().find(".oSpan").remove();
                $(e).parent().find("input").css({ "background": "#fff" });
            });
          
        });
    }
}
$(function () {
    zhm_placeholder();
});
