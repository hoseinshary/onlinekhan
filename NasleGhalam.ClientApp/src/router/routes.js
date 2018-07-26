export default [
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
      }
    ]
  },

  {
    // Always leave this as last one
    path: '*',
    component: () => import('pages/404')
  }
];
