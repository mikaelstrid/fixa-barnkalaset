import { Constants } from "../utilities/constants";

export abstract class CreateOrEditCityPageBase {

    private mapElement: HTMLElement;

    private map: google.maps.Map;
    private marker: google.maps.Marker;
    private geocoder: google.maps.Geocoder;

    protected initialLatitude: number;
    protected initialLongitude: number;

    protected initMap() {
        this.mapElement = $("#map")[0];
        this.geocoder = new google.maps.Geocoder();
        this.map = new google.maps.Map(this.mapElement, {
            zoom: 4,
            center: { lat: 63.0, lng: 17.0 }
        });

        if (this.initialLatitude && this.initialLongitude) {
            this.updateMap(new google.maps.LatLng(this.initialLatitude, this.initialLongitude));
        }

        $("#btnGetCoordinatesFromName").click(() => {
            const lookupAddress = this.getLookupAddress();
            this.geocoder.geocode({ 'address': lookupAddress }, (results, status) => {
                if (status === google.maps.GeocoderStatus.OK) {
                    this.updateMap(results[0].geometry.location);
                    this.updateLatLngTextboxes(results[0].geometry.location);
                } else {
                    console.log(`Kunde inte hitta position pga ${status}`);
                }
            });
            return false;
        });
    }

    private getLookupAddress(): string {
        return $("#Name").val();
    }

    private updateMap(location: google.maps.LatLng): void {
        if (this.marker) {
            this.marker.setPosition(location);
        } else {
            this.marker = new google.maps.Marker({
                map: this.map,
                position: location,
                draggable: true
            });
            this.marker.addListener("dragend", ev => {
                this.updateLatLngTextboxes(ev.latLng);
            });
            this.map.setZoom(11);
        }
        this.map.setCenter(location);
    }

    private updateLatLngTextboxes(location: google.maps.LatLng): void  {
        $("#Latitude").val(location.lat().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
    }
}