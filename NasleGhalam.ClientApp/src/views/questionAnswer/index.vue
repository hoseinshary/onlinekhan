<template>
  <bs-modal :show="questionAnswerStore.openModal.index" size="xl" @open="open">
    <template slot="header">
      <q-toolbar slot="header" color="purple-6">
        <q-toolbar-title>{{questionAnswerStore.modelName}}</q-toolbar-title>
        <q-btn dense icon="close" @click="questionAnswerStore.OPEN_MODAL_INDEX(false)" />
      </q-toolbar>
    </template>

    <q-card inline class="col-12 q-mb-sm" v-show="question.FileName">
      <q-card-media>
        سوال:
        <img :src="question.QuestionPicturePath" class="img-original-width" />
      </q-card-media>
    </q-card>
    <q-card inline class="col-12 q-mb-sm" v-show="questionAnswer.FilePath">
      <q-card-media>
        جواب:
        <img :src="questionAnswer.QuestionAnswerPicturePath" class="img-original-width" />
      </q-card-media>
    </q-card>
    <div class="col-8">
      <q-tabs inverted color="cyan-9" v-model="detailSelectedTab">
        <q-tab slot="title" name="detail-tab" label="جزییات جواب ها" default class="text-bold" />
        <q-tab slot="title" name="image-tab" label="تصاویر جواب ها" class="text-bold" />
        <q-tab
          v-if="questionAnswerJudgeAccess"
          :disable="!visibleQuestionAnswerJudge"
          slot="title"
          name="question-answer-judge-tab"
          label="ارزیابی جواب"
          class="text-bold"
        />

        <q-tab-pane name="detail-tab" keep-alive>
          <base-table
            :grid-data="questionAnswerStore.gridData"
            :columns="questionAnswerGridColumn"
            hasIndex
          >
            <template slot="Writer.Name" slot-scope="data">{{data.row.Writer.Name}}</template>
            <template slot="IsMaster" slot-scope="data">{{data.row.IsMaster? "بلی" : "خیر"}}</template>
            <template slot="IsActive" slot-scope="data">{{data.row.IsActive? "بلی" : "خیر"}}</template>
            <template slot="Context" slot-scope="data">
              <div v-if="data.row.Context && data.row.Context.length> 100">
                {{(`${data.row.Context.substring(0,50)} ...`)}}
                <q-tooltip>{{data.row.Context}}</q-tooltip>
              </div>
              <div v-else>{{data.row.Context}}</div>
            </template>
            <template slot="Id" slot-scope="data">
              <a v-if="canEdit" :href="data.row.QuestionWordPath" target="_blank" class="q-mr-sm">فایل ورد</a>
              <q-btn
                v-if="questionAnswerJudgeAccess"
                outline
                round
                icon="list"
                color="cyan"
                size="sm"
                class="shadow-1 bg-white q-mr-sm"
                @click="showQuestionAnswerJudgeTab(data.row.Id)"
              >
                <q-tooltip>ارزیابی جواب</q-tooltip>
              </q-btn>
              <q-btn
                v-if="questionAnswerJudgeAccess"
                round
                @click="showQuestionAnswerImage(data.row.Id)"
                outline
                icon="image"
                size="sm"
                class="shadow-1 bg-white q-mr-sm"
              >
                <q-tooltip>عکس جواب</q-tooltip>
              </q-btn>
              <base-btn-edit
                v-if="canEdit"
                round
                @click="$refs.createEditTab.showTabEdit(data.row.Id)"
              />
              <btn-delete v-if="canDelete" :recordIdProp="data.row.Id"></btn-delete>
            </template>
          </base-table>
        </q-tab-pane>
        <q-tab-pane name="image-tab" keep-alive class="row">
          <q-card
            inline
            v-for="(img, index) in questionAnswerStore.gridData"
            v-bind:key="img.Id"
            class="col-12 q-mb-sm"
          >
            <q-card-media>
              جواب {{index + 1}}
              <img
                :src="img.QuestionAnswerPicturePath"
                class="img-original-width"
              />
            </q-card-media>
          </q-card>
        </q-tab-pane>

        <q-tab-pane name="question-answer-judge-tab" keep-alive>
          <tab-list-question-answer-judge
            v-if="questionAnswerJudgeAccess"
            :canEditProp="questionAnswerJudgeEditAccess"
            :canDeleteProp="questionAnswerJudgeAccess"
            @showTabEdit="showQuestionAnswerJudgeEditTab"
          ></tab-list-question-answer-judge>
        </q-tab-pane>
      </q-tabs>
    </div>
    <div class="col-4">
      <q-slide-transition>
         <create-edit-tab
         v-if="canEdit"
          v-show="!visibleQuestionAnswerJudge"
          :canCreateProp="canCreate"
          :canEditProp="canEdit"
          :lessonIdProp="lessonIdProp"
          :questionIdProp="question.Id"
          ref="createEditTab"
        ></create-edit-tab>
      </q-slide-transition> 
      <q-slide-transition>
        <question-answer-judge-create-edit-tab
          v-show="visibleQuestionAnswerJudge"
          v-if="questionAnswerJudgeAccess"
          :canCreateProp="questionAnswerJudgeCreateAccess"
          :canEditProp="questionAnswerJudgeEditAccess"
          :lessonIdProp="lessonIdProp"
          ref="QuestionAnswerJudgeCreateEditTab"
        ></question-answer-judge-create-edit-tab>
      </q-slide-transition>
    </div>
    

    <template slot="footer">
      <base-btn-back @click="questionAnswerStore.OPEN_MODAL_INDEX(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>


