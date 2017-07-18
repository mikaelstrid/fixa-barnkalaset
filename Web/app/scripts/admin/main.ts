import { CreateCityPage } from "./pages/createCityPage";

$(document).ready(() => {
    bootstrap();
});

function bootstrap() {
    console.log("Starting bootstrapping procedure...");

    if ($(".pxl-admin-page--city-create").length > 0) {
        console.log("Found \"Create city page\", start bootstrapping it...");
        let page = new CreateCityPage();
        page.initPage();
        console.log("Bootstrapping \"Create city page\" finished.");
    }

    console.log("Bootstrapping procedure finished.");
}