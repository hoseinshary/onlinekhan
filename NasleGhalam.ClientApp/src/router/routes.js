// export default [
//   {
//     path: '/',
//     component: () => import('layouts/default'),
//     children: [
//       { path: '', component: () => import('pages/index') }
//     ]
//   },

//   { // Always leave this as last one
//     path: '*',
//     component: () => import('pages/404')
//   }
// ]

const routes = [
  {
    path: "/",
    component: () => import("views/user/login")
  },
  { path: "/user/login", component: () => import("views/user/login"),meta: {
    title: 'ورود',
   } },
  {
    path: "/",
    component: () => import("layouts/default"),
    children: [
      { path: "", component: () => import("pages/index")  },
      { path: "/dashboard", component: () => import("views/dashboard/index"),meta: {
        title: 'داشبورد',
       } },
      {
        path: "/dashboard/map",
        component: () => import("views/dashboard/map"),meta: {
          title: 'داشبورد',
         }
      },
      {
        path: "/dashboard/changeStates",
        component: () => import("views/dashboard/changeStates"),meta: {
          title: 'تغییرات',
         }
      },
      {
        path: "/region",
        component: () => import("views/region/index"),meta: {
          title: 'منطقه',
         }
      },
      {
        path: "/sensor",
        component: () => import("views/sensor/index"),meta: {
          title: 'سنسور',
         }
      },
      {
        path: "/role",
        component: () => import("views/role/index"),meta: {
          title: 'نقش',
         }
      },
      {
        path: "/user",
        component: () => import("views/user/index"),meta: {
          title: 'کاربر',
         }
      }
    ]
  },

  {
    path: "*",
    component: () => import("pages/404")
  }
];

export default routes;
