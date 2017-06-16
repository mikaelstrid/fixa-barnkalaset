function slugify(text) {
    var a = "àáåäâèéëêìíïîòóöôùúüûñçßÿœæŕśńṕẃǵǹḿǘẍźḧ·/_,:;";
    var b = "aaaaaeeeeiiiioooouuuuncsyoarsnpwgnmuxzh------";
    var p = new RegExp(a.split("").join("|"), "g");
    return text.toString().toLowerCase()
        .replace(/\s+/g, "-")
        .replace(p, function (c) {
        return b.charAt(a.indexOf(c));
    })
        .replace(/&/g, "-and-")
        .replace(/[^\w\-]+/g, "")
        .replace(/\-\-+/g, "-")
        .replace(/^-+/, "")
        .replace(/-+$/, "");
}
//# sourceMappingURL=slugify.js.map