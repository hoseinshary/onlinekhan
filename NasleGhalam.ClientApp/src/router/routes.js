export default [
  {
    path: "/",
    component: () => import("src/views/home/index.vue")
  },
  {
    path: "/home/index",
    component: () => import("src/views/home/index.vue")
  },
  {
    path: "/user/login",
    component: () => import("src/views/user/login.vue")
  },
  {
    path: "/resume/registration",
    component: () => import("src/views/resume/registration.vue")
  },
  {
    path: "/topic/printTopic",
    component: () => import("src/views/topic/printTopic.vue")
  },
  {
    path: "/",
    component: () => import("layouts/default.vue"),
    children: [
      // {
      //   path: "",
      //   component: () => import("src/views/city/index.vue")
      // },
      {
        path: "/province",
        component: () => import("src/views/province/index.vue")
      },
      {
        path: "/city",
        component: () => import("src/views/city/index.vue")
      },
      {
        path: "/lesson",
        component: () => import("src/views/lesson/index.vue")
      },
      {
        path: "/role",
        component: () => import("src/views/role/index.vue")
      },
      {
        path: "/user",
        component: () => import("src/views/user/index.vue")
      },
      {
        path: "/educationSubGroup",
        component: () => import("src/views/educationSubGroup/index.vue")
      },
      {
        path: "/topic",
        component: () => import("src/views/topic/index.vue")
      },
      {
        path: "/educationBook",
        component: () => import("src/views/educationBook/index.vue")
      },
      {
        path: "/publisher",
        component: () => import("src/views/publisher/index.vue")
      },
      {
        path: "/tag",
        component: () => import("src/views/tag/index.vue")
      },
      {
        path: "/educationYear",
        component: () => import("src/views/educationYear/index.vue")
      },
      {
        path: "/axillaryBook",
        component: () => import("src/views/axillaryBook/index.vue")
      },
      {
        path: "/question",
        component: () => import("src/views/question/index.vue")
      },
      {
        path: "/question/:id/:lessonId",
        component: () => import("src/views/question/index.vue")
      },
      {
        path: "/student",
        component: () => import("src/views/student/index.vue")
      },
      {
        path: "/teacher",
        component: () => import("src/views/teacher/index.vue")
      },
      {
        path: "/educationTree",
        component: () => import("src/views/educationTree/index.vue")
      },
      {
        path: "/questionGroup",
        component: () => import("src/views/questionGroup/index.vue")
      },
      {
        path: "/writer",
        component: () => import("src/views/writer/index.vue")
      },
      {
        path: "/lesson_User",
        component: () => import("src/views/lesson_User/index.vue")
      },
      {
        path: "/lessonDepartment",
        component: () => import("src/views/lessonDepartment/index.vue")
      },
      {
        path: "/assay",
        component: () => import("src/views/assay/index.vue")
      },
      {
        path: "/resume",
        component: () => import("src/views/resume/index.vue")
      },
      {
        path: "/package",
        component: () => import("src/views/package/index.vue")
      },
      {
        path: "/user/register",
        component: () => import("src/views/user/register.vue")
      },
      {
        path: "/panel/expertPanel",
        component: () => import("src/views/panel/expertPanel.vue")
      },
      {
        path: "/panel/adminPanel",
        component: () => import("src/views/panel/adminPanel.vue")
      },
      {
        path: "/panel/teacherPanel",
        component: () => import("src/views/panel/teacherPanel.vue")
      }
    ]
  },
  {
    // Always leave this as last one
    path: "*",
    component: () => import("pages/404.vue")
  }
];
