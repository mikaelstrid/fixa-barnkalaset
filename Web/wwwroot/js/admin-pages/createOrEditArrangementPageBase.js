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
        this.placesService = new google.maps.places.PlacesService(this.map);
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
                    console.log("Kunde inte hitta position pga " + status);
                }
            });
            return false;
        });
        $("#btnGetInformationFromGooglePlaces").click(function () {
            var name = $("#Name").val();
            _this.placesService.textSearch({ query: name }, function (results, status) {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    console.log("\"Tr\u00E4ffar p\u00E5 '" + name + "':\"");
                    for (var i = 0; i < results.length; i++) {
                        console.log("\t" + results[i].name);
                    }
                    _this.placesService.getDetails({ placeId: results[0].place_id }, function (place, detailedStatus) {
                        if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                            _this.updateInformationFromGooglePlaces(place);
                        }
                        else {
                            console.log("\"Kunde inte h\u00E4mta detailjer f\u00F6r '" + results[0].place_id + "' pga " + detailedStatus + "\"");
                        }
                    });
                }
                else {
                    console.log("\"Inga tr\u00E4ffar p\u00E5 '" + name + "' pga " + status + "\"");
                }
            });
            return false;
        });
        $("#btnOpenWebsite").click(function () {
            $("<a>").attr("href", $("#Website").val()).attr("target", "_blank")[0].click();
            return false;
        });
    };
    CreateOrEditArrangementPageBase.prototype.updateInformationFromGooglePlaces = function (place) {
        $("#GooglePlacesId").val(place.place_id);
        if (place.photos.length > 0)
            $("#CoverImage").val(place.photos[0].getUrl({ maxWidth: 1000 }));
        this.updateStreetAddress(place.address_components, "StreetAddress");
        this.updateAddressComponent(place.address_components, "postal_code", "PostalCode");
        this.updateAddressComponent(place.address_components, "postal_town", "PostalCity");
        this.updateAddressComponent(place.address_components, "street_address", "StreetAddress");
        $("#PhoneNumber").val(place.formatted_phone_number);
        $("#Website").val(place.website);
        if (place.geometry && place.geometry.location) {
            this.updateLatLngTextboxes(place.geometry.location);
            this.updateMap(place.geometry.location);
        }
        $("#GooglePlacesName").text(place.name);
        $.get("/api/cities/closest?latitude=" + place.geometry.location.lat() + "&longitude=" + place.geometry.location.lng(), function (data) {
            $("#CitySlug").val(data.slug);
        });
    };
    CreateOrEditArrangementPageBase.prototype.updateAddressComponent = function (addressComponents, googleName, fieldId) {
        var value = GoogleMapsUtilties.getAddressComponent(addressComponents, googleName);
        if (value) {
            $("#" + fieldId).val(value);
        }
    };
    CreateOrEditArrangementPageBase.prototype.updateStreetAddress = function (addressComponents, fieldId) {
        var streetAddress = GoogleMapsUtilties.getAddressComponent(addressComponents, "street_address");
        var route = GoogleMapsUtilties.getAddressComponent(addressComponents, "route");
        var streetNumber = GoogleMapsUtilties.getAddressComponent(addressComponents, "street_number");
        var result = streetAddress ? streetAddress : route;
        if (streetNumber)
            result = result + " " + streetNumber;
        $("#" + fieldId).val(result);
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
        $("#Latitude").val(location.lat().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
    };
    return CreateOrEditArrangementPageBase;
}());
//# sourceMappingURL=createOrEditArrangementPageBase.js.map