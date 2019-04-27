<template>
  <bs-modal :show="questionAnswerStore.openModal.index" size="xl" @open="open">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>{{questionAnswerStore.modelName}}</q-toolbar-title>
        <q-btn dense icon="close" @click="questionAnswerStore.OPEN_MODAL_INDEX(false)"/>
      </q-toolbar>
    </template>

    <q-card inline class="col-12" v-if="question.FileName">
      <q-card-media>
        <img :src="question.QuestionPicturePath" class="img-original-width">
      </q-card-media>
    </q-card>
    <div class="col-4">
      <q-tabs color="cyan-9" v-model="selectedTab">
        <!-- tabs -->
        <q-tab slot="title" name="tab-create" label="ایجاد" icon="library_add"/>
        <q-tab slot="title" :disable="!editMode" name="tab-edit" label="ویرایش" icon="create"/>
        <!-- tab-panes -->
        <q-tab-pane keep-alive name="tab-create">
          <tab-create></tab-create>
        </q-tab-pane>
        <q-tab-pane keep-alive name="tab-edit">
          <!-- <tab-edit></tab-edit> -->
        </q-tab-pane>
      </q-tabs>
    </div>
    <div class="col-8">
      <base-table
        :grid-data="questionAnswerStore.gridData"
        :columns="questionAnswerGridColumn"
        hasIndex
      >
        <template slot="Context" slot-scope="data">
          <div v-if="data.row.Context && data.row.Context.length> 100">
            {{(`${data.row.Context.substring(0,100)} ...`)}}
            <q-tooltip>{{data.row.Context}}</q-tooltip>
          </div>
          <div v-else>{{data.row.Context}}</div>
        </template>
        <!-- <template
          slot="Lookup_QuestionHardnessType.Value"
          slot-scope="data"
        >{{data.row.Lookup_QuestionHardnessType.Value}}</template>-->
        <template slot="Id" slot-scope="data">
          <!-- <base-btn-edit v-if="canEdit" round @click="showTabEdit(data.row.Id)"/> -->
          <btn-delete v-if="canDelete" :recordIdProp="data.row.Id"></btn-delete>
        </template>
      </base-table>
    </div>

    <template slot="footer">
      <base-btn-back @click="questionAnswerStore.OPEN_MODAL_INDEX(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>


<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    TabCreate: () => import("./create.vue"),
    // TabEdit: () => import("./edit.vue"),
    BtnDelete: () => import("./delete.vue")
  }
})
export default class QuestionAnswerVue extends Vue {
  //#region ### data ###
  questionAnswerStore = vxm.questionAnswerStore;
  questionStore = vxm.questionStore;
  lookupStore = vxm.lookupStore;
  questionAnswer = this.questionAnswerStore.questionAnswer;
  question = this.questionStore.question;
  pageAccess = util.getAccess(this.questionStore.modelName);
  questionAnswerGridColumn = [
    {
      title: "عنوان",
      data: "Title"
    },
    {
      title: "متن جواب",
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
  selectedTab = "tab-create";
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

  get editMode() {
    return this.selectedTab == "tab-edit";
  }
  //#endregion

  //#region ### methods ###
  showTabEdit(id) {
    this.questionAnswerStore.resetEdit();
    this.questionAnswerStore.getById(id).then(() => {
      this.selectedTab = "tab-edit";
    });
  }

  open() {
    if (this.canCreate || this.canEdit) {
      this.lookupStore.fillAnswerType();

      this.questionAnswerStore.resetCreate();
      this.questionAnswer.QuestionId = this.question.Id;
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.questionAnswerStore.SET_INDEX_VUE(this);
  }
  //#endregion
}
</script>