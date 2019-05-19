<template>
  <section class="col-md-11">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{lesson_UserStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-md-5">
            <base-table :grid-data="lesson_UserStore.userData" :columns="userGridColumn">
              <template slot="Checked" slot-scope="data">
                <q-checkbox v-model="data.row.Checked"/>
              </template>
            </base-table>
          </div>
          <div class="col-md-2 text-center">
            <q-btn outline color="primary" class="shadow-1 bg-white q-ma-sm">
              کاربرهای منتسب به درس
              <q-icon name="arrow_back"/>
            </q-btn>
            <br>
            <q-btn outline color="primary" class="shadow-1 bg-white q-ma-sm">
              <q-icon name="arrow_forward"/>درس های منتسب به کاربر
            </q-btn>
          </div>
          <div class="col-md-5">
            <base-table :grid-data="lesson_UserStore.lessonData" :columns="lessonGridColumn">
              <template slot="Checked" slot-scope="data">
                <q-checkbox v-model="data.row.Checked"/>
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </base-panel>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({})
export default class Lesson_UserVue extends Vue {
  //#region ### data ###
  lesson_UserStore = vxm.lesson_UserStore;
  pageAccess = util.getAccess(this.lesson_UserStore.modelName);
  lessonGridColumn = [
    {
      title: "انتخاب",
      data: "Checked"
    },
    {
      title: "نام",
      data: "Name"
    }
  ];
  userGridColumn = [
    {
      title: "انتخاب",
      data: "Checked"
    },
    {
      title: "نام",
      data: "FullName"
    },
    {
      title: "کد ملی",
      data: "NationalNo"
    }
  ];
  //#endregion

  //#region ### computed ###
  get canCreateDelete() {
    return this.pageAccess.indexOf("ایجاد/حذف") > -1;
  }
  //#endregion

  //#region ### methods ###

  //#endregion

  //#region ### hooks ###
  created() {
    this.lesson_UserStore.SET_INDEX_VUE(this);
    this.lesson_UserStore.fillLessonList();
    this.lesson_UserStore.fillUserList();
  }
  //#endregion
}
</script>