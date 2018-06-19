

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

                <!--  <q-td slot="body-cell-Value" slot-scope="props" :props="props">
               <q-btn size="sm" round dense color="blue" icon="format align center"
        @click="loadEditModal(props.row.Value)" class="q-mr-xs" ><q-tooltip>ویرایش</q-tooltip></q-btn>

        <q-btn size="sm" round dense color="red" icon="delete"
        @click="loadDeleteModal(props.row.Value)" class="q-mr-xs" ><q-tooltip>حذف</q-tooltip></q-btn>
 
            </q-td>-->

            </q-table>
        </div>
    </div>
</template>

<script>
import _dashboardService from "serviceLayers/DashboardService.js";

export default {
  props: ["mytableData"],
  data: () => ({
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
        name: "DateTime",
        label: "زمان",
        field: "DateTime",
        align: "left",
        sortable: true
        //     format (value, row) {
        //   return new Date(value).toLocaleString()
        // }
      },
      {
        name: "Value",
        label: "مقدار",
        field: "Value",
        align: "left",
        sortable: true
      },
      {
        name: "SensorGroupName",
        label: "گروه سنسور",
        field: "SensorGroupName",
        align: "left",
        sortable: true
      },
      {
        name: "RuleLow",
        label: "کران پایین",
        field: "RuleLow",
        align: "left",
        sortable: true
      },
      {
        name: "RuleHigh",
        label: "کران بالا",
        field: "RuleHigh",
        align: "left",
        sortable: true
      }
    ],
    separator: "cell",
    paginationControl: { rowsPerPage: 10 },
    filter: "",
    myvisibleColumns: [
      "RegionName",
      "AreaName",
      "DateTime",
      "Value",
      "SensorGroupName",
      "RuleLow",
      "RuleHigh"
    ]
  })
};
</script>

<style >

</style>


