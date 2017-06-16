class EditCityPage extends CreateOrEditCityPageBase {

    initPage(latitude: number, longitude: number) {
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
    }
}