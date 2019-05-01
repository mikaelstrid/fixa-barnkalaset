(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "+Dlt":
/*!*************************************************************************!*\
  !*** ./src/pages/blog-posts-details-page/_blog-posts-details-page.scss ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "/7QA":
/*!**********************!*\
  !*** ./src/index.ts ***!
  \**********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _scss_bootstrap_config_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./scss/bootstrap/_config.scss */ "dWim");
/* harmony import */ var _scss_bootstrap_config_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_scss_bootstrap_config_scss__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _layouts_main_layout__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./layouts/main-layout */ "G+IT");
/* harmony import */ var _pages_arrangements_index_page_arrangements_index_page__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./pages/arrangements-index-page/arrangements-index-page */ "bLcr");
/* harmony import */ var _pages_arrangements_details_page_arrangements_details_page__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./pages/arrangements-details-page/arrangements-details-page */ "mhd1");
/* harmony import */ var _pages_blog_posts_index_page_blog_posts_index_page__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./pages/blog-posts-index-page/blog-posts-index-page */ "F6DD");
/* harmony import */ var _pages_blog_posts_details_page_blog_posts_details_page__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./pages/blog-posts-details-page/blog-posts-details-page */ "T2C1");
/* harmony import */ var _admin_pages_arrangements_create_page_arrangements_create_page__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./admin/pages/arrangements-create-page/arrangements-create-page */ "HnfR");
/* harmony import */ var _components_header_header__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./components/header/header */ "vPR2");
/* harmony import */ var _components_footer_footer__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./components/footer/footer */ "AO2n");
/* harmony import */ var _components_cookie_info_cookie_info__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./components/cookie-info/cookie-info */ "hrqH");
/* harmony import */ var _components_hero_hero__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./components/hero/hero */ "tLqx");
/* harmony import */ var _bootstrapper__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./bootstrapper */ "6gNi");
// Bootstrap customization

// import './sass/bootstrap-ext/_buttons.scss';
// Global styling
// import './sass/_buttons.scss';
// Layouts

// Public pages




// Admin pages

// Regular 'components'







/***/ }),

/***/ "2OUR":
/*!*********************************************************************!*\
  !*** ./src/pages/blog-posts-index-page/_blog-posts-index-page.scss ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "4tIy":
/*!***********************************************************************************************************!*\
  !*** ./src/admin/pages/arrangements-create-or-edit-page-base/_arrangements-create-or-edit-page-base.scss ***!
  \***********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "6gNi":
/*!*****************************!*\
  !*** ./src/bootstrapper.ts ***!
  \*****************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! jquery */ "EVdn");
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(jquery__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _pages_arrangements_index_page_arrangements_index_page__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./pages/arrangements-index-page/arrangements-index-page */ "bLcr");
/* harmony import */ var _pages_arrangements_details_page_arrangements_details_page__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./pages/arrangements-details-page/arrangements-details-page */ "mhd1");
/* harmony import */ var _admin_pages_arrangements_create_page_arrangements_create_page__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./admin/pages/arrangements-create-page/arrangements-create-page */ "HnfR");




// Required for the vue components
// import Vue from 'vue';
// import TravelplannerComponent from './app/travel-planner/travel-planner.component.vue';
// Initialize vue components
// if (document.getElementById('travelplanner') != null) {
//     new Vue({
//         el: '#travelplanner',
//         components: { TravelplannerComponent },
//         template: '<TravelplannerComponent/>'
//     });
// }
jquery__WEBPACK_IMPORTED_MODULE_0__(document).ready(function () {
    bootstrap();
});
function bootstrap() {
    // PUBLIC
    if (jquery__WEBPACK_IMPORTED_MODULE_0__('.pxl-arrangements-index-page').length > 0) {
        new _pages_arrangements_index_page_arrangements_index_page__WEBPACK_IMPORTED_MODULE_1__["IndexArrangementsPage"]().initPage();
    }
    if (jquery__WEBPACK_IMPORTED_MODULE_0__('.pxl-arrangements-details-page').length > 0) {
        new _pages_arrangements_details_page_arrangements_details_page__WEBPACK_IMPORTED_MODULE_2__["DetailsArrangementPage"]().initPage();
    }
    // ADMIN
    // if ($(".pxl-admin-page--cities-create").length > 0) {
    //     let page = new CreateCityPage();
    //     page.initPage();
    // }
    // let editCityPage = $(".pxl-admin-page--cities-edit");
    // if (editCityPage.length > 0) {
    //     let page = new EditCityPage();
    //     page.initPage(editCityPage.data("latitude"), editCityPage.data("longitude"));
    // }
    if (jquery__WEBPACK_IMPORTED_MODULE_0__('.pxl-arrangements-create-page').length > 0) {
        new _admin_pages_arrangements_create_page_arrangements_create_page__WEBPACK_IMPORTED_MODULE_3__["CreateArrangementPage"]().initPage();
    }
    // let editArrangementPage = $(".pxl-admin-page--arrangements-edit");
    // if (editArrangementPage.length > 0) {
    //     let page = new EditArrangementPage();
    //     page.initPage(editArrangementPage.data("latitude"), editArrangementPage.data("longitude"));
    // }
    // if ($(".pxl-admin-page--blog-posts-create").length > 0) {
    //     let page = new CreateBlogPostPage();
    //     page.initPage();
    // }
    // let editBlogPostPage = $(".pxl-admin-page--blog-posts-edit");
    // if (editBlogPostPage.length > 0) {
    //     let page = new EditBlogPostPage();
    //     page.initPage();
    // }
}


/***/ }),

/***/ "7Qv9":
/*!********************************************!*\
  !*** ./src/components/footer/_footer.scss ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "AO2n":
/*!*****************************************!*\
  !*** ./src/components/footer/footer.ts ***!
  \*****************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _footer_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_footer.scss */ "7Qv9");
/* harmony import */ var _footer_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_footer_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "BG8X":
/*!***************************************!*\
  !*** ./src/layouts/_main-layout.scss ***!
  \***************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "F6DD":
/*!******************************************************************!*\
  !*** ./src/pages/blog-posts-index-page/blog-posts-index-page.ts ***!
  \******************************************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _blog_posts_index_page_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_blog-posts-index-page.scss */ "2OUR");
/* harmony import */ var _blog_posts_index_page_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_blog_posts_index_page_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "G+IT":
/*!************************************!*\
  !*** ./src/layouts/main-layout.ts ***!
  \************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _main_layout_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_main-layout.scss */ "BG8X");
/* harmony import */ var _main_layout_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_main_layout_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "HnfR":
/*!******************************************************************************!*\
  !*** ./src/admin/pages/arrangements-create-page/arrangements-create-page.ts ***!
  \******************************************************************************/
/*! exports provided: CreateArrangementPage */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CreateArrangementPage", function() { return CreateArrangementPage; });
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! jquery */ "EVdn");
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(jquery__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _arrangements_create_or_edit_page_base_arrangements_create_or_edit_page_base__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../arrangements-create-or-edit-page-base/arrangements-create-or-edit-page-base */ "XIFe");
/* harmony import */ var _typescript_admin_utilities_slugify__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../typescript/admin/utilities/slugify */ "nUTY");
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();



var CreateArrangementPage = /** @class */ (function (_super) {
    __extends(CreateArrangementPage, _super);
    function CreateArrangementPage() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CreateArrangementPage.prototype.initPage = function () {
        jquery__WEBPACK_IMPORTED_MODULE_0__('#Name').change(function () {
            var val = jquery__WEBPACK_IMPORTED_MODULE_0__('#Name').val();
            if (val) {
                jquery__WEBPACK_IMPORTED_MODULE_0__('#Slug').val(Object(_typescript_admin_utilities_slugify__WEBPACK_IMPORTED_MODULE_2__["slugify"])(val.toString()));
            }
        });
        CKEDITOR.replace('Description');
        CKEDITOR.replace('BookingConditions');
        CKEDITOR.replace('PriceInformation');
        this.initMap();
    };
    return CreateArrangementPage;
}(_arrangements_create_or_edit_page_base_arrangements_create_or_edit_page_base__WEBPACK_IMPORTED_MODULE_1__["CreateOrEditArrangementPageBase"]));



/***/ }),

/***/ "T2C1":
/*!**********************************************************************!*\
  !*** ./src/pages/blog-posts-details-page/blog-posts-details-page.ts ***!
  \**********************************************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _blog_posts_details_page_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_blog-posts-details-page.scss */ "+Dlt");
/* harmony import */ var _blog_posts_details_page_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_blog_posts_details_page_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "XIFe":
/*!********************************************************************************************************!*\
  !*** ./src/admin/pages/arrangements-create-or-edit-page-base/arrangements-create-or-edit-page-base.ts ***!
  \********************************************************************************************************/
/*! exports provided: CreateOrEditArrangementPageBase */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CreateOrEditArrangementPageBase", function() { return CreateOrEditArrangementPageBase; });
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! jquery */ "EVdn");
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(jquery__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _typescript_admin_utilities_constants__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../typescript/admin/utilities/constants */ "tAbC");
/* harmony import */ var _typescript_admin_utilities_googleMapsUtilities__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../typescript/admin/utilities/googleMapsUtilities */ "bfnI");
/* harmony import */ var _arrangements_create_or_edit_page_base_scss__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./_arrangements-create-or-edit-page-base.scss */ "4tIy");
/* harmony import */ var _arrangements_create_or_edit_page_base_scss__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_arrangements_create_or_edit_page_base_scss__WEBPACK_IMPORTED_MODULE_3__);
// tslint:disable: no-console




