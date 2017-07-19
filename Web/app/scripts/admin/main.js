import { CreateCityPage } from "./pages/createCityPage";
import { EditCityPage } from "./pages/editCityPage";
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
    console.log("Bootstrapping procedure finished.");
}
