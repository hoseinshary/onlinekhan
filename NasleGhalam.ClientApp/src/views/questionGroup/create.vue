<template>
  <bs-modal :show="isOpenModalCreate"
            :size="modalSize">
    <template slot="header">
      <q-toolbar slot="header"
                 color="cyan-9">
        <q-toolbar-title>
          ثبت
          <span class="text-orange">{{modelName}}</span>
          جدید
        </q-toolbar-title>
        <q-btn dense
               icon="close"
               @click="toggleModalCreateStore(false)" />
      </q-toolbar>
    </template>

    <q-tabs v-model="selectedTab"
            class="col-12"
            inverted
            color="primary">
      <q-tab slot="title"
             name="preCreateTab"
             label="ثبت موقت" />
      <q-tab slot="title"
             name="previewTab"
             label="پیش نمایش" />

      <q-tab-pane name="preCreateTab"
                  keep-alive
                  class="row">
        <my-input :model="$v.questionGroupObj.Title"
                  class="col-md-6" />

        <q-field class="col-12">
          <q-uploader url="wordUrl"
                      float-label="فایل Word"
                      name="word"
                      hide-upload-button
                      auto-expand
                      ref="wordFileUpload"
                      extensions=".docx" />
        </q-field>

        <q-field class="col-12">
          <q-uploader url="excelUrl"
                      float-label="فایل Excel"
                      name="excel"
                      hide-upload-button
                      auto-expand
                      ref="excelFileUpload"
                      extensions=".xlsx" />
        </q-field>
      </q-tab-pane>

      <q-tab-pane name="previewTab"
                  keep-alive
                  class="row">
        <q-card inline
                v-for="src in previewImages"
                v-bind:key="src"
                class="col-12">
          <q-card-media>
            <img :src="'http://localhost:63839/Content/QuestionGroupTemp/' + src + '.png'">
          </q-card-media>
        </q-card>
      </q-tab-pane>
    </q-tabs>
    <template slot="footer">
      <q-btn v-if="isPreCreate"
             outline
             color="positive"
             class="shadow-1 bg-white q-mr-sm"
             type="submit"
             @click="submitPreCreateStore()">
        <q-icon name="save" /> ثبت موقت
      </q-btn>
      <q-btn v-else
             outline
             color="primary"
             class="shadow-1 bg-white q-mr-sm"
             type="submit"
             @click="submitCreateStore()">
        <q-icon name="save" /> ثبت نهایی
      </q-btn>
      <my-btn-back @click="toggleModalCreateStore(false)"></my-btn-back>
    </template>
  </bs-modal>
</template>

<script>
import viewModel from "viewModels/questionGroup/questionGroupViewModel";
import { mapState, mapActions } from "vuex";

export default {
  data() {
    return {
      selectedTab: "preCreateTab",
      modalSize: "lg",
      previewImages: [],
      isPreCreate: true
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions("questionGroupStore", [
      "toggleModalCreateStore",
      "createVueStore",
      "submitPreCreateStore",
      "submitCreateStore",
      "resetCreateStore"
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState("questionGroupStore", {
      modelName: "modelName",
      questionGroupObj: "questionGroupObj",
      isOpenModalCreate: "isOpenModalCreate"
    })
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.createVueStore(this);
  }
};
</script>

