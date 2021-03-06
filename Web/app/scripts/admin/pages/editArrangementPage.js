import { CreateOrEditArrangementPageBase } from "./createOrEditArrangementPageBase";
export class EditArrangementPage extends CreateOrEditArrangementPageBase {
    initPage(latitude, longitude) {
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
        CKEDITOR.replace("BookingConditions");
        CKEDITOR.replace("PriceInformation");
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
        this.initMap();
    }
}
