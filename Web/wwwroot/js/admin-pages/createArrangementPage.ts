class CreateArrangementPage extends CreateOrEditArrangementPageBase {

    initPage() {
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val()));
        });
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
        CKEDITOR.replace("BookingConditions");
    }
}