import { CreateOrEditBlogPostPageBase } from "./createOrEditBlogPostPageBase";
export class EditBlogPostPage extends CreateOrEditBlogPostPageBase {
    initPage() {
        this.initEditor();
    }
}