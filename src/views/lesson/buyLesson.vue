<template>
  <section class="col-md-12 q-px-lg">
    <section slot="body" class="row gutter-sm">
      <div class="col-md-4">
        <q-select
          v-model="educationTree.id"
          :options="educationTree_GradeDdl"
          float-label="جستجو درخت آموزش با مقطع"
          clearable
        />
        <q-tree
          :nodes="educationTreeData"
          :expanded.sync="educationTree.expanded"
          :ticked.sync="educationTree.leafTicked"
          tick-strategy="leaf"
          color="blue"
          node-key="Id"
        />
      </div>
      <div class="col-md-8">
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${lessonStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br />
        <base-table
          :grid-data="lessonStore.gridDataByEducationTreeIds(educationTree.leafTicked)"
          :columns="lessonGridColumn"
          hasIndex
        >
          <template slot="Id" slot-scope="data">
            <base-btn-edit round v-if="canEdit" @click="showModalEdit(data.row.Id)" />
            <base-btn-delete round v-if="canDelete" @click="showModalDelete(data.row.Id)" />

            <q-btn class="q-ma-sm" size="sm" round color="purple" icon="shopping_cart" @click="buyLesson(data.row.Id)">
              خرید
              <q-tooltip>خرید</q-tooltip>
            </q-btn>
          </template>
        </base-table>
      </div>
    </section>

    <!-- modals -->
    <modal-create
      v-if="canCreate"
      :educationTreeIdProp="educationTree.id"
      :expandedTreeIdsProp="educationTree.expanded"
    ></modal-create>
    <modal-edit v-if="canEdit" :expandedTreeIdsProp="educationTree.expanded"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
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
    ModalDelete: () => import("./delete.vue")
  }
})
export default class LessonVue extends Vue {
  //#region ### data ###
  lessonStore = vxm.lessonStore;
  lessonUserStore = vxm.lesson_UserStore;
  educationTreeStore = vxm.educationTreeStore;
  pageAccess = util.getAccess(this.lessonStore.modelName);
  lessonGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete || this.isStudent
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

  get isStudent() {
    return true;
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
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.lessonStore.resetCreate();
    this.lessonStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.lessonStore.resetEdit();
    this.lessonStore.getById(id).then(() => {
      this.lessonStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.lessonStore.getById(id).then(() => {
      this.lessonStore.OPEN_MODAL_DELETE(true);
    });
  }

  buyLesson(id){
    this.lessonUserStore.buyLesson(id);
  }

  //#endregion

  //#region ### hooks ###
  created() {
        this.lessonUserStore.SET_BUY_LESSON_VUE(this);

    this.lessonStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>

