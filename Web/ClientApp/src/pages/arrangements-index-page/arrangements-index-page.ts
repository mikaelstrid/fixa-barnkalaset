import './_arrangements-index-page.scss';

export class IndexArrangementsPage {
    public initPage() {
        $('select#typeFilter').change(function(ev) {
            const selectedType = $(this).val() as string;
            if (!selectedType) {
                $('.pxl-arrangement').show();
            } else {
                $('.pxl-arrangement').each(() => { $(this).toggle($(this).data('type') === selectedType); });
            }
            $('#arrangementCount').text($('.pxl-arrangement:visible').length);

            if ((window as any).ga) {
                ga('send', 'event', {
                    eventCategory: 'Search',
                    eventAction: 'typeFilter',
                    eventLabel: selectedType,
                    transport: 'beacon'
                });
            }
        });
    }
}
