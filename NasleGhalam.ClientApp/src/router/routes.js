export default [{
    path: '/',
    component: () =>
      import ('layouts/default'),
    children: [{
        path: '',
        component: () =>
          import ('views/grade')
      },
      {
        path: '/grade',
        component: () =>
          import ('views/grade')
      },
      {
        path: '/gradelevel',
        component: () =>
          import ('views/gradeLevel')
      }
    ]
  },

  { // Always leave this as last one
    path: '*',
    component: () =>
      import ('pages/404')
  }
]
