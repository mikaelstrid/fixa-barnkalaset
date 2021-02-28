import { CreateOrEditArrangementPageBase } from "./createOrEditArrangementPageBase";
import { slugify } from "../utilities/slugify";
export class CreateArrangementPage extends CreateOrEditArrangementPageBase {
    initPage() {
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val().toString()));
        });
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
        CKEDITOR.replace("BookingConditions");
        CKEDITOR.replace("PriceInformation");
        this.initMap();
    }
}
