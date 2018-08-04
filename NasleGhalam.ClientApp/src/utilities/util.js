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
    if (cloneFrom[key] !== undefined) cloneTo[key] = cloneFrom[key];
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
    if (isString(obj[key])) {
      obj[key] = '';
    } else if (isNumber(obj[key])) {
      // obj[key] = 0;
      obj[key] = undefined;
    } else if (isBoolean(obj[key])) {
      obj[key] = false;
      // obj[key] = undefined;
    } else if (isArray(obj[key])) {
      clearArray(obj[key]);
    } else if (isObject(obj[key])) {
      clearObject(obj[key]);
    } else {
      obj[key] = undefined;
    }
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
  LocalStorage.remove('Token');
  LocalStorage.remove('FullName');
  axios.defaults.headers.common['Token'] = '';
  router.push('/user/login');
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
  isString,
  isNumber,
  isArray,
  isObject,
  isBoolean,
  logout
};
