<template>
  <q-tabs color="blue-6" v-model="selectedTab">
    <!-- tabs -->
    <q-tab
      slot="title"
      name="tab-create"
      v-if="canCreateProp"
      label="ایجاد ارزیابی جواب"
      icon="library_add"
      @click="questionAnswerJudgeStore.resetCreate()"
    />
    <q-tab
      slot="title"
      name="tab-edit"
      v-if="canEditProp"
      :disable="!editMode"
      label="ویرایش ارزیابی جواب"
      icon="create"
    />
    <!-- tab-panes -->
    <q-tab-pane keep-alive name="tab-create">
      <tab-create :lessonIdProp="lessonIdProp"></tab-create>
    </q-tab-pane>
    <q-tab-pane keep-alive name="tab-edit">
      <tab-edit :lessonIdProp="lessonIdProp"></tab-edit>
    </q-tab-pane>
  </q-tabs>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    TabCreate: () => import("./create.vue"),
    TabEdit: () => import("./edit.vue")
  }
})
export default class QuestionAnswerJudgeVue extends Vue {
  //#region ### props ###
  @Prop({ type: Boolean, required: true }) canCreateProp;
  @Prop({ type: Boolean, required: true }) canEditProp;
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  questionAnswerJudgeStore = vxm.questionAnswerJudgeStore;
  questionAnswerStore =vxm.questionAnswerStore;
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  questionAnswerJudge = this.questionAnswerJudgeStore.questionAnswerJudge;
  question = this.questionStore.question;
  selectedTab = "tab-create";
  //#endregion

  //#region ### computed ###
  get editMode() {
    return this.selectedTab == "tab-edit";
  }
  //#endregion

  //#region ### methods ###
  showTabCreate(id) {
    this.selectedTab = "tab-create";
    this.questionAnswerStore.getById(id);
    this.questionAnswerJudgeStore.resetCreate();
    this.questionAnswerJudgeStore.questionAnswerJudge.QuestionAnswerId = id;
    this.questionAnswerJudgeStore.fillListByQuestionAnswerId(id);
  }

  showTabEdit(id) {
    
    this.questionAnswerJudgeStore.resetEdit();
    this.questionAnswerJudgeStore.getById(id).then(() => {
      this.selectedTab = "tab-edit";
    });
  }
  //#endregion
}
</script>