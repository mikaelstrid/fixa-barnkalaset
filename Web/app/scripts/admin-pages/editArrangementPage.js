var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var EditArrangementPage = (function (_super) {
    __extends(EditArrangementPage, _super);
    function EditArrangementPage() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    EditArrangementPage.prototype.initPage = function (latitude, longitude) {
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
        this.initialLatitude = latitude;
        this.initialLongitude = longitude;
    };
    return EditArrangementPage;
}(CreateOrEditArrangementPageBase));
//# sourceMappingURL=editArrangementPage.js.map