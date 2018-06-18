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
          <q-btn size="sm" round dense color="blue" icon="format align center" @click="loadSecondModalAreasDetail(props.row.RegionId,props.row.RegionName)" class="q-mr-xs">
            <q-tooltip>جزئیات</q-tooltip>
          </q-btn>
        </q-td>

      </q-table>
    </div>

    <my-modal v-bind:options="secondModalRegionOptions">
      <template slot="modal-body">
        <div class="row">
          <myAreaTable :mytableData="myAreaTableData">
          </myAreaTable>
        </div>
      </template>
    </my-modal>

  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import _dashboardService from "serviceLayers/DashboardService.js";
import myAreaTable from "views/dashboard/my-area-table.vue";

export default {
  props: ["mytableData"],
  data: () => ({
    myAreaTableData: [],
    secondModalRegionOptions: {
      title: "",
      width: "60vw",
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
        required: true,
        classes: "bg-primary"
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
    myvisibleColumns: ["Region", "Count", "RegionId"]
  }),
  components: { myModal, myAreaTable },
  methods: {
    loadSecondModalAreasDetail(RegionId, RegionName) {
      this.secondModalRegionOptions.title = "محدوده های " + RegionName;
      this.secondModalRegionOptions.showModal = true;
      _dashboardService.DetailOfAreaByRegionId(RegionId).then(response => {
        this.myAreaTableData = response.data;
      });
    }
  }
};
</script>

<style >

</style>
