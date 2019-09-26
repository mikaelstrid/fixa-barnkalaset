import Vue from 'vue';
import AppComponent from './app/app.component.vue';

if (document.getElementById('app') != null) {
    // tslint:disable-next-line: no-unused-expression
    new Vue({
        el: '#app',
        components: { AppComponent },
        template: '<AppComponent />'
    });
}
