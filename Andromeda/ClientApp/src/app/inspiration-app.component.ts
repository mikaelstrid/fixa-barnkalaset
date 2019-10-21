import { Vue, Component } from 'vue-property-decorator';
import { IArrangement } from './shared/services/arrangements/models/arrangement.model';
import { ArrangementsService } from './shared/services/arrangements/arrangements.service';

@Component({
   components: {}
})
export default class InspirationAppComponent extends Vue {
   public arrangements: IArrangement[] = [];

   constructor() {
      super();
   }

   public created() {
      ArrangementsService.getRandom(4)
         .then(response => {
            this.arrangements = response;
         })
         .catch(error => console.log(error));
   }
}
