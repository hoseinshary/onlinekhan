<template>
  <bs-modal :show="questionStore.openModal.questions" size="lg">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>مشاهده سوال ها</q-toolbar-title>
        <q-btn dense icon="close" @click="questionStore.OPEN_MODAL_QUESTIONS(false)" />
      </q-toolbar>
    </template>

    <q-card
      inline
      v-for="questionPic in questionStore.gridDataByQuestionGroupId"
      v-bind:key="questionPic.Id"
      class="col-12 q-mb-sm"
    >
      <q-card-media>
        <img :src="questionPic.QuestionPicturePath" class="img-original-width" />
      </q-card-media>
      <router-link
        :to="`/question/${questionPic.Id}/${lessonIdProp}`"
      >مشاهده سوال ({{questionPic.Id}})</router-link>
    </q-card>

    <template slot="footer">
      <base-btn-back @click="questionStore.OPEN_MODAL_QUESTIONS(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";

@Component
export default class QuestionsVue extends Vue {
  //#region ### props ###
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  questionStore = vxm.questionStore;
  //#endregion
}
</script>

