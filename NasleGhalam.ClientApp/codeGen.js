var fs = require('fs');

var arg = process.argv.slice(2);
if (arg.length < 1) {
  console.log("modelName doesn't define.");
  process.exit();
} else if (arg.length < 2) {
  console.log("viewModel object props doesn't not define.");
  process.exit();
}
var modelName = arg[0];
var arrObjProp = arg[1].split(',').map(item => {
  let arr = item.split(':');
  let obj = {};
  if (arr.length == 1) {
    obj.name = arr[0];
    obj.type = 'string';
  } else if (arr.length == 2) {
    obj.name = arr[0];
    obj.type =
      arr[1] == '0'
        ? 'number'
        : arr[1] == 'true' || arr[1] == 'false'
          ? 'boolean'
          : arr[1] == '[]'
            ? 'array'
            : 'string';
  } else {
    console.log('error on parsing viewModelProps');
    process.exit();
  }
  return obj;
});
var viewModelDir = `${__dirname}/src/viewModels/${modelName}ViewModel.js`;
var storelDir = `${__dirname}/src/store/${modelName}Store.js`;
var viewDir = `${__dirname}/src/views/${modelName}`;

// generate viewModel
var viewModelProp = [];
arrObjProp.forEach(item => {
  viewModelProp.push(`${item.name}: {
    displayName: displayName('${item.name}'),
    ${
      item.type == 'string'
        ? 'maxLength: maxLength(50),'
        : item.type == 'number'
          ? 'numeric,'
          : ''
    }
    ${item.type == 'array' ? 'requiredDdl: requiredDdl(0)' : 'required'}
  }`);
});

var viewModel = fs
  .readFileSync('./@CodeTemplates/viewModel.js.temp', 'utf8')
  .replace(/{__props}/g, viewModelProp.join(','));

fs.writeFileSync(`${viewModelDir}`, viewModel, 'utf8');
//--------------------------------------------------------------------------

// generate store
var storeProp = [];
arrObjProp.forEach(item => {
  storeProp.push(
    `${item.name}:${
      item.type == 'number'
        ? '0'
        : item.type == 'boolean'
          ? 'false'
          : item.type == 'array'
            ? '[]'
            : "''"
    }`
  );
});

var store = fs
  .readFileSync('./@CodeTemplates/store.js.temp', 'utf8')
  .replace(/{__modelName}/g, modelName.toUpperCase())
  .replace(/{__props}/g, storeProp.join(','));

fs.writeFileSync(`${storelDir}`, store, 'utf8');
//--------------------------------------------------------------------------

// create vue dir
if (!dirExist(viewDir)) {
  fs.mkdirSync(viewDir);
}

// generate index.vue
var indexVueProp = [];
arrObjProp.forEach(item => {
  indexVueProp.push(
    `{
      title:'${item.name}',
      data:'${item.name}'
    }`
  );
});

var indexVue = fs
  .readFileSync('./@CodeTemplates/index.vue.temp', 'utf8')
  .replace(/{__modelName}/g, `${modelName}Store`)
  .replace(/{__props}/g, indexVueProp.join(','));

fs.writeFileSync(`${viewDir}/index.vue`, indexVue, 'utf8');
//--------------------------------------------------------------------------

var createEditVueProp = [];
arrObjProp.forEach(item => {
  createEditVueProp.push(
    `${
      item.type == 'array'
        ? `<my-select :model="$v.instanceObj.${
            item.name
          }" :options="" class="col-md-6" clearable />
          
          `
        : item.type == 'boolean'
          ? `<my-field class="col-md-6" :model="$v.instance.${item.name}">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            `
          : `<my-input :model="$v.instanceObj.${item.name}" class="col-md-6" />
          
          `
    }`
  );
});

// generate create.vue
var createVue = fs
  .readFileSync('./@CodeTemplates/create.vue.temp', 'utf8')
  .replace(/{__viewModelName}/g, modelName)
  .replace(/{__modelName}/g, `${modelName}Store`)
  .replace(/{__props}/g, createEditVueProp.join(''));

fs.writeFileSync(`${viewDir}/create.vue`, createVue, 'utf8');
//--------------------------------------------------------------------------

// generate edit.vue
var editVue = fs
  .readFileSync('./@CodeTemplates/edit.vue.temp', 'utf8')
  .replace(/{__viewModelName}/g, modelName)
  .replace(/{__modelName}/g, `${modelName}Store`)
  .replace(/{__props}/g, createEditVueProp.join(''));

fs.writeFileSync(`${viewDir}/edit.vue`, editVue, 'utf8');
//--------------------------------------------------------------------------

// generate delete.vue
var deleteVue = fs
  .readFileSync('./@CodeTemplates/delete.vue.temp', 'utf8')
  .replace(/{__modelName}/g, `${modelName}Store`);
fs.writeFileSync(`${viewDir}/delete.vue`, deleteVue, 'utf8');
//--------------------------------------------------------------------------

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
