/// <binding />
var gulp = require("gulp");
var sass = require("gulp-sass");
var del = require("del");
var uglify = require("gulp-uglify");
var pump = require("pump");
var sourcemaps = require("gulp-sourcemaps");
var concat = require('gulp-concat');

gulp.task("default", ["js", "css"]);
gulp.task("watch",
    function() {
        gulp.watch(["app/scripts/**/*.*", "app/styles/**/*.*"], ["js", "css"]);
    });


// STYLES
gulp.task("css", function() {
    return gulp.src("app/styles/*.scss")
        .pipe(sass({
            outputStyle: "compressed"
        })
            .on("error", sass.logError))
        .pipe(gulp.dest("wwwroot/css"));
});

// SCRIPTS
gulp.task("js-admin", function(cb) {
    pump([
        gulp.src([
            "app/scripts/admin/utilities/constants.js",
            "app/scripts/admin/utilities/googleMapsUtilities.js",
            "app/scripts/admin/utilities/slugify.js",
            "app/scripts/pages/utilities/createOrEditCityPageBase.js",
            "app/scripts/pages/utilities/createCityPage.js",
            "app/scripts/pages/utilities/editCityPage.js",
            "app/scripts/pages/utilities/createOrEditArrangementPageBase.js",
            "app/scripts/pages/utilities/createArrangementPage.js",
            "app/scripts/pages/utilities/editArrangementPage.js"
        ]),
        sourcemaps.init(),
        concat("admin-scripts.js"),
        uglify(),
        sourcemaps.write(),
        gulp.dest("wwwroot/js")
    ],
        cb
    );
});

gulp.task("clean-js", function() {
    return del(["wwwroot/js/**/*"]);
});


// SEMANTIC
var buildSemantic = require("./app/lib/semantic/tasks/build");
gulp.task("build-semantic", buildSemantic);
gulp.task("copy-semantic", function() {
    return gulp.src("app/lib/semantic/dist/**/*.*")
        .pipe(gulp.dest("wwwroot/lib"));
});
