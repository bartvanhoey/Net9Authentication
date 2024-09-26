const {src, dest, watch, series} = require('gulp')
const sass = require('gulp-sass')(require('sass'))
const purgeCss = require('gulp-purgecss')

const root = "./wwwroot/css";
const bootstrap = "./node_modules/bootstrap/dist/css/bootstrap.min.css";
const bootstrapIconsMinCss = "./node_modules/bootstrap-icons/font/bootstrap-icons.min.css";
const bootstrapIconsFontsWoff = "./node_modules/bootstrap-icons/font/fonts/bootstrap-icons.woff";
const bootstrapIconsFontsWoff2 = "./node_modules/bootstrap-icons/font/fonts/bootstrap-icons.woff2";
const grawColors = "./node_modules/@graw/ci/dist/colors.css"
const grawFonts = "./node_modules/@graw/ci/dist/fonts.css"

async function buildStyles() {
    return await src('Sass/**/*.scss')
        .pipe(sass({outputStyle: 'compressed'}))
        .pipe(purgeCss({ content: ['**/*.html', '**/*.razor'] })) // WARNING: only used CSS will be rendered
        .pipe(dest('wwwroot/css'))
}

async function watchTask() {
    await watch(['Sass/**/*.scss', '*.html'], buildStyles)
}

async function bootstrapTask() {
    src([bootstrap]).pipe(dest(root+'/bootstrap/'));
}

async function bootstrapIconsMinCssTask() {
    src([bootstrapIconsMinCss]).pipe(dest(root+'/bootstrap-icons'));
}

async function bootstrapIconsFontsWoff2Task() {
    src([bootstrapIconsFontsWoff2], { encoding: false }).pipe(dest(root+'/bootstrap-icons/fonts'));
}

async function bootstrapIconsFontsWoffTask() {
    src([bootstrapIconsFontsWoff], { encoding: false }).pipe(dest(root+'/bootstrap-icons/fonts'));
}

async function grawFontsTask() {
    src([grawFonts]).pipe(dest(root+'/graw-fonts/'));
}

async function grawColorsTask() {
    src([grawColors]).pipe(dest(root+'/graw-colors/'));
}

exports.default = series(buildStyles, watchTask, bootstrapTask, bootstrapIconsMinCssTask, bootstrapIconsFontsWoffTask, bootstrapIconsFontsWoff2Task, grawFontsTask, grawColorsTask)