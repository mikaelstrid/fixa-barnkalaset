import * as $ from 'jquery';

import { IndexArrangementsPage } from './pages/arrangements-index-page/arrangements-index-page';
import { DetailsArrangementPage } from './pages/arrangements-details-page/arrangements-details-page';
import { CreateArrangementPage } from './admin/pages/arrangements-create-page/arrangements-create-page';

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
    // PUBLIC
    if ($('.pxl-arrangements-index-page').length > 0) {
        new IndexArrangementsPage().initPage();
    }

    if ($('.pxl-arrangements-details-page').length > 0) {
        new DetailsArrangementPage().initPage();
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

    if ($('.pxl-arrangements-create-page').length > 0) {
        new CreateArrangementPage().initPage();
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
