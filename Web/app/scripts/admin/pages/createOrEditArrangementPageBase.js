import { Constants } from "../utilities/constants";
import { GoogleMapsUtilties } from "../utilities/googleMapsUtilities";
export class CreateOrEditArrangementPageBase {
    initMap() {
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
        $("#btnGetCoordinatesFromAddress").click(() => {
            const lookupAddress = this.getLookupAddress();
            this.geocoder.geocode({ 'address': lookupAddress }, (results, status) => {
                if (status === google.maps.GeocoderStatus.OK) {
                    this.updateMap(results[0].geometry.location);
                    this.updateLatLngTextboxes(results[0].geometry.location);
                }
                else {
                    console.log(`Kunde inte hitta position pga ${status}`);
                }
            });
            return false;
        });
        $("#btnGetInformationFromGooglePlaces").click(() => {
            const name = $("#Name").val();
            if (!name)
                return false;
            this.placesService.textSearch({ query: name }, (results, status) => {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    console.log(`"Träffar på '${name}':"`);
                    for (let i = 0; i < results.length; i++) {
                        console.log(`\t${results[i].name}`);
                    }
                    this.placesService.getDetails({ placeId: results[0].place_id }, (place, detailedStatus) => {
                        if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                            this.updateInformationFromGooglePlaces(place);
                        }
                        else {
                            console.log(`"Kunde inte hämta detailjer för '${results[0].place_id}' pga ${detailedStatus}"`);
                        }
                    });
                }
                else {
                    console.log(`"Inga träffar på '${name}' pga ${status}"`);
                }
            });
            return false;
        });
        $("#btnOpenCoverImage").click(() => {
            var url = $("#CoverImage").val();
            if (url)
                $("<a>").attr("href", url).attr("target", "_blank")[0].click();
            return false;
        });
        $("#btnGetImagesFromGooglePlaces").click(() => {
            var placeId = $("#GooglePlacesId").val();
            if (!placeId) {
                console.log("No Google Places id specified");
                return false;
            }
            this.placesService.getDetails({ placeId: placeId }, (place, detailedStatus) => {
                if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                    this.updateImageList(place, false);
                }
                else {
                    console.log(`"Kunde inte hämta detailjer för '${placeId}' pga ${detailedStatus}"`);
                }
            });
            return false;
        });
        $("#btnOpenWebsite").click(() => {
            $("<a>").attr("href", $("#Website").val()).attr("target", "_blank")[0].click();
            return false;
        });
    }
    updateInformationFromGooglePlaces(place) {
        $("#GooglePlacesId").val(place.place_id);
        this.updateImageList(place, true);
        this.updateStreetAddress(place.address_components, "StreetAddress");
        this.updateAddressComponent(place.address_components, "postal_code", "PostalCode");
        this.updateAddressComponent(place.address_components, "postal_town", "PostalCity");
        this.updateAddressComponent(place.address_components, "street_address", "StreetAddress");
        $("#PhoneNumber").val(place.formatted_phone_number);
        $("#Website").val(place.website.replace(/\/+$/, ""));
        if (place.geometry && place.geometry.location) {
            this.updateLatLngTextboxes(place.geometry.location);
            this.updateMap(place.geometry.location);
        }
        $("#GooglePlacesName").text(place.name);
        $.get(`/api/cities/closest?latitude=${place.geometry.location.lat()}&longitude=${place.geometry.location.lng()}`, function (data) {
            $("#CitySlug").val(data.slug);
        });
    }
    updateImageList(place, setCoverImage) {
        let listElementInDom = $("#lstImagesFromGooglePlaces");
        listElementInDom.addClass("hidden");
        listElementInDom.empty();
        if (place.photos.length > 0) {
            listElementInDom.removeClass("hidden");
            for (let photo of place.photos) {
                listElementInDom.append(`<img 
                    class=\"ui image\"
                    data-url=\"${photo.getUrl({ maxWidth: 812 })}\" src=\"${photo.getUrl({ maxWidth: 600, maxHeight: 600 })}\"
                    data-html=\"${this.makeAttributionsHtmlList(photo.html_attributions)}\"
                >`);
            }
            $("#lstImagesFromGooglePlaces .image").popup();
            $("#lstImagesFromGooglePlaces .image").click((e) => {
                this.updateCoverImageUrl($(e.currentTarget).data("url"));
                this.updateCoverImageAttributions($(e.currentTarget).data("html"));
                return false;
            });
            if (setCoverImage) {
                this.updateCoverImageUrl(place.photos[0].getUrl({ maxWidth: 812 }));
                this.updateCoverImageAttributions(this.makeAttributionsHtmlList(place.photos[0].html_attributions));
            }
        }
    }
    makeAttributionsHtmlList(html_attributions) {
        let attributions = "";
        for (let attrib of html_attributions) {
            attributions += `${attrib}, `;
        }
        if (attributions.length >= 2)
            attributions = attributions.substring(0, attributions.length - 2);
        return attributions.replace(/"/g, "'");
    }
    updateCoverImageUrl(url) {
        $("#CoverImage").val(url);
    }
    updateCoverImageAttributions(attributions) {
        $("#CoverImageAttributions").val(attributions);
    }
    updateAddressComponent(addressComponents, googleName, fieldId) {
        const value = GoogleMapsUtilties.getAddressComponent(addressComponents, googleName);
        if (value) {
            $(`#${fieldId}`).val(value);
        }
    }
    updateStreetAddress(addressComponents, fieldId) {
        const streetAddress = GoogleMapsUtilties.getAddressComponent(addressComponents, "street_address");
        const route = GoogleMapsUtilties.getAddressComponent(addressComponents, "route");
        const streetNumber = GoogleMapsUtilties.getAddressComponent(addressComponents, "street_number");
        let result = streetAddress ? streetAddress : route;
        if (streetNumber)
            result = result + " " + streetNumber;
        $(`#${fieldId}`).val(result);
    }
    getLookupAddress() {
        const streetAddress = $("#StreetAddress").val();
        const postalCode = $("#PostalCode").val();
        const postalCity = $("#PostalCity").val();
        const lookupAddress = `${streetAddress}, ${postalCode} ${postalCity}`;
        return lookupAddress;
    }
    updateMap(location) {
        if (this.marker) {
            this.marker.setPosition(location);
        }
        else {
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
    updateLatLngTextboxes(location) {
        $("#Latitude").val(location.lat().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
        $("#Longitude").val(location.lng().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
    }
}
