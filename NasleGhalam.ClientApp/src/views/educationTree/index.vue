<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{educationTreeStore.modelName}}</span>
      <div slot="body">
        <q-input v-model="educationTreeFilter" float-label="جستجو در درخت آموزش" clearable/>
        <q-tree
          :nodes="educationTreeStore.treeData"
          :expanded.sync="expanded"
          :filter="educationTreeFilter"
          class="col-md-12"
          color="blue"
          node-key="Id"
        >
          <div slot="header-custom" slot-scope="prop">
            {{prop.node.label}}
            <template>
              <q-btn
                v-if="canCreate"
                outline
                color="positive"
                class="shadow-1 bg-white q-mr-sm q-px-xs"
                icon="save"
                size="sm"
                @click.stop="showModalCreate(prop.node.Id, prop.node.label)"
              />
              <q-btn
                v-if="canEdit"
                outline
                color="purple"
                class="shadow-1 bg-white q-mr-sm q-px-xs"
                icon="create"
                size="sm"
                @click.stop="showModalEdit(prop.node.Id, prop.node.label)"
              />
              <q-btn
                v-if="canDelete && !(prop.node.children && prop.node.children.length)"
                outline
                color="red"
                class="shadow-1 bg-white q-mr-sm q-px-xs"
                icon="delete"
                size="sm"
                @click.stop="showModalDelete(prop.node.Id)"
              />
            </template>
          </div>
        </q-tree>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <modal-edit v-if="canEdit"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class EducationTreeVue extends Vue {
  //#region ### data ###
  educationTreeStore = vxm.educationTreeStore;
  educationTree = vxm.educationTreeStore.educationTree;
  pageAccess = util.getAccess(this.educationTreeStore.modelName);
  expanded: Array<Object> = [];
  educationTreeFilter = "";

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
  //#endregion

  //#region ### methods ###
  showModalCreate(id, name) {
    this.educationTreeStore.resetCreate();
    this.educationTree.ParentEducationTreeId = id;
    this.educationTree.ParentEducationTree = {
      Id: id,
      Name: name,
      LookupId_EducationTreeState: 0
    };
    this.educationTreeStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id, name) {
    this.educationTreeStore.resetEdit();
    this.educationTreeStore.getById(id).then(() => {
      this.educationTree.ParentEducationTree = {
        Id: id,
        Name: name,
        LookupId_EducationTreeState: 0
      };
      this.educationTreeStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.educationTreeStore.getById(id).then(() => {
      this.educationTreeStore.OPEN_MODAL_DELETE(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    var _this = this;
    this.educationTreeStore.fillList().then(function(res) {
      _this.expanded = _this.educationTreeStore.expandedTreeData;
    });
  }
  //#endregion
}
</script>
