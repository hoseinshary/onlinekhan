var fs = require('fs');

var arg = process.argv.slice(2);
// console.log(arg);
if (arg.length != 1) {
  console.log('modelName is not define.');
  process.exit();
}
var modelName = arg[0];
var configUrl = fs
  .readFileSync(`${__dirname}/src/utilities/site-config.js`, 'utf8')
  .replace(
    '\r\nexport {',
    `const ${modelName.toUpperCase()}_URL =  '/api/${modelName.replace(
      '_',
      ''
    )}';\r\n\r\nexport {`
  )
  .replace('_URL\r\n};', `_URL,\r\n  ${modelName.toUpperCase()}_URL\r\n};`);
fs.writeFileSync(
  `${__dirname}/src/utilities/site-config.js`,
  configUrl,
  'utf8'
);