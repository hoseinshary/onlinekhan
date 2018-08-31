// example => node codeGen city1 Name:string:'shirez',Province:array,Code:number:71

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
// parsing arrObjProp
var arrObjProp = arg[1].split(',').map(item => {
  let arr = item.split(':');
  let obj = {};

  if (arr.length == 1) {
    obj.name = arr[0];
    obj.type = 'string';
    obj.default = `''`;
  } else if (arr.length == 2) {
    obj.name = arr[0];
    obj.type = arr[1];
    if (obj.type == 'number') {
      obj.default = '0';
    } else if (obj.type == 'boolean') {
      obj.default = 'false';
    } else if (obj.type == 'array') {
      obj.default = '0';
    } else {
      obj.default = `''`;
      obj.type = 'string';
    }
  } else if (arr.length == 3) {
    obj.name = arr[0];
    obj.type = arr[1];
    obj.default = arr[2];
  } else {
    console.log('error on parsing viewModelProps');
    process.exit();
  }

  return obj;
});
//------------

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
  .replace(/{__modelName}/g, modelName)
  .replace(/{__props}/g, viewModelProp.join(','));

fs.writeFileSync(`${viewModelDir}`, viewModel, 'utf8');
//--------------------------------------------------------------------------

// generate store
var storeProp = [];
arrObjProp.forEach(item => {
  storeProp.push(`${item.name}:${item.default}`);
});

var store = fs
  .readFileSync('./@CodeTemplates/store.js.temp', 'utf8')
  .replace(/{__modelNameUpperCase}/g, modelName.toUpperCase())
  .replace(/{__modelName}/g, modelName)
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
  .replace(/{__modelName}/g, `${modelName}`)
  .replace(/{__modelNameStore}/g, `${modelName}Store`)
  .replace(/{__props}/g, indexVueProp.join(','));

fs.writeFileSync(`${viewDir}/index.vue`, indexVue, 'utf8');
//--------------------------------------------------------------------------

var createEditVueProp = [];
arrObjProp.forEach(item => {
  createEditVueProp.push(
    `${
      item.type == 'array'
        ? `<my-select :model="$v.${modelName}Obj.${
            item.name
          }" :options="" class="col-md-6" clearable />
          
          `
        : item.type == 'boolean'
          ? `<my-field class="col-md-6" :model="$v.${modelName}Obj.${
              item.name
            }">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            `
          : `<my-input :model="$v.${modelName}Obj.${
              item.name
            }" class="col-md-6" />
          
          `
    }`
  );
});

// generate create.vue
var createVue = fs
  .readFileSync('./@CodeTemplates/create.vue.temp', 'utf8')
  .replace(/{__viewModelName}/g, modelName)
  .replace(/{__modelName}/g, modelName)
  .replace(/{__modelNameStore}/g, `${modelName}Store`)
  .replace(/{__props}/g, createEditVueProp.join(''));

fs.writeFileSync(`${viewDir}/create.vue`, createVue, 'utf8');
//--------------------------------------------------------------------------

// generate edit.vue
var editVue = fs
  .readFileSync('./@CodeTemplates/edit.vue.temp', 'utf8')
  .replace(/{__viewModelName}/g, modelName)
  .replace(/{__modelName}/g, modelName)
  .replace(/{__modelNameStore}/g, `${modelName}Store`)
  .replace(/{__props}/g, createEditVueProp.join(''));

fs.writeFileSync(`${viewDir}/edit.vue`, editVue, 'utf8');
//--------------------------------------------------------------------------

// generate delete.vue
var deleteVue = fs
  .readFileSync('./@CodeTemplates/delete.vue.temp', 'utf8')
  .replace(/{__modelName}/g, `${modelName}Store`);
fs.writeFileSync(`${viewDir}/delete.vue`, deleteVue, 'utf8');
//--------------------------------------------------------------------------

// insert import into store/index.js
let storePropImport = `import ${modelName}Store from './${modelName}Store'`;
let storeIndex = fs.readFileSync(`${__dirname}/src/store/index.js`, 'utf8');

if (!storeIndex.includes(storePropImport)) {
  storeIndex = storeIndex
    .replace('\r\nVue.use(Vuex);', `${storePropImport};\r\n\r\nVue.use(Vuex);`)
    .replace(
      'Store\r\n  }\r\n});\r\n\r\nexport default store;',
      `Store,\r\n    ${modelName}Store\r\n  }\r\n});\r\n\r\nexport default store;`
    );
  fs.writeFileSync(`${__dirname}/src/store/index.js`, storeIndex, 'utf8');
}

//--------------------------------------------------------------------------

// insert const Url into utilities/site-config.js
let configProp = `const ${modelName.toUpperCase()}_URL`;
var configUrl = fs.readFileSync(
  `${__dirname}/src/utilities/site-config.js`,
  'utf8'
);
if (!configUrl.includes(configProp)) {
  configUrl = configUrl
    .replace(
      '\r\nexport {',
      `${configProp} =  '/api/${modelName.replace('_', '')}';\r\n\r\nexport {`
    )
    .replace('_URL\r\n};', `_URL,\r\n  ${modelName.toUpperCase()}_URL\r\n};`);

  fs.writeFileSync(
    `${__dirname}/src/utilities/site-config.js`,
    configUrl,
    'utf8'
  );
}

//--------------------------------------------------------------------------

// insert lazy router  into router/routes.js
let routerProp = `\r\n        path: '/${modelName}',\r\n        component: () => import('views/${modelName}')`;
var configFile = fs.readFileSync(`${__dirname}/src/router/routes.js`, 'utf8');
if (!configFile.includes(routerProp)) {
  configFile = configFile.replace(
    `')\r\n      }\r\n    ]\r\n  },`,
    `')\r\n      },\r\n      {${routerProp}\r\n      }\r\n    ]\r\n  },`
  );
  fs.writeFileSync(`${__dirname}/src/router/routes.js`, configFile, 'utf8');
}

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
