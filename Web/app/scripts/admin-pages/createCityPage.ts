class CreateCityPage extends CreateOrEditCityPageBase {

    initPage() {
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val()));
        });
    }
}