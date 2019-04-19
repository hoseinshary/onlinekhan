<template>
  <base-modal-create
    :title="questionStore.modelName"
    :show="questionStore.openModal.create"
    size="lg"
    @confirm="questionStore.submitCreate"
    @reset="questionStore.resetCreate"
    @close="questionStore.OPEN_MODAL_CREATE(false)"
    @open="fillAllDdl()"
  >
    <div class="col-sm-4">
      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-input v-model="topicFilter" float-label="جستجوی مبحث" clearable/>
        <q-tree
          :nodes="topicTreeData"
          tick-strategy="leaf"
          color="primary"
          accordion
          node-key="Id"
          ref="topicTree"
          :filter="topicFilter"
          :ticked.sync="questionObj.TopicsId"
        />
      </section>

      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-select
          filter
          chips
          multiple
          v-model="questionObj.TagsId"
          :options="tagDdl"
          float-label="تگ ها"
        />
      </section>

      <q-slide-transition>
        <section v-if="questionObj.LookupId_QuestionType==6" class="q-ma-sm q-pa-sm shadow-1">
          گزینه صحیح
          <base-select
            :model="$v.questionObj.AnswerNumber"
            :options="answersDdl"
            class="col-md-4"
            filter
          />
        </section>
      </q-slide-transition>
    </div>

    <div class="col-sm-8 row gutter-md">
      <q-field class="col-sm-4">
        <q-uploader
          url="url"
          float-label="فایل سوال"
          name="word"
          hide-upload-button
          ref="wordFile"
          extensions=".doc,.docx"
        />
      </q-field>

      <base-input :model="$v.questionObj.QuestionNumber" class="col-md-4"/>

      <base-select
        :model="$v.questionObj.LookupId_QuestionType"
        :options="lookupQuestionType"
        class="col-md-4"
        filter
      />

      <base-input :model="$v.questionObj.QuestionPoint" class="col-md-4"/>

      <base-select
        :model="$v.questionObj.LookupId_QuestionHardnessType"
        :options="lookupQuestionHardnessType"
        class="col-md-4"
        filter
      />

      <base-select
        :model="$v.questionObj.LookupId_RepeatnessType"
        :options="lookupQuestionRepeatnessType"
        class="col-md-4"
        filter
      />

      <base-field class="col-md-4" :model="$v.questionObj.UseEvaluation">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
          <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
        </template>
      </base-field>

      <base-field class="col-md-4" :model="$v.questionObj.IsStandard">
        <template slot-scope="data">
          <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
          <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
        </template>
      </base-field>

      <base-select
        :model="$v.questionObj.LookupId_AuthorType"
        :options="lookupQuestionAuthorType"
        class="col-md-4"
        filter
      />

      <base-input :model="$v.questionObj.AuthorName" class="col-md-4"/>

      <base-select
        :model="$v.questionObj.LookupId_AreaType"
        :options="lookupTopicAreaTypeDdl"
        class="col-md-4"
        filter
      />

      <base-input :model="$v.questionObj.ResponseSecond" class="col-md-4"/>

      <base-input :model="$v.questionObj.Description" class="col-md-4"/>
    </div>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionValidations } from "src/validations/questionValidation";

@Component({
  validations: questionValidations
})
export default class QuestionCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  questionStore = vxm.questionStore;
  question = vxm.questionStore.question;
  //#endregion

  //#region ### methods ###
  fillAllDdl() {}
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
// import viewModel from "viewModels/question/questionViewModel";
// import { mapState, mapActions } from "vuex";
// import util from "src/utilities";

// export default {
//   /**
//    * methods
//    */
//   data() {
//     return {
//       topicFilter: "",
//       answersDdl: [
//         { value: 1, label: "1" },
//         { value: 2, label: "2" },
//         { value: 3, label: "3" },
//         { value: 4, label: "4" }
//       ]
//     };
//   },
//   methods: {
//     ...mapActions("questionStore", [
//       "toggleModalCreateStore",
//       "createVueStore",
//       "submitCreateStore",
//       "resetCreateStore"
//     ]),
//     ...mapActions("tagStore", {
//       fillTagDdlStore: "fillDdlStore"
//     }),
//     ...mapActions("lookupStore", [
//       "fillTopicAreaTypeDdlStore",
//       "getLookupQuestionType",
//       "getLookupQuestionHardnessType",
//       "getLookupQuestionRepeatnessType",
//       "getLookupQuestionAuthorType"
//     ]),
//     ...mapActions("topicStore", ["GetAllTreeStore"]),
//     fillAllDdls() {
//       this.fillTagDdlStore();
//       this.getLookupQuestionType();
//       this.getLookupQuestionHardnessType();
//       this.getLookupQuestionRepeatnessType();
//       this.getLookupQuestionAuthorType();
//       this.fillTopicAreaTypeDdlStore();
//     }
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("questionStore", {
//       modelName: "modelName",
//       questionObj: "questionObj",
//       isOpenModalCreate: "isOpenModalCreate"
//     }),
//     ...mapState("tagStore", {
//       tagDdl: "tagDdl"
//     }),
//     ...mapState("lookupStore", [
//       "lookupTopicAreaTypeDdl",
//       "lookupQuestionType",
//       "lookupQuestionHardnessType",
//       "lookupQuestionRepeatnessType",
//       "lookupQuestionAuthorType"
//     ]),
//     ...mapState("topicStore", {
//       topicTreeData: "topicTreeData"
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

