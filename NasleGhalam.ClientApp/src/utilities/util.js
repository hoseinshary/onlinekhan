/**
 * map all value of source object to dest object
 * @param {Object} objSrc
 * @param {Object} objDest
 */
const mapObject = function(objSrc, objDest) {
  Object.keys(objSrc).forEach(key => {
    if (objDest[key] !== undefined) objSrc[key] = objDest[key];
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
    //   if(obj[key])
    obj[key] = '';
  });
};

/**
 * export data
 */
export default { mapObject, cloneObject, clearObject };
