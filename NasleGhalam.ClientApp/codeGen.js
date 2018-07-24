var fs = require('fs');

var arg = process.argv.slice(2);
// console.log(arg);
if (arg.length != 1) {
  console.log('modelName is not define.');
  process.exit();
}
var modelName = arg[0];
var viewModelDir = `${__dirname}/src/viewModels/${modelName}ViewModel.js`;
var storelDir = `${__dirname}/src/store/${modelName}Store.js`;
var viewDir = `${__dirname}/src/views/${modelName}`;

// generate viewModel
var viewModel = fs.readFileSync('./@CodeTemplates/viewModel.js.temp', 'utf8');
fs.writeFileSync(`${viewModelDir}`, viewModel, 'utf8');

// generate store
var store = fs
  .readFileSync('./@CodeTemplates/store.js.temp', 'utf8')
  .replace(/{__modelName}/g, modelName.toUpperCase());
fs.writeFileSync(`${storelDir}`, store, 'utf8');

// create vue dir
if (!dirExist(viewDir)) {
  fs.mkdirSync(viewDir);
}

// generate index.vue
var indexVue = fs
  .readFileSync('./@CodeTemplates/index.vue.temp', 'utf8')
  .replace(/{__modelName}/g, `${modelName}Store`);

fs.writeFileSync(`${viewDir}/index.vue`, indexVue, 'utf8');

// generate create.vue
var createVue = fs
  .readFileSync('./@CodeTemplates/create.vue.temp', 'utf8')
  .replace(/{__viewModelName}/g, modelName)
  .replace(/{__modelName}/g, `${modelName}Store`);
fs.writeFileSync(`${viewDir}/create.vue`, createVue, 'utf8');

// generate edit.vue
var editVue = fs
  .readFileSync('./@CodeTemplates/edit.vue.temp', 'utf8')
  .replace(/{__viewModelName}/g, modelName)
  .replace(/{__modelName}/g, `${modelName}Store`);
fs.writeFileSync(`${viewDir}/edit.vue`, editVue, 'utf8');

// generate delete.vue
var deleteVue = fs
  .readFileSync('./@CodeTemplates/delete.vue.temp', 'utf8')
  .replace(/{__modelName}/g, `${modelName}Store`);
fs.writeFileSync(`${viewDir}/delete.vue`, deleteVue, 'utf8');

/**
 * check dir or file exist
 * @param {String} path
 */
function dirExist(path) {
  try {
    fs.statSync(path);
    return true;
  } catch (err) {
    return false;
    // if (err.code === 'ENOENT') {
    //   console.log('file or directory does not exist');
    // }
  }
}
