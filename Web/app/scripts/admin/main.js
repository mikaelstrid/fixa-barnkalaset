import { CreateCityPage } from "./pages/createCityPage";
import { EditCityPage } from "./pages/editCityPage";
import { CreateArrangementPage } from "./pages/createArrangementPage";
$(document).ready(() => {
    bootstrap();
});
function bootstrap() {
    console.log("Starting bootstrapping procedure...");
    if ($(".pxl-admin-page--cities-create").length > 0) {
        console.log("Found \"create city page\", start bootstrapping it...");
        let page = new CreateCityPage();
        page.initPage();
        console.log("Bootstrapping \"create city page\" finished.");
    }
    let editCityPage = $(".pxl-admin-page--cities-edit");
    if (editCityPage.length > 0) {
        console.log("Found \"edit city page\", start bootstrapping it...");
        let page = new EditCityPage();
        page.initPage(editCityPage.data("latitude"), editCityPage.data("longitude"));
        console.log("Bootstrapping \"edit city page\" finished.");
    }
    if ($(".pxl-admin-page--arrangements-create").length > 0) {
        console.log("Found \"create arrangement page\", start bootstrapping it...");
        let page = new CreateArrangementPage();
        page.initPage();
        console.log("Bootstrapping \"create arrangement page\" finished.");
    }
    let editArrangementPage = $(".pxl-admin-page--arrangements-edit");
    if (editArrangementPage.length > 0) {
        console.log("Found \"edit arrangement page\", start bootstrapping it...");
        let page = new EditCityPage();
        page.initPage(editArrangementPage.data("latitude"), editArrangementPage.data("longitude"));
        console.log("Bootstrapping \"edit arrangement page\" finished.");
    }
    console.log("Bootstrapping procedure finished.");
}
