var CreateOrEditArrangementPageBase = (function () {
    function CreateOrEditArrangementPageBase() {
    }
    CreateOrEditArrangementPageBase.prototype.initMap = function () {
        var _this = this;
        this.mapElement = $("#map")[0];
        this.geocoder = new google.maps.Geocoder();
        this.map = new google.maps.Map(this.mapElement, {
            zoom: 4,
            center: { lat: 63.0, lng: 17.0 }
        });
        if (this.initialLatitude && this.initialLongitude) {
            this.updateMap(new google.maps.LatLng(this.initialLatitude, this.initialLongitude));
        }
        $("#btnGetCoordinatesFromAddress").click(function () {
            var lookupAddress = _this.getLookupAddress();
            _this.geocoder.geocode({ 'address': lookupAddress }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    _this.updateMap(results[0].geometry.location);
                    _this.updateLatLngTextboxes(results[0].geometry.location);
                }
                else {
                    alert('Geocode was not successful for the following reason: ' + status);
                }
            });
            return false;
        });
    };
    CreateOrEditArrangementPageBase.prototype.getLookupAddress = function () {
        var streetAddress = $("#StreetAddress").val();
        var postalCode = $("#PostalCode").val();
        var postalCity = $("#PostalCity").val();
        var lookupAddress = streetAddress + ", " + postalCode + " " + postalCity;
        return lookupAddress;
    };
    CreateOrEditArrangementPageBase.prototype.updateMap = function (location) {
        var _this = this;
        if (this.marker) {
            this.marker.setPosition(location);
        }
        else {
            this.marker = new google.maps.Marker({
                map: this.map,
                position: location,
                draggable: true
            });
            this.marker.addListener("dragend", function (ev) {
                _this.updateLatLngTextboxes(ev.latLng);
            });
            this.map.setZoom(11);
        }
        this.map.setCenter(location);
    };
    CreateOrEditArrangementPageBase.prototype.updateLatLngTextboxes = function (location) {
        $("#Latitude").val(location.lat().toLocaleString(undefined, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(undefined, { maximumFractionDigits: 14 }));
    };
    return CreateOrEditArrangementPageBase;
}());
//# sourceMappingURL=createOrEditArrangementPageBase.js.map