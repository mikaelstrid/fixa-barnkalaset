var CreateOrEditCityPageBase = (function () {
    function CreateOrEditCityPageBase() {
    }
    CreateOrEditCityPageBase.prototype.initMap = function () {
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
        $("#btnGetCoordinatesFromName").click(function () {
            var lookupAddress = _this.getLookupAddress();
            _this.geocoder.geocode({ 'address': lookupAddress }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    _this.updateMap(results[0].geometry.location);
                    _this.updateLatLngTextboxes(results[0].geometry.location);
                }
                else {
                    console.log("Kunde inte hitta position pga " + status);
                }
            });
            return false;
        });
    };
    CreateOrEditCityPageBase.prototype.getLookupAddress = function () {
        return $("#Name").val();
    };
    CreateOrEditCityPageBase.prototype.updateMap = function (location) {
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
    CreateOrEditCityPageBase.prototype.updateLatLngTextboxes = function (location) {
        $("#Latitude").val(location.lat().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
    };
    return CreateOrEditCityPageBase;
}());
//# sourceMappingURL=createOrEditCityPageBase.js.map