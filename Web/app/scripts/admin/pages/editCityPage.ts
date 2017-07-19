import { CreateOrEditCityPageBase } from "./createOrEditCityPageBase";
import { slugify } from "../utilities/slugify";

export class EditCityPage extends CreateOrEditCityPageBase {
    initPage(latitude: number, longitude: number) {
        console.log("EditCityPage.initPage", latitude, longitude)
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
        this.initMap();
    }
}