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
      },
      {
        path: '/educationBook',
        component: () => import('views/educationBook')
      },
      {
        path: '/exam',
        component: () => import('views/exam')
      },
      {
        path: '/publisher',
        component: () => import('views/publisher')
      },
      {
        path: '/tag',
        component: () => import('views/tag')
      },
      {
        path: '/educationYear',
        component: () => import('views/educationYear')
      },
      {
        path: '/axillaryBook',
        component: () => import('views/axillaryBook')
      },
      {
        path: '/universityBranch',
        component: () => import('views/universityBranch')
      },
      {
        path: '/question',
        component: () => import('views/question')
      },
      {
        path: '/student',
        component: () => import('views/student')
      },
      {
        path: '/educationTree',
        component: () => import('views/educationTree')
      }
    ]
  },

  {
    // Always leave this as last one
    path: '*',
    component: () => import('pages/404')
  }
];
