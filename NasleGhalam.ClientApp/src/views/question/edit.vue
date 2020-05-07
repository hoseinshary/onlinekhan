<template>
  <base-modal-edit
    :title="questionStore.modelName"
    :show="questionStore.openModal.edit"
    size="xl"
    @confirm="questionStore.submitEdit(activeAccess)"
    @reset="questionStore.resetEdit"
    @close="close"
    @open="open"
  >
    <q-card inline class="col-12" v-if="question.FileName">
      <q-card-media>
        <img :src="question.QuestionPicturePath" class="img-original-width" />
      </q-card-media>
    </q-card>

    <div class="col-12" v-if="showElement('TopicIds')">
      <p v-for="(elem, index) in concatTopicArray" :key="index">{{elem}}</p>
    </div>
    <div class="col-md-3 col-sm-6">
      <section v-if="showElement('TopicIds')" class="q-my-sm shadow-1">
        <q-input v-model="topicFilter" float-label="جستجوی مبحث" clearable />
        <q-tree
          :nodes="topicTreeDataProp"
          :filter="topicFilter"
          :ticked.sync="question.TopicIds"
          tick-strategy="leaf"
          color="primary"
          accordion
          node-key="Id"
          ref="topicTree"
        />
      </section>

      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-select
          filter
          chips
          multiple
          v-model="question.TagIds"
          :options="tagStore.ddl"
          float-label="تگ ها"
        />
      </section>

      <q-slide-transition>
        <section
          v-if="showElement('LookupId_QuestionType') && question.LookupId_QuestionType==6"
          class="q-ma-sm q-pa-sm shadow-1"
        >
          گزینه صحیح
          <base-select
            :model="$v.question.AnswerNumber"
            :options="answersDdl"
            class="col-md-4"
            filter
          />
        </section>
      </q-slide-transition>
    </div>

    <div class="col-md-6 col-sm-12">
      <section class="row gutter-md">
        <q-field class="col-sm-4" v-if="showElement('wordFile')">
          <q-uploader
            url
            float-label="فایل سوال"
            name="word"
            hide-upload-button
            ref="wordFile"
            extensions=".doc,.docx"
          />
        </q-field>
        <base-input
          v-if="showElement('QuestionNumber')"
          :model="$v.question.QuestionNumber"
          class="col-md-4"
        />
        <base-select
          v-if="showElement('LookupId_QuestionType')"
          :model="$v.question.LookupId_QuestionType"
          :options="lookupStore.questionTypeDdl"
          class="col-md-4"
          filter
        />
        <base-input
          v-if="showElement('QuestionPoint')"
          :model="$v.question.QuestionPoint"
          class="col-md-4"
        />
        <base-select
          v-if="showElement('LookupId_QuestionHardnessType')"
          :model="$v.question.LookupId_QuestionHardnessType"
          :options="lookupStore.questionHardnessTypeDdl"
          class="col-md-4"
          filter
        />
        <base-select
          v-if="showElement('LookupId_RepeatnessType')"
          :model="$v.question.LookupId_RepeatnessType"
          :options="lookupStore.repeatnessTypeDdl"
          class="col-md-4"
          filter
        />

        <base-select
          :model="$v.question.LookupId_QuestionRank"
          v-if="showElement('LookupId_QuestionRank')"
          :options="lookupStore.questionRankDdl"
          class="col-md-4"
          filter
        />
        <base-field
          v-if="showElement('UseEvaluation')"
          class="col-md-4"
          :model="$v.question.UseEvaluation"
        >
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-field
          v-if="showElement('IsStandard')"
          class="col-md-4"
          :model="$v.question.IsStandard"
        >
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-select
          v-if="showElement('LookupId_AuthorType')"
          :model="$v.question.LookupId_AuthorType"
          :options="lookupStore.authorTypeDdl"
          class="col-md-4"
          filter
        />
        <base-select
          v-if="showElement('WriterId')"
          :model="$v.question.WriterId"
          :options="writerStore.ddl"
          class="col-md-4"
          filter
        />
        <base-select
          v-if="showElement('LookupId_AreaType')"
          :model="$v.question.LookupId_AreaType"
          :options="lookupStore.areaTypeDdl"
          class="col-md-4"
          filter
        />
        <base-input
          v-if="showElement('ResponseSecond')"
          :model="$v.question.ResponseSecond"
          class="col-md-4"
        />
        <base-field class="col-md-4" :model="$v.question.IsHybrid">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>

        <base-field v-if="showElement('IsDelete')" class="col-md-4" :model="$v.question.IsDelete">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>

        <base-input
          v-if="showElement('Description')"
          :model="$v.question.Description"
          class="col-md-8"
        />
      </section>
    </div>

    <div class="col-md-3 col-sm-6" v-if="showElement('TopicIds')">
      <q-input v-model="topicFilter2" float-label="جستجوی مبحث 2" clearable />
      <q-tree
        :nodes="topicStore.treeDataByLessonIds(lessonStore.relatedLessonIds(lessonIdProp))"
        :ticked.sync="question.TopicIds"
        :filter="topicFilter2"
        tick-strategy="leaf"
        color="primary"
        accordion
        node-key="Id"
        ref="topicTree2"
      />
    </div>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { questionValidations } from "src/validations/questionValidation";
