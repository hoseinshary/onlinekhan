<template>
  <section class="col-md-8">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{resumeStore.modelName}}</span>
      <div slot="body">
        <base-table :grid-data="resumeStore.gridData" :columns="resumeGridColumn" hasIndex>
          <template slot="City.Name" slot-scope="data">{{data.row.City.Name}}</template>
          <template slot="Id" slot-scope="data">
            <q-btn
              outline
              dense
              color="primary"
              class="shadow-1 bg-white q-mr-sm"
              @click="showModalDetail(data.row.Id)"
            >جزییات</q-btn>
          </template>
        </base-table>
      </div>
    </base-panel>
    <!-- modals -->
    <modal-detail></modal-detail>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {
    ModalDetail: () => import("./detail.vue")
  }
})
export default class ResumeVue extends Vue {
  //#region ### data ###
  resumeStore = vxm.resumeStore;
  pageAccess = util.getAccess(this.resumeStore.modelName);
  resumeGridColumn = [
    // {
    //   title: "زمان ثبت",
    //   data: "PCreationDateTime"
    // },
    {
      title: "نام",
      data: "Name"
    },
    {
      title: "نام خانوادگی",
      data: "Family"
    },
    {
      title: "جنسیت",
      data: "GenderName"
    },
    {
      title: "موبایل",
      data: "Mobile"
    },
    {
      title: "شهر",
      data: "City.Name"
    },
    {
      title: "آخرین مدرک",
      data: "LastEducationCertificate"
    },
    {
      title: "جزییات",
      data: "Id",
      searchable: false,
      sortable: false
    }
  ];
  //#endregion

  //#region ### computed ###
  //#endregion

  //#region ### methods ###
  showModalDetail(id) {
    this.resumeStore.getById(id).then(() => {
      this.resumeStore.OPEN_MODAL_DETAIL(true);
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.resumeStore.fillList();
  }
  //#endregion
}
</script>