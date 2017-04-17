class CreateOrEditArrangementPage {

    private mapElement: HTMLElement;

    private map: google.maps.Map;
    private marker: google.maps.Marker;
    private geocoder: google.maps.Geocoder;

    initPage() {
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val()));
        });
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
    }

    private initMap() {
        this.mapElement = $("#map")[0];
        this.geocoder = new google.maps.Geocoder();

        this.map = new google.maps.Map(this.mapElement, {
            zoom: 4,
            center: { lat: 63.0, lng: 17.0 }
        });

        $("#btnGetCoordinatesFromAddress").click(() => {
            const lookupAddress = this.getLookupAddress();
            this.geocoder.geocode({ 'address': lookupAddress }, (results, status) => {
                if (status === google.maps.GeocoderStatus.OK) {
                    this.updateMap(results[0].geometry.location);
                    this.updateLatLngTextboxes(results[0].geometry.location);
                } else {
                    alert('Geocode was not successful for the following reason: ' + status);
                }
            });
            return false;
        });
    }

    private getLookupAddress(): string {
        const streetAddress = $("#StreetAddress").val();
        const postalCode = $("#PostalCode").val();
        const postalCity = $("#PostalCity").val();
        const lookupAddress = `${streetAddress}, ${postalCode} ${postalCity}`;
        console.log(`getLookupAddress: ${lookupAddress}`);
        return lookupAddress;
    }

    private updateMap(location: google.maps.LatLng) {
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

    private updateLatLngTextboxes(location: google.maps.LatLng) {
        $("#Latitude").val(location.lat().toLocaleString(undefined, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(undefined, { maximumFractionDigits: 14 }));
    }
}