import util from "src/utilities";

@Component({
  validations: questionValidations
})
export default class QuestionEditVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) topicTreeDataProp;
  @Prop({ type: Boolean, required: true }) canEditAdminProp;
  @Prop({ type: Boolean, required: true }) canEditTopicProp;
  @Prop({ type: Boolean, required: true }) canEditImportProp;
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  tagStore = vxm.tagStore;
  topicStore = vxm.topicStore;
  lessonStore = vxm.lessonStore;
  question = vxm.questionStore.question;
  writerStore = vxm.writerStore;
  topicFilter = "";
  topicFilter2 = "";
  questionAccessElement = {
    QuestionNumber: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: true
    },
    QuestionPoint: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    UseEvaluation: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    IsStandard: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    WriterId: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: true
    },
    ResponseSecond: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    Description: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: true
    },
    LookupId_QuestionType: {
      canEditAdminProp: true,
      canEditTopicProp: true,
      canEditImportProp: true
    },
    LookupId_QuestionHardnessType: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    LookupId_RepeatnessType: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    LookupId_QuestionRank: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: false
    },
    LookupId_AuthorType: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: true
    },
    LookupId_AreaType: {
      canEditAdminProp: true,
      canEditTopicProp: true,
      canEditImportProp: false
    },
    TopicIds: {
      canEditAdminProp: true,
      canEditTopicProp: true,
      canEditImportProp: false
    },
    wordFile: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: true
    },
    IsDelete: {
      canEditAdminProp: true,
      canEditTopicProp: false,
      canEditImportProp: true
    }
  };
  concatTopicArray: Array<string> = [];
  //#endregion

  //#region ### computed ###
  get answersDdl() {
    return [
      { value: 1, label: "1" },
      { value: 2, label: "2" },
      { value: 3, label: "3" },
      { value: 4, label: "4" }
    ];
  }

  get activeAccess() {
    if (this.canEditAdminProp) {
      return "canEditAdminProp";
    } else if (this.canEditImportProp) {
      return "canEditImportProp";
    } else {
      return "canEditTopicProp";
    }
  }
  //#endregion

  //#region ### watch ###
  @Watch("question.TopicIds")
  questionTopicIdsChanged(newVal) {
    var getNodeByKey = this.$refs["topicTree"]["getNodeByKey"];
    util.clearArray(this.concatTopicArray);
    var strArr: Array<string> = [];
    newVal.forEach(x => {
      strArr = [];
      var node = getNodeByKey(x);
      while (node) {
        strArr.unshift(node.label);
        node = node.parent;
      }
      this.concatTopicArray.push(strArr.join(" => "));
    });

    // topic 2
    getNodeByKey = this.$refs["topicTree2"]["getNodeByKey"];
    newVal.forEach(x => {
      strArr = [];
      var node = getNodeByKey(x);
      while (node) {
        strArr.unshift(node.label);
        node = node.parent;
      }
      this.concatTopicArray.push(strArr.join(" => "));
    });
  }
  //#endregion

  //#region ### methods ###
  showElement(elementName) {
    var elem = this.questionAccessElement[elementName];
    return elem && elem[this.activeAccess];
  }

  open() {
    this.tagStore.fillList();
    this.lookupStore.fillQuestionType();
    this.lookupStore.fillQuestionRank();
    this.lookupStore.fillQuestionHardnessType();
    this.lookupStore.fillRepeatnessType();
    this.lookupStore.fillAuthorType();
    this.lookupStore.fillAreaType();
    this.writerStore.fillList();
  }

  close() {
    this.questionStore.OPEN_MODAL_EDIT(false);

    var route = this["$route"];
    if (route && route.params.id) {
      var router = this["$router"];
      router.push("/question");
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

