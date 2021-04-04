<template>
  <section class="col-12 q-px-sm">
    <section class="row">
      <div class="col-md-4 col-lg-3">
        <section class="s-border s-spacing">
          <div v-if="showElement('showNoJudgement')">
            <q-checkbox
              v-model="showNoJudgement"
              label="نمایش سوال های بدون ارزیابی"
              @input="fillGrid()"
            />
            <br />
          </div>
          <div v-if="showElement('showWithoutTopic')">
            <q-checkbox
              v-model="showWithoutTopic"
              label="نمایش سوال های بدون مبحث"
              @input="fillGrid()"
            />
            <br />
          </div>
          <div v-if="showElement('showJudged')">
            <q-checkbox
              v-model="showJudged"
              label="نمایش سوال های ارزیابی شده"
              @input="fillGrid()"
            />
            <br />
          </div>
          <div v-if="showElement('showActived')">
            <q-checkbox
              v-model="showActived"
              label="نمایش سوال های فعال سایت"
              @input="fillGrid()"
            />
            <br />
          </div>
          <div v-if="showElement('showUnActived')">
            <q-checkbox
              v-model="showUnActived"
              label="نمایش سوال های غیر فعال سایت"
              @input="fillGrid()"
            />

            <br />
          </div>
          <div v-if="showElement('showNoAnswer')">
            <q-checkbox
              v-model="showNoAnswer"
              label="نمایش سوال های بدون پاسخ"
              @input="fillGrid()"
            />
            <br />
          </div>
          <div v-if="showElement('showNoAnswerJudge')">
            <q-checkbox
              v-model="showNoAnswerJudge"
              label="نمایش سوال های بدون پاسخ ارزیابی شده"
              @input="fillGrid()"
            />
          </div>

          <hr style="width: 100%; text-align: left; margin-left: 0" />

          <q-checkbox
            v-model="showNumberForTopic"
            v-if="showElement('showNumberForTopic')"
            label="نمایش تعداد سوالات برای هر مبحث"
            @input="lessonIdChanged()"
          />
        </section>
        <!-- v-if="showElementTree('educationTree_GradeDdl')" -->
        <q-select
          v-model="educationTree.id"
          :options="educationTree_GradeDdl"
          float-label="جستجو درخت آموزش با مقطع"
          clearable
          class="s-q-input-border s-spacing"
        />
        <!-- v-if="showElementTree('educationTreeData')" -->
        <q-tree
          :nodes="educationTreeData"
          :expanded.sync="educationTree.expanded"
          :ticked.sync="educationTree.leafTicked"
          tick-strategy="leaf"
          class="tree-max-height s-spacing s-border"
          color="blue"
          node-key="Id"
        />
        <q-select
          :value="lessonId"
          @change="lessonIdChanged"
          filter
          :options="lessonStore.ddlByEducationTreeIds(educationTree.leafTicked)"
          float-label="انتخاب درس"
          class="s-q-input-border s-spacing s-border"
        />
      </div>
      <div class="col-md-8 col-lg-9">
        <section class="s-border s-spacing row">
          <!-- v-show="!showWithoutTopic" -->
          <q-input
            v-model="topicTree.filter"
            float-label="جستجوی مبحث"
            clearable
            class="col-md-5 s-q-input-border"
          />
          <!-- v-show="!showWithoutTopic" -->
          <q-tree
            :nodes="topicStore.treeDataByLessonId2"
            :expanded.sync="topicTree.expanded"
            :selected.sync="topicTree.selected"
            :ticked.sync="topicTree.leafTicked"
            :filter="topicTree.filter"
            tick-strategy="leaf"
            class="q-py-lg col-12"
            color="primary"
            accordion
            node-key="Id"
          />

          <hr class="col-12 q-mb-sm" />

          <base-btn-create
            v-if="canCreate"
            :label="`ایجاد (${questionStore.modelName}) جدید`"
            @click="showModalCreate"
            class="s-spacing"
          />

          <div class="col-12">
            <base-table
              :grid-data="questionStore.gridData"
              :columns="questionGridColumn"
              hasIndex
            >
              <template slot="Context" slot-scope="data">
                <div v-if="data.row.Context && data.row.Context.length > 100">
                  {{ `${data.row.Context.substring(0, 100)} ...` }}
                  <q-tooltip>{{ data.row.Context }}</q-tooltip>
                </div>
                <div v-else>{{ data.row.Context }}</div>
              </template>
              <template slot="Id" slot-scope="data">
                <a
                  v-if="showElementDownloadFile('showDownloadFile')"
                  :href="data.row.QuestionWordPath"
                  target="_blank"
                  class="q-mr-sm"
                  >فایل ورد</a
                >
                <q-btn
                  v-if="canQuestionJudge"
                  outline
                  round
                  icon="list"
                  color="brown"
                  size="sm"
                  class="shadow-1 bg-white q-mr-sm"
                  @click="showModalQuestionJudge(data.row.Id)"
                >
                  <q-tooltip>ارزیابی</q-tooltip>
                </q-btn>
                <q-btn
                  v-if="canQuestionAnswer"
                  outline
                  round
                  icon="list"
                  color="cyan"
                  size="sm"
                  class="shadow-1 bg-white q-mr-sm"
                  @click="showModalQuestionAnswer(data.row.Id)"
                >
                  <q-tooltip>جواب سوال</q-tooltip>
                </q-btn>
                <base-btn-edit
                  v-if="canEdit"
                  round
                  @click="showModalEdit(data.row.Id)"
                />
                <base-btn-delete
                  v-if="canDelete"
                  round
                  @click="showModalDelete(data.row.Id)"
                />
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </section>

    <!-- modals -->
    <modal-create
      v-if="canCreate"
      :topicTreeDataProp="topicTreeData"
      :topicTickedIdsProp="topicTree.leafTicked"
      :lessonIdProp="lessonId"
    ></modal-create>
    <modal-edit
      v-if="canEdit"
      :canEditAdminProp="canEditAdmin"
      :canEditTopicProp="canEditTopic"
      :canEditImportProp="canEditImport"
      :canEditFinalImportProp="canEditFinalImport"
      :topicTreeDataProp="topicTreeData"
      :lessonIdProp="lessonId"
    ></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
    <modal-question-judge
      v-if="canQuestionJudge"
      :topicTreeDataProp="topicTreeData"
      :lessonIdProp="lessonId"
    ></modal-question-judge>
    <modal-question-answer
      v-if="canQuestionAnswer"
      :lessonIdProp="lessonId"
    ></modal-question-answer>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import utilities from "src/utilities";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue"),
    ModalQuestionJudge: () => import("../questionJudge/index.vue"),
    ModalQuestionAnswer: () => import("../questionAnswer/index.vue")
  }
})
export default class QuestionVue extends Vue {
  //#region ### data ###

