<template>
  <fieldset class="col-12 q-my-sm">
    <legend>سابقه تدریس</legend>
    <section class="row gutter-sm">
      <base-input :model="$v.teachingResume.School" class="col-md-3" />
      <base-input :model="$v.teachingResume.LessonName" class="col-md-3" />
      <base-input :model="$v.teachingResume.Grade" class="col-md-3" />
      <base-input :model="$v.teachingResume.StartYear" class="col-md-3" />
      <base-input :model="$v.teachingResume.EndYear" class="col-md-3" />

      <div class="col-3">
        <base-btn-create label="ایجاد" @click="resumeStore.addTeachingResume" />
      </div>

      <div class="col-12">
        <table class="my-table">
          <thead>
            <tr>
              <th>آموزشگاه</th>
              <th>درس</th>
              <th>مقطع</th>
              <th>سال شروع</th>
              <th>سال پایان</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(teachingResume, index) in teachingResumeList" :key="index">
              <td>{{teachingResume.School}}</td>
              <td>{{teachingResume.LessonName}}</td>
              <td>{{teachingResume.Grade}}</td>
              <td>{{teachingResume.StartYear}}</td>
              <td>{{teachingResume.EndYear}}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </section>
  </fieldset>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { teachingResumeValidations } from "src/validations/ResumeValidation";
import { KindRequest } from "../../utilities/enumeration";

@Component({
  validations: teachingResumeValidations
})
export default class TeachingResumeVue extends Vue {
  $v: any;

  //#region ### data ###
  resumeStore = vxm.resumeStore;
  teachingResume = vxm.resumeStore.teachingResume;
  //#endregion

  //#region ### computed ###
  get teachingResumeList() {
    return this.resumeStore.resume.TeachingResumes;
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.resumeStore.SET_TEACHING_RESUME_VUE(this);
  }
  //#endregion
}
</script>