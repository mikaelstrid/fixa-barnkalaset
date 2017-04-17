var CreateOrEditArrangementPage = (function () {
    function CreateOrEditArrangementPage() {
    }
    CreateOrEditArrangementPage.prototype.initPage = function () {
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val()));
        });
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
    };
    CreateOrEditArrangementPage.prototype.initMap = function () {
        var _this = this;
        this.mapElement = $("#map")[0];
        this.geocoder = new google.maps.Geocoder();
        this.map = new google.maps.Map(this.mapElement, {
            zoom: 4,
            center: { lat: 63.0, lng: 17.0 }
        });
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
    CreateOrEditArrangementPage.prototype.getLookupAddress = function () {
        var streetAddress = $("#StreetAddress").val();
        var postalCode = $("#PostalCode").val();
        var postalCity = $("#PostalCity").val();
        var lookupAddress = streetAddress + ", " + postalCode + " " + postalCity;
        console.log("getLookupAddress: " + lookupAddress);
        return lookupAddress;
    };
    CreateOrEditArrangementPage.prototype.updateMap = function (location) {
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
    CreateOrEditArrangementPage.prototype.updateLatLngTextboxes = function (location) {
        $("#Latitude").val(location.lat().toLocaleString(undefined, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(undefined, { maximumFractionDigits: 14 }));
    };
    return CreateOrEditArrangementPage;
}());
//# sourceMappingURL=createOrEditArrangementPage.js.map