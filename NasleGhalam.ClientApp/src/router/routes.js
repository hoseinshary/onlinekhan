let str = 'views/user';
export default [
  {
    path: '/user/login',
    component: () => import('views/user/login')
  },
  {
    path: '/',
    component: () => import('layouts/default'),
    children: [
      {
        path: '',
        component: () => import('views/grade')
      },
      {
        path: '/grade',
        component: () => import('views/grade')
      },
      {
        path: '/gradelevel',
        component: () => import('views/gradeLevel')
      },
      {
        path: '/province',
        component: () => import('views/province')
      },
      {
        path: '/city',
        component: () => import('views/city')
      },
      {
        path: '/lesson',
        component: () => import('views/lesson')
      },
      {
        path: '/role',
        component: () => import('views/role')
      },
      {
        path: '/user',
        component: () => import(str)
      }
    ]
  },

  {
    // Always leave this as last one
    path: '*',
    component: () => import('pages/404')
  }
];
