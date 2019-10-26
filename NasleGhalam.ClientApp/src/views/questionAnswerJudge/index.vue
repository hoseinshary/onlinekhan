<template>
  <section class="s-border s-spacing">
    <base-table
      :grid-data="questionAnswerJudgeStore.gridData"
      :columns="questionAnswerJudgeGridColumn"
      hasIndex
    >
      <template
        slot="Lookup_ReasonProblem.Value"
        slot-scope="data"
      >{{data.row.Lookup_ReasonProblem.Value}}</template>
      <template slot="User.FullName" slot-scope="data">{{data.row.User.FullName}}</template>
      <!-- <template slot="IsDelete" slot-scope="data">
        <span v-if="data.row.IsDelete">حذف</span>
        <span v-else-if="data.row.IsUpdate">ویرایش</span>
        <span v-else>تایید</span>
      </template>-->
      <!-- <template slot="Id" slot-scope="data">
        <base-btn-edit v-if="canEdit" round @click="showTabEdit(data.row.Id)" />
        <btn-delete v-if="canDelete" :recordIdProp="data.row.Id"></btn-delete>
      </template>-->
    </base-table>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    TabCreate: () => import("./create.vue")
    // TabEdit: () => import("./edit.vue"),
    // BtnDelete: () => import("./delete.vue")
  }
})
export default class QuestionAnswerJudgeVue extends Vue {
  //#region ### data ###
  questionAnswerJudgeStore = vxm.questionAnswerJudgeStore;
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  questionAnswerJudge = this.questionAnswerJudgeStore.questionAnswerJudge;
  question = this.questionStore.question;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionAnswerJudgeGridColumn = [
    {
      title: "فعال",
      data: "IsActiveQuestionAnswer"
    },
    {
      title: "درس",
      data: "LessonName"
    },
    {
      title: "دلیل مشکل",
      data: "Lookup_ReasonProblem.Value"
    },
    {
      title: "کاربر",
      data: "User.FullName"
    }
    // {
    //   title: "عملیات",
    //   data: "Id",
    //   searchable: false,
    //   sortable: false,
    //   visible: this.canEdit || this.canDelete
    // }
  ];
  selectedTab = "tab-create";
  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد کارشناسی جواب سوال") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش کارشناسی جواب سوال") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف کارشناسی جواب سوال") > -1;
  }

  get editMode() {
    return this.selectedTab == "tab-edit";
  }
  //#endregion

  //#region ### methods ###
  showTabCreate() {
    this.questionAnswerJudgeStore.resetCreate();
    this.questionAnswerJudge.QuestionAnswerId = this.question.Id;
  }

  showTabEdit(id) {
    this.questionAnswerJudgeStore.resetEdit();
    this.questionAnswerJudgeStore.getById(id).then(() => {
      this.selectedTab = "tab-edit";
    });
  }

  open() {
    if (this.canCreate || this.canEdit) {
      this.lookupStore.fillReasonProblem();

      this.questionAnswerJudgeStore.resetCreate();
      this.questionAnswerJudge.QuestionAnswerId = this.question.Id;
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    // this.questionAnswerJudgeStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>