var CreateOrEditArrangementPageBase = /** @class */ (function () {
    function CreateOrEditArrangementPageBase() {
        this.geocoder = new google.maps.Geocoder();
        this.mapElement = jquery__WEBPACK_IMPORTED_MODULE_0__('#map')[0];
        this.map = new google.maps.Map(this.mapElement, {
            zoom: 4,
            center: { lat: 63.0, lng: 17.0 }
        });
        this.placesService = new google.maps.places.PlacesService(this.map);
    }
    CreateOrEditArrangementPageBase.prototype.initMap = function () {
        var _this = this;
        if (this.initialLatitude && this.initialLongitude) {
            this.updateMap(new google.maps.LatLng(this.initialLatitude, this.initialLongitude));
        }
        jquery__WEBPACK_IMPORTED_MODULE_0__('#btnGetCoordinatesFromAddress').click(function () {
            var lookupAddress = _this.getLookupAddress();
            _this.geocoder.geocode({ address: lookupAddress }, function (results, status) {
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
        jquery__WEBPACK_IMPORTED_MODULE_0__('#btnGetInformationFromGooglePlaces').click(function () {
            var val = jquery__WEBPACK_IMPORTED_MODULE_0__('#Name').val();
            if (!val) {
                return false;
            }
            var name = val.toString();
            if (!name) {
                return false;
            }
            _this.placesService.textSearch({ query: name }, function (results, status) {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    console.log("\"Tr\u00E4ffar p\u00E5 '" + name + "':\"");
                    for (var _i = 0, results_1 = results; _i < results_1.length; _i++) {
                        var entry = results_1[_i];
                        console.log("\t" + entry.name);
                    }
                    if (results[0].place_id) {
                        _this.placesService.getDetails({ placeId: results[0].place_id }, function (place, detailedStatus) {
                            if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                                _this.updateInformationFromGooglePlaces(place);
                            }
                            else {
                                console.log("\"Kunde inte h\u00E4mta detailjer f\u00F6r '" + results[0].place_id + "' pga " + detailedStatus + "\"");
                            }
                        });
                    }
                }
                else {
                    console.log("\"Inga tr\u00E4ffar p\u00E5 '" + name + "' pga " + status + "\"");
                }
            });
            return false;
        });
        jquery__WEBPACK_IMPORTED_MODULE_0__('#btnOpenCoverImage').click(function () {
            var val = jquery__WEBPACK_IMPORTED_MODULE_0__('#CoverImage').val();
            if (!val) {
                return false;
            }
            var url = val.toString();
            if (url) {
                jquery__WEBPACK_IMPORTED_MODULE_0__('<a>')
                    .attr('href', url)
                    .attr('target', '_blank')[0]
                    .click();
            }
            return false;
        });
        jquery__WEBPACK_IMPORTED_MODULE_0__('#btnGetImagesFromGooglePlaces').click(function () {
            var placeId = jquery__WEBPACK_IMPORTED_MODULE_0__('#GooglePlacesId').val();
            if (!placeId) {
                console.log('No Google Places id specified');
                return false;
            }
            _this.placesService.getDetails({ placeId: placeId.toString() }, function (place, detailedStatus) {
                if (detailedStatus === google.maps.places.PlacesServiceStatus.OK) {
                    _this.updateImageList(place, false);
                }
                else {
                    console.log("\"Kunde inte h\u00E4mta detailjer f\u00F6r '" + placeId + "' pga " + detailedStatus + "\"");
                }
            });
            return false;
        });
        jquery__WEBPACK_IMPORTED_MODULE_0__('#btnOpenWebsite').click(function () {
            var val = jquery__WEBPACK_IMPORTED_MODULE_0__('#Website').val();
            if (val) {
                jquery__WEBPACK_IMPORTED_MODULE_0__('<a>')
                    .attr('href', val.toString())
                    .attr('target', '_blank')[0]
                    .click();
            }
            return false;
        });
    };
    CreateOrEditArrangementPageBase.prototype.updateInformationFromGooglePlaces = function (place) {
        if (place.place_id) {
            jquery__WEBPACK_IMPORTED_MODULE_0__('#GooglePlacesId').val(place.place_id);
        }
        this.updateImageList(place, true);
        if (place.address_components) {
            this.updateStreetAddress(place.address_components, 'StreetAddress');
            this.updateAddressComponent(place.address_components, 'postal_code', 'PostalCode');
            this.updateAddressComponent(place.address_components, 'postal_town', 'PostalCity');
            this.updateAddressComponent(place.address_components, 'street_address', 'StreetAddress');
        }
        if (place.formatted_phone_number) {
            jquery__WEBPACK_IMPORTED_MODULE_0__('#PhoneNumber').val(place.formatted_phone_number);
        }
        if (place.website) {
            jquery__WEBPACK_IMPORTED_MODULE_0__('#Website').val(place.website.replace(/\/+$/, ''));
        }
        if (place.geometry && place.geometry.location) {
            this.updateLatLngTextboxes(place.geometry.location);
            this.updateMap(place.geometry.location);
        }
        jquery__WEBPACK_IMPORTED_MODULE_0__('#GooglePlacesName').text(place.name);
        if (place.geometry) {
            jquery__WEBPACK_IMPORTED_MODULE_0__["get"]("/api/cities/closest?latitude=" + place.geometry.location.lat() + "&longitude=" + place.geometry.location.lng(), function (data) {
                jquery__WEBPACK_IMPORTED_MODULE_0__('#CitySlug').val(data.slug);
            });
        }
    };
    CreateOrEditArrangementPageBase.prototype.updateImageList = function (place, setCoverImage) {
        var _this = this;
        var listElementInDom = jquery__WEBPACK_IMPORTED_MODULE_0__('#lstImagesFromGooglePlaces');
        listElementInDom.addClass('d-none');
        listElementInDom.empty();
        if (place.photos && place.photos.length > 0) {
            listElementInDom.removeClass('d-none');
            for (var _i = 0, _a = place.photos; _i < _a.length; _i++) {
                var photo = _a[_i];
                listElementInDom.append("<img\n                    class=\"\"\n                    data-url=\"" + photo.getUrl({ maxWidth: 812 }) + "\" src=\"" + photo.getUrl({ maxWidth: 600, maxHeight: 600 }) + "\"\n                    data-html=\"" + this.makeAttributionsHtmlList(photo.html_attributions) + "\"\n                >");
            }
            // $('#lstImagesFromGooglePlaces .image').popup();
            jquery__WEBPACK_IMPORTED_MODULE_0__('#lstImagesFromGooglePlaces img').click(function (e) {
                _this.updateCoverImageUrl(jquery__WEBPACK_IMPORTED_MODULE_0__(e.currentTarget).data('url'));
                _this.updateCoverImageAttributions(jquery__WEBPACK_IMPORTED_MODULE_0__(e.currentTarget).data('html'));
                return false;
            });
            if (setCoverImage) {
                this.updateCoverImageUrl(place.photos[0].getUrl({ maxWidth: 812 }));
                this.updateCoverImageAttributions(this.makeAttributionsHtmlList(place.photos[0].html_attributions));
            }
        }
    };
    CreateOrEditArrangementPageBase.prototype.makeAttributionsHtmlList = function (htmlAttributions) {
        var attributions = '';
        for (var _i = 0, htmlAttributions_1 = htmlAttributions; _i < htmlAttributions_1.length; _i++) {
            var attrib = htmlAttributions_1[_i];
            attributions += attrib + ", ";
        }
        if (attributions.length >= 2) {
            attributions = attributions.substring(0, attributions.length - 2);
        }
        return attributions.replace(/"/g, '\'');
    };
    CreateOrEditArrangementPageBase.prototype.updateCoverImageUrl = function (url) {
        jquery__WEBPACK_IMPORTED_MODULE_0__('#CoverImage').val(url);
    };
    CreateOrEditArrangementPageBase.prototype.updateCoverImageAttributions = function (attributions) {
        jquery__WEBPACK_IMPORTED_MODULE_0__('#CoverImageAttributions').val(attributions);
    };
    CreateOrEditArrangementPageBase.prototype.updateAddressComponent = function (addressComponents, googleName, fieldId) {
        var value = _typescript_admin_utilities_googleMapsUtilities__WEBPACK_IMPORTED_MODULE_2__["GoogleMapsUtilties"].getAddressComponent(addressComponents, googleName);
        if (value) {
            jquery__WEBPACK_IMPORTED_MODULE_0__("#" + fieldId).val(value);
        }
    };
    CreateOrEditArrangementPageBase.prototype.updateStreetAddress = function (addressComponents, fieldId) {
        var streetAddress = _typescript_admin_utilities_googleMapsUtilities__WEBPACK_IMPORTED_MODULE_2__["GoogleMapsUtilties"].getAddressComponent(addressComponents, 'street_address');
        var route = _typescript_admin_utilities_googleMapsUtilities__WEBPACK_IMPORTED_MODULE_2__["GoogleMapsUtilties"].getAddressComponent(addressComponents, 'route');
        var streetNumber = _typescript_admin_utilities_googleMapsUtilities__WEBPACK_IMPORTED_MODULE_2__["GoogleMapsUtilties"].getAddressComponent(addressComponents, 'street_number');
        var result = streetAddress ? streetAddress : route;
        if (streetNumber) {
            result = result + ' ' + streetNumber;
        }
        jquery__WEBPACK_IMPORTED_MODULE_0__("#" + fieldId).val(result);
    };
    CreateOrEditArrangementPageBase.prototype.getLookupAddress = function () {
        var streetAddress = jquery__WEBPACK_IMPORTED_MODULE_0__('#StreetAddress').val();
        var postalCode = jquery__WEBPACK_IMPORTED_MODULE_0__('#PostalCode').val();
        var postalCity = jquery__WEBPACK_IMPORTED_MODULE_0__('#PostalCity').val();
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
            this.marker.addListener('dragend', function (ev) {
                _this.updateLatLngTextboxes(ev.latLng);
            });
            this.map.setZoom(11);
        }
        this.map.setCenter(location);
    };
    CreateOrEditArrangementPageBase.prototype.updateLatLngTextboxes = function (location) {
        jquery__WEBPACK_IMPORTED_MODULE_0__('#Latitude').val(location.lat().toLocaleString(_typescript_admin_utilities_constants__WEBPACK_IMPORTED_MODULE_1__["Constants"].locale, { maximumFractionDigits: 14 }));
        jquery__WEBPACK_IMPORTED_MODULE_0__('#Longitude').val(location.lng().toLocaleString(_typescript_admin_utilities_constants__WEBPACK_IMPORTED_MODULE_1__["Constants"].locale, { maximumFractionDigits: 14 }));
    };
    return CreateOrEditArrangementPageBase;
}());



/***/ }),

/***/ "bLcr":
/*!**********************************************************************!*\
  !*** ./src/pages/arrangements-index-page/arrangements-index-page.ts ***!
  \**********************************************************************/
/*! exports provided: IndexArrangementsPage */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IndexArrangementsPage", function() { return IndexArrangementsPage; });
/* harmony import */ var _arrangements_index_page_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_arrangements-index-page.scss */ "xvu1");
/* harmony import */ var _arrangements_index_page_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_arrangements_index_page_scss__WEBPACK_IMPORTED_MODULE_0__);

