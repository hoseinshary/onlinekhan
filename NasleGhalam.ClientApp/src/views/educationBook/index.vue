<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{educationBookStore.modelName}}</span>
      <div slot="body">
        <section class="row gutter-sm">
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
                :selected.sync="educationTree.selected"
                class="tree-max-height"
                tick-strategy="leaf"
                color="blue"
                node-key="Id"
              />
              <q-select
                :value="lessonId"
                @change="lessonIdChanged"
                :options="lessonStore.ddlByEducationTreeIds(educationTree.leafTicked)"
                float-label="انتخاب درس"
                class="q-pt-lg"
              />
            </section>
          </div>

          <div class="col-sm-7">
            <base-btn-create
              v-if="canCreate && lessonId > 0"
              :label="`ایجاد (${educationBookStore.modelName}) جدید`"
              @click="showModalCreate"
            />
            <br />
            <base-table
              :grid-data="educationBookStore.gridData"
              :columns="educationBookGridColumn"
              hasIndex
            >
              <template slot="IsActive" slot-scope="data">
                <q-checkbox v-model="data.row.IsActive" readonly />
              </template>
              <template slot="IsExamSource" slot-scope="data">
                <q-checkbox v-model="data.row.IsExamSource" readonly />
              </template>
              <template slot="IsChanged" slot-scope="data">
                <q-checkbox v-model="data.row.IsChanged" readonly />
              </template>
              <template slot="Id" slot-scope="data">
                <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
                <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <modal-edit v-if="canEdit"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import IEducationBook, {
  DefaultEducationBook
} from "../../models/IEducationBook";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class EducationBookVue extends Vue {
  //#region ### data ###
  educationBookStore = vxm.educationBookStore;
  educationBook = vxm.educationBookStore.educationBook;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  pageAccess = util.getAccess(this.educationBookStore.modelName);
  educationBookGridColumn = [
    {
      title: "نام کتاب",
      data: "Name"
    },
    {
      title: "سال انتشار",
      data: "PublishYear"
    },
    {
      title: "منبع کنکوری",
      data: "IsExamSource"
    },
    {
      title: "فعال",
      data: "IsActive"
    },
    {
      title: "تغییر نسبت به سال قبل",
      data: "IsChanged"
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
    if (!this.educationTree.id) {
      return this.educationTreeStore.treeData;
    } else {
      return this.educationTreeStore.treeData[0].children.filter(
        x => x.Id == this.educationTree.id
      );
    }
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.lessonId = 0;
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
  }
  //#endregion

  //#region ### methods ###
  showModalCreate(id, title) {
    this.educationBookStore.resetCreate();
    this.educationBook.LessonId = this.lessonId;
    this.educationBookStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id, title) {
    this.educationBookStore.resetEdit();
    this.educationBookStore.getById(id).then(() => {
      this.educationBookStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.educationBookStore.getById(id).then(() => {
      this.educationBookStore.OPEN_MODAL_DELETE(true);
    });
  }

  lessonIdChanged(val) {
    this.lessonId = val;
    this.educationBookStore.fillListByLessonId(val);
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>

