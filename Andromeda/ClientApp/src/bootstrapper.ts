import Vue from 'vue';
import InspirationAppComponent from './app/inspiration-app.component.vue';

if (document.getElementById('inspiration-app') != null) {
    // tslint:disable-next-line: no-unused-expression
    new Vue({
        el: '#inspiration-app',
        components: { InspirationAppComponent },
        template: '<InspirationAppComponent />'
    });
}
