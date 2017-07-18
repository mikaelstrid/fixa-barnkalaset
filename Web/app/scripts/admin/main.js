$(document).ready(() => {
    bootstrap();
});
function bootstrap() {
    console.log("Starting bootstrapping procedure...");
    if ($(".pxl-admin-page--city-create")) {
        console.log("Found \"Create city page\", start bootstrapping it...");
        let page = new CreateCityPage();
    }
    console.log("Bootstrapping procedure finished.");
}
