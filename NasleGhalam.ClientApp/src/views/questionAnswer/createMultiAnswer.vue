<template>
  <bs-modal :show="questionAnswerMultiStore.openModal.create" size="lg">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>
          ثبت
          <span class="text-orange">{{questionAnswerMultiStore.modelName}}</span>
          جدید
        </q-toolbar-title>
        <q-btn dense icon="close" @click="questionAnswerMultiStore.OPEN_MODAL_CREATE(false)"/>
      </q-toolbar>
    </template>

    <q-tabs v-model="selectedTab" class="col-12" inverted color="primary">
      <q-tab
        slot="title"
        name="preCreateTab"
        label="ثبت موقت"
        :disabled="questionStore.hasQuestionAnswer"
      />
      <q-tab
        slot="title"
        name="previewTab"
        label="پیش نمایش"
        :disable="preCreateMode || questionStore.hasQuestionAnswer"
      />
      <q-tab slot="title" name="questionTab" label="سوال و جواب ها"/>

      <q-tab-pane name="preCreateTab" keep-alive class="row gutter-sm">
        <base-input :model="$v.questionAnswerMulti.Title" class="col-md-6"/>
        <base-input :model="$v.questionAnswerMulti.Author" class="col-md-6"/>
        <q-field class="col-12">
          <q-uploader
            url="wordUrl"
            float-label="فایل Word"
            name="word"
            hide-upload-button
            auto-expand
            ref="wordFile"
            extensions=".docx"
          />
        </q-field>
      </q-tab-pane>

      <q-tab-pane name="previewTab" keep-alive class="row">
        <q-card
          inline
          v-for="(img, index) in previewImages"
          v-bind:key="img.questionPath"
          class="col-12 q-mb-sm"
        >
          <q-card-media>
            سوال {{index + 1}}
            <img :src="img.questionPath" class="img-original-width">
            جواب {{index + 1}}
            <img :src="img.answerPath" class="img-original-width">
          </q-card-media>
        </q-card>
      </q-tab-pane>

      <q-tab-pane name="questionTab" keep-alive class="row">
        <q-card
          inline
          v-for="(questionPic, index) in questionStore.gridDataByQuestionGroupId"
          v-bind:key="questionPic.Id"
          class="col-12 q-mb-sm"
        >
          <q-card-media>
            سوال {{index+1}}:
            <img :src="questionPic.QuestionPicturePath" class="img-original-width">
            <div
              v-for="(answerPic, indexAnswer) in questionPic.QuestionAnswers"
              v-bind:key="answerPic.Id"
            >
              جواب {{indexAnswer+1}}:
              <img
                :src="answerPic.QuestionAnswerPicturePath"
                class="img-original-width"
              >
            </div>
          </q-card-media>
        </q-card>
      </q-tab-pane>
    </q-tabs>
    <template slot="footer">
      <q-btn
        v-if="preCreateMode"
        outline
        color="positive"
        class="shadow-1 bg-white q-mr-sm"
        type="submit"
        @click="questionAnswerMultiStore.submitPreCreateMulti"
      >
        <q-icon name="save"/>ثبت موقت
      </q-btn>
      <q-btn
        v-else
        outline
        color="primary"
        class="shadow-1 bg-white q-mr-sm"
        type="submit"
        @click="questionAnswerMultiStore.submitCreateMulti"
      >
        <q-icon name="save"/>ثبت نهایی
      </q-btn>
      <base-btn-back @click="questionAnswerMultiStore.OPEN_MODAL_CREATE(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>


<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionAnswerMultiValidations } from "src/validations/questionAnswerMultiValidation";

@Component({
  validations: questionAnswerMultiValidations
})
export default class QuestionAnswerCreateMultiVue extends Vue {
  $v: any;

  //#region ### data ###
  questionAnswerMultiStore = vxm.questionAnswerMultiStore;
  questionAnswerMulti = vxm.questionAnswerMultiStore.questionAnswerMulti;
  questionStore = vxm.questionStore;
  selectedTab = "preCreateTab";
  previewImages = [];
  //#endregion

  //#region ### computed ###
  get preCreateMode() {
    return this.selectedTab == "preCreateTab";
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionAnswerMultiStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

