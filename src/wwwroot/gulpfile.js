﻿var
    gulp = require('gulp'),
    $ = require('gulp-load-plugins')({ lazy: true }),
    config = require('./gulp.config')(),
    tsProject = $.typescript.createProject({
        "compilerOptions": {
            "target": "es5",
            "noImplicitAny": true,
            "sourceMap": false,
            "noExternalResolve": true
        }
    }),
    runSequence = require('run-sequence');

/**
 * Wire-up the bower dependencies and custom js files
 * @return {Stream}
 */
gulp.task('_js-inject', function () {
    log('Wiring the bower dependencies into the html');

    var wiredep = require('wiredep').stream;
    var options = config.getWiredepDefaultOptions();

    return gulp
        .src(config.index)
        .pipe(wiredep(options))
        .pipe(inject(config.js.appFiles, '', config.js.order))
        .pipe(gulp.dest(config.root));
});

/**
 * Created and inject bundles
 */
gulp.task('bundle', function (callback) {
    runSequence('_js-clean', 'ts', '_js-inject', callback);
});


/**
* Removes all *.js and *.js.map files from ./app
* @return {Stream}
*/
gulp.task('_js-clean', function () {
    return gulp
        .src([config.js.all, config.js.maps])
        .pipe($.clean());
});

/**
*  Processess javascript files and adds dependency injection annotations ($inject) where /* @ngInject */
/* is found or for angular standard injections, ex controllers
*  @return {Stream}
*/
gulp.task('_js-annotate', function () {
    return gulp.src(config.js.appFiles)
		.pipe($.ngAnnotate())
		.pipe(gulp.dest(config.app));
});

/**
 * Lint all typescript files
 * @return {Stream}
 */
gulp.task('_ts-lint', function () {    
    return gulp
        .src(config.ts.allTs)
        .pipe($.tslint())
        .pipe($.tslint.report('prose'));
});

/**
 * Compiling Typescript --> Javascript
 * @return {Stream}
 */
gulp.task('_ts-compile', function () {    
    var tsResult = gulp
        .src([config.ts.allTs, config.ts.libTypingsAllTs])
        .pipe($.sourcemaps.init())
        .pipe($.typescript(tsProject));

    tsResult.dts.pipe(gulp.dest('.'));

    return tsResult.js.pipe($.sourcemaps.write('.'))
                      .pipe(gulp.dest(config.app));
});

/**
*  Lints and compiles typescript files
*  @return {Stream}
*/
gulp.task('ts', function (callback) {
    log('Compiling typescript to javascript');
    runSequence('_ts-lint', '_ts-compile', '_js-annotate', callback);    
});

/**
 * Watching changes in typescript files
 */
gulp.task('ts-watch', function () {
    gulp.watch([config.ts.allTs], ['ts']);
});

/**
 * Inject all the spec files into the specRunner.html
 * @return {Stream}
 */
gulp.task('specs-build', function () {
    log('Building the spec runner');

    var wiredep = require('wiredep').stream;
    var options = config.getWiredepDefaultOptions();

    options.devDependencies = true;

    return gulp
        .src(config.specRunner)
        .pipe(wiredep(options))
        .pipe(inject(config.js.appFilesToTest, '', config.js.order))
        .pipe(inject(config.js.specsAndMocks, 'specs', ['**/*']))
        .pipe(gulp.dest(config.root));
});

/**
 * Building everything
 * @return {Stream}
 */
gulp.task('build', ['_optimize'], function () {
    log('Building everything');

    gulp.src(config.build.temp)
        .pipe($.clean());

    gulp.src(config.nuspec)
        .pipe(gulp.dest(config.build.output));

    log('Deployed to build folder');
});

/**
 * Building everything in local env
 * @return {Stream}
 */
gulp.task('build-local', function (callback) {
    runSequence('bundle', 'specs-build', callback);
});

/**
 * Optimizing javascript, css and html files and saving these in build folder
 * @return {Stream}
 */
gulp.task('_optimize', ['bundle'], function () {
    log('Optimizing javascript, css and html files and saving these in build folder');

    // TODO add angular and configure template cache for html files
    //var templateCache = config.temp + config.templateCache.file;

    return gulp
        .src(config.index)
        //.pipe(inject(templateCache, 'templates'))
        .pipe($.useref())
        .pipe(gulp.dest(config.build.output));
});


/**
 * Inject files in a sorted sequence at a specified inject label
 * @param   {Array} src   glob pattern for source files
 * @param   {String} label   The label name
 * @param   {Array} order   glob pattern for sort order of the files
 * @returns {Stream}   The stream
 */
function inject(src, label, order, addPrefix) {
    var options = { read: false, addRootSlash: false };
    if (label) {
        options.name = 'inject:' + label;
    }
    if (addPrefix) {
        options.addPrefix = addPrefix;
    }

    return $.inject(orderSrc(src, order), options);
}

/**
 * Order a stream
 * @param   {Stream} src   The gulp.src stream
 * @param   {Array} order Glob array pattern
 * @returns {Stream} The ordered stream
 */
function orderSrc(src, order) {
    //order = order || ['**/*'];
    return gulp
        .src(src)
        .pipe($.if(order, $.order(order)));
}

/**
 * Log a message or series of messages using chalk's blue color.
 * Can pass in a string, object or array.
 */
function log(msg) {
    if (typeof (msg) === 'object') {
        for (var item in msg) {
            if (msg.hasOwnProperty(item)) {
                $.util.log($.util.colors.blue(msg[item]));
            }
        }
    } else {
        $.util.log($.util.colors.blue(msg));
    }
}