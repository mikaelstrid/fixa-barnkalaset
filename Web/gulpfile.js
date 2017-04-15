/// <binding BeforeBuild='clean, default' Clean='clean' />
var gulp = require("gulp");
var sass = require("gulp-sass");
var del = require("del");

var paths = {
    scripts: ["app/scripts/**/*.js", "app/scripts/**/*.ts", "app/scripts/**/*.map"],
};

gulp.task("css", function () {
    return gulp.src("app/styles/*.scss")
        //.pipe(plumber())
        //.pipe(sourcemaps.init())
        .pipe(sass({
            outputStyle: "compressed"
        })
        .on("error", sass.logError))
        //.pipe(header(banner))
        //.pipe(sourcemaps.write('.', {
        //    sourceRoot: '/app-main-savings/sass'
        //}))
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
