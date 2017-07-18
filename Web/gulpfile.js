var gulp = require("gulp");
var sass = require("gulp-sass");
var del = require("del");
var browserify = require("browserify");
var source = require("vinyl-source-stream");
var watchify = require("watchify");
var gutil = require("gulp-util");
var tsify = require("tsify");
var uglify = require("gulp-uglify");
var sourcemaps = require("gulp-sourcemaps");
var buffer = require("vinyl-buffer");

gulp.task("default", ["watch-js", "watch-css", "watch-semantic"]);

// SCRIPTS
var watchedBrowserify = watchify(browserify({
    basedir: ".",
    debug: true,
    entries: ["app/scripts/admin/main.ts"],
    cache: {},
    packageCache: {}
}).plugin(tsify));

function bundleJS() {
    return watchedBrowserify
        .transform("babelify", {
            presets: ["es2015"],
            extensions: [".ts"]
        })
        .bundle()
        .pipe(source("bundle.min.js"))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(uglify())
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest("wwwroot/js"));
}

gulp.task("watch-js", bundleJS);
watchedBrowserify.on("update", bundleJS);
watchedBrowserify.on("log", gutil.log);

gulp.task("clean-js", function() {
    return del(["wwwroot/js/**/*"]);
});


// STYLES
gulp.task("build-styles", function() {
    return gulp.src("app/styles/*.scss")
        .pipe(sass({
                outputStyle: "compressed"
            })
            .on("error", sass.logError))
        .pipe(gulp.dest("wwwroot/css"));
});
gulp.task("watch-css", function() { gulp.watch(["app/styles/**/*.*"], ["build-styles"]); });

gulp.task("clean-css", function() {
    return del(["wwwroot/css/**/*"]);
});


// SEMANTIC
var buildSemantic = require("./app/lib/semantic/tasks/build");
gulp.task("build-semantic", buildSemantic);
gulp.task("copy-semantic", ["build-semantic"], function() {
    return gulp.src("app/lib/semantic/dist/semantic*.*")
        .pipe(gulp.dest("wwwroot/lib"));
});
gulp.task("watch-semantic", function() { gulp.watch(["app/lib/semantic/src/site/**/*.*"], ["build-semantic", "copy-semantic"]); });

gulp.task("clean-semantic", function() {
    return del(["wwwroot/lib/semantic*.*"]);
});
