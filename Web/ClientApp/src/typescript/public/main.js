import { IndexArrangementsPage } from "./pages/indexArrangementsPage";
import { DetailsArrangementPage } from "./pages/detailsArrangementPage";
$(document).ready(() => {
    bootstrap();
});
function bootstrap() {
    console.log("Starting public bootstrapping procedure...");
    if ($(".pxl-public-page--arrangements-index").length > 0) {
        console.log("Found \"index arrangements page\", start bootstrapping it...");
        let page = new IndexArrangementsPage();
        page.initPage();
        console.log("Bootstrapping \"index arrangements page\" finished.");
    }
    if ($(".pxl-public-page--arrangements-details").length > 0) {
        console.log("Found \"details arrangement page\", start bootstrapping it...");
        let page = new DetailsArrangementPage();
        page.initPage();
        console.log("Bootstrapping \"details arrangement page\" finished.");
    }
    console.log("Bootstrapping public procedure finished.");
}
