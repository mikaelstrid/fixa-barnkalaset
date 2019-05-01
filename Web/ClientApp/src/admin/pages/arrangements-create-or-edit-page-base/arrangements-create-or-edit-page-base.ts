// tslint:disable: no-console
import * as $ from 'jquery';

import { Constants } from '../../../typescript/admin/utilities/constants';
import { GoogleMapsUtilties } from '../../../typescript/admin/utilities/googleMapsUtilities';

import './_arrangements-create-or-edit-page-base.scss';

export abstract class CreateOrEditArrangementPageBase {
    protected initialLatitude?: number;
    protected initialLongitude?: number;

    private mapElement: HTMLElement;

    private map: google.maps.Map;
    private marker?: google.maps.Marker;
    private geocoder: google.maps.Geocoder;
    private placesService: google.maps.places.PlacesService;

    constructor() {
        this.geocoder = new google.maps.Geocoder();
        this.mapElement = $('#map')[0];
        this.map = new google.maps.Map(this.mapElement, {
            zoom: 4,
            center: { lat: 63.0, lng: 17.0 }
        });
        this.placesService = new google.maps.places.PlacesService(this.map);
    }

    protected initMap() {
        if (this.initialLatitude && this.initialLongitude) {
            this.updateMap(new google.maps.LatLng(this.initialLatitude, this.initialLongitude));
        }

        $('#btnGetCoordinatesFromAddress').click(() => {
            const lookupAddress = this.getLookupAddress();
            this.geocoder.geocode({ address: lookupAddress }, (results, status) => {
                if (status === google.maps.GeocoderStatus.OK) {
                    this.updateMap(results[0].geometry.location);
                    this.updateLatLngTextboxes(results[0].geometry.location);
                } else {
                    console.log(`Kunde inte hitta position pga ${status}`);
                }
            });
            return false;
        });

        $('#btnGetInformationFromGooglePlaces').click(() => {
            const val = $('#Name').val();
            if (!val) {
                return false;
            }

            const name = val.toString();

            if (!name) {
                return false;
            }

            this.placesService.textSearch({ query: name }, (results, status) => {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    console.log(`"Träffar på '${name}':"`);
                    for (const entry of results) {
                        console.log(`\t${entry.name}`);
                    }
                    if (results[0].place_id) {
                        this.placesService.getDetails({ placeId: results[0].place_id }, (place, detailedStatus) => {
                            if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                                this.updateInformationFromGooglePlaces(place);
                            } else {
                                console.log(`"Kunde inte hämta detailjer för '${results[0].place_id}' pga ${detailedStatus}"`);
                            }
                        });
                    }
                } else {
                    console.log(`"Inga träffar på '${name}' pga ${status}"`);
                }
            });
            return false;
        });

        $('#btnOpenCoverImage').click(() => {
            const val = $('#CoverImage').val();
            if (!val) {
                return false;
            }

            const url = val.toString();
            if (url) {
                $('<a>')
                    .attr('href', url)
                    .attr('target', '_blank')[0]
                    .click();
            }
            return false;
        });

        $('#btnGetImagesFromGooglePlaces').click(() => {
            const placeId = $('#GooglePlacesId').val();
            if (!placeId) {
                console.log('No Google Places id specified');
                return false;
            }

            this.placesService.getDetails({ placeId: placeId.toString() }, (place, detailedStatus) => {
                if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                    this.updateImageList(place, false);
                } else {
                    console.log(`"Kunde inte hämta detailjer för '${placeId}' pga ${detailedStatus}"`);
                }
            });

            return false;
        });

        $('#btnOpenWebsite').click(() => {
            const val = $('#Website').val();
            if (val) {
                $('<a>')
                    .attr('href', val.toString())
                    .attr('target', '_blank')[0]
                    .click();
            }
            return false;
        });
    }

    private updateInformationFromGooglePlaces(place: google.maps.places.PlaceResult): void {
        if (place.place_id) {
            $('#GooglePlacesId').val(place.place_id);
        }

        this.updateImageList(place, true);

        if (place.address_components) {
            this.updateStreetAddress(place.address_components, 'StreetAddress');
            this.updateAddressComponent(place.address_components, 'postal_code', 'PostalCode');
            this.updateAddressComponent(place.address_components, 'postal_town', 'PostalCity');
            this.updateAddressComponent(place.address_components, 'street_address', 'StreetAddress');
        }

        if (place.formatted_phone_number) {
            $('#PhoneNumber').val(place.formatted_phone_number);
        }

        if (place.website) {
            $('#Website').val(place.website.replace(/\/+$/, ''));
        }

        if (place.geometry && place.geometry.location) {
            this.updateLatLngTextboxes(place.geometry.location);
            this.updateMap(place.geometry.location);
        }

        $('#GooglePlacesName').text(place.name);

        if (place.geometry) {
            $.get(`/api/cities/closest?latitude=${place.geometry.location.lat()}&longitude=${place.geometry.location.lng()}`, data => {
                $('#CitySlug').val(data.slug);
            });
        }
    }

    private updateImageList(place: google.maps.places.PlaceResult, setCoverImage: boolean) {
        const listElementInDom = $('#lstImagesFromGooglePlaces');
        listElementInDom.addClass('d-none');
        listElementInDom.empty();

        if (place.photos && place.photos.length > 0) {
            listElementInDom.removeClass('d-none');
            for (const photo of place.photos) {
                listElementInDom.append(`<img
                    class=\"\"
                    data-url=\"${photo.getUrl({ maxWidth: 812 })}\" src=\"${photo.getUrl({ maxWidth: 600, maxHeight: 600 })}\"
                    data-html=\"${this.makeAttributionsHtmlList(photo.html_attributions)}\"
                >`);
            }
            // $('#lstImagesFromGooglePlaces .image').popup();
            $('#lstImagesFromGooglePlaces img').click(e => {
                this.updateCoverImageUrl($(e.currentTarget).data('url'));
                this.updateCoverImageAttributions($(e.currentTarget).data('html'));
                return false;
            });
            if (setCoverImage) {
                this.updateCoverImageUrl(place.photos[0].getUrl({ maxWidth: 812 }));
                this.updateCoverImageAttributions(this.makeAttributionsHtmlList(place.photos[0].html_attributions));
            }
        }
    }

    private makeAttributionsHtmlList(htmlAttributions: string[]): string {
        let attributions = '';
        for (const attrib of htmlAttributions) {
            attributions += `${attrib}, `;
        }
        if (attributions.length >= 2) {
            attributions = attributions.substring(0, attributions.length - 2);
        }
        return attributions.replace(/"/g, '\'');
    }

    private updateCoverImageUrl(url: string) {
        $('#CoverImage').val(url);
    }

    private updateCoverImageAttributions(attributions: string) {
        $('#CoverImageAttributions').val(attributions);
    }

    private updateAddressComponent(addressComponents: google.maps.GeocoderAddressComponent[], googleName: string, fieldId: string): void {
        const value = GoogleMapsUtilties.getAddressComponent(addressComponents, googleName);
        if (value) {
            $(`#${fieldId}`).val(value);
        }
    }

    private updateStreetAddress(addressComponents: google.maps.GeocoderAddressComponent[], fieldId: string): void {
        const streetAddress = GoogleMapsUtilties.getAddressComponent(addressComponents, 'street_address');
        const route = GoogleMapsUtilties.getAddressComponent(addressComponents, 'route');
        const streetNumber = GoogleMapsUtilties.getAddressComponent(addressComponents, 'street_number');

        let result = streetAddress ? streetAddress : route;
        if (streetNumber) {
            result = result + ' ' + streetNumber;
        }

        $(`#${fieldId}`).val(result);
    }

    private getLookupAddress(): string {
        const streetAddress = $('#StreetAddress').val();
        const postalCode = $('#PostalCode').val();
        const postalCity = $('#PostalCity').val();
        const lookupAddress = `${streetAddress}, ${postalCode} ${postalCity}`;
        return lookupAddress;
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
            this.marker.addListener('dragend', ev => {
                this.updateLatLngTextboxes(ev.latLng);
            });
            this.map.setZoom(11);
        }
        this.map.setCenter(location);
    }

    private updateLatLngTextboxes(location: google.maps.LatLng): void {
        $('#Latitude').val(location.lat().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
        $('#Longitude').val(location.lng().toLocaleString(Constants.locale, { maximumFractionDigits: 14 }));
    }
}
