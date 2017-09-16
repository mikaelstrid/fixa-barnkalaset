export class IndexArrangementsPage {
    initPage() {
        console.log("IndexArrangementsPage.initPage");
        $('select#typeFilter').change(function (ev) {
            var selectedTypes = $(this).val();
            if (selectedTypes.length == 0) {
                $('.pxl-arrangement').show();
            }
            else {
                $('.pxl-arrangement').each(function () {
                    var visible = false;
                    for (var i = 0; !visible && i < selectedTypes.length; i++) {
                        if ($(this).data('type') === selectedTypes[i]) {
                            visible = true;
                        }
                    }
                    $(this).toggle(visible);
                });
            }
            $(this).dropdown('hide');
            $('#arrangementCount').text($('.pxl-arrangement:visible').length);
        });
    }
}
