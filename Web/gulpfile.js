/// <binding BeforeBuild='default' />
var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('css', function () {
    return gulp.src('app/style.scss')
        //.pipe(plumber())
        //.pipe(sourcemaps.init())
        .pipe(sass({
            outputStyle: 'compressed'
        })
        .on('error', sass.logError))
        //.pipe(header(banner))
        //.pipe(sourcemaps.write('.', {
        //    sourceRoot: '/app-main-savings/sass'
        //}))
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('default', [/*'js', 'templates', */'css']);
