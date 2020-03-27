<template>
  <section class="col-md-8">
    <base-btn-create
      v-if="canCreate"
      :label="`ایجاد (${assayStore.modelName}) جدید`"
      @click="showModalCreate"
    />
    <br />
    <base-table :grid-data="assayStore.gridData" :columns="assayGridColumn" hasIndex>
      <template slot="Id" slot-scope="data">
        
      </template>
    </base-table>

    <!-- modals -->
    
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    
  }
})
export default class AssayVue extends Vue {
  //#region ### data ###
  assayStore = vxm.assayStore;
  pageAccess = util.getAccess(this.assayStore.modelName);
  assayGridColumn = [
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "کد",
      data: "Code"
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
  //#endregion

  //#region ### methods ###
  
  //#endregion

  //#region ### hooks ###
  created() {
    //this.assayStore.fillList();
  }
  //#endregion
}
</script>