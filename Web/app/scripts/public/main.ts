import { IndexArrangementsPage } from "./pages/indexArrangementsPage";

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

    console.log("Bootstrapping public procedure finished.");
}