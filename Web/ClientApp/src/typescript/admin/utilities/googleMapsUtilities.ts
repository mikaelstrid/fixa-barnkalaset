export class GoogleMapsUtilties {
    public static getAddressComponent(addressComponents: google.maps.GeocoderAddressComponent[], componentName: string): string {
        for (const addressComponent of addressComponents) {
            for (const type of addressComponent.types) {
                if (type === componentName) {
                    return addressComponent.long_name;
                }
            }
        }
        return '';
    }
}