  questionStore = vxm.questionStore;
  topicStore = vxm.topicStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  questionJudgeStore = vxm.questionJudgeStore;
  questionAnswerStore = vxm.questionAnswerStore;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionGridColumn = [
    {
      title: "کد",
      data: "Code"
    },
    {
      title: "متن سوال",
      data: "Context"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible:
        this.canEdit ||
        this.canDelete ||
        this.canQuestionAnswer ||
        this.canQuestionJudge
    }
  ];
  lessonId = 0;
  educationTree = this.educationTreeStore.qTreeData;
  topicTree = this.topicStore.qTreeData;
  showNoJudgement = false;
  showWithoutTopic = false;
  showJudged = false;
  showActived = false;
  showUnActived = false;
  showNoAnswer = false;
  showNoAnswerJudge = false;

  showNumberForTopic = false;

  questionFilterAccessElement = {
    showNoJudgement: {
      canEditAdminProp: true,
      canEditExpertProp: true,
      canEditImportProp: true,
      canEditFinalImportProp: false
    },
    showWithoutTopic: {
      canEditAdminProp: true,
      canEditExpertProp: false,
      canEditImportProp: true,
      canEditFinalImportProp: false
    },
    showJudged: {
      canEditAdminProp: true,
      canEditExpertProp: false,
      canEditImportProp: false,
      canEditFinalImportProp: false
    },
    showActived: {
      canEditAdminProp: true,
      canEditExpertProp: false,
      canEditImportProp: false,
      canEditFinalImportProp: true
    },
    showUnActived: {
      canEditAdminProp: true,
      canEditExpertProp: true,
      canEditImportProp: false,
      canEditFinalImportProp: true
    },
    showDownloadFile: {
      canEditAdminProp: true,
      canEditExpertProp: false,
      canEditImportProp: true,
      canEditFinalImportProp: true
    },

    showNumberForTopic: {
      canEditAdminProp: true,
      canEditExpertProp: false,
      canEditImportProp: true,
      canEditFinalImportProp: false
    },
    showNoAnswer: {
      canEditAdminProp: true,
      canEditExpertProp: true,
      canEditImportProp: true,
      canEditFinalImportProp: true
    },
    showNoAnswerJudge: {
      canEditAdminProp: true,
      canEditExpertProp: true,
      canEditImportProp: true,
      canEditFinalImportProp: false
    }
  };

