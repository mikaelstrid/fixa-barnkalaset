// https://gist.github.com/mathewbyrne/1280286

function slugify(text: string) {
    const a = "àáåäâèéëêìíïîòóöôùúüûñçßÿœæŕśńṕẃǵǹḿǘẍźḧ·/_,:;";
    const b = "aaaaaeeeeiiiioooouuuuncsyoarsnpwgnmuxzh------";
    const p = new RegExp(a.split("").join("|"), "g");

    return text.toString().toLowerCase()
        .replace(/\s+/g, "-") // Replace spaces with -
        .replace(p,
            c =>
            b.charAt(a.indexOf(c))) // Replace special chars
        .replace(/&/g, "-and-")     // Replace & with 'and'
        .replace(/[^\w\-]+/g, "")   // Remove all non-word chars
        .replace(/\-\-+/g, "-")     // Replace multiple - with single -
        .replace(/^-+/, "")         // Trim - from start of text
        .replace(/-+$/, "");        // Trim - from end of text
}