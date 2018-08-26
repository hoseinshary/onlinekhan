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
        component: () => import('views/user')
      },
      {
        path: '/educationGroup',
        component: () => import('views/educationGroup')
      },
      {
        path: '/educationSubGroup',
        component: () => import('views/educationSubGroup')
      },
      {
        path: '/topic',
        component: () => import('views/topic')
      }
    ]
  },

  {
    // Always leave this as last one
    path: '*',
    component: () => import('pages/404')
  }
];
