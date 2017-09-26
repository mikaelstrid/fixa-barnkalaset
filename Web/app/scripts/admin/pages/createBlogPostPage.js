import { slugify } from "../utilities/slugify";
export class CreateBlogPostPage {
    initPage() {
        $("#Title").change(function () {
            $("#Slug").val(slugify($(this).val().toString()));
        });
        CKEDITOR.replace("Body", {
            toolbarGroups: [
                { name: 'clipboard', groups: ['clipboard', 'undo'] },
                { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
                { name: 'forms', groups: ['forms'] },
                { name: 'others', groups: ['others'] },
                { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
                { name: 'links', groups: ['links'] },
                { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
                { name: 'insert', groups: ['insert'] },
                { name: 'styles', groups: ['styles'] },
                { name: 'colors', groups: ['colors'] },
                { name: 'about', groups: ['about'] },
                { name: 'document', groups: ['mode', 'document', 'doctools'] },
                { name: 'tools', groups: ['tools'] }
            ],
            removeButtons: 'Subscript,Superscript,About,Outdent,Blockquote,RemoveFormat,Strike,Image,SpecialChar,Anchor,Scayt,Undo,Redo,HorizontalRule,Indent,Styles,Cut,Copy,Paste,PasteText,PasteFromWord,Table'
        });
    }
}
