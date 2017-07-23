import { CreateOrEditArrangementPageBase } from "./createOrEditArrangementPageBase";
import { slugify } from "../utilities/slugify";

export class EditArrangementPage extends CreateOrEditArrangementPageBase {
    initPage(latitude: number, longitude: number) {
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
        CKEDITOR.replace("BookingConditions");
        CKEDITOR.replace("PriceInformation");
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
        this.initMap();
    }
}