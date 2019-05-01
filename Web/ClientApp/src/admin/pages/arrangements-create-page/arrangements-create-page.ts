import * as $ from 'jquery';

import { CreateOrEditArrangementPageBase } from '../arrangements-create-or-edit-page-base/arrangements-create-or-edit-page-base';
import { slugify } from '../../../typescript/admin/utilities/slugify';

export class CreateArrangementPage extends CreateOrEditArrangementPageBase {
    public initPage() {
        $('#Name').change(() => {
            const val = $('#Name').val();
            if (val) {
                $('#Slug').val(slugify(val.toString()));
            }
        });
        CKEDITOR.replace('Description');
        CKEDITOR.replace('BookingConditions');
        CKEDITOR.replace('PriceInformation');

        this.initMap();
    }
}
