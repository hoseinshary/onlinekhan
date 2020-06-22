<template>
  <q-tabs color="purple-6" v-model="selectedTab">
    <!-- tabs -->
    <q-tab
      slot="title"
      v-if="canCreateProp"
      name="tab-create"
      label="ایجاد جواب"
      icon="library_add"
      @click="questionAnswerStore.resetCreate()"
    />
    <q-tab
      slot="title"
      v-if="canEditProp"
      :disable="!editMode"
      name="tab-edit"
      label="ویرایش جواب"
      icon="create"
    />
    <q-tab
      slot="title"
      name="tab-show-question-judge"
      label="نمایش ارزیابی"
      class="text-bold"
      @click="showQuestionJudge"
    />
    <!-- tab-panes -->
    <q-tab-pane keep-alive name="tab-create">
      <tab-create :lessonIdProp="lessonIdProp"></tab-create>
    </q-tab-pane>
    <q-tab-pane keep-alive name="tab-edit">
      <tab-edit :lessonIdProp="lessonIdProp" ></tab-edit>
    </q-tab-pane>
    <q-tab-pane keep-alive name="tab-show-question-judge">
      <tab-edit></tab-edit>
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
  @Prop({ type: Number, required: true }) questionIdProp;
  //#endregion

  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  questionAnswerJudge = this.questionAnswerStore.questionAnswer;
  selectedTab = "tab-create";
  //#endregion

  //#region ### computed ###
  get editMode() {
    return this.selectedTab == "tab-edit";
  }
  //#endregion

  //#region ### methods ###
  showQuestionJudge() {
    this.questionAnswerStore.OPEN_MODAL_INDEX(false);
    vxm.questionJudgeStore
      .fillListByQuestionId(this.questionIdProp)
      .then(() => {
        vxm.questionJudgeStore.OPEN_MODAL_INDEX(true);
        this.selectedTab = "tab-create";
      });
  }

  showTabEdit(id) {
    this.questionAnswerStore.resetEdit();
    this.questionAnswerStore.getById(id).then(() => {
      this.selectedTab = "tab-edit";
    });
  }

   showQuestionAnswerImage(id) {
    this.questionAnswerStore.getById(id);
  }
  //#endregion
}
</script>