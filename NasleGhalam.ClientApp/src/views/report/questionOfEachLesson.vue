<template>
  <section class="col-md-8">
   
    <br />
    <base-table :grid-data="reportStore.gridDataQuestionOfEachLesson" :columns="reportGridColumns" hasIndex>
      
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
      title: "سوالات وارد شده",
      data: "AllQuestionNum"
    },
    {
      title: "سوالات شاخه بندی شده",
      data: "AllQuestionTopiced"
    },
    {
      title: "سوالات دارای کارشناسی",
      data: "AllQuestionJudged"
    },
    
    {
      title: "سوالات کارشناسی شده کامل",
      data: "AllQuestionJudgedFull"
    },
    {
      title: "سوالات فعال",
      data: "AllQuestionActived"
    },
    {
      title: "سوالات ترکیبی",
      data: "AllQuestionHybrid"
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