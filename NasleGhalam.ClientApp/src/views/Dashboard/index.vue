<template>
  <div class="row">

    <div class="col-12">
      <div class="row gutter-xs">
        <my-box @detailsEvent="loadModalSensorsDetail()" cssClass="col-sm-4" title="کل هشدارها" variant="box-blue1" v-bind:counter="DashboardData.CountOfTotalWarning"></my-box>
        <my-box @detailsEvent="loadModalAreasDetail()" cssClass="col-sm-4" title="نواحی دارای هشدار" variant="box-yellow1" v-bind:counter="DashboardData.CountOfAreaByWarning"></my-box>
        <my-box @detailsEvent="loadModalReginsDetail()" cssClass="col-sm-4" title="مناطق دارای هشدار" variant="box-red1" v-bind:counter="DashboardData.CountOfRegionByWarning"></my-box>
      </div>
    </div>

    <my-modal :options="modalSensorOptions">
      <template slot="modal-body">
        <div class="row">
          <mySensorTable :mytableData="mySensorTableData">
          </mySensorTable>
        </div>
      </template>
    </my-modal>

    <my-modal v-bind:options="modalAreaOptions">
      <template slot="modal-body">
        <div class="row">
          <myAreTable :mytableData="myAreaTableData">
          </myAreTable>
        </div>
      </template>
    </my-modal>

    <my-modal v-bind:options="modalRegionOptions">
      <template slot="modal-body">
        <div class="row">
          <myRegionTable :mytableData="myRegionTableData">
          </myRegionTable>
        </div>
      </template>
    </my-modal>

  </div>
</template>

<script>
import myBox from "components/my-box.vue";
import myModal from "components/my-modal.vue";
import mySensorTable from "views/dashboard/my-sensor-table.vue";
import myAreTable from "views/dashboard/my-area-table.vue";
import myRegionTable from "views/dashboard/my-region-table.vue";
import _dashboardService from "serviceLayers/DashboardService.js";

export default {
  data: () => ({
    timerList: [],
    mySensorTableData: [],
    myAreaTableData: [],
    myRegionTableData: [],
    DashboardData: {
      CountOfTotalWarning: 0,
      CountOfAreaByWarning: 0,
      CountOfRegionByWarning: 0
    },
    modalSensorOptions: {
      title: "کل هشدارها",
      width: "90vw",
      height: "90vh",
      showModal: false
    },
    modalAreaOptions: {
      title: "محدوده های دارای هشدار",
      width: "60vw",
      height: "90vh",
      showModal: false
    },
    modalRegionOptions: {
      title: "مناطق دارای هشدار",
      width: "55vw",
      height: "90vh",
      showModal: false
    }
  }),
  components: {
    myBox,
    myModal,
    mySensorTable,
    myAreTable,
    myRegionTable
  },
  methods: {
    loadModalSensorsDetail() {
      this.modalSensorOptions.showModal = true;
      _dashboardService.getsensorwarningsModal().then(response => {
        this.mySensorTableData = response.data;
      });
    },
    loadModalAreasDetail() {
      this.modalAreaOptions.showModal = true;
      _dashboardService.DetailOfAreaByWarning().then(response => {
        this.myAreaTableData = response.data;
      });
    },
    loadModalReginsDetail() {
      this.modalRegionOptions.showModal = true;
      _dashboardService.DetailOfregionByWarning().then(response => {
        this.myRegionTableData = response.data;
      });
    },
    fillBoxes: function() {
      _dashboardService.fillBoxes().then(response => {
        this.DashboardData = response.data;
      });
    }
  }, ///////ttrt
  mounted: function() {
    this.fillBoxes();
    this.timerList.push(window.setInterval(this.fillBoxes, 10 * 1000));
  },
  destroyed: function() {
    this.timerList.forEach(element => {
      clearInterval(element);
    });
  }
};
</script>

<style>
</style>


