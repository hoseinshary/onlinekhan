<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{questionStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-12">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-checkbox
                v-model="showNoJudgement"
                label="نمایش سوال های بدون ارزیابی"
                @input="fillGrid()"
              />
              <br>
              <q-checkbox
                v-model="showWithoutTopic"
                label="نمایش سوال های بدون مبحث"
                @input="fillGrid()"
              />
            </section>
          </div>
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-select
                v-model="educationTree.id"
                :options="educationTree_GradeDdl"
                float-label="فیلتر درخت آموزش با مقطع"
                clearable
              />
              <!-- <q-field class="col-12">
                <q-input v-model="educationTreeFilter" float-label="جستجو در درخت آموزش" clearable/>
              </q-field>-->
              <q-tree
                :nodes="educationTreeData"
                :expanded.sync="educationTree.expanded"
                :ticked.sync="educationTree.leafTicked"
                tick-strategy="leaf"
                class="tree-max-height"
                color="blue"
                node-key="Id"
              />
              <q-select
                :value="lessonId"
                @change="lessonIdChanged"
                :options="lessonStore.ddl"
                float-label="انتخاب درس"
                class="q-pt-lg"
              />
            </section>
          </div>
          <div class="col-sm-7" v-if="!showWithoutTopic">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-field class="col-12">
                <q-input v-model="topicTree.filter" float-label="جستجوی مبحث" clearable/>
              </q-field>
              <q-tree
                :nodes="topicTreeData"
                :expanded.sync="topicTree.expanded"
                :selected.sync="topicTree.selected"
                :ticked.sync="topicTree.leafTicked"
                :filter="topicTree.filter"
                tick-strategy="leaf"
                class="q-pt-lg"
                color="primary"
                accordion
                node-key="Id"
              />
            </section>
          </div>
          <br>
        </section>
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${questionStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br>
        <base-table :grid-data="questionStore.gridData" :columns="questionGridColumn" hasIndex>
          <template slot="Context" slot-scope="data">
            <div v-if="data.row.Context && data.row.Context.length> 100">
              {{(`${data.row.Context.substring(0,100)} ...`)}}
              <q-tooltip>{{data.row.Context}}</q-tooltip>
            </div>
            <div v-else>{{data.row.Context}}</div>
          </template>
          <template slot="Id" slot-scope="data">
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
            <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)"/>
            <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)"/>
          </template>
        </base-table>
      </div>
    </base-panel>
    <!-- modals -->
    <modal-create
      v-if="canCreate"
      :topicTreeDataProp="topicTreeData"
      :topicTickedIdsProp="topicTree.leafTicked"
    ></modal-create>
    <modal-edit
      v-if="canEdit"
      :canEditAdminProp="canEditAdmin"
      :canEditTopicProp="canEditTopic"
      :canEditImportProp="canEditImport"
      :topicTreeDataProp="topicTreeData"
    ></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
    <modal-question-judge v-if="canQuestionJudge"></modal-question-judge>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue"),
    ModalQuestionJudge: () => import("../questionJudge/index.vue")
  }
})
export default class QuestionVue extends Vue {
  //#region ### data ###
  questionStore = vxm.questionStore;
  topicStore = vxm.topicStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  questionJudgeStore = vxm.questionJudgeStore;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionGridColumn = [
    {
      title: "متن سوال",
      data: "Context"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete
    }
  ];
  lessonId = 0;
  educationTree = this.educationTreeStore.qTreeData;
  topicTree = this.topicStore.qTreeData;
  showNoJudgement = false;
  showWithoutTopic = false;
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

  get canEdit() {
    return this.canEditAdmin || this.canEditTopic || this.canEditImport;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }

  get canQuestionJudge() {
    return this.pageAccess.indexOf("مشاهده کارشناسی") > -1;
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
    var treeData = this.topicStore.treeDataByLessonId(this.lessonId);
    if (this.topicTree.setToFirstLevel) {
      this.topicTree.expanded = this.topicTree.firstLevel;
      this.topicTree.setToFirstLevel = false;
    }
    return treeData;
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

  @Watch("educationTree.leafTicked")
  educationTreeTickedIdsChanged(newVal) {
    this.lessonId = 0;
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }

  @Watch("topicTree.leafTicked")
  topicTreeTickedIdsChanged(newVal) {
    this.fillGrid();
  }
  //#endregion

  //#region ### methods ###
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
    this.lessonId = val;
    this.topicTree.setToFirstLevel = true;
    // clear topicTree leaf
    this.topicTree.leafTicked.splice(0, this.topicTree.leafTicked.length);
  }

  fillGrid() {
    this.questionStore.fillList({
      lessonId: this.lessonId,
      topicIds: this.topicTree.leafTicked,
      showNoJudgement: this.showNoJudgement,
      showWithoutTopic: this.showWithoutTopic
    });
  }

  showModalQuestionJudge(id) {
    this.questionStore.getById(id).then(() => {
      this.questionJudgeStore.fillListByQuestionId(id).then(() => {
        this.questionJudgeStore.OPEN_MODAL_INDEX(true);
      });
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationTreeStore.fillList().then(res => {
      this.topicStore.fillList();
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>

