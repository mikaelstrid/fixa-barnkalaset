import { CreateOrEditCityPageBase } from "./createOrEditCityPageBase";
import { slugify } from "../utilities/slugify";
export class CreateCityPage extends CreateOrEditCityPageBase {
    initPage() {
        console.log("CreateCityPage.initPage");
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val()));
        });
        this.initMap();
    }
}
