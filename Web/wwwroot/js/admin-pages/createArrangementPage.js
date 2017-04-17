var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var CreateArrangementPage = (function (_super) {
    __extends(CreateArrangementPage, _super);
    function CreateArrangementPage() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CreateArrangementPage.prototype.initPage = function () {
        $("#Name").change(function () {
            $("#Slug").val(slugify($(this).val()));
        });
        $("select.dropdown").dropdown();
        CKEDITOR.replace("Description");
    };
    return CreateArrangementPage;
}(CreateOrEditArrangementPageBase));
//# sourceMappingURL=createArrangementPage.js.map