import { LocalStorage } from "quasar";
import router from "src/router";
import axios from "src/plugins/axios";

/**
 * map all value of source object to dest object
 * @param {Object} cloneFrom
 * @param {Object} cloneTo
 */
const mapObject = function (cloneFrom, cloneTo) {
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
const cloneObject = function (obj) {
  return JSON.parse(JSON.stringify(obj));
  //Object.assign({}, obj)
};

/**
 * clear value of object
 * @param {Object} obj
 */
const clearObject = function (obj) {
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
const clearArray = function (arr) {
  arr.splice(0, arr.length);
};

/**
 * turn an object into query string parameters
 * @param {Object} obj
 */
const toParam = function (obj) {
  return encodeURI(toParamWithoutEncode(obj));
};

const toParamWithoutEncode = function (obj) {
  return Object.keys(obj)
    .map(key => {
      var value = obj[key];
      if (isObject(value)) {
        return toParam(value);
      }
      if (isArray(value)) {
        return value.map(x => key + "=" + x).join("&");
      }
      return key + "=" + value;
    })
    .join("&");
};

/**
 * access to nested object by string
 * @param {*} theObject
 * @param {String} path
 */
const getNested = function (theObject, path) {
  try {
    var separator = ".";

    return path
      .replace("[", separator)
      .replace("]", "")
      .split(separator)
      .reduce(function (obj, property) {
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
const isString = function (value) {
  return typeof value === "string" || value instanceof String;
};

/**
 * check type value be array
 * @param {*} value
 */
const isNumber = function (value) {
  return typeof value === "number" && isFinite(value);
};

/**
 * check type value be array
 * @param {*} value
 */
const isArray = function (value) {
  return value && typeof value === "object" && value.constructor === Array;
};

/**
 * check type value be object
 * @param {*} value
 */
const isObject = function (value) {
  return value && typeof value === "object" && value.constructor === Object;
};

/**
 * check type value be boolean
 * @param {*} value
 */
const isBoolean = function (value) {
  return typeof value === "boolean";
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
 * handle logout
 */
const logout = function () {
  LocalStorage.remove("authList");
  LocalStorage.remove("menuList");
  LocalStorage.remove("subMenuList");
  LocalStorage.remove("Token");
  LocalStorage.remove("FullName");
  axios.defaults.headers.common["Token"] = "";
  router.push("/user/login");
};

/**
 * مقدار دهی اولیه دسترسی
 */
const initAccess = function (modelName) {
  var actionsLst = {
    ایجاد: "canCreate",
    ویرایش: "canEdit",
    حذف: "canDelete",
    دسترسی: "canAccess"
  };
  var accessControl = LocalStorage.get
    .item("subMenuList")
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
const objToFormdata = function (obj) {
  var formdata = new FormData();
  Object.keys(obj).forEach(key => formdata.append(key, obj[key]));
  return formdata;
};

const fileAdd = function (model, prop, number, isRequired, chObj, vueObj) {
  if (chObj.$refs["file" + number + ""].files.length > 0)
    vueObj[model][prop] = chObj.$refs["file" + number + ""].files[0];
  if (isRequired) vueObj.$v[model][prop].$touch();
};

const fileRemove = function (model, prop, isRequired, vueObj) {
  vueObj[model][prop] = "";
  if (isRequired) vueObj.$v[model][prop].$touch();
};

/**
 * convert flatern list to tree
 * @param {array} list
 * @param {string} key
 * @param {string} parentKey
 */
const listToTree = function (list, key, parentKey) {
  var map = {},
    node,
    roots: any = [],
    i;
  for (i = 0; i < list.length; i += 1) {
    map[list[i][key]] = i; // initialize the map
    list[i].children = []; // initialize the children
  }
  for (i = 0; i < list.length; i += 1) {
    node = list[i];
    if (node[parentKey] !== null) {
      // if you have dangling branches check that map[node.parentId] exists
      list[map[node[parentKey]]].children.push(node);
    } else {
      roots.push(node);
    }
  }
  return roots;
};

/**
 * search through tree
 * @param {array} array
 * @param {string} key
 * @param {string} value
 */
const searchTreeArray = function (array, key, value) {
  var i;
  var parentNode = null;
  for (i = 0; parentNode == null && i < array.length; i++) {
    parentNode = searchTree(array[i], key, value);
  }
  return parentNode;
};

const searchTree = function (element, key, value) {
  if (element[key] == value) {
    return element;
  } else if (element.children != null) {
    var i;
    var result = null;
    for (i = 0; result == null && i < element.children.length; i++) {
      result = searchTree(element.children[i], key, value);
    }
    return result;
  }
  return null;
};
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
  fileRemove,
  listToTree,
  // searchTree,
  searchTreeArray
};