  treeAccessElement = {
    educationTreeData: {
      canEditAdminProp: true
    },
    educationTree_GradeDdl: {
      canEditAdminProp: true
    }
  };

  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEditAdmin() {
    return this.pageAccess.indexOf("ویرایش ادمین") > -1;
  }

  get canEditTopic() {
    return this.pageAccess.indexOf("ویرایش مبحث") > -1;
  }

  get canEditImport() {
    return this.pageAccess.indexOf("ویرایش ورود سوال") > -1;
  }

  get canEditFinalImport() {
    return this.pageAccess.indexOf("ویرایش ویراستار نهایی") > -1;
  }
  get canEdit() {
    return this.canEditAdmin || this.canEditTopic || this.canEditImport || this.canEditFinalImport;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }

  get canQuestionJudge() {
    return this.pageAccess.indexOf("مشاهده کارشناسی") > -1;
  }

  get canQuestionAnswer() {
    return this.pageAccess.indexOf("مشاهده جواب سوال") > -1;
  }

  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }

  get topicTreeData() {
    // var treeData = this.topicStore.treeDataByLessonId(this.lessonId);
    // if (this.topicTree.setToFirstLevel) {
    //   this.topicTree.expanded = this.topicTree.firstLevel;
    //   this.topicTree.setToFirstLevel = false;
    // }
    // return treeData;
    return this.topicStore.treeDataByLessonId2;
  }

  get canShowAdminFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر ادمین") > -1;
  }

  get canShowImportFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر ورود") > -1;
  }
  
  get canShowFinalImportFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر ویراستار نهایی") > -1;
  }

  get canShowExpertFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر کارشناس") > -1;
  }

  get canShowTree() {
    return this.pageAccess.indexOf("مشاهده درخت پایه") > -1;
  }

  get activeAccess() {
    if (this.canShowAdminFilter) {
      return "canEditAdminProp";
    } else if (this.canShowImportFilter) {
      return "canEditImportProp";
    } else {
      return "canEditExpertProp";
    }
  }

  get activeAccessDownloadFile() {
    if (this.canEditAdmin) {
      return "canEditAdminProp";
    } else if (this.canEditImport) {
      return "canEditImportProp";
    } else if (this.canEditFinalImport) {
      return "canEditImportProp"
    }else {
      return "canEditExpertProp";
    }
  }

  get activeAccessTree() {
    if (this.canShowTree) {
      return "canEditAdminProp";
    } else {
      return "canEditExpertProp";
    }
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.lessonId = 0;
    // clear educationTree leaf
    this.educationTree.leafTicked.splice(
      0,
      this.educationTree.leafTicked.length
    );

    let index = this.educationTree.expanded.indexOf(oldVal);
    if (index > -1) {
      this.educationTree.expanded.splice(index, 1);
    }

    if (this.educationTree.expanded.indexOf(newVal) == -1) {
      this.educationTree.expanded.push(newVal);
    }
  }

  @Watch("showWithoutTopic")
  questionShowWithoutTopicChanged(newVal) {
    if (newVal) {
      this.showJudged = false;
      this.showActived = false;
      this.showNoJudgement = false;
      this.showUnActived = false;
      this.showNoAnswer = false;
      this.showNoAnswerJudge = false;
    }
  }
  @Watch("showNoJudgement")
  questionShowNoJudgementChanged(newVal) {
    if (newVal) {
      this.showJudged = false;
      this.showActived = false;
      this.showWithoutTopic = false;
      this.showUnActived = false;
      this.showNoAnswer = false;
      this.showNoAnswerJudge = false;
    }
  }
  @Watch("showJudged")
  questionShowJudgedChanged(newVal) {
    if (newVal) {
      this.showWithoutTopic = false;
      this.showActived = false;
      this.showNoJudgement = false;
      this.showUnActived = false;
      this.showNoAnswer = false;
      this.showNoAnswerJudge = false;
    }
  }
  @Watch("showActived")
  questionShowActivedChanged(newVal) {
    if (newVal) {
      this.showJudged = false;
      this.showWithoutTopic = false;
      this.showNoJudgement = false;
      this.showUnActived = false;
      this.showNoAnswer = false;
      this.showNoAnswerJudge = false;
    }
  }
  @Watch("showUnActived")
  questionShowUnActivedChanged(newVal) {
    if (newVal) {
      this.showJudged = false;
      this.showWithoutTopic = false;
      this.showNoJudgement = false;
      this.showNoAnswer = false;
      this.showActived = false;
      this.showNoAnswerJudge = false;
    }
  }

  @Watch("showNoAnswer")
  questionShowNoAnswerChanged(newVal) {
    if (newVal) {
      this.showJudged = false;
      this.showWithoutTopic = false;
      this.showNoJudgement = false;
      this.showUnActived = false;
      this.showActived = false;
      this.showNoAnswerJudge = false;
    }
  }

  @Watch("showNoAnswerJudge")
  questionShowNoAnswerJudgeChanged(newVal) {
    if (newVal) {
      this.showJudged = false;
      this.showWithoutTopic = false;
      this.showNoJudgement = false;
      this.showUnActived = false;
      this.showActived = false;
      this.showNoAnswer = false;
    }
  }
  @Watch("educationTree.leafTicked")
  educationTreeTickedIdsChanged(newVal) {
    this.lessonId = 0;
  }

  @Watch("topicTree.leafTicked")
  topicTreeTickedIdsChanged(newVal) {
    this.fillGrid();
  }
  //#endregion

  //#region ### methods ###

  showElement(elementName) {
    var elem = this.questionFilterAccessElement[elementName];
    return elem && elem[this.activeAccess];
  }

  showElementDownloadFile(elementName) {
    var elem = this.questionFilterAccessElement[elementName];
    return elem && elem[this.activeAccessDownloadFile];
  }

  showElementTree(elementName) {
    var elem = this.treeAccessElement[elementName];
    return elem && elem[this.activeAccessTree];
  }
  showModalCreate() {
    this.questionStore.resetCreate();
    this.questionStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.questionStore.resetEdit();
    this.questionStore.getById(id).then(() => {
      this.questionStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.questionStore.getById(id).then(() => {
      this.questionStore.OPEN_MODAL_DELETE(true);
    });
  }

  lessonIdChanged(val) {
    if (val != undefined) {
      this.lessonId = val;
    }
    debugger;
    this.topicStore
      .fillListByLessonId({
        id: this.lessonId,
        showNumberForTopic: this.showNumberForTopic
      })
      .then(() => {
        this.topicTree.setToFirstLevel = true;
        // clear topicTree leaf
        utilities.clearArray(this.topicTree.leafTicked);
      });
  }

  fillGrid() {
    this.questionStore.fillList({
      lessonId: this.lessonId,
      topicIds: this.topicTree.leafTicked,
      showNoJudgement: this.showNoJudgement,
      showWithoutTopic: this.showWithoutTopic,
      showJudged: this.showJudged,
      showActived: this.showActived,
      showUnActived: this.showUnActived,
      showNoAnswer: this.showNoAnswer,
      showNoAnswerJudge: this.showNoAnswerJudge
    });
  }

  showModalQuestionJudge(id) {
    this.questionStore.getById(id).then(() => {
      this.questionJudgeStore.fillListByQuestionId(id).then(() => {
        this.questionJudgeStore.OPEN_MODAL_INDEX(true);
      });
    });
  }

  showModalQuestionAnswer(id) {
    this.questionStore.getById(id).then(() => {
      this.questionAnswerStore.fillListByQuestionId(id).then(() => {
        this.questionAnswerStore.OPEN_MODAL_INDEX(true);
      });
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.topicStore.fillList();
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }

  mounted() {
    let route = this["$route"];
    if (route && route.params.id && route.params.lessonId) {
      // fill educationTree
      this.educationTreeStore
        .GetAllByLessonId(route.params.lessonId)
        .then(tickedEducationTreeIds => {
          utilities.clearArray(this.educationTree.leafTicked);
          tickedEducationTreeIds.forEach(x => {
            this.educationTree.leafTicked.push(x);
          });
        })
        .then(() => {
          this.lessonIdChanged(+route.params.lessonId);
        })
        .then(() => {
          this.questionStore.getById(+route.params.id).then(() => {
            // open edit modal
            this.questionStore.OPEN_MODAL_EDIT(true);
            // topic leaf and fill grid
            if (this.questionStore.question.TopicIds) {
              this.questionStore.question.TopicIds.forEach(x => {
                this.topicTree.leafTicked.push(x);
              });
            }
          });
        });
    }
  }
  //#endregion
}
</script>

