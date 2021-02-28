export class GoogleMapsUtilties {
    static getAddressComponent(addressComponents, componentName) {
        for (let i = 0; i < addressComponents.length; i++) {
            const addressComponent = addressComponents[i];
            for (let j = 0; j < addressComponent.types.length; j++) {
                if (addressComponent.types[j] === componentName) {
                    return addressComponent.long_name;
                }
            }
        }
        return undefined;
    }
}
