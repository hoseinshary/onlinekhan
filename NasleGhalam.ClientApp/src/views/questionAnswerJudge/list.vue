<template>
  <base-table
    :grid-data="questionAnswerJudgeStore.gridData"
    :columns="questionAnswerJudgeGridColumn"
    hasIndex
  >
    <template slot="IsActiveQuestionAnswer" slot-scope="data">
      <q-checkbox v-model="data.row.IsActiveQuestionAnswer" />
    </template>
    <template slot="IsMaster" slot-scope="data">
      <q-checkbox v-model="data.row.IsMaster" />
    </template>
    <template
      slot="Lookup_ReasonProblem.Value"
      slot-scope="data"
    >{{data.row.Lookup_ReasonProblem.Value}}</template>
    <template slot="User.FullName" slot-scope="data">{{data.row.User.FullName}}</template>

    <template slot="Id" slot-scope="data">
      <base-btn-edit v-if="canEditProp" round @click="$emit('showTabEdit',data.row.Id)" />
      <btn-delete v-if="canDeleteProp" :recordIdProp="data.row.Id"></btn-delete>
    </template>
  </base-table>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    BtnDelete: () => import("./delete.vue")
  }
})
export default class QuestionAnswerJudgeVue extends Vue {
  //#region ### props ###
  @Prop({ type: Boolean, required: true }) canEditProp;
  @Prop({ type: Boolean, required: true }) canDeleteProp;
  //#endregion

  //#region ### data ###
  questionAnswerJudgeStore = vxm.questionAnswerJudgeStore;
  questionAnswerJudgeGridColumn = [
    {
      title: "تایید",
      data: "IsActiveQuestionAnswer"
    },
    {
      title: "آنلاین خوان",
      data: "IsMaster"
    },
    {
      title: "دلیل مشکل",
      data: "Lookup_ReasonProblem.Value"
    },
    {
      title: "کاربر",
      data: "User.FullName"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEditProp || this.canDeleteProp
    }
  ];
  //#endregion
}
</script>