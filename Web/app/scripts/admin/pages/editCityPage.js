import { CreateOrEditCityPageBase } from "./createOrEditCityPageBase";
export class EditCityPage extends CreateOrEditCityPageBase {
    initPage(latitude, longitude) {
        console.log("EditCityPage.initPage", latitude, longitude);
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
        this.initMap();
    }
}
