<template>
  <section class="col-md-8">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${reportStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="reportStore.gridData" :columns="reportGridColumns" hasIndex>
      
    </base-table>


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
    this.reportStore.fillList();
  }

  //#endregion
}
</script>