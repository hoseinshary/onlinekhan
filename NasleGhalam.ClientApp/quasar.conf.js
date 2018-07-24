// Configuration for your app
const path = require('path');

module.exports = function(ctx) {
  return {
    // app plugins (/src/plugins)
    plugins: ['i18n', 'vuelidate', 'main'],
    css: ['app.styl'],
    extras: [
      ctx.theme.mat ? 'roboto-font' : null,
      'material-icons' // optional, you are not bound to it
      // 'ionicons',
      // 'mdi',
      // 'fontawesome'
    ],
    supportIE: true,
    build: {
      rtl: true,
      scopeHoisting: true,
      vueRouterMode: 'history',
      // vueCompiler: true,
      // gzip: true,
      // analyze: true,
      // extractCSS: false,
      extendWebpack(cfg) {
        var rules = cfg.module.rules;

        rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /(node_modules|quasar)/
        });
        // rules.push({
        //   test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
        //   loader: "url-loader?limit=10000&mimetype=application/font-woff"
        // });
        rules.push({
          test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
          loader: 'file-loader'
        });

        cfg.resolve.alias = {
          ...cfg.resolve.alias,
          assets: path.resolve(__dirname, './src/assets/'),
          components: path.resolve(__dirname, './src/components/'),
          serviceLayers: path.resolve(__dirname, './src/serviceLayers/'),
          utilities: path.resolve(__dirname, './src/utilities/'),
          views: path.resolve(__dirname, './src/views/'),
          viewModels: path.resolve(__dirname, './src/viewModels/'),
          plugins: path.resolve(__dirname, './src/plugins/'),
          store: path.resolve(__dirname, './src/store/')
        };
      }
    },
    devServer: {
      // https: true,
      // port: 8080,
      open: true // opens browser window automatically
    },
    // framework: 'all' --- includes everything; for dev only!
    framework: {
      components: [
        'QLayout',
        'QLayoutHeader',
        'QLayoutDrawer',
        'QPageContainer',
        'QPage',
        'QToolbar',
        'QToolbarTitle',
        'QBtn',
        'QIcon',
        'QList',
        'QListHeader',
        'QItem',
        'QItemMain',
        'QItemSide',
        'QModal',
        'QModalLayout',
        'QInput',
        'QCard',
        'QCardTitle',
        'QCardMain',
        'QCardSeparator',
        'QField',
        'QSelect',
        'QRadio',
        'QCheckbox',
        'QToggle',
        'QPagination',
        'QTooltip',
        'Loading'
      ],
      directives: ['Ripple', 'CloseOverlay'],
      // Quasar plugins
      plugins: ['Notify'],
      // iconSet: ctx.theme.mat ? 'material-icons' : 'ionicons'
      i18n: 'fa-ir' // Quasar language
    },
    // animations: 'all' --- includes all animations
    animations: [],
    pwa: {
      // workboxPluginMode: 'InjectManifest',
      // workboxOptions: {},
      manifest: {
        // name: 'Quasar App',
        // short_name: 'Quasar-PWA',
        // description: 'Best PWA App in town!',
        display: 'standalone',
        orientation: 'portrait',
        background_color: '#ffffff',
        theme_color: '#027be3',
        icons: [
          {
            src: 'statics/icons/icon-128x128.png',
            sizes: '128x128',
            type: 'image/png'
          },
          {
            src: 'statics/icons/icon-192x192.png',
            sizes: '192x192',
            type: 'image/png'
          },
          {
            src: 'statics/icons/icon-256x256.png',
            sizes: '256x256',
            type: 'image/png'
          },
          {
            src: 'statics/icons/icon-384x384.png',
            sizes: '384x384',
            type: 'image/png'
          },
          {
            src: 'statics/icons/icon-512x512.png',
            sizes: '512x512',
            type: 'image/png'
          }
        ]
      }
    },
    cordova: {
      // id: 'org.cordova.quasar.app'
    },
    electron: {
      // bundler: 'builder', // or 'packager'
      extendWebpack(cfg) {
        // do something with Electron process Webpack cfg
      },
      packager: {
        // https://github.com/electron-userland/electron-packager/blob/master/docs/api.md#options
        // OS X / Mac App Store
        // appBundleId: '',
        // appCategoryType: '',
        // osxSign: '',
        // protocol: 'myapp://path',
        // Window only
        // win32metadata: { ... }
      },
      builder: {
        // https://www.electron.build/configuration/configuration
        // appId: 'quasar-app'
      }
    }
  };
};