var IndexArrangementsPage = /** @class */ (function () {
    function IndexArrangementsPage() {
    }
    IndexArrangementsPage.prototype.initPage = function () {
        $('select#typeFilter').change(function (ev) {
            var _this = this;
            var selectedType = $(this).val();
            if (!selectedType) {
                $('.pxl-arrangement').show();
            }
            else {
                $('.pxl-arrangement').each(function () { $(_this).toggle($(_this).data('type') === selectedType); });
            }
            $('#arrangementCount').text($('.pxl-arrangement:visible').length);
            if (window.ga) {
                ga('send', 'event', {
                    eventCategory: 'Search',
                    eventAction: 'typeFilter',
                    eventLabel: selectedType,
                    transport: 'beacon'
                });
            }
        });
    };
    return IndexArrangementsPage;
}());



/***/ }),

/***/ "bfnI":
/*!***************************************************************!*\
  !*** ./src/typescript/admin/utilities/googleMapsUtilities.ts ***!
  \***************************************************************/
/*! exports provided: GoogleMapsUtilties */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoogleMapsUtilties", function() { return GoogleMapsUtilties; });
var GoogleMapsUtilties = /** @class */ (function () {
    function GoogleMapsUtilties() {
    }
    GoogleMapsUtilties.getAddressComponent = function (addressComponents, componentName) {
        for (var _i = 0, addressComponents_1 = addressComponents; _i < addressComponents_1.length; _i++) {
            var addressComponent = addressComponents_1[_i];
            for (var _a = 0, _b = addressComponent.types; _a < _b.length; _a++) {
                var type = _b[_a];
                if (type === componentName) {
                    return addressComponent.long_name;
                }
            }
        }
        return '';
    };
    return GoogleMapsUtilties;
}());



/***/ }),

/***/ "dWim":
/*!*****************************************!*\
  !*** ./src/scss/bootstrap/_config.scss ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "gInr":
/*!******************************************************!*\
  !*** ./src/components/cookie-info/_cookie-info.scss ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "gnk4":
/*!****************************************!*\
  !*** ./src/components/hero/_hero.scss ***!
  \****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "hrqH":
/*!***************************************************!*\
  !*** ./src/components/cookie-info/cookie-info.ts ***!
  \***************************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _cookie_info_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_cookie-info.scss */ "gInr");
/* harmony import */ var _cookie_info_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_cookie_info_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "mhd1":
/*!**************************************************************************!*\
  !*** ./src/pages/arrangements-details-page/arrangements-details-page.ts ***!
  \**************************************************************************/
/*! exports provided: DetailsArrangementPage */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DetailsArrangementPage", function() { return DetailsArrangementPage; });
var DetailsArrangementPage = /** @class */ (function () {
    function DetailsArrangementPage() {
    }
    DetailsArrangementPage.prototype.initPage = function () {
        var _this = this;
        $('#emailAddressLink').click(function () {
            ga('send', 'event', {
                eventCategory: 'Contact',
                eventAction: 'mail',
                eventLabel: $(_this).attr('href'),
                transport: 'beacon'
            });
        });
        $('#externalWebsiteLink').click(function () {
            ga('send', 'event', {
                eventCategory: 'Outbound Link',
                eventAction: 'click',
                eventLabel: $(_this).attr('href'),
                transport: 'beacon'
            });
        });
    };
    return DetailsArrangementPage;
}());



/***/ }),

/***/ "nUTY":
/*!***************************************************!*\
  !*** ./src/typescript/admin/utilities/slugify.ts ***!
  \***************************************************/
/*! exports provided: slugify */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "slugify", function() { return slugify; });
// https://gist.github.com/mathewbyrne/1280286
function slugify(text) {
    var a = 'àáåäâèéëêìíïîòóöôùúüûñçßÿœæŕśńṕẃǵǹḿǘẍźḧ·/_,:;';
    var b = 'aaaaaeeeeiiiioooouuuuncsyoarsnpwgnmuxzh------';
    var p = new RegExp(a.split('').join('|'), 'g');
    return text.toString().toLowerCase()
        .replace(/\s+/g, '-') // Replace spaces with -
        .replace(p, function (c) {
        return b.charAt(a.indexOf(c));
    }) // Replace special chars
        .replace(/&/g, '-and-') // Replace & with 'and'
        .replace(/[^\w\-]+/g, '') // Remove all non-word chars
        .replace(/\-\-+/g, '-') // Replace multiple - with single -
        .replace(/^-+/, '') // Trim - from start of text
        .replace(/-+$/, ''); // Trim - from end of text
}


/***/ }),

/***/ "tAbC":
/*!*****************************************************!*\
  !*** ./src/typescript/admin/utilities/constants.ts ***!
  \*****************************************************/
/*! exports provided: Constants */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Constants", function() { return Constants; });
var Constants = /** @class */ (function () {
    function Constants() {
    }
    Constants.locale = 'sv';
    return Constants;
}());



/***/ }),

/***/ "tLqx":
/*!*************************************!*\
  !*** ./src/components/hero/hero.ts ***!
  \*************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _hero_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_hero.scss */ "gnk4");
/* harmony import */ var _hero_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_hero_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "vPR2":
/*!*****************************************!*\
  !*** ./src/components/header/header.ts ***!
  \*****************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _header_scss__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./_header.scss */ "xtaw");
/* harmony import */ var _header_scss__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_header_scss__WEBPACK_IMPORTED_MODULE_0__);



/***/ }),

