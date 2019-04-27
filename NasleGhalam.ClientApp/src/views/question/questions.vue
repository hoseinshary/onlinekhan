<template>
  <bs-modal :show="questionStore.openModal.questions" size="lg">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>مشاهده سوال ها</q-toolbar-title>
        <q-btn dense icon="close" @click="questionStore.OPEN_MODAL_QUESTIONS(false)"/>
      </q-toolbar>
    </template>

    <q-card
      inline
      v-for="questionPic in questionStore.gridDataByQuestionGroupId"
      v-bind:key="questionPic.Id"
      class="col-12"
    >
      <q-card-media>
        <img :src="questionPic.QuestionPicturePath" class="img-original-width">
      </q-card-media>
    </q-card>

    <template slot="footer">
      <base-btn-back @click="questionStore.OPEN_MODAL_QUESTIONS(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";

@Component
export default class QuestionsVue extends Vue {
  //#region ### data ###
  questionStore = vxm.questionStore;
  //#endregion
}
</script>

