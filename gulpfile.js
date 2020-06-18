'use strict';

const gulp = require('gulp');
const sass = require('gulp-sass');

sass.compiler = require('node-sass');

gulp.task('default', watch);
gulp.task('sass', compile);

function compile() {
    return gulp
        .src('./wwwroot/scss/*.scss')
        .pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))
        .pipe(gulp.dest('./wwwroot/css/min/'));
}

function watch() {
    gulp.watch('./wwwroot/scss/*.scss', compile);
}