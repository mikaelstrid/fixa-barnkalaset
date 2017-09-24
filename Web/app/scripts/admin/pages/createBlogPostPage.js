import { slugify } from "../utilities/slugify";
export class CreateBlogPostPage {
    initPage() {
        $("#Title").change(function () {
            $("#Slug").val(slugify($(this).val().toString()));
        });
        CKEDITOR.replace("Body");
    }
}
