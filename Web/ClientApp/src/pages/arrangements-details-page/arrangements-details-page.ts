export class DetailsArrangementPage {
    public initPage() {
        $('#emailAddressLink').click(() => {
            ga('send', 'event', {
                eventCategory: 'Contact',
                eventAction: 'mail',
                eventLabel: $(this).attr('href'),
                transport: 'beacon'
            });
        });

        $('#externalWebsiteLink').click(() => {
            ga('send', 'event', {
                eventCategory: 'Outbound Link',
                eventAction: 'click',
                eventLabel: $(this).attr('href'),
                transport: 'beacon'
            });
        });
    }
}