/***/ "xtaw":
/*!********************************************!*\
  !*** ./src/components/header/_header.scss ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),

/***/ "xvu1":
/*!*************************************************************************!*\
  !*** ./src/pages/arrangements-index-page/_arrangements-index-page.scss ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ })

},[["/7QA","runtime","vendors"]]]);
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9zcmMvcGFnZXMvYmxvZy1wb3N0cy1kZXRhaWxzLXBhZ2UvX2Jsb2ctcG9zdHMtZGV0YWlscy1wYWdlLnNjc3MiLCJ3ZWJwYWNrOi8vLy4vc3JjL2luZGV4LnRzIiwid2VicGFjazovLy8uL3NyYy9wYWdlcy9ibG9nLXBvc3RzLWluZGV4LXBhZ2UvX2Jsb2ctcG9zdHMtaW5kZXgtcGFnZS5zY3NzIiwid2VicGFjazovLy8uL3NyYy9hZG1pbi9wYWdlcy9hcnJhbmdlbWVudHMtY3JlYXRlLW9yLWVkaXQtcGFnZS1iYXNlL19hcnJhbmdlbWVudHMtY3JlYXRlLW9yLWVkaXQtcGFnZS1iYXNlLnNjc3MiLCJ3ZWJwYWNrOi8vLy4vc3JjL2Jvb3RzdHJhcHBlci50cyIsIndlYnBhY2s6Ly8vLi9zcmMvY29tcG9uZW50cy9mb290ZXIvX2Zvb3Rlci5zY3NzIiwid2VicGFjazovLy8uL3NyYy9jb21wb25lbnRzL2Zvb3Rlci9mb290ZXIudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL2xheW91dHMvX21haW4tbGF5b3V0LnNjc3MiLCJ3ZWJwYWNrOi8vLy4vc3JjL3BhZ2VzL2Jsb2ctcG9zdHMtaW5kZXgtcGFnZS9ibG9nLXBvc3RzLWluZGV4LXBhZ2UudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL2xheW91dHMvbWFpbi1sYXlvdXQudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL2FkbWluL3BhZ2VzL2FycmFuZ2VtZW50cy1jcmVhdGUtcGFnZS9hcnJhbmdlbWVudHMtY3JlYXRlLXBhZ2UudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL3BhZ2VzL2Jsb2ctcG9zdHMtZGV0YWlscy1wYWdlL2Jsb2ctcG9zdHMtZGV0YWlscy1wYWdlLnRzIiwid2VicGFjazovLy8uL3NyYy9hZG1pbi9wYWdlcy9hcnJhbmdlbWVudHMtY3JlYXRlLW9yLWVkaXQtcGFnZS1iYXNlL2FycmFuZ2VtZW50cy1jcmVhdGUtb3ItZWRpdC1wYWdlLWJhc2UudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL3BhZ2VzL2FycmFuZ2VtZW50cy1pbmRleC1wYWdlL2FycmFuZ2VtZW50cy1pbmRleC1wYWdlLnRzIiwid2VicGFjazovLy8uL3NyYy90eXBlc2NyaXB0L2FkbWluL3V0aWxpdGllcy9nb29nbGVNYXBzVXRpbGl0aWVzLnRzIiwid2VicGFjazovLy8uL3NyYy9zY3NzL2Jvb3RzdHJhcC9fY29uZmlnLnNjc3MiLCJ3ZWJwYWNrOi8vLy4vc3JjL2NvbXBvbmVudHMvY29va2llLWluZm8vX2Nvb2tpZS1pbmZvLnNjc3MiLCJ3ZWJwYWNrOi8vLy4vc3JjL2NvbXBvbmVudHMvaGVyby9faGVyby5zY3NzIiwid2VicGFjazovLy8uL3NyYy9jb21wb25lbnRzL2Nvb2tpZS1pbmZvL2Nvb2tpZS1pbmZvLnRzIiwid2VicGFjazovLy8uL3NyYy9wYWdlcy9hcnJhbmdlbWVudHMtZGV0YWlscy1wYWdlL2FycmFuZ2VtZW50cy1kZXRhaWxzLXBhZ2UudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL3R5cGVzY3JpcHQvYWRtaW4vdXRpbGl0aWVzL3NsdWdpZnkudHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL3R5cGVzY3JpcHQvYWRtaW4vdXRpbGl0aWVzL2NvbnN0YW50cy50cyIsIndlYnBhY2s6Ly8vLi9zcmMvY29tcG9uZW50cy9oZXJvL2hlcm8udHMiLCJ3ZWJwYWNrOi8vLy4vc3JjL2NvbXBvbmVudHMvaGVhZGVyL2hlYWRlci50cyIsIndlYnBhY2s6Ly8vLi9zcmMvY29tcG9uZW50cy9oZWFkZXIvX2hlYWRlci5zY3NzIiwid2VicGFjazovLy8uL3NyYy9wYWdlcy9hcnJhbmdlbWVudHMtaW5kZXgtcGFnZS9fYXJyYW5nZW1lbnRzLWluZGV4LXBhZ2Uuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7QUFBQSx1Qzs7Ozs7Ozs7Ozs7O0FDQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUFBLDBCQUEwQjtBQUNhO0FBQ3ZDLCtDQUEr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QUFDLENBQUMsTUFBTSxDQUFDLENBQUMsQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLENBQUM7SUFBdEIsQ0FBc0IsQ0FBQyxDQUFDLHdCQUF3QjtTQUNuRCxPQUFPLENBQUMsSUFBSSxFQUFFLE9BQU8sQ0FBQyxDQUFLLHVCQUF1QjtTQUNsRCxPQUFPLENBQUMsV0FBVyxFQUFFLEVBQUUsQ0FBQyxDQUFHLDRCQUE0QjtTQUN2RCxPQUFPLENBQUMsUUFBUSxFQUFFLEdBQUcsQ0FBQyxDQUFLLG1DQUFtQztTQUM5RCxPQUFPLENBQUMsS0FBSyxFQUFFLEVBQUUsQ0FBQyxDQUFTLDRCQUE0QjtTQUN2RCxPQUFPLENBQUMsS0FBSyxFQUFFLEVBQUUsQ0FBQyxDQUFDLENBQVEsMEJBQTBCO0FBQzlELENBQUM7Ozs7Ozs7Ozs7Ozs7QUNqQkQ7QUFBQTtBQUFBO0lBQUE7SUFFQSxDQUFDO0lBRGlCLGdCQUFNLEdBQUcsSUFBSSxDQUFDO0lBQ2hDLGdCQUFDO0NBQUE7QUFGcUI7Ozs7Ozs7Ozs7Ozs7QUNBdEI7QUFBQTtBQUFBO0FBQXNCOzs7Ozs7Ozs7Ozs7O0FDQXRCO0FBQUE7QUFBQTtBQUF3Qjs7Ozs7Ozs7Ozs7O0FDQXhCLHVDOzs7Ozs7Ozs7OztBQ0FBLHVDIiwiZmlsZSI6Im1haW4uanMiLCJzb3VyY2VzQ29udGVudCI6WyIvLyBleHRyYWN0ZWQgYnkgbWluaS1jc3MtZXh0cmFjdC1wbHVnaW4iLCIvLyBCb290c3RyYXAgY3VzdG9taXphdGlvblxyXG5pbXBvcnQgJy4vc2Nzcy9ib290c3RyYXAvX2NvbmZpZy5zY3NzJztcclxuLy8gaW1wb3J0ICcuL3Nhc3MvYm9vdHN0cmFwLWV4dC9fYnV0dG9ucy5zY3NzJztcclxuXHJcbi8vIEdsb2JhbCBzdHlsaW5nXHJcbi8vIGltcG9ydCAnLi9zYXNzL19idXR0b25zLnNjc3MnO1xyXG5cclxuLy8gTGF5b3V0c1xyXG5pbXBvcnQgJy4vbGF5b3V0cy9tYWluLWxheW91dCc7XHJcblxyXG4vLyBQdWJsaWMgcGFnZXNcclxuaW1wb3J0ICcuL3BhZ2VzL2FycmFuZ2VtZW50cy1pbmRleC1wYWdlL2FycmFuZ2VtZW50cy1pbmRleC1wYWdlJztcclxuaW1wb3J0ICcuL3BhZ2VzL2FycmFuZ2VtZW50cy1kZXRhaWxzLXBhZ2UvYXJyYW5nZW1lbnRzLWRldGFpbHMtcGFnZSc7XHJcbmltcG9ydCAnLi9wYWdlcy9ibG9nLXBvc3RzLWluZGV4LXBhZ2UvYmxvZy1wb3N0cy1pbmRleC1wYWdlJztcclxuaW1wb3J0ICcuL3BhZ2VzL2Jsb2ctcG9zdHMtZGV0YWlscy1wYWdlL2Jsb2ctcG9zdHMtZGV0YWlscy1wYWdlJztcclxuXHJcbi8vIEFkbWluIHBhZ2VzXHJcbmltcG9ydCAnLi9hZG1pbi9wYWdlcy9hcnJhbmdlbWVudHMtY3JlYXRlLXBhZ2UvYXJyYW5nZW1lbnRzLWNyZWF0ZS1wYWdlJztcclxuXHJcbi8vIFJlZ3VsYXIgJ2NvbXBvbmVudHMnXHJcbmltcG9ydCAnLi9jb21wb25lbnRzL2hlYWRlci9oZWFkZXInO1xyXG5pbXBvcnQgJy4vY29tcG9uZW50cy9mb290ZXIvZm9vdGVyJztcclxuaW1wb3J0ICcuL2NvbXBvbmVudHMvY29va2llLWluZm8vY29va2llLWluZm8nO1xyXG5pbXBvcnQgJy4vY29tcG9uZW50cy9oZXJvL2hlcm8nO1xyXG5cclxuaW1wb3J0ICcuL2Jvb3RzdHJhcHBlcic7XHJcbiIsIi8vIGV4dHJhY3RlZCBieSBtaW5pLWNzcy1leHRyYWN0LXBsdWdpbiIsIi8vIGV4dHJhY3RlZCBieSBtaW5pLWNzcy1leHRyYWN0LXBsdWdpbiIsImltcG9ydCAqIGFzICQgZnJvbSAnanF1ZXJ5JztcclxuXHJcbmltcG9ydCB7IEluZGV4QXJyYW5nZW1lbnRzUGFnZSB9IGZyb20gJy4vcGFnZXMvYXJyYW5nZW1lbnRzLWluZGV4LXBhZ2UvYXJyYW5nZW1lbnRzLWluZGV4LXBhZ2UnO1xyXG5pbXBvcnQgeyBEZXRhaWxzQXJyYW5nZW1lbnRQYWdlIH0gZnJvbSAnLi9wYWdlcy9hcnJhbmdlbWVudHMtZGV0YWlscy1wYWdlL2FycmFuZ2VtZW50cy1kZXRhaWxzLXBhZ2UnO1xyXG5pbXBvcnQgeyBDcmVhdGVBcnJhbmdlbWVudFBhZ2UgfSBmcm9tICcuL2FkbWluL3BhZ2VzL2FycmFuZ2VtZW50cy1jcmVhdGUtcGFnZS9hcnJhbmdlbWVudHMtY3JlYXRlLXBhZ2UnO1xyXG5cclxuLy8gUmVxdWlyZWQgZm9yIHRoZSB2dWUgY29tcG9uZW50c1xyXG4vLyBpbXBvcnQgVnVlIGZyb20gJ3Z1ZSc7XHJcbi8vIGltcG9ydCBUcmF2ZWxwbGFubmVyQ29tcG9uZW50IGZyb20gJy4vYXBwL3RyYXZlbC1wbGFubmVyL3RyYXZlbC1wbGFubmVyLmNvbXBvbmVudC52dWUnO1xyXG5cclxuLy8gSW5pdGlhbGl6ZSB2dWUgY29tcG9uZW50c1xyXG4vLyBpZiAoZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoJ3RyYXZlbHBsYW5uZXInKSAhPSBudWxsKSB7XHJcbi8vICAgICBuZXcgVnVlKHtcclxuLy8gICAgICAgICBlbDogJyN0cmF2ZWxwbGFubmVyJyxcclxuLy8gICAgICAgICBjb21wb25lbnRzOiB7IFRyYXZlbHBsYW5uZXJDb21wb25lbnQgfSxcclxuLy8gICAgICAgICB0ZW1wbGF0ZTogJzxUcmF2ZWxwbGFubmVyQ29tcG9uZW50Lz4nXHJcbi8vICAgICB9KTtcclxuLy8gfVxyXG5cclxuJChkb2N1bWVudCkucmVhZHkoKCkgPT4ge1xyXG4gICAgYm9vdHN0cmFwKCk7XHJcbn0pO1xyXG5cclxuZnVuY3Rpb24gYm9vdHN0cmFwKCkge1xyXG4gICAgLy8gUFVCTElDXHJcbiAgICBpZiAoJCgnLnB4bC1hcnJhbmdlbWVudHMtaW5kZXgtcGFnZScpLmxlbmd0aCA+IDApIHtcclxuICAgICAgICBuZXcgSW5kZXhBcnJhbmdlbWVudHNQYWdlKCkuaW5pdFBhZ2UoKTtcclxuICAgIH1cclxuXHJcbiAgICBpZiAoJCgnLnB4bC1hcnJhbmdlbWVudHMtZGV0YWlscy1wYWdlJykubGVuZ3RoID4gMCkge1xyXG4gICAgICAgIG5ldyBEZXRhaWxzQXJyYW5nZW1lbnRQYWdlKCkuaW5pdFBhZ2UoKTtcclxuICAgIH1cclxuXHJcbiAgICAvLyBBRE1JTlxyXG4gICAgLy8gaWYgKCQoXCIucHhsLWFkbWluLXBhZ2UtLWNpdGllcy1jcmVhdGVcIikubGVuZ3RoID4gMCkge1xyXG4gICAgLy8gICAgIGxldCBwYWdlID0gbmV3IENyZWF0ZUNpdHlQYWdlKCk7XHJcbiAgICAvLyAgICAgcGFnZS5pbml0UGFnZSgpO1xyXG4gICAgLy8gfVxyXG5cclxuICAgIC8vIGxldCBlZGl0Q2l0eVBhZ2UgPSAkKFwiLnB4bC1hZG1pbi1wYWdlLS1jaXRpZXMtZWRpdFwiKTtcclxuICAgIC8vIGlmIChlZGl0Q2l0eVBhZ2UubGVuZ3RoID4gMCkge1xyXG4gICAgLy8gICAgIGxldCBwYWdlID0gbmV3IEVkaXRDaXR5UGFnZSgpO1xyXG4gICAgLy8gICAgIHBhZ2UuaW5pdFBhZ2UoZWRpdENpdHlQYWdlLmRhdGEoXCJsYXRpdHVkZVwiKSwgZWRpdENpdHlQYWdlLmRhdGEoXCJsb25naXR1ZGVcIikpO1xyXG4gICAgLy8gfVxyXG5cclxuICAgIGlmICgkKCcucHhsLWFycmFuZ2VtZW50cy1jcmVhdGUtcGFnZScpLmxlbmd0aCA+IDApIHtcclxuICAgICAgICBuZXcgQ3JlYXRlQXJyYW5nZW1lbnRQYWdlKCkuaW5pdFBhZ2UoKTtcclxuICAgIH1cclxuXHJcbiAgICAvLyBsZXQgZWRpdEFycmFuZ2VtZW50UGFnZSA9ICQoXCIucHhsLWFkbWluLXBhZ2UtLWFycmFuZ2VtZW50cy1lZGl0XCIpO1xyXG4gICAgLy8gaWYgKGVkaXRBcnJhbmdlbWVudFBhZ2UubGVuZ3RoID4gMCkge1xyXG4gICAgLy8gICAgIGxldCBwYWdlID0gbmV3IEVkaXRBcnJhbmdlbWVudFBhZ2UoKTtcclxuICAgIC8vICAgICBwYWdlLmluaXRQYWdlKGVkaXRBcnJhbmdlbWVudFBhZ2UuZGF0YShcImxhdGl0dWRlXCIpLCBlZGl0QXJyYW5nZW1lbnRQYWdlLmRhdGEoXCJsb25naXR1ZGVcIikpO1xyXG4gICAgLy8gfVxyXG5cclxuICAgIC8vIGlmICgkKFwiLnB4bC1hZG1pbi1wYWdlLS1ibG9nLXBvc3RzLWNyZWF0ZVwiKS5sZW5ndGggPiAwKSB7XHJcbiAgICAvLyAgICAgbGV0IHBhZ2UgPSBuZXcgQ3JlYXRlQmxvZ1Bvc3RQYWdlKCk7XHJcbiAgICAvLyAgICAgcGFnZS5pbml0UGFnZSgpO1xyXG4gICAgLy8gfVxyXG5cclxuICAgIC8vIGxldCBlZGl0QmxvZ1Bvc3RQYWdlID0gJChcIi5weGwtYWRtaW4tcGFnZS0tYmxvZy1wb3N0cy1lZGl0XCIpO1xyXG4gICAgLy8gaWYgKGVkaXRCbG9nUG9zdFBhZ2UubGVuZ3RoID4gMCkge1xyXG4gICAgLy8gICAgIGxldCBwYWdlID0gbmV3IEVkaXRCbG9nUG9zdFBhZ2UoKTtcclxuICAgIC8vICAgICBwYWdlLmluaXRQYWdlKCk7XHJcbiAgICAvLyB9XHJcbn1cclxuIiwiLy8gZXh0cmFjdGVkIGJ5IG1pbmktY3NzLWV4dHJhY3QtcGx1Z2luIiwiaW1wb3J0ICcuL19mb290ZXIuc2Nzcyc7XHJcbiIsIi8vIGV4dHJhY3RlZCBieSBtaW5pLWNzcy1leHRyYWN0LXBsdWdpbiIsImltcG9ydCAnLi9fYmxvZy1wb3N0cy1pbmRleC1wYWdlLnNjc3MnO1xyXG4iLCJpbXBvcnQgJy4vX21haW4tbGF5b3V0LnNjc3MnO1xyXG4iLCJpbXBvcnQgKiBhcyAkIGZyb20gJ2pxdWVyeSc7XHJcblxyXG5pbXBvcnQgeyBDcmVhdGVPckVkaXRBcnJhbmdlbWVudFBhZ2VCYXNlIH0gZnJvbSAnLi4vYXJyYW5nZW1lbnRzLWNyZWF0ZS1vci1lZGl0LXBhZ2UtYmFzZS9hcnJhbmdlbWVudHMtY3JlYXRlLW9yLWVkaXQtcGFnZS1iYXNlJztcclxuaW1wb3J0IHsgc2x1Z2lmeSB9IGZyb20gJy4uLy4uLy4uL3R5cGVzY3JpcHQvYWRtaW4vdXRpbGl0aWVzL3NsdWdpZnknO1xyXG5cclxuZXhwb3J0IGNsYXNzIENyZWF0ZUFycmFuZ2VtZW50UGFnZSBleHRlbmRzIENyZWF0ZU9yRWRpdEFycmFuZ2VtZW50UGFnZUJhc2Uge1xyXG4gICAgcHVibGljIGluaXRQYWdlKCkge1xyXG4gICAgICAgICQoJyNOYW1lJykuY2hhbmdlKCgpID0+IHtcclxuICAgICAgICAgICAgY29uc3QgdmFsID0gJCgnI05hbWUnKS52YWwoKTtcclxuICAgICAgICAgICAgaWYgKHZhbCkge1xyXG4gICAgICAgICAgICAgICAgJCgnI1NsdWcnKS52YWwoc2x1Z2lmeSh2YWwudG9TdHJpbmcoKSkpO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgfSk7XHJcbiAgICAgICAgQ0tFRElUT1IucmVwbGFjZSgnRGVzY3JpcHRpb24nKTtcclxuICAgICAgICBDS0VESVRPUi5yZXBsYWNlKCdCb29raW5nQ29uZGl0aW9ucycpO1xyXG4gICAgICAgIENLRURJVE9SLnJlcGxhY2UoJ1ByaWNlSW5mb3JtYXRpb24nKTtcclxuXHJcbiAgICAgICAgdGhpcy5pbml0TWFwKCk7XHJcbiAgICB9XHJcbn1cclxuIiwiaW1wb3J0ICcuL19ibG9nLXBvc3RzLWRldGFpbHMtcGFnZS5zY3NzJztcclxuIiwiLy8gdHNsaW50OmRpc2FibGU6IG5vLWNvbnNvbGVcclxuaW1wb3J0ICogYXMgJCBmcm9tICdqcXVlcnknO1xyXG5cclxuaW1wb3J0IHsgQ29uc3RhbnRzIH0gZnJvbSAnLi4vLi4vLi4vdHlwZXNjcmlwdC9hZG1pbi91dGlsaXRpZXMvY29uc3RhbnRzJztcclxuaW1wb3J0IHsgR29vZ2xlTWFwc1V0aWx0aWVzIH0gZnJvbSAnLi4vLi4vLi4vdHlwZXNjcmlwdC9hZG1pbi91dGlsaXRpZXMvZ29vZ2xlTWFwc1V0aWxpdGllcyc7XHJcblxyXG5pbXBvcnQgJy4vX2FycmFuZ2VtZW50cy1jcmVhdGUtb3ItZWRpdC1wYWdlLWJhc2Uuc2Nzcyc7XHJcblxyXG5leHBvcnQgYWJzdHJhY3QgY2xhc3MgQ3JlYXRlT3JFZGl0QXJyYW5nZW1lbnRQYWdlQmFzZSB7XHJcbiAgICBwcm90ZWN0ZWQgaW5pdGlhbExhdGl0dWRlPzogbnVtYmVyO1xyXG4gICAgcHJvdGVjdGVkIGluaXRpYWxMb25naXR1ZGU/OiBudW1iZXI7XHJcblxyXG4gICAgcHJpdmF0ZSBtYXBFbGVtZW50OiBIVE1MRWxlbWVudDtcclxuXHJcbiAgICBwcml2YXRlIG1hcDogZ29vZ2xlLm1hcHMuTWFwO1xyXG4gICAgcHJpdmF0ZSBtYXJrZXI/OiBnb29nbGUubWFwcy5NYXJrZXI7XHJcbiAgICBwcml2YXRlIGdlb2NvZGVyOiBnb29nbGUubWFwcy5HZW9jb2RlcjtcclxuICAgIHByaXZhdGUgcGxhY2VzU2VydmljZTogZ29vZ2xlLm1hcHMucGxhY2VzLlBsYWNlc1NlcnZpY2U7XHJcblxyXG4gICAgY29uc3RydWN0b3IoKSB7XHJcbiAgICAgICAgdGhpcy5nZW9jb2RlciA9IG5ldyBnb29nbGUubWFwcy5HZW9jb2RlcigpO1xyXG4gICAgICAgIHRoaXMubWFwRWxlbWVudCA9ICQoJyNtYXAnKVswXTtcclxuICAgICAgICB0aGlzLm1hcCA9IG5ldyBnb29nbGUubWFwcy5NYXAodGhpcy5tYXBFbGVtZW50LCB7XHJcbiAgICAgICAgICAgIHpvb206IDQsXHJcbiAgICAgICAgICAgIGNlbnRlcjogeyBsYXQ6IDYzLjAsIGxuZzogMTcuMCB9XHJcbiAgICAgICAgfSk7XHJcbiAgICAgICAgdGhpcy5wbGFjZXNTZXJ2aWNlID0gbmV3IGdvb2dsZS5tYXBzLnBsYWNlcy5QbGFjZXNTZXJ2aWNlKHRoaXMubWFwKTtcclxuICAgIH1cclxuXHJcbiAgICBwcm90ZWN0ZWQgaW5pdE1hcCgpIHtcclxuICAgICAgICBpZiAodGhpcy5pbml0aWFsTGF0aXR1ZGUgJiYgdGhpcy5pbml0aWFsTG9uZ2l0dWRlKSB7XHJcbiAgICAgICAgICAgIHRoaXMudXBkYXRlTWFwKG5ldyBnb29nbGUubWFwcy5MYXRMbmcodGhpcy5pbml0aWFsTGF0aXR1ZGUsIHRoaXMuaW5pdGlhbExvbmdpdHVkZSkpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgJCgnI2J0bkdldENvb3JkaW5hdGVzRnJvbUFkZHJlc3MnKS5jbGljaygoKSA9PiB7XHJcbiAgICAgICAgICAgIGNvbnN0IGxvb2t1cEFkZHJlc3MgPSB0aGlzLmdldExvb2t1cEFkZHJlc3MoKTtcclxuICAgICAgICAgICAgdGhpcy5nZW9jb2Rlci5nZW9jb2RlKHsgYWRkcmVzczogbG9va3VwQWRkcmVzcyB9LCAocmVzdWx0cywgc3RhdHVzKSA9PiB7XHJcbiAgICAgICAgICAgICAgICBpZiAoc3RhdHVzID09PSBnb29nbGUubWFwcy5HZW9jb2RlclN0YXR1cy5PSykge1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMudXBkYXRlTWFwKHJlc3VsdHNbMF0uZ2VvbWV0cnkubG9jYXRpb24pO1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMudXBkYXRlTGF0TG5nVGV4dGJveGVzKHJlc3VsdHNbMF0uZ2VvbWV0cnkubG9jYXRpb24pO1xyXG4gICAgICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICAgICBjb25zb2xlLmxvZyhgS3VuZGUgaW50ZSBoaXR0YSBwb3NpdGlvbiBwZ2EgJHtzdGF0dXN9YCk7XHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgICQoJyNidG5HZXRJbmZvcm1hdGlvbkZyb21Hb29nbGVQbGFjZXMnKS5jbGljaygoKSA9PiB7XHJcbiAgICAgICAgICAgIGNvbnN0IHZhbCA9ICQoJyNOYW1lJykudmFsKCk7XHJcbiAgICAgICAgICAgIGlmICghdmFsKSB7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgIGNvbnN0IG5hbWUgPSB2YWwudG9TdHJpbmcoKTtcclxuXHJcbiAgICAgICAgICAgIGlmICghbmFtZSkge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIGZhbHNlO1xyXG4gICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICB0aGlzLnBsYWNlc1NlcnZpY2UudGV4dFNlYXJjaCh7IHF1ZXJ5OiBuYW1lIH0sIChyZXN1bHRzLCBzdGF0dXMpID0+IHtcclxuICAgICAgICAgICAgICAgIGlmIChzdGF0dXMgPT09IGdvb2dsZS5tYXBzLnBsYWNlcy5QbGFjZXNTZXJ2aWNlU3RhdHVzLk9LKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgY29uc29sZS5sb2coYFwiVHLDpGZmYXIgcMOlICcke25hbWV9JzpcImApO1xyXG4gICAgICAgICAgICAgICAgICAgIGZvciAoY29uc3QgZW50cnkgb2YgcmVzdWx0cykge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICBjb25zb2xlLmxvZyhgXFx0JHtlbnRyeS5uYW1lfWApO1xyXG4gICAgICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgICAgICAgICBpZiAocmVzdWx0c1swXS5wbGFjZV9pZCkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICB0aGlzLnBsYWNlc1NlcnZpY2UuZ2V0RGV0YWlscyh7IHBsYWNlSWQ6IHJlc3VsdHNbMF0ucGxhY2VfaWQgfSwgKHBsYWNlLCBkZXRhaWxlZFN0YXR1cykgPT4ge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgaWYgKGRldGFpbGVkU3RhdHVzID09PSBnb29nbGUubWFwcy5wbGFjZXMuUGxhY2VzU2VydmljZVN0YXR1cy5PSykge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIHRoaXMudXBkYXRlSW5mb3JtYXRpb25Gcm9tR29vZ2xlUGxhY2VzKHBsYWNlKTtcclxuICAgICAgICAgICAgICAgICAgICAgICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgY29uc29sZS5sb2coYFwiS3VuZGUgaW50ZSBow6RtdGEgZGV0YWlsamVyIGbDtnIgJyR7cmVzdWx0c1swXS5wbGFjZV9pZH0nIHBnYSAke2RldGFpbGVkU3RhdHVzfVwiYCk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgICAgICAgICAgY29uc29sZS5sb2coYFwiSW5nYSB0csOkZmZhciBww6UgJyR7bmFtZX0nIHBnYSAke3N0YXR1c31cImApO1xyXG4gICAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgcmV0dXJuIGZhbHNlO1xyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICAkKCcjYnRuT3BlbkNvdmVySW1hZ2UnKS5jbGljaygoKSA9PiB7XHJcbiAgICAgICAgICAgIGNvbnN0IHZhbCA9ICQoJyNDb3ZlckltYWdlJykudmFsKCk7XHJcbiAgICAgICAgICAgIGlmICghdmFsKSB7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgIGNvbnN0IHVybCA9IHZhbC50b1N0cmluZygpO1xyXG4gICAgICAgICAgICBpZiAodXJsKSB7XHJcbiAgICAgICAgICAgICAgICAkKCc8YT4nKVxyXG4gICAgICAgICAgICAgICAgICAgIC5hdHRyKCdocmVmJywgdXJsKVxyXG4gICAgICAgICAgICAgICAgICAgIC5hdHRyKCd0YXJnZXQnLCAnX2JsYW5rJylbMF1cclxuICAgICAgICAgICAgICAgICAgICAuY2xpY2soKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgICQoJyNidG5HZXRJbWFnZXNGcm9tR29vZ2xlUGxhY2VzJykuY2xpY2soKCkgPT4ge1xyXG4gICAgICAgICAgICBjb25zdCBwbGFjZUlkID0gJCgnI0dvb2dsZVBsYWNlc0lkJykudmFsKCk7XHJcbiAgICAgICAgICAgIGlmICghcGxhY2VJZCkge1xyXG4gICAgICAgICAgICAgICAgY29uc29sZS5sb2coJ05vIEdvb2dsZSBQbGFjZXMgaWQgc3BlY2lmaWVkJyk7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgIHRoaXMucGxhY2VzU2VydmljZS5nZXREZXRhaWxzKHsgcGxhY2VJZDogcGxhY2VJZC50b1N0cmluZygpIH0sIChwbGFjZSwgZGV0YWlsZWRTdGF0dXMpID0+IHtcclxuICAgICAgICAgICAgICAgIGlmIChkZXRhaWxlZFN0YXR1cyA9PT0gZ29vZ2xlLm1hcHMucGxhY2VzLlBsYWNlc1NlcnZpY2VTdGF0dXMuT0spIHtcclxuICAgICAgICAgICAgICAgICAgICB0aGlzLnVwZGF0ZUltYWdlTGlzdChwbGFjZSwgZmFsc2UpO1xyXG4gICAgICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICAgICBjb25zb2xlLmxvZyhgXCJLdW5kZSBpbnRlIGjDpG10YSBkZXRhaWxqZXIgZsO2ciAnJHtwbGFjZUlkfScgcGdhICR7ZGV0YWlsZWRTdGF0dXN9XCJgKTtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfSk7XHJcblxyXG4gICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgICQoJyNidG5PcGVuV2Vic2l0ZScpLmNsaWNrKCgpID0+IHtcclxuICAgICAgICAgICAgY29uc3QgdmFsID0gJCgnI1dlYnNpdGUnKS52YWwoKTtcclxuICAgICAgICAgICAgaWYgKHZhbCkge1xyXG4gICAgICAgICAgICAgICAgJCgnPGE+JylcclxuICAgICAgICAgICAgICAgICAgICAuYXR0cignaHJlZicsIHZhbC50b1N0cmluZygpKVxyXG4gICAgICAgICAgICAgICAgICAgIC5hdHRyKCd0YXJnZXQnLCAnX2JsYW5rJylbMF1cclxuICAgICAgICAgICAgICAgICAgICAuY2xpY2soKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSB1cGRhdGVJbmZvcm1hdGlvbkZyb21Hb29nbGVQbGFjZXMocGxhY2U6IGdvb2dsZS5tYXBzLnBsYWNlcy5QbGFjZVJlc3VsdCk6IHZvaWQge1xyXG4gICAgICAgIGlmIChwbGFjZS5wbGFjZV9pZCkge1xyXG4gICAgICAgICAgICAkKCcjR29vZ2xlUGxhY2VzSWQnKS52YWwocGxhY2UucGxhY2VfaWQpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgdGhpcy51cGRhdGVJbWFnZUxpc3QocGxhY2UsIHRydWUpO1xyXG5cclxuICAgICAgICBpZiAocGxhY2UuYWRkcmVzc19jb21wb25lbnRzKSB7XHJcbiAgICAgICAgICAgIHRoaXMudXBkYXRlU3RyZWV0QWRkcmVzcyhwbGFjZS5hZGRyZXNzX2NvbXBvbmVudHMsICdTdHJlZXRBZGRyZXNzJyk7XHJcbiAgICAgICAgICAgIHRoaXMudXBkYXRlQWRkcmVzc0NvbXBvbmVudChwbGFjZS5hZGRyZXNzX2NvbXBvbmVudHMsICdwb3N0YWxfY29kZScsICdQb3N0YWxDb2RlJyk7XHJcbiAgICAgICAgICAgIHRoaXMudXBkYXRlQWRkcmVzc0NvbXBvbmVudChwbGFjZS5hZGRyZXNzX2NvbXBvbmVudHMsICdwb3N0YWxfdG93bicsICdQb3N0YWxDaXR5Jyk7XHJcbiAgICAgICAgICAgIHRoaXMudXBkYXRlQWRkcmVzc0NvbXBvbmVudChwbGFjZS5hZGRyZXNzX2NvbXBvbmVudHMsICdzdHJlZXRfYWRkcmVzcycsICdTdHJlZXRBZGRyZXNzJyk7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBpZiAocGxhY2UuZm9ybWF0dGVkX3Bob25lX251bWJlcikge1xyXG4gICAgICAgICAgICAkKCcjUGhvbmVOdW1iZXInKS52YWwocGxhY2UuZm9ybWF0dGVkX3Bob25lX251bWJlcik7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBpZiAocGxhY2Uud2Vic2l0ZSkge1xyXG4gICAgICAgICAgICAkKCcjV2Vic2l0ZScpLnZhbChwbGFjZS53ZWJzaXRlLnJlcGxhY2UoL1xcLyskLywgJycpKTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGlmIChwbGFjZS5nZW9tZXRyeSAmJiBwbGFjZS5nZW9tZXRyeS5sb2NhdGlvbikge1xyXG4gICAgICAgICAgICB0aGlzLnVwZGF0ZUxhdExuZ1RleHRib3hlcyhwbGFjZS5nZW9tZXRyeS5sb2NhdGlvbik7XHJcbiAgICAgICAgICAgIHRoaXMudXBkYXRlTWFwKHBsYWNlLmdlb21ldHJ5LmxvY2F0aW9uKTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgICQoJyNHb29nbGVQbGFjZXNOYW1lJykudGV4dChwbGFjZS5uYW1lKTtcclxuXHJcbiAgICAgICAgaWYgKHBsYWNlLmdlb21ldHJ5KSB7XHJcbiAgICAgICAgICAgICQuZ2V0KGAvYXBpL2NpdGllcy9jbG9zZXN0P2xhdGl0dWRlPSR7cGxhY2UuZ2VvbWV0cnkubG9jYXRpb24ubGF0KCl9JmxvbmdpdHVkZT0ke3BsYWNlLmdlb21ldHJ5LmxvY2F0aW9uLmxuZygpfWAsIGRhdGEgPT4ge1xyXG4gICAgICAgICAgICAgICAgJCgnI0NpdHlTbHVnJykudmFsKGRhdGEuc2x1Zyk7XHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIHVwZGF0ZUltYWdlTGlzdChwbGFjZTogZ29vZ2xlLm1hcHMucGxhY2VzLlBsYWNlUmVzdWx0LCBzZXRDb3ZlckltYWdlOiBib29sZWFuKSB7XHJcbiAgICAgICAgY29uc3QgbGlzdEVsZW1lbnRJbkRvbSA9ICQoJyNsc3RJbWFnZXNGcm9tR29vZ2xlUGxhY2VzJyk7XHJcbiAgICAgICAgbGlzdEVsZW1lbnRJbkRvbS5hZGRDbGFzcygnZC1ub25lJyk7XHJcbiAgICAgICAgbGlzdEVsZW1lbnRJbkRvbS5lbXB0eSgpO1xyXG5cclxuICAgICAgICBpZiAocGxhY2UucGhvdG9zICYmIHBsYWNlLnBob3Rvcy5sZW5ndGggPiAwKSB7XHJcbiAgICAgICAgICAgIGxpc3RFbGVtZW50SW5Eb20ucmVtb3ZlQ2xhc3MoJ2Qtbm9uZScpO1xyXG4gICAgICAgICAgICBmb3IgKGNvbnN0IHBob3RvIG9mIHBsYWNlLnBob3Rvcykge1xyXG4gICAgICAgICAgICAgICAgbGlzdEVsZW1lbnRJbkRvbS5hcHBlbmQoYDxpbWdcclxuICAgICAgICAgICAgICAgICAgICBjbGFzcz1cXFwiXFxcIlxyXG4gICAgICAgICAgICAgICAgICAgIGRhdGEtdXJsPVxcXCIke3Bob3RvLmdldFVybCh7IG1heFdpZHRoOiA4MTIgfSl9XFxcIiBzcmM9XFxcIiR7cGhvdG8uZ2V0VXJsKHsgbWF4V2lkdGg6IDYwMCwgbWF4SGVpZ2h0OiA2MDAgfSl9XFxcIlxyXG4gICAgICAgICAgICAgICAgICAgIGRhdGEtaHRtbD1cXFwiJHt0aGlzLm1ha2VBdHRyaWJ1dGlvbnNIdG1sTGlzdChwaG90by5odG1sX2F0dHJpYnV0aW9ucyl9XFxcIlxyXG4gICAgICAgICAgICAgICAgPmApO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIC8vICQoJyNsc3RJbWFnZXNGcm9tR29vZ2xlUGxhY2VzIC5pbWFnZScpLnBvcHVwKCk7XHJcbiAgICAgICAgICAgICQoJyNsc3RJbWFnZXNGcm9tR29vZ2xlUGxhY2VzIGltZycpLmNsaWNrKGUgPT4ge1xyXG4gICAgICAgICAgICAgICAgdGhpcy51cGRhdGVDb3ZlckltYWdlVXJsKCQoZS5jdXJyZW50VGFyZ2V0KS5kYXRhKCd1cmwnKSk7XHJcbiAgICAgICAgICAgICAgICB0aGlzLnVwZGF0ZUNvdmVySW1hZ2VBdHRyaWJ1dGlvbnMoJChlLmN1cnJlbnRUYXJnZXQpLmRhdGEoJ2h0bWwnKSk7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICBpZiAoc2V0Q292ZXJJbWFnZSkge1xyXG4gICAgICAgICAgICAgICAgdGhpcy51cGRhdGVDb3ZlckltYWdlVXJsKHBsYWNlLnBob3Rvc1swXS5nZXRVcmwoeyBtYXhXaWR0aDogODEyIH0pKTtcclxuICAgICAgICAgICAgICAgIHRoaXMudXBkYXRlQ292ZXJJbWFnZUF0dHJpYnV0aW9ucyh0aGlzLm1ha2VBdHRyaWJ1dGlvbnNIdG1sTGlzdChwbGFjZS5waG90b3NbMF0uaHRtbF9hdHRyaWJ1dGlvbnMpKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIG1ha2VBdHRyaWJ1dGlvbnNIdG1sTGlzdChodG1sQXR0cmlidXRpb25zOiBzdHJpbmdbXSk6IHN0cmluZyB7XHJcbiAgICAgICAgbGV0IGF0dHJpYnV0aW9ucyA9ICcnO1xyXG4gICAgICAgIGZvciAoY29uc3QgYXR0cmliIG9mIGh0bWxBdHRyaWJ1dGlvbnMpIHtcclxuICAgICAgICAgICAgYXR0cmlidXRpb25zICs9IGAke2F0dHJpYn0sIGA7XHJcbiAgICAgICAgfVxyXG4gICAgICAgIGlmIChhdHRyaWJ1dGlvbnMubGVuZ3RoID49IDIpIHtcclxuICAgICAgICAgICAgYXR0cmlidXRpb25zID0gYXR0cmlidXRpb25zLnN1YnN0cmluZygwLCBhdHRyaWJ1dGlvbnMubGVuZ3RoIC0gMik7XHJcbiAgICAgICAgfVxyXG4gICAgICAgIHJldHVybiBhdHRyaWJ1dGlvbnMucmVwbGFjZSgvXCIvZywgJ1xcJycpO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgdXBkYXRlQ292ZXJJbWFnZVVybCh1cmw6IHN0cmluZykge1xyXG4gICAgICAgICQoJyNDb3ZlckltYWdlJykudmFsKHVybCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSB1cGRhdGVDb3ZlckltYWdlQXR0cmlidXRpb25zKGF0dHJpYnV0aW9uczogc3RyaW5nKSB7XHJcbiAgICAgICAgJCgnI0NvdmVySW1hZ2VBdHRyaWJ1dGlvbnMnKS52YWwoYXR0cmlidXRpb25zKTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIHVwZGF0ZUFkZHJlc3NDb21wb25lbnQoYWRkcmVzc0NvbXBvbmVudHM6IGdvb2dsZS5tYXBzLkdlb2NvZGVyQWRkcmVzc0NvbXBvbmVudFtdLCBnb29nbGVOYW1lOiBzdHJpbmcsIGZpZWxkSWQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGNvbnN0IHZhbHVlID0gR29vZ2xlTWFwc1V0aWx0aWVzLmdldEFkZHJlc3NDb21wb25lbnQoYWRkcmVzc0NvbXBvbmVudHMsIGdvb2dsZU5hbWUpO1xyXG4gICAgICAgIGlmICh2YWx1ZSkge1xyXG4gICAgICAgICAgICAkKGAjJHtmaWVsZElkfWApLnZhbCh2YWx1ZSk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgdXBkYXRlU3RyZWV0QWRkcmVzcyhhZGRyZXNzQ29tcG9uZW50czogZ29vZ2xlLm1hcHMuR2VvY29kZXJBZGRyZXNzQ29tcG9uZW50W10sIGZpZWxkSWQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGNvbnN0IHN0cmVldEFkZHJlc3MgPSBHb29nbGVNYXBzVXRpbHRpZXMuZ2V0QWRkcmVzc0NvbXBvbmVudChhZGRyZXNzQ29tcG9uZW50cywgJ3N0cmVldF9hZGRyZXNzJyk7XHJcbiAgICAgICAgY29uc3Qgcm91dGUgPSBHb29nbGVNYXBzVXRpbHRpZXMuZ2V0QWRkcmVzc0NvbXBvbmVudChhZGRyZXNzQ29tcG9uZW50cywgJ3JvdXRlJyk7XHJcbiAgICAgICAgY29uc3Qgc3RyZWV0TnVtYmVyID0gR29vZ2xlTWFwc1V0aWx0aWVzLmdldEFkZHJlc3NDb21wb25lbnQoYWRkcmVzc0NvbXBvbmVudHMsICdzdHJlZXRfbnVtYmVyJyk7XHJcblxyXG4gICAgICAgIGxldCByZXN1bHQgPSBzdHJlZXRBZGRyZXNzID8gc3RyZWV0QWRkcmVzcyA6IHJvdXRlO1xyXG4gICAgICAgIGlmIChzdHJlZXROdW1iZXIpIHtcclxuICAgICAgICAgICAgcmVzdWx0ID0gcmVzdWx0ICsgJyAnICsgc3RyZWV0TnVtYmVyO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgJChgIyR7ZmllbGRJZH1gKS52YWwocmVzdWx0KTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGdldExvb2t1cEFkZHJlc3MoKTogc3RyaW5nIHtcclxuICAgICAgICBjb25zdCBzdHJlZXRBZGRyZXNzID0gJCgnI1N0cmVldEFkZHJlc3MnKS52YWwoKTtcclxuICAgICAgICBjb25zdCBwb3N0YWxDb2RlID0gJCgnI1Bvc3RhbENvZGUnKS52YWwoKTtcclxuICAgICAgICBjb25zdCBwb3N0YWxDaXR5ID0gJCgnI1Bvc3RhbENpdHknKS52YWwoKTtcclxuICAgICAgICBjb25zdCBsb29rdXBBZGRyZXNzID0gYCR7c3RyZWV0QWRkcmVzc30sICR7cG9zdGFsQ29kZX0gJHtwb3N0YWxDaXR5fWA7XHJcbiAgICAgICAgcmV0dXJuIGxvb2t1cEFkZHJlc3M7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSB1cGRhdGVNYXAobG9jYXRpb246IGdvb2dsZS5tYXBzLkxhdExuZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICh0aGlzLm1hcmtlcikge1xyXG4gICAgICAgICAgICB0aGlzLm1hcmtlci5zZXRQb3NpdGlvbihsb2NhdGlvbik7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgdGhpcy5tYXJrZXIgPSBuZXcgZ29vZ2xlLm1hcHMuTWFya2VyKHtcclxuICAgICAgICAgICAgICAgIG1hcDogdGhpcy5tYXAsXHJcbiAgICAgICAgICAgICAgICBwb3NpdGlvbjogbG9jYXRpb24sXHJcbiAgICAgICAgICAgICAgICBkcmFnZ2FibGU6IHRydWVcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgIHRoaXMubWFya2VyLmFkZExpc3RlbmVyKCdkcmFnZW5kJywgZXYgPT4ge1xyXG4gICAgICAgICAgICAgICAgdGhpcy51cGRhdGVMYXRMbmdUZXh0Ym94ZXMoZXYubGF0TG5nKTtcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgIHRoaXMubWFwLnNldFpvb20oMTEpO1xyXG4gICAgICAgIH1cclxuICAgICAgICB0aGlzLm1hcC5zZXRDZW50ZXIobG9jYXRpb24pO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgdXBkYXRlTGF0TG5nVGV4dGJveGVzKGxvY2F0aW9uOiBnb29nbGUubWFwcy5MYXRMbmcpOiB2b2lkIHtcclxuICAgICAgICAkKCcjTGF0aXR1ZGUnKS52YWwobG9jYXRpb24ubGF0KCkudG9Mb2NhbGVTdHJpbmcoQ29uc3RhbnRzLmxvY2FsZSwgeyBtYXhpbXVtRnJhY3Rpb25EaWdpdHM6IDE0IH0pKTtcclxuICAgICAgICAkKCcjTG9uZ2l0dWRlJykudmFsKGxvY2F0aW9uLmxuZygpLnRvTG9jYWxlU3RyaW5nKENvbnN0YW50cy5sb2NhbGUsIHsgbWF4aW11bUZyYWN0aW9uRGlnaXRzOiAxNCB9KSk7XHJcbiAgICB9XHJcbn1cclxuIiwiaW1wb3J0ICcuL19hcnJhbmdlbWVudHMtaW5kZXgtcGFnZS5zY3NzJztcclxuXHJcbmV4cG9ydCBjbGFzcyBJbmRleEFycmFuZ2VtZW50c1BhZ2Uge1xyXG4gICAgcHVibGljIGluaXRQYWdlKCkge1xyXG4gICAgICAgICQoJ3NlbGVjdCN0eXBlRmlsdGVyJykuY2hhbmdlKGZ1bmN0aW9uKGV2KSB7XHJcbiAgICAgICAgICAgIGNvbnN0IHNlbGVjdGVkVHlwZSA9ICQodGhpcykudmFsKCkgYXMgc3RyaW5nO1xyXG4gICAgICAgICAgICBpZiAoIXNlbGVjdGVkVHlwZSkge1xyXG4gICAgICAgICAgICAgICAgJCgnLnB4bC1hcnJhbmdlbWVudCcpLnNob3coKTtcclxuICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICQoJy5weGwtYXJyYW5nZW1lbnQnKS5lYWNoKCgpID0+IHsgJCh0aGlzKS50b2dnbGUoJCh0aGlzKS5kYXRhKCd0eXBlJykgPT09IHNlbGVjdGVkVHlwZSk7IH0pO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgICAgICQoJyNhcnJhbmdlbWVudENvdW50JykudGV4dCgkKCcucHhsLWFycmFuZ2VtZW50OnZpc2libGUnKS5sZW5ndGgpO1xyXG5cclxuICAgICAgICAgICAgaWYgKCh3aW5kb3cgYXMgYW55KS5nYSkge1xyXG4gICAgICAgICAgICAgICAgZ2EoJ3NlbmQnLCAnZXZlbnQnLCB7XHJcbiAgICAgICAgICAgICAgICAgICAgZXZlbnRDYXRlZ29yeTogJ1NlYXJjaCcsXHJcbiAgICAgICAgICAgICAgICAgICAgZXZlbnRBY3Rpb246ICd0eXBlRmlsdGVyJyxcclxuICAgICAgICAgICAgICAgICAgICBldmVudExhYmVsOiBzZWxlY3RlZFR5cGUsXHJcbiAgICAgICAgICAgICAgICAgICAgdHJhbnNwb3J0OiAnYmVhY29uJ1xyXG4gICAgICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9KTtcclxuICAgIH1cclxufVxyXG4iLCJleHBvcnQgY2xhc3MgR29vZ2xlTWFwc1V0aWx0aWVzIHtcclxuICAgIHB1YmxpYyBzdGF0aWMgZ2V0QWRkcmVzc0NvbXBvbmVudChhZGRyZXNzQ29tcG9uZW50czogZ29vZ2xlLm1hcHMuR2VvY29kZXJBZGRyZXNzQ29tcG9uZW50W10sIGNvbXBvbmVudE5hbWU6IHN0cmluZyk6IHN0cmluZyB7XHJcbiAgICAgICAgZm9yIChjb25zdCBhZGRyZXNzQ29tcG9uZW50IG9mIGFkZHJlc3NDb21wb25lbnRzKSB7XHJcbiAgICAgICAgICAgIGZvciAoY29uc3QgdHlwZSBvZiBhZGRyZXNzQ29tcG9uZW50LnR5cGVzKSB7XHJcbiAgICAgICAgICAgICAgICBpZiAodHlwZSA9PT0gY29tcG9uZW50TmFtZSkge1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybiBhZGRyZXNzQ29tcG9uZW50LmxvbmdfbmFtZTtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH1cclxuICAgICAgICByZXR1cm4gJyc7XHJcbiAgICB9XHJcbn1cclxuIiwiLy8gZXh0cmFjdGVkIGJ5IG1pbmktY3NzLWV4dHJhY3QtcGx1Z2luIiwiLy8gZXh0cmFjdGVkIGJ5IG1pbmktY3NzLWV4dHJhY3QtcGx1Z2luIiwiLy8gZXh0cmFjdGVkIGJ5IG1pbmktY3NzLWV4dHJhY3QtcGx1Z2luIiwiaW1wb3J0ICcuL19jb29raWUtaW5mby5zY3NzJztcclxuIiwiZXhwb3J0IGNsYXNzIERldGFpbHNBcnJhbmdlbWVudFBhZ2Uge1xyXG4gICAgcHVibGljIGluaXRQYWdlKCkge1xyXG4gICAgICAgICQoJyNlbWFpbEFkZHJlc3NMaW5rJykuY2xpY2soKCkgPT4ge1xyXG4gICAgICAgICAgICBnYSgnc2VuZCcsICdldmVudCcsIHtcclxuICAgICAgICAgICAgICAgIGV2ZW50Q2F0ZWdvcnk6ICdDb250YWN0JyxcclxuICAgICAgICAgICAgICAgIGV2ZW50QWN0aW9uOiAnbWFpbCcsXHJcbiAgICAgICAgICAgICAgICBldmVudExhYmVsOiAkKHRoaXMpLmF0dHIoJ2hyZWYnKSxcclxuICAgICAgICAgICAgICAgIHRyYW5zcG9ydDogJ2JlYWNvbidcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgICQoJyNleHRlcm5hbFdlYnNpdGVMaW5rJykuY2xpY2soKCkgPT4ge1xyXG4gICAgICAgICAgICBnYSgnc2VuZCcsICdldmVudCcsIHtcclxuICAgICAgICAgICAgICAgIGV2ZW50Q2F0ZWdvcnk6ICdPdXRib3VuZCBMaW5rJyxcclxuICAgICAgICAgICAgICAgIGV2ZW50QWN0aW9uOiAnY2xpY2snLFxyXG4gICAgICAgICAgICAgICAgZXZlbnRMYWJlbDogJCh0aGlzKS5hdHRyKCdocmVmJyksXHJcbiAgICAgICAgICAgICAgICB0cmFuc3BvcnQ6ICdiZWFjb24nXHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG59XHJcbiIsIi8vIGh0dHBzOi8vZ2lzdC5naXRodWIuY29tL21hdGhld2J5cm5lLzEyODAyODZcclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBzbHVnaWZ5KHRleHQ6IHN0cmluZykge1xyXG4gICAgY29uc3QgYSA9ICfDoMOhw6XDpMOiw6jDqcOrw6rDrMOtw6/DrsOyw7PDtsO0w7nDusO8w7vDscOnw5/Dv8WTw6bFlcWbxYThuZXhuoPHtce54bi/x5jhuo3FuuG4p8K3L18sOjsnO1xyXG4gICAgY29uc3QgYiA9ICdhYWFhYWVlZWVpaWlpb29vb3V1dXVuY3N5b2Fyc25wd2dubXV4emgtLS0tLS0nO1xyXG4gICAgY29uc3QgcCA9IG5ldyBSZWdFeHAoYS5zcGxpdCgnJykuam9pbignfCcpLCAnZycpO1xyXG5cclxuICAgIHJldHVybiB0ZXh0LnRvU3RyaW5nKCkudG9Mb3dlckNhc2UoKVxyXG4gICAgICAgIC5yZXBsYWNlKC9cXHMrL2csICctJykgLy8gUmVwbGFjZSBzcGFjZXMgd2l0aCAtXHJcbiAgICAgICAgLnJlcGxhY2UocCxcclxuICAgICAgICAgICAgYyA9PlxyXG4gICAgICAgICAgICBiLmNoYXJBdChhLmluZGV4T2YoYykpKSAvLyBSZXBsYWNlIHNwZWNpYWwgY2hhcnNcclxuICAgICAgICAucmVwbGFjZSgvJi9nLCAnLWFuZC0nKSAgICAgLy8gUmVwbGFjZSAmIHdpdGggJ2FuZCdcclxuICAgICAgICAucmVwbGFjZSgvW15cXHdcXC1dKy9nLCAnJykgICAvLyBSZW1vdmUgYWxsIG5vbi13b3JkIGNoYXJzXHJcbiAgICAgICAgLnJlcGxhY2UoL1xcLVxcLSsvZywgJy0nKSAgICAgLy8gUmVwbGFjZSBtdWx0aXBsZSAtIHdpdGggc2luZ2xlIC1cclxuICAgICAgICAucmVwbGFjZSgvXi0rLywgJycpICAgICAgICAgLy8gVHJpbSAtIGZyb20gc3RhcnQgb2YgdGV4dFxyXG4gICAgICAgIC5yZXBsYWNlKC8tKyQvLCAnJyk7ICAgICAgICAvLyBUcmltIC0gZnJvbSBlbmQgb2YgdGV4dFxyXG59XHJcbiIsImV4cG9ydCBjbGFzcyBDb25zdGFudHMge1xyXG4gICAgcHVibGljIHN0YXRpYyBsb2NhbGUgPSAnc3YnO1xyXG59XHJcbiIsImltcG9ydCAnLi9faGVyby5zY3NzJztcclxuIiwiaW1wb3J0ICcuL19oZWFkZXIuc2Nzcyc7XHJcbiIsIi8vIGV4dHJhY3RlZCBieSBtaW5pLWNzcy1leHRyYWN0LXBsdWdpbiIsIi8vIGV4dHJhY3RlZCBieSBtaW5pLWNzcy1leHRyYWN0LXBsdWdpbiJdLCJzb3VyY2VSb290IjoiIn0=