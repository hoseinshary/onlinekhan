<template>
  <bs-modal :show="questionAnswerStore.openModal.createMulti" size="lg">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>
          ثبت
          <span class="text-orange">{{questionAnswerStore.modelName}}</span>
          جدید
        </q-toolbar-title>
        <q-btn dense icon="close" @click="questionAnswerStore.OPEN_MODAL_CREATE_MULTI(false)"/>
      </q-toolbar>
    </template>

    <q-tabs v-model="selectedTab" class="col-12" inverted color="primary">
      <q-tab slot="title" name="preCreateTab" label="ثبت موقت"/>
      <q-tab slot="title" name="previewTab" label="پیش نمایش" :disable="preCreateMode"/>

      <q-tab-pane name="preCreateTab" keep-alive class="row">
        <base-input :model="$v.questionAnswer.Title" class="col-md-6"/>
        <base-input :model="$v.questionAnswer.Author" class="col-md-6"/>
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
        <q-card inline v-for="src in previewImages" v-bind:key="src" class="col-12">
          <q-card-media>
            <img :src="src">
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
        @click="questionAnswerStore.submitPreCreateMulti"
      >
        <q-icon name="save"/>ثبت موقت
      </q-btn>
      <q-btn
        v-else
        outline
        color="primary"
        class="shadow-1 bg-white q-mr-sm"
        type="submit"
        @click="questionAnswerStore.submitCreateMulti"
      >
        <q-icon name="save"/>ثبت نهایی
      </q-btn>
      <base-btn-back @click="questionAnswerStore.OPEN_MODAL_CREATE_MULTI(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>


<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionAnswerValidations } from "src/validations/questionAnswerValidation";

@Component({
  validations: questionAnswerValidations
})
export default class QuestionAnswerCreateMultiVue extends Vue {
  $v: any;

  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  questionAnswer = vxm.questionAnswerStore.questionAnswer;
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
    this.questionAnswerStore.SET_PRE_CREATE_MULTI_VUE(this);
  }
  //#endregion
}
</script>

