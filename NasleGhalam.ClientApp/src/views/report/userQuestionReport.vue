<template>
  <section class="col-md-8">
  


  
    <br />
    <base-table
      :grid-data="reportStore.gridDataUserQuestionReport"
      :columns="reportGridColumns"
      hasIndex
    >
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
      title: "نام",
      data: "Name"
    },
    {
      title: "نام خانواده",
      data: "Family"
    },
    {
      title: "سوالات کارشناسی شده",
      data: "NumberOfQuestionJudged"
    },
    {
      title: "جواب سوالات کارشناسی شده",
      data: "NumberOfQuestionAnswerJudged"
    },

    {
      title: "سوالات تالیف",
      data: "NumberOfWriteQuestion"
    },
    {
      title: "سوالات ناظر",
      data: "NumberOfSupervisorQuestion"
    },
    {
      title: "دپارتمان",
      data: "Department"
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
    this.reportStore.GetAllUsersReport();
  }

  //#endregion
}
</script>

