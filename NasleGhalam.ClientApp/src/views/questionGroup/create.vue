<template>
  <bs-modal :show="questionGroupStore.openModal.create"
            :size="modalSize">
    <template slot="header">
      <q-toolbar slot="header"
                 color="cyan-9">
        <q-toolbar-title>
          ثبت
          <span class="text-orange">{{questionGroupStore.modelName}}</span>
          جدید
        </q-toolbar-title>
        <q-btn dense
               icon="close"
               @click="questionGroupStore.OPEN_MODAL_CREATE(false)" />
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
             label="پیش نمایش" 
             :disable="!previewMode"/>

      <q-tab-pane name="preCreateTab"
                  keep-alive
                  class="row">
        <base-input :model="$v.questionGroup.Title"
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
            <img :src="src">
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
             @click="questionGroupStore.submitPreCreate">
        <q-icon name="save" /> ثبت موقت
      </q-btn>
      <q-btn v-else
             outline
             color="primary"
             class="shadow-1 bg-white q-mr-sm"
             type="submit"
             @click="questionGroupStore.submitCreate">
        <q-icon name="save" /> ثبت نهایی
      </q-btn>
      <base-btn-back @click="questionGroupStore.OPEN_MODAL_CREATE(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>


<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionGroupValidations } from "src/validations/QuestionGroupValidation";

@Component({
  validations: questionGroupValidations
})
export default class QuestionGroupCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  questionGroupStore = vxm.questionGroupStore;
  questionGroup = vxm.questionGroupStore.questionGroup;
  selectedTab= "preCreateTab";
  modalSize= "lg";
  previewImages= [];
  isPreCreate= true;
  //#endregion

  //#region ### computed ###
  get previewMode() {
    return this.selectedTab == "previewTab";
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionGroupStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
// import viewModel from "viewModels/questionGroup/questionGroupViewModel";
// import { mapState, mapActions } from "vuex";

// export default {
//   data() {
//     return {
//       selectedTab: "preCreateTab",
//       modalSize: "lg",
//       previewImages: [],
//       isPreCreate: true
//     };
//   },
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("questionGroupStore", [
//       "toggleModalCreateStore",
//       "createVueStore",
//       "submitPreCreateStore",
//       "submitCreateStore",
//       "resetCreateStore"
//     ])
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("questionGroupStore", {
//       questionGroupStore.modelName: "questionGroupStore.modelName",
//       questionGroup: "questionGroup",
//       isOpenModalCreate: "isOpenModalCreate"
//     })
//   },
//   /**
//    * validations
//    */
//   validations: viewModel,
//   /**
//    * created
//    */
//   created() {
//     this.createVueStore(this);
//   }
// };
</script>

