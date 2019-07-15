<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{questionGroupStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-select
                v-model="educationTree.id"
                :options="educationTree_GradeDdl"
                float-label="فیلتر درخت آموزش با مقطع"
                clearable
              />
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
                :value="questionGroup.LessonId"
                @change="lessonIdChanged"
                :options="lessonStore.ddl"
                float-label="انتخاب درس"
                class="q-pt-lg"
              />
            </section>
          </div>
          <div class="col-sm-7">
            <base-btn-create
              v-if="canCreate && questionGroup.LessonId>0"
              :label="`ایجاد (${questionGroupStore.modelName}) جدید`"
              @click="showModalCreate"
            />
            <br />
            <base-table
              :grid-data="questionGroupStore.gridData"
              :columns="questionGroupGridColumn"
              hasIndex
            >
              <template slot="Id" slot-scope="data">
                <q-btn
                  outline
                  dense
                  color="primary"
                  class="shadow-1 bg-white q-mr-sm"
                  @click="showModalQuestions(data.row.Id)"
                >سوال ها</q-btn>
                <q-btn
                  outline
                  dense
                  color="primary"
                  class="shadow-1 bg-white q-mr-sm"
                  @click="showModalCreateMultiAnswer(data.row.Id)"
                >ثبت جواب گروهی</q-btn>
                <a :href="data.row.QuestionGroupWordPath" target="_blank" class="q-mr-sm">فایل ورد</a>
                <a :href="data.row.QuestionGroupExcelPath" target="_blank" class="q-mr-sm">فایل اکسل</a>
                <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <modal-questions></modal-questions>
    <modal-delete v-if="canDelete"></modal-delete>
    <modal-create-multi-answer></modal-create-multi-answer>
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
    ModalQuestions: () => import("../question/questions.vue"),
    ModalDelete: () => import("./delete.vue"),
    ModalCreateMultiAnswer: () =>
      import("../questionAnswer/createMultiAnswer.vue")
  }
})
export default class QuestionGroupVue extends Vue {
  //#region ### data ###
  questionGroupStore = vxm.questionGroupStore;
  questionGroup = vxm.questionGroupStore.questionGroup;
  questionStore = vxm.questionStore;
  questionAnswerMultiStore = vxm.questionAnswerMultiStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  pageAccess = util.getAccess(this.questionGroupStore.modelName);
  questionGroupGridColumn = [
    {
      title: "عنوان",
      data: "Title"
    },
    {
      title: "زمان ثبت",
      data: "PInsertTime"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete
    }
  ];
  educationTree = this.educationTreeStore.qTreeData;
  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }

  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.questionGroup.LessonId = 0;
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
    this.questionGroup.LessonId = 0;
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.questionGroupStore.resetCreate();
    this.questionGroupStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.questionGroupStore.resetEdit();
    this.questionGroupStore.getById(id).then(() => {
      this.questionGroupStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.questionGroupStore.getById(id).then(() => {
      this.questionGroupStore.OPEN_MODAL_DELETE(true);
    });
  }

  showModalQuestions(id) {
    this.questionStore.fillListByQuestionGroupId(id).then(() => {
      this.questionStore.OPEN_MODAL_QUESTIONS(true);
    });
  }

  showModalCreateMultiAnswer(id) {
    this.questionStore.fillListByQuestionGroupId(id).then(() => {
      this.questionAnswerMultiStore.resetCreate();
      this.questionAnswerMultiStore.questionAnswerMulti.QuestionGroupId = id;
      this.questionAnswerMultiStore.OPEN_MODAL_CREATE(true);
    });
  }

  lessonIdChanged(val) {
    this.questionGroup.LessonId = val;
    this.questionGroupStore.fillListByLessonId(val);
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>

