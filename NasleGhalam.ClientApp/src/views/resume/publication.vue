<template>
  <fieldset class="col-12 q-my-sm">
    <legend>سابقه تالیف</legend>
    <section class="row gutter-sm">
      <base-input :model="$v.publication.Publisher" class="col-md-3" />
      <base-input :model="$v.publication.Grade" class="col-md-3" />
      <base-input :model="$v.publication.LessonName" class="col-md-3" />
      <base-select :model="$v.publication.KindRequest" :options="kindRequestDdl" class="col-md-3" />
      <base-input :model="$v.publication.Year" class="col-md-3" />
      <base-field class="col-md-3" :model="$v.publication.PublishedOrEdit">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
          <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
        </template>
      </base-field>

      <div class="col-3">
        <base-btn-create label="ایجاد" @click="resumeStore.addPublication" />
      </div>

      <div class="col-12">
        <table class="my-table">
          <thead>
            <tr>
              <th>ناشر</th>
              <th>مقطع</th>
              <th>درس</th>
              <th>نوع کتاب</th>
              <th>سال</th>
              <th>تالیف یا ویرایش</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(publication, index) in publicationList" :key="index">
              <td>{{publication.Publisher}}</td>
              <td>{{publication.Grade}}</td>
              <td>{{publication.LessonName}}</td>
              <td>{{kindRequestName(publication.KindRequest)}}</td>
              <td>{{publication.Year}}</td>
              <td>{{publication.PublishedOrEdit}}</td>
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
import { publicationValidations } from "src/validations/ResumeValidation";
import { KindRequest } from "../../utilities/enumeration";

@Component({
  validations: publicationValidations
})
export default class PublicationVue extends Vue {
  $v: any;

  //#region ### data ###
  resumeStore = vxm.resumeStore;
  publication = vxm.resumeStore.publication;
  //#endregion

  //#region ### computed ###
  get kindRequestDdl() {
    return util.enumToDdl(KindRequest);
  }

  get publicationList() {
    return this.resumeStore.resume.Publications;
  }

  get kindRequestName() {
    return (index: number) => {
      return KindRequest[index];
    };
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.resumeStore.SET_PUBLICATION_VUE(this);
  }
  //#endregion
}
</script>