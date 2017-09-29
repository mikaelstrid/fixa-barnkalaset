export class DetailsArrangementPage {
    initPage() {
        console.log("DetailsArrangementPage.initPage");
        $("#emailAddressLink").click(function (event) {
            ga('send', 'event', {
                eventCategory: 'Contact',
                eventAction: 'mail',
                eventLabel: $(this).attr("href"),
                transport: 'beacon'
            });
        });
        $("#externalWebsiteLink").click(function (event) {
            ga('send', 'event', {
                eventCategory: 'Outbound Link',
                eventAction: 'click',
                eventLabel: $(this).attr("href"),
                transport: 'beacon'
            });
        });
    }
}
