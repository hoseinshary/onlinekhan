<template>
  <bs-modal :show="questionJudgeStore.openModal.index" size="xl" @open="open">
    <template slot="header">
      <q-toolbar slot="header" color="blue-8">
        <q-toolbar-title>{{ questionJudgeStore.modelName }}</q-toolbar-title>
        <q-btn
          dense
          icon="close"
          @click="questionJudgeStore.OPEN_MODAL_INDEX(false)"
        />
      </q-toolbar>
    </template>

    <q-tree
      v-show="false"
      :nodes="topicTreeDataProp"
      :ticked.sync="question.TopicIds"
      tick-strategy="leaf"
      color="primary"
      accordion
      ref="topicTree"
      node-key="Id"
    />

    <q-tree
      v-show="false"
      :nodes="
        topicStore.treeDataByLessonIds(
          lessonStore.relatedLessonIds(lessonIdProp)
        )
      "
      :ticked.sync="question.TopicIds"
      tick-strategy="leaf"
      color="primary"
      accordion
      ref="topicTree2"
      node-key="Id"
    />

    <div class="col-md-5 col-lg-4">
      <section class="row s-border s-spacing">
        <p class="col-12 text-primary text-weight-bold q-pa-sm">
          مشخصه های سوال
        </p>
        <div class="col-12 q-pa-sm" style="color: red">
          جواب صحیح:
          <span class="text-red">{{ question.AnswerNumber }}</span>
        </div>
        <div class="col-12 q-pa-sm">
          مبحث پاسخ صحیح:
          <span class="text-black">{{ question.TopicAnswer }}</span>
        </div>
        <div class="col-12 q-pa-sm">
          نویسنده:
          <span class="text-black">{{ question.Writer.Name }}</span>
        </div>
        <div class="col-12 q-pa-sm">
          حیطه :
          {{ question.Lookup_AreaTypes.map((x) => x.Value).join(", ") }}
        </div>
      </section>

      <section class="row s-border s-spacing">
        <q-tabs color="blue-8" v-model="selectedTab">
          <!-- tabs -->
          <q-tab
            slot="title"
            name="tab-create"
            label="ایجاد"
            icon="library_add"
            @select="showTabCreate()"
          />
          <q-tab
            slot="title"
            :disable="!editMode"
            name="tab-edit"
            label="ویرایش"
            icon="create"
          />
          <q-tab
            slot="title"
            :disable="!editMode"
            name="tab-detail"
            label="جزئیات"
            icon="create"
          />
          <!-- tab-panes -->
          <q-tab-pane keep-alive name="tab-create" class="no-border">
            <tab-create></tab-create>
          </q-tab-pane>
          <q-tab-pane keep-alive name="tab-edit" class="no-border">
            <tab-edit></tab-edit>
          </q-tab-pane>
          <q-tab-pane keep-alive name="tab-detail" class="no-border">
            <tab-detail></tab-detail>
          </q-tab-pane>
        </q-tabs>
      </section>
    </div>

    <div class="col-md-7 col-lg-8">
      <section class="s-border s-spacing" v-show="question.FileName">
        <img :src="question.QuestionPicturePath" class="img-original-width" />
      </section>

      <section class="s-border s-spacing">
        <p v-for="(elem, index) in concatTopicArray" :key="index">{{ elem }}</p>
        <div class="col-12"></div>
      </section>

      <section class="s-border s-spacing">
        <base-table
          :grid-data="questionJudgeStore.gridData"
          :columns="questionJudgeGridColumn"
          hasIndex
        >
          <template slot="User.Name" slot-scope="data">{{
            data.row.User.Name
          }}</template>
          <template slot="User.Family" slot-scope="data">{{
            data.row.User.Family
          }}</template>
          <template slot="Lookup_RepeatnessType.Value" slot-scope="data">{{
            data.row.Lookup_RepeatnessType.Value
          }}</template>
          <template
            slot="Lookup_QuestionHardnessType.Value"
            slot-scope="data"
            >{{ data.row.Lookup_QuestionHardnessType.Value }}</template
          >
          <template slot="IsDelete" slot-scope="data">
            <span v-if="data.row.IsDelete">حذف</span>
            <span v-else-if="data.row.IsUpdate">ویرایش</span>
            <span v-else>تایید</span>
          </template>
          <template slot="Id" slot-scope="data">
            <base-btn-edit
              v-if="canEdit"
              round
              @click="showTabEdit(data.row.Id)"
            />
            <btn-delete
              v-if="canDelete"
              :recordIdProp="data.row.Id"
            ></btn-delete>
            &nbsp;
         

            <q-btn
              outline
              v-if="!canEdit"
              round
              color="brown-14"
              class="shadow-1 bg-white q-mr-sm"
              @click="showTabDetail(data.row.Id)"
            >
              <q-icon name="search" />
            </q-btn>
          </template>
        </base-table>
      </section>
    </div>

    <template slot="footer">
      <base-btn-back
        @click="questionJudgeStore.OPEN_MODAL_INDEX(false)"
      ></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    TabCreate: () => import("./create.vue"),
    TabEdit: () => import("./edit.vue"),
    BtnDelete: () => import("./delete.vue"),
    TabDetail: () => import("./detail.vue")
  }
})
export default class QuestionJudgeVue extends Vue {
  //#region ### props ###
  @Prop({ type: Array, required: true }) topicTreeDataProp;
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  questionJudgeStore = vxm.questionJudgeStore;
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  topicStore = vxm.topicStore;
  lessonStore = vxm.lessonStore;
  questionJudge = this.questionJudgeStore.questionJudge;
  question = this.questionStore.question;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionJudgeGridColumn = [
    {
      title: "نام",
      data: "User.Name"
    },
    {
      title: "نام خانوادگی",
      data: "User.Family"
    },
    // {
    //   title: "استاندارد",
    //   data: "IsStandardName"
    // },
    // {
    //   title: "یادگیری",
    //   data: "IsLearningName"
    // },
    // {
    //   title: "وضعیت",
    //   data: "IsDelete"
    // },
    // {
    //   title: "تایید جواب",
    //   data: "IsActiveQuestionAnswerName"
    // },
    // {
    //   title: "مدت پاسخ",
    //   data: "ResponseSecond"
    // },
    // {
    //   title: "درجه تکرار",
    //   data: "Lookup_RepeatnessType.Value"
    // },
    // {
    //   title: "درجه سختی",
    //   data: "Lookup_QuestionHardnessType.Value"
    // },
    // {
    //   title: "گروه آموزشی",
    //   data: "EducationGroup"
    // },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false
      
    }
  ];
  selectedTab = "tab-create";
  concatTopicArray: Array<string> = [];
  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد کارشناسی") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش کارشناسی") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف کارشناسی") > -1;
  }

  get editMode() {
    return this.selectedTab == "tab-edit";
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
  showTabCreate() {
    this.questionJudgeStore.resetCreate();
    this.questionJudge.QuestionId = this.question.Id;
  }

  showTabEdit(id) {
    this.questionJudgeStore.resetEdit();
    this.questionJudgeStore.getById(id).then(() => {
      this.selectedTab = "tab-edit";
    });
  }

  showTabDetail(id) {

    this.questionJudgeStore.getById(id).then(() => {
      this.selectedTab = "tab-detail";
    });
  }

  open() {
    if (this.canCreate || this.canEdit) {
      this.lookupStore.fillQuestionHardnessType();
      this.lookupStore.fillRepeatnessType();
      this.lookupStore.fillReasonProblem();
      this.lookupStore.fillWhereProblem();
      this.lookupStore.fillQuestionRank();

      this.questionJudgeStore.resetCreate();
      this.questionJudge.QuestionId = this.question.Id;
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionJudgeStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>