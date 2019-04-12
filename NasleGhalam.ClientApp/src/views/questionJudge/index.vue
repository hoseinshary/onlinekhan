<template>
  <bs-modal :show="isOpenModal" size="xl">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>{{modelName}}</q-toolbar-title>
        <q-btn dense icon="close" @click="toggleModalQuestionStore({isOpen:false})"/>
      </q-toolbar>
    </template>

    <q-card inline class="col-12" v-if="questionObj.FileName">
      <q-card-media>
        <img :src="questionObj.QuestionPicturePath">
      </q-card-media>
    </q-card>
    <div class="col-4">
      <create-question-judge></create-question-judge>
    </div>
    <div class="col-8">
      <my-table :grid-data="questionJudgeGridData" :columns="questionJudgeGridColumn" hasIndex>
        <template slot="Id" slot-scope="data">
          <my-btn-edit v-if="pageAccess.canEdit" round @click="showModalEdit(data.row.Id)"/>
          <my-btn-delete v-if="pageAccess.canDelete" round @click="showModalDelete(data.row.Id)"/>
        </template>
      </my-table>
    </div>

    <template slot="footer">
      <my-btn-back @click="toggleModalQuestionStore({isOpen:false})"></my-btn-back>
    </template>
    <!--<modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>-->
  </bs-modal>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  components: {
    "create-question-judge": () => import("./create")
  },
  /**
   * data
   */
  data() {
    return {
      questionJudgeGridColumn: [
        {
          title: "استاندارد",
          data: "IsStandard"
        },
        {
          title: "حذف",
          data: "IsDelete"
        },
        {
          title: "ویرایش",
          data: "IsUpdate"
        },
        {
          title: "یادگیری",
          data: "IsLearning"
        },
        {
          title: "مدت پاسخ",
          data: "ResponseSecond"
        },
        {
          title: "درجه تکرار",
          data: "LookupId_RepeatnessType"
        },
        {
          title: "درجه سختی",
          data: "LookupId_QuestionHardnessType"
        }
        //,
        // {
        //   title: "عملیات",
        //   data: "Id",
        //   searchable: false,
        //   sortable: false,
        //   visible: pageAccess.canEdit || pageAccess.canDelete
        // }
      ]
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions("questionJudgeStore", [
      "getByIdStore",
      "fillGridStore",
      "resetCreateStore",
      "resetEditStore"
    ]),
    ...mapActions("questionJudgeStore", {
      toggleModalQuestionStore: "toggleModalStore"
    })
  },
  computed: {
    ...mapState("questionJudgeStore", {
      modelName: "modelName",
      questionJudgeGridData: "questionJudgeGridData",
      isOpenModal: "isOpenModal"
    }),
    ...mapState("questionStore", {
      questionObj: "questionObj"
    })
  },
  created() {
    // this.fillGridStore();
  }
};
</script>