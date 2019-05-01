import { IndexArrangementsPage } from './pages/arrangements-index-page/arrangements-index-page';
import { DetailsArrangementPage } from './pages/arrangements-details-page/arrangements-details-page';

// import * as $ from 'jquery';

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

$(document).ready(() => {
    bootstrap();
});

function bootstrap() {
    if ($('.pxl-arrangements-index-page').length > 0) {
        new IndexArrangementsPage().initPage();
    }

    if ($('.pxl-arrangements-details-page').length > 0) {
        new DetailsArrangementPage().initPage();
    }
}
