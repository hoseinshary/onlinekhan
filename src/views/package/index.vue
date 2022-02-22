<template>
  <section class="col-12 q-px-sm">
    <section class="row">
      <div class="col-md-4 col-lg-3">
        <q-select
          v-model="educationTree.id"
          :options="educationTree_GradeDdl"
          float-label="جستجو درخت آموزش با مقطع"
          clearable
          class="s-q-input-border s-spacing"
        />

        <q-tree
          :nodes="educationTreeData"
          :expanded.sync="educationTree.expanded"
          :ticked.sync="educationTree.leafTicked"
          tick-strategy="leaf"
          class="tree-max-height s-spacing s-border"
          color="blue"
          node-key="Id"
        />
      </div>
      <div class="col-md-8 col-lg-9">
        <section class="s-border s-spacing row">
          <base-btn-create
            v-if="canCreate"
            v-show="educationTree.leafTicked.length"
            :label="`ایجاد (${packageStore.modelName}) جدید`"
            @click="showModalCreate"
          />
          <br />
          <div class="col-12">
            <base-table :grid-data="packageStore.gridData" :columns="packageGridColumn" hasIndex>
              <template slot="IsActive" slot-scope="data">
                <q-checkbox v-model="data.row.IsActive" readonly />
              </template>
              <template slot="Id" slot-scope="data">
                <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
                <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </section>

    <!-- modals -->
    <modal-create v-if="canCreate" :educationTreeLeafTickedProp="educationTree.leafTicked"></modal-create>
    <modal-edit v-if="canEdit" :educationTreeLeafTickedProp="educationTree.leafTicked"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import utilities from "src/utilities";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class PackageVue extends Vue {
  //#region ### data ###
  packageStore = vxm.packageStore;
  thePackage = vxm.packageStore.thePackage;
  pageAccess = util.getAccess(this.packageStore.modelName);
  educationTreeStore = vxm.educationTreeStore;
  educationTree = this.educationTreeStore.qTreeData;
  // educationTree.leafTicked
  packageGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "قیمت",
      data: "Price"
    },
    {
      title: "مدت به روز",
      data: "TimeDays"
    },
    {
      title: "تاریخ ایجاد",
      data: "PCreateDateTime"
    },
    {
      title: "فعال",
      data: "IsActive"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete
    }
  ];
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
    utilities.clearArray(this.thePackage.LessonIds);
    // clear educationTree leaf
    utilities.clearArray(this.educationTree.leafTicked);

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
    utilities.clearArray(this.thePackage.LessonIds);
    this.packageStore.fillByEducationTreeIdList(newVal);
  }
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.packageStore.resetCreate();
    this.packageStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.packageStore.resetEdit();
    this.packageStore.getById(id).then(() => {
      this.packageStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.packageStore.getById(id).then(() => {
      this.packageStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationTreeStore.fillList();
  }
  //#endregion
}
</script>