import { IndexArrangementsPage } from './pages/arrangements-index-page/arrangements-index-page';
import { DetailsArrangementPage } from './typescript/public/pages/detailsArrangementPage';

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
    console.log('Starting public bootstrapping procedure...');

    if ($('.pxl-arrangements-index-page').length > 0) {
        console.log('Found "index arrangements page", start bootstrapping it...');
        new IndexArrangementsPage().initPage();
        console.log('Bootstrapping "index arrangements page" finished.');
    }

    if ($('.pxl-arrangements-details-page').length > 0) {
        console.log('Found "details arrangement page", start bootstrapping it...');
        new DetailsArrangementPage().initPage();
        console.log('Bootstrapping "details arrangement page" finished.');
    }

    console.log('Bootstrapping public procedure finished.');
}
