<template>
  <bs-modal :show="isOpenModalQuestions"
            size="lg">
    <template slot="header">
      <q-toolbar slot="header"
                 color="cyan-9">
        <q-toolbar-title>
          مشاهده سوال ها
        </q-toolbar-title>
        <q-btn dense
               icon="close"
               @click="toggleModalQuestionStore(false)" />
      </q-toolbar>
    </template>

    <q-card inline
            v-for="question in questionByQuestionGroupIdData"
            v-bind:key="question.Id"
            class="col-12">
      <q-card-media>
        <img :src="question.QuestionPicturePath">
      </q-card-media>
    </q-card>

    <template slot="footer">
      <my-btn-back @click="toggleModalQuestionStore(false)"></my-btn-back>
    </template>
  </bs-modal>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions("questionStore", ["toggleModalQuestionStore"])
  },
  /**
   * computed
   */
  computed: {
    ...mapState("questionStore", {
      questionByQuestionGroupIdData: "questionByQuestionGroupIdData",
      isOpenModalQuestions: "isOpenModalQuestions"
    })
  }
};
</script>

