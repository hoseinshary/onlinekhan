export default [
  // {
  //   path: "/home/index",
  //   component: () => import("src/views/home/index.vue")
  // },
  {
    path: "/",
    name: "home",
    // component: () => import(/* webpackChunkName: "home" */"src/views/home/index.vue")
    component: () => import(/* webpackChunkName: "login" */"src/views/user/login.vue")

  },
  {
    path: "/user/login",
    name: "login",
    component: () => import(/* webpackChunkName: "login" */"src/views/user/login.vue")
  },
  {
    path: "/resume/registration",
    name: "registration",
    component: () => import(/* webpackChunkName: "registration" */"src/views/resume/registration.vue")
  },
  {
    path: "/topic/printTopic",
    name: "printTopic",
    component: () => import(/* webpackChunkName: "printTopic" */"src/views/topic/printTopic.vue")
  },
  {
    path: "/studentMajorList/printMajorList",
    name: "printMajorList",
    component: () => import(/* webpackChunkName: "printMajorList" */"src/views/studentMajorList/printMajorList.vue")
  },
  {
    path: "",
    name:"layouts",
    component: () => import(/* webpackChunkName: "layouts" */"layouts/default.vue"),
    children: [
      // {
      //   path: "",
      //   component: () => import("src/views/city/index.vue")
      // },
      {
        path: "/province",
        name: "province",
        component: () => import(/* webpackChunkName: "province" */"src/views/province/index.vue")
      },
      {
        path: "/city",
        name: "city",
        component: () => import(/* webpackChunkName: "city" */"src/views/city/index.vue")
      },
      {
        path: "/lesson",
        name: "lesson",
        component: () => import(/* webpackChunkName: "lesson" */"src/views/lesson/index.vue")
      },
      {
        path: "/role",
        name: "role",
        component: () => import(/* webpackChunkName: "role" */"src/views/role/index.vue")
      },
      {
        path: "/user",
        name: "user",
        component: () => import(/* webpackChunkName: "user" */"src/views/user/index.vue")
      },
      {
        path: "/educationSubGroup",
        name: "educationSubGroup",
        component: () => import(/* webpackChunkName: "educationSubGroup" */"src/views/educationSubGroup/index.vue")
      },
      {
        path: "/topic",
        name: "topic",
        component: () => import(/* webpackChunkName: "topic" */"src/views/topic/index.vue")
      },
      {
        path: "/educationBook",
        name: "educationBook",
        component: () => import(/* webpackChunkName: "educationBook" */"src/views/educationBook/index.vue")
      },
      {
        path: "/publisher",
        name: "publisher",
        component: () => import(/* webpackChunkName: "publisher" */"src/views/publisher/index.vue")
      },
      {
        path: "/tag",
        name: "tag",
        component: () => import(/* webpackChunkName: "tag" */"src/views/tag/index.vue")
      },
      {
        path: "/educationYear",
        name: "educationYear",
        component: () => import(/* webpackChunkName: "educationYear" */"src/views/educationYear/index.vue")
      },
      {
        path: "/axillaryBook",
        name: "axillaryBook",
        component: () => import(/* webpackChunkName: "axillaryBook" */"src/views/axillaryBook/index.vue")
      },
      {
        path: "/question",
        name: "question",
        component: () => import(/* webpackChunkName: "question" */"src/views/question/index.vue")
      },
      {
        path: "/question/:id/:lessonId",
        name: "question2",
        component: () => import(/* webpackChunkName: "question2" */"src/views/question/index.vue")
      },
      {
        path: "/student",
        name: "student",
        component: () => import(/* webpackChunkName: "student" */"src/views/student/index.vue")
      },
      {
        path: "/teacher",
        name: "teacher",
        component: () => import(/* webpackChunkName: "teacher" */"src/views/teacher/index.vue")
      },
      {
        path: "/educationTree",
        name: "educationTree",
        component: () => import(/* webpackChunkName: "educationTree" */"src/views/educationTree/index.vue")
      },
      {
        path: "/questionGroup",
        name: "questionGroup",
        component: () => import(/* webpackChunkName: "questionGroup" */"src/views/questionGroup/index.vue")
      },
      {
        path: "/writer",
        name: "writer",
        component: () => import(/* webpackChunkName: "writer" */"src/views/writer/index.vue")
      },
      {
        path: "/lesson_User",
        name: "lesson_User",
        component: () => import(/* webpackChunkName: "lesson_User" */"src/views/lesson_User/index.vue")
      },
      {
        path: "/lessonDepartment",
        name: "lessonDepartment",
        component: () => import(/* webpackChunkName: "lessonDepartment" */"src/views/lessonDepartment/index.vue")
      },
      {
        path: "/assay",
        name: "assay",
        component: () => import(/* webpackChunkName: "assay" */"src/views/assay/index.vue")
      },
      {
        path: "/resume",
        name: "resume",
        component: () => import(/* webpackChunkName: "resume" */"src/views/resume/index.vue")
      },
      {
        path: "/package",
        name: "package",
        component: () => import(/* webpackChunkName: "package" */"src/views/package/index.vue")
      },
      {
        path: "/user/register",
        name: "register",
        component: () => import(/* webpackChunkName: "register" */"src/views/user/register.vue")
      },
      {
        path: "/panel/expertPanel",
        name: "expertPanel",
        component: () => import(/* webpackChunkName: "expertPanel" */"src/views/panel/expertPanel.vue")
      },
      {
        path: "/panel/adminPanel",
        name: "adminPanel",
        component: () => import(/* webpackChunkName: "adminPanel" */"src/views/panel/adminPanel.vue")
      },
      {
        path: "/panel/teacherPanel",
        name: "teacherPanel",
        component: () => import(/* webpackChunkName: "teacherPanel" */"src/views/panel/teacherPanel.vue")
      },
      {
        path: "/panel/studentPanel",
        name: "studentPanel",
        component: () => import(/* webpackChunkName: "teacherPanel" */"src/views/panel/studentPanel.vue")
      },
      {
        path: "/report/questionOfEachLesson",
        name: "questionOfEachLesson",
        component: () => import(/* webpackChunkName: "questionOfEachLesson" */"src/views/report/questionOfEachLesson.vue")
      },
      {
        path: "/report/userQuestionReport",
        name: "userQuestionReport",
        component: () => import(/* webpackChunkName: "userQuestionReport" */"src/views/report/userQuestionReport.vue")
      },
      {
        path: "/report/QuestionsReport",
        name: "QuestionsReport",
        component: () => import(/* webpackChunkName: "QuestionsReport" */"src/views/report/questionsReport.vue")
      },
      {
        path: "/program",
        name: "program",
        component: () => import(/* webpackChunkName: "program" */"src/views/program/index.vue")
      },
      {
        path: "/media",
        name: "media",
        component: () => import(/* webpackChunkName: "program" */"src/views/media/index.vue")
      },
      {
        path: "/AssayAdvisor",
        name: "assayAdvisor",
        component: () => import(/* webpackChunkName: "program" */"src/views/assay/AssayAdvisor/index.vue")
      },
      {
        path: "/AssayTeacher",
        name: "assayTeacher",
        component: () => import(/* webpackChunkName: "program" */"src/views/assay/AssayTeacher/index.vue")
      },
      {
        path: "/StudentMajorList",
        name: "studentMajorList",
        component: () => import(/* webpackChunkName: "program" */"src/views/studentMajorList/index.vue")
      },
      {
        path: "/Assay/runAssay",
        name: "runAssay",
        component: () => import(/* webpackChunkName: "program" */"src/views/assay/runAssay.vue")
      },
      {
        path: "/Assay/resualtAssay",
        name: "resualtAssay",
        component: () => import(/* webpackChunkName: "program" */"src/views/assay/resualtAssay.vue")
      }
      /* webpackChunkName: "teacherGroup" */
      ,
      {
        path: "/teacherGroup",
        name: "teacherGroup",
        component: () => import("src/views/teacherGroup/index.vue")
      
      },
      {
        path: "/Assay/report",
        name: "report",
        component: () => import(/* webpackChunkName: "program" */"src/views/assay/report.vue")
      },
      {
        path: "/Assay/index_resualt",
        name: "index_resualt",
        component: () => import(/* webpackChunkName: "program" */"src/views/assay/index_resualt.vue")
      },
      {
        path: "/student/buyLesson",
        name: "buyLesson",
        component: () => import(/* webpackChunkName: "lesson" */"src/views/lesson/buyLesson.vue")
      },
      {
        path: "/student/myLesson",
        name: "myLesson",
        component: () => import(/* webpackChunkName: "lesson" */"src/views/lesson/myLesson.vue")
      },
      {
        path: "/panel/studentPanel",
        name: "studentPanel",
        component: () => import(/* webpackChunkName: "teacherPanel" */"src/views/panel/studentPanel.vue")
      }
    ]

  },
  {
    // Always leave this as last one
    path: "*",
    name:"notFound",
    component: () => import(/* webpackChunkName: "notFound" */"pages/404.vue")
  }
];
