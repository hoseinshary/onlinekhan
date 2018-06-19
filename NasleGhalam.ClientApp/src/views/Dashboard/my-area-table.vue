<template>
  <div style="margin: 0 auto;">
    <div class="col-12">

      <q-table title="Table Title" :data="mytableData" :columns="mycolumns" row-key="name" :pagination.sync="paginationControl" :visible-columns="myvisibleColumns" :separator="separator" :filter="filter">
        <template slot="top-left" slot-scope="props">
          <q-search color="secondary" v-model="filter" class="col-12" />
        </template>

        <template slot="top-right" slot-scope="props">
          <q-table-columns color="secondary" class="q-mr-sm" v-model="myvisibleColumns" :columns="mycolumns" />
          <q-select color="secondary" v-model="separator" :options="[
            { label: 'افقی', value: 'horizontal' },
            { label: 'عمودی', value: 'vertical' },
            { label: 'سلول', value: 'cell' },
            { label: 'هیچکدام', value: 'none' }
            ]" hide-underline />
          <q-btn flat round dense :icon="props.inFullscreen ? 'fullscreen_exit' : 'fullscreen'" @click="props.toggleFullscreen" />
        </template>

        <q-td slot="body-cell-RegionId" slot-scope="props" :props="props">
          <q-btn size="sm" round dense color="blue" icon="format align center" @click="loadSecondModalAreasDetail(props.row.RegionId,props.row.AreaId,props.row.RegionName,props.row.AreaName)" class="q-mr-xs">
            <q-tooltip>جزئیات</q-tooltip>
          </q-btn>
        </q-td>

      </q-table>
    </div>

    <my-modal v-bind:options="secondModalAreaOptions">

      <template slot="modal-body">
        <div class="row">
          <mySensorTable :mytableData="mySensorTableDate">
          </mySensorTable>
        </div>
      </template>
    </my-modal>

  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import _dashboardService from "serviceLayers/DashboardService.js";
import mySensorTable from "views/dashboard/my-sensor-table.vue";

export default {
  props: ["mytableData"],
  data: () => ({
    mySensorTableDate: [],
    tmpRegionId: 0,
    tmpAreaId: 0,
    secondModalAreaOptions: {
      title: "کل هشدارها",
      width: "85vw",
      height: "85vh",
      showModal: false
    },
    mycolumns: [
      {
        name: "RegionName",
        label: "منطقه",
        field: "RegionName",
        align: "left",
        sortable: true,
        required: true
      },
      {
        name: "AreaName",
        label: "محدوده",
        field: "AreaName",
        align: "left",
        sortable: true
      },
      {
        name: "Count",
        label: "تعداد",
        field: "Count",
        align: "center",
        sortable: true
      },
      {
        name: "RegionId",
        label: "جزئیات",
        field: "RegionId",
        align: "center",
        sortable: true
      }
    ],
    separator: "cell",
    paginationControl: { rowsPerPage: 10 },
    filter: "",
    myvisibleColumns: ["RegionName", "AreaName", "Count", "RegionId"]
  }),
  components: { myModal, mySensorTable },
  methods: {
    loadSecondModalAreasDetail(RegionId, AreaId, RegionName, AreaName) {
      this.tmpRegionId = RegionId;
      this.tmpAreaId = AreaId;
      this.secondModalAreaOptions.title =
        "کل هشدارهای " + RegionName + " - " + AreaName;
      this.secondModalAreaOptions.showModal = true;
      _dashboardService.GetSensorWarningsByAreaId(AreaId).then(response => {
        this.mySensorTableDate = response.data;
      });
    }
  }
};
</script>

<style >
</style>
