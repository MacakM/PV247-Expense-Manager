var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');
var watch = require('gulp-watch');

gulp.task('default', function () {
    convertLessToCss();
});

gulp.task('less', convertLessToCss);

gulp.task('watch', function() {
    gulp.watch('./wwwroot/less/**/*.less', convertLessToCss);
});

function convertLessToCss() {
    return gulp.src('./wwwroot/less/**/*.less')
      .pipe(less({
          paths: [path.join(__dirname, 'less', 'includes')]
      }))
      .pipe(gulp.dest('./wwwroot/css'));
}