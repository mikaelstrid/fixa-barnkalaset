/// <binding BeforeBuild='clean, default' Clean='clean' />
var gulp = require("gulp");
var sass = require("gulp-sass");
var del = require("del");

// Semantic UI
var buildSemantic = require('./app/lib/semantic/tasks/build');
gulp.task("build-semantic", buildSemantic);
gulp.task("copy-semantic", function () {
    return gulp.src(paths.semantic)
        .pipe(gulp.dest('wwwroot/lib'));
});

var paths = {
    semantic: "app/lib/semantic/dist/semantic*.*",
    scripts: ["app/scripts/**/*.js", "app/scripts/**/*.ts", "app/scripts/**/*.map"],
};

gulp.task("css", function () {
    return gulp.src("app/styles/*.scss")
        .pipe(sass({
            outputStyle: "compressed"
        })
            .on("error", sass.logError))
        .pipe(gulp.dest("wwwroot/css"));
});

gulp.task("js", function () {
    return gulp.src(paths.scripts)
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task("clean", function () {
    return del([
        "wwwroot/js/**/*"
    ]);
});

gulp.task("default", ["js", "css"]);

gulp.task("watch",
    function () {
        gulp.watch("app/**/*.*", ["js", "css"]);
    });