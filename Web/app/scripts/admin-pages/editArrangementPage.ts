class EditArrangementPage extends CreateOrEditArrangementPageBase {

    initPage(latitude: number, longitude: number) {
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
    }
}