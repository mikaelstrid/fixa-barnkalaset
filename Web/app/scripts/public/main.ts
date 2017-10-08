import { IndexArrangementsPage } from "./pages/indexArrangementsPage";
import { DetailsArrangementPage } from "./pages/detailsArrangementPage";
import { WhichInvitationCardsPage } from "./pages/whichInvitationCardsPage";

$(document).ready(() => {
    bootstrap();
});

function bootstrap() {
    var pages = [
        { pageClassSuffix: "arrangements-index", ctorFunc: () => new IndexArrangementsPage() },
        { pageClassSuffix: "arrangements-details", ctorFunc: () => new IndexArrangementsPage() },
        { pageClassSuffix: "invitation-cards-which", ctorFunc: () => new WhichInvitationCardsPage() }
    ];

    for (let p of pages) {
        if ($(`.pxl-public-page--${p.pageClassSuffix}`).length > 0) {
            p.ctorFunc().initPage();
        }
    }
}