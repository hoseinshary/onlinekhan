export default [
  {
    path: "/user/login",
    component: () => import("views/user/login.vue")
  },
  {
    path: "/",
    component: () => import("layouts/default.vue"),
    children: [
      // {
      //   path: '',
      //   component: () => import('views/test/HelloDecorator')
      // },
      {
        path: "",
        component: () => import("views/city.vue")
      },
      {
        path: "/province",
        component: () => import("views/province.vue")
      },
      {
        path: "/city",
        component: () => import("views/city.vue")
      },
      {
        path: "/lesson",
        component: () => import("views/lesson.vue")
      },
      {
        path: "/role",
        component: () => import("views/role.vue")
      },
      {
        path: "/user",
        component: () => import("views/user.vue")
      },
      {
        path: "/educationSubGroup",
        component: () => import("views/educationSubGroup.vue")
      },
      {
        path: "/topic",
        component: () => import("views/topic.vue")
      },
      {
        path: "/educationBook",
        component: () => import("views/educationBook.vue")
      },
      {
        path: "/publisher",
        component: () => import("views/publisher.vue")
      },
      {
        path: "/tag",
        component: () => import("views/tag.vue")
      },
      {
        path: "/educationYear",
        component: () => import("views/educationYear.vue")
      },
      {
        path: "/axillaryBook",
        component: () => import("views/axillaryBook.vue")
      },
      {
        path: "/question",
        component: () => import("views/question.vue")
      },
      {
        path: "/student",
        component: () => import("views/student.vue")
      },
      {
        path: "/educationTree",
        component: () => import("views/educationTree.vue")
      },
      {
        path: "/questionGroup",
        component: () => import("views/questionGroup.vue")
      }
    ]
  },

  {
    // Always leave this as last one
    path: "*",
    component: () => import("pages/404.vue")
  }
];