<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    CreateEditTab: () => import("./createEditTab.vue"),
    BtnDelete: () => import("./delete.vue"),
    QuestionAnswerJudgeCreateEditTab: () =>
      import("../questionAnswerJudge/createEditTab.vue"),
    TabListQuestionAnswerJudge: () => import("../questionAnswerJudge/list.vue"),
    TabCreateQuestionAnswerJudge: () =>
      import("../questionAnswerJudge/create.vue"),
    TabEditQuestionAnswerJudge: () => import("../questionAnswerJudge/edit.vue")
  }
})
export default class QuestionAnswerVue extends Vue {
  //#region ### props ###
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  questionAnswerJudgeStore = vxm.questionAnswerJudgeStore;
  questionAnswer = this.questionAnswerStore.questionAnswer;
  writerStore = vxm.writerStore;
  question = this.questionStore.question;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionAnswerGridColumn = [
    {
      title: "عنوان",
      data: "Title"
    },
    {
      title: "نویسنده",
      data: "Writer.Name"
    },
    {
      title: "آنلاین خوان",
      data: "IsMaster"
    },
    {
      title: "فعال",
      data: "IsActive"
    },
    // {
    //   title: "متن جواب",
    //   data: "Context"
    // },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete || this.questionAnswerJudgeAccess
    }
  ];
  detailSelectedTab = "detail-tab";
  questionAnswerJudgeSelectedTab = "tab-create";
  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد جواب سوال") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش جواب سوال") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف جواب سوال") > -1;
  }

  get questionAnswerJudgeAccess() {
    return this.pageAccess.indexOf("مشاهده کارشناسی جواب سوال") > -1;
  }

  get questionAnswerJudgeCreateAccess() {
    return this.pageAccess.indexOf("ایجاد کارشناسی جواب سوال") > -1;
  }

  get questionAnswerJudgeEditAccess() {
    return this.pageAccess.indexOf("ویرایش کارشناسی جواب سوال") > -1;
  }

  get questionAnswerJudgeDeleteAccess() {
    return this.pageAccess.indexOf("حذف کارشناسی جواب سوال") > -1;
  }

  get visibleQuestionAnswerJudge() {
    return this.detailSelectedTab == "question-answer-judge-tab";
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.questionAnswer.QuestionId = this.question.Id;
    this.detailSelectedTab = "detail-tab";
    this.questionAnswer.QuestionAnswerPicturePath ="";

    if (this.canCreate || this.canEdit) {
      this.lookupStore.fillAnswerType();
      this.writerStore.fillList();

      this.questionAnswerStore.resetCreate();
    }

    if (this.questionAnswerJudgeAccess) {
      this.lookupStore.fillReasonProblem();
    }
  }

  showQuestionAnswerJudgeTab(id) {
    this.questionAnswerStore.getById(id);
    this.detailSelectedTab = "question-answer-judge-tab";
    var createEditTab = this.$refs["QuestionAnswerJudgeCreateEditTab"];
    createEditTab["showTabCreate"](id);
  }

  showQuestionAnswerJudgeEditTab(id) {
    var createEditTab = this.$refs["QuestionAnswerJudgeCreateEditTab"];
    createEditTab["showTabEdit"](id);
  }

   showQuestionAnswerImage(id) {
    this.questionAnswerStore.getById(id);
  }

  //#endregion

  //#region ### hooks ###
  created() {
    this.questionAnswerStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>