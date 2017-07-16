var GoogleMapsUtilties = (function () {
    function GoogleMapsUtilties() {
    }
    GoogleMapsUtilties.getAddressComponent = function (addressComponents, componentName) {
        for (var i = 0; i < addressComponents.length; i++) {
            var addressComponent = addressComponents[i];
            for (var j = 0; j < addressComponent.types.length; j++) {
                if (addressComponent.types[j] === componentName) {
                    return addressComponent.long_name;
                }
            }
        }
        return undefined;
    };
    return GoogleMapsUtilties;
}());
//# sourceMappingURL=googleMapsUtilities.js.map