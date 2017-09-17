export class DetailsArrangementPage {
    initPage() {
        console.log("DetailsArrangementPage.initPage");
        $("#emailAddressLink").click(function (event) {
            console.log("test");
            var t = $(this).attr("href");
            console.log(t);
            ga('send', 'event', {
                eventCategory: 'Contact',
                eventAction: 'mail',
                eventLabel: event.target.getAttribute("href"),
                transport: 'beacon'
            });
        });
        $("#externalWebsiteLink").click(function (event) {
            console.log(event.target.getAttribute("href"));
            ga('send', 'event', {
                eventCategory: 'Outbound Link',
                eventAction: 'click',
                eventLabel: event.target.getAttribute("href"),
                transport: 'beacon'
            });
        });
    }
}
