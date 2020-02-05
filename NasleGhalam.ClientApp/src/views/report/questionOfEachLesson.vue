<template>
  <section class="col-md-8 q-px-lg">
    <section slot="body" class="row gutter-sm">
      <div class>
   
  <base-table :grid-data="reportStore.getAllQuestionOfEachLesson" :columns="reportGridColumns" hasIndex>
      <template slot="IsActive" slot-scope="data">
        <q-checkbox v-model="data.row.IsActive" readonly />
      </template>
      <template slot="Id" slot-scope="data">
        <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
        <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
      </template>
    </base-table>


       
      </div>
    </section>
  </section>
</template>


<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {}
})
export default class QuestionVue extends Vue {
  //#region ### data ###
  reportStore = vxm.reportStore;

   reportGridColumns = [
    {
      title: "درس",
      data: "Name"
    },
    {
      title: "تعداد سوالات وارد شده",
      data: "AllQuestionNum"
    },
    {
      title: "تعداد سوالات شاخه بندی شده",
      data: "AllQuestionTopiced"
    },
    {
      title: "تعداد سوالات دارای کارشناسی",
      data: "AllQuestionJudged"
    },
    {
      title: "تعداد سوالات تایید شده",
      data: "AllQuestionActived"
    },
    {
      title: "تعداد سوالات ترکیبی",
      data: "AllQuestionHybrid"
    },
    {
      title: "تعداد سوالات کارشناسی شده کامل",
      data: "AllQuestionJudgedFull"
    }
  ];

  //#endregion

  //#region ### computed ###

  //#endregion

  //#region ### watch ###

  //#endregion

  //#region ### methods ###

  //#endregion

  //#region ### hooks ###
  created() {
    this.reportStore.getAllQuestionOfEachLesson();
  }

  //#endregion
}
</script>