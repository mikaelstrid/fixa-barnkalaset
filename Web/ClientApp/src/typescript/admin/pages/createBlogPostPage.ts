import { CreateOrEditBlogPostPageBase } from "./createOrEditBlogPostPageBase";
import { slugify } from "../utilities/slugify";

export class CreateBlogPostPage extends CreateOrEditBlogPostPageBase {
    initPage() {
        $("#Title").change(function () {
            $("#Slug").val(slugify($(this).val().toString()));
        });
        this.initEditor();
    }
}