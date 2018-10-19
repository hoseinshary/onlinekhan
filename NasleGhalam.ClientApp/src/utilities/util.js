import { LocalStorage } from 'quasar';
import router from 'router';
import axios from 'utilities/axios';

/**
 * map all value of source object to dest object
 * @param {Object} cloneFrom
 * @param {Object} cloneTo
 */
const mapObject = function(cloneFrom, cloneTo) {
  Object.keys(cloneTo).forEach(key => {
    if (cloneFrom[key] !== undefined) {
      if (isObject(cloneFrom[key])) {
        mapObject(cloneFrom[key], cloneTo[key]);
      } else {
        cloneTo[key] = cloneFrom[key];
      }
    }
  });
};

/**
 * return new object
 * @param {Object} obj
 */
const cloneObject = function(obj) {
  return JSON.parse(JSON.stringify(obj));
};

/**
 * clear value of object
 * @param {Object} obj
 */
const clearObject = function(obj) {
  Object.keys(obj).forEach(key => {
    if (isObject(obj[key])) {
      clearObject(obj[key]);
    } else {
      obj[key] = undefined;
    }

    // if (isString(obj[key])) {
    //   obj[key] = '';
    // } else if (isNumber(obj[key])) {
    //   // obj[key] = 0;
    //   obj[key] = undefined;
    // } else if (isBoolean(obj[key])) {
    //   //obj[key] = false;
    //   obj[key] = undefined;
    // } else if (isArray(obj[key])) {
    //   clearArray(obj[key]);
    // } else if (isObject(obj[key])) {
    //   clearObject(obj[key]);
    // } else {
    //   obj[key] = undefined;
    // }
  });
};

/**
 * clear array
 * @param {Array} arr
 */
const clearArray = function(arr) {
  arr.splice(0, arr.length);
};

/**
 * turn an object into query string parameters
 * @param {Object} obj
 */
const toParam = function(obj) {
  return Object.keys(obj)
    .map(key => key + '=' + obj[key])
    .join('&');
};

/**
 * access to nested object by string
 * @param {*} theObject
 * @param {String} path
 */
const getNested = function(theObject, path) {
  try {
    var separator = '.';

    return path
      .replace('[', separator)
      .replace(']', '')
      .split(separator)
      .reduce(function(obj, property) {
        return obj[property];
      }, theObject);
  } catch (err) {
    return undefined;
  }
};

/**
 * check type value be string
 * @param {*} value
 */
const isString = function(value) {
  return typeof value === 'string' || value instanceof String;
};

/**
 * check type value be array
 * @param {*} value
 */
const isNumber = function(value) {
  return typeof value === 'number' && isFinite(value);
};

/**
 * check type value be array
 * @param {*} value
 */
const isArray = function(value) {
  return value && typeof value === 'object' && value.constructor === Array;
};

/**
 * check type value be object
 * @param {*} value
 */
const isObject = function(value) {
  return value && typeof value === 'object' && value.constructor === Object;
};

/**
 * check type value be boolean
 * @param {*} value
 */
const isBoolean = function(value) {
  return typeof value === 'boolean';
};

/**
 * handle logout
 */
const logout = function() {
  LocalStorage.remove('authList');
  LocalStorage.remove('menuList');
  LocalStorage.remove('subMenuList');
  LocalStorage.remove('Token');
  LocalStorage.remove('FullName');
  axios.defaults.headers.common['Token'] = '';
  router.push('/user/login');
};

/**
 * مقدار دهی اولیه دسترسی
 */
const initAccess = function(modelName) {
  var actionsLst = {
    ایجاد: 'canCreate',
    ویرایش: 'canEdit',
    حذف: 'canDelete',
    دسترسی: 'canAccess',
    شهر: 'canCity'
  };

  var accessControl = LocalStorage.get
    .item('subMenuList')
    .find(x => x.EnName.toLowerCase() == modelName.toLowerCase());

  if (accessControl) {
    accessControl = accessControl.UserAccess;
  }
  var pageAccess = {};
  Object.keys(actionsLst).forEach(key => {
    pageAccess[actionsLst[key]] = accessControl
      ? accessControl.includes(key)
      : false;
  });
  return pageAccess;
};
/**
 * تبدیل obj به formData
 */
const objToFormdata = function(obj) {
  var formdata = new FormData();
  Object.keys(obj).forEach(key => formdata.append(key, obj[key]));
  return formdata;
};
const fileAdd = function(model, prop, number, isRequired, chObj, vueObj) {
  debugger;
  if (chObj.$refs['file' + number + ''].files.length > 0)
    vueObj[model][prop] = chObj.$refs['file' + number + ''].files[0];
  if (isRequired) vueObj.$v[model][prop].$touch();
};
const fileRemove = function(model, prop, isRequired, vueObj) {
  debugger;
  vueObj[model][prop] = '';
  if (isRequired) vueObj.$v[model][prop].$touch();
};
//   isNull(value) {
//     return value === null;
//   },
//   isUndefined(value) {
//     return typeof value === 'undefined';
//   },
//   isFunction(value) {
//     return typeof value === 'function';
//   },
//   isBoolean(value) {
//     return typeof value === 'boolean';
//   },
//   isRegExp(value) {
//     return value && typeof value === 'object' && value.constructor === RegExp;
//   },
//   isError(value) {
//     return value instanceof Error && typeof value.message !== 'undefined';
//   },
//   isDate(value) {
//     return value instanceof Date;
//   },
//   isSymbol(value) {
//     return typeof value === 'symbol';
//   }

/**
 * export data
 */
export default {
  mapObject,
  cloneObject,
  clearObject,
  clearArray,
  toParam,
  getNested,
  isString,
  isNumber,
  isArray,
  isObject,
  isBoolean,
  logout,
  initAccess,
  objToFormdata,
  fileAdd,
  fileRemove
};
