<template>
  <div class="row">
    <!-- <router-link to="/Region">
      <q-item>
        <q-item-side icon="terrain" />
        <q-item-main label="منطقه" sublabel="" />
      </q-item>
    </router-link> -->
    <div class="col-12">
      <div class="row gutter-xs">
        <div class="col-sm-3 col-lg-2">
          <section id="sideTree">
            <div class="text-center" style="    background: #c1c1c16b;">
              مناطق
            </div>
            <q-tree :selected.sync="selectedTreeNode" default-expand-all :nodes="treeData" :expanded.sync="propsExpanded" node-key="label" />
          </section>

          <section id="helpArea">
            <div class="text-center" style="    background: #c1c1c16b;">
              راهنما
            </div>
            <q-tree default-expand-all :nodes="colorHelp" node-key="label" />
          </section>
        </div>
        <div class="col-sm-9 col-lg-10">
          <section id="mapArea">
            <google-map id="map" height=595 :polyAllData="allAreas" :selectedTreeNode="selectedTreeNode" :polyStateData="allAreasState" :polySensorValue="allSensorsValue">

            </google-map>
          </section>
          <section style="margin-top:10px;">
            <div class="text-center" style="    background: #c1c1c16b;">
              داشبورد
            </div>
            <div id="boxArea">

              <myDashboardBoxes></myDashboardBoxes>
            </div>
          </section>
        </div>
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

            <q-td slot="body-cell-RegionName" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.RegionName}}
            </q-td>
            <q-td slot="body-cell-AreaName" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.AreaName}}
            </q-td>
            <q-td slot="body-cell-DateTime" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.DateTime}}
            </q-td>

            <q-td slot="body-cell-Value" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.Value}}
            </q-td>
            <q-td slot="body-cell-SensorGroupName" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.SensorGroupName}}
            </q-td>
            <q-td slot="body-cell-RuleLow" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.RuleLow}}
            </q-td>
            <q-td slot="body-cell-RuleHigh" slot-scope="props" :props="props" style="font-weight: bolder;" :style="{'background-color': props.row.State==1?'#FF594E':props.row.State==2?'#ffc711':props.row.State==3?'#00ff21':props.row.State==4?'#93CBFD':props.row.State==5?'#5290D5':'' }">
              {{props.row.RuleHigh}}
            </q-td>

          </q-table>

        </div>
      </div>
    </div>

  </div>
</template>

<script>
import googleMap from "components/google-map.vue";
import myDashboardBoxes from "views/dashboard/index.vue";
import _dashboardService from "serviceLayers/DashboardService.js";
import eventhub from "eventhub";

export default {
  data: () => ({
    selectedTreeNode: null,
    propsExpanded: ["شیراز"],
    subPolyColor: "",
    allAreas: null,
    allAreasState: null,
    allSensorsValue: null,
    regionList: null,
    colorHelp: [
      {
        label: "",
        children: [
          { label: "بسیار کم فشار", img: "assets/img/1.png" },
          { label: "کم فشار", img: "assets/img/2.png" },
          { label: "معمولی", img: "assets/img/3.png" },
          { label: "پر فشار", img: "assets/img/4.png" },
          { label: "بسیار پر فشار", img: "assets/img/5.png" },
          { label: "قطع ارتباط", img: "assets/img/6.png" }
        ]
      }
    ],
    treeData: [],
    timerList: [],
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
        // format(value, row) {
        //   return new Date(value).toLocaleString();
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

    mytableData: [],
    myvisibleColumns: [
      "RegionName",
      "AreaName",
      "DateTime",
      "Value",
      "SensorGroupName",
      "RuleLow",
      "RuleHigh"
    ]
  }),
  created: function() {
    this.fillDataTable();
    _dashboardService.GetAllRegions().then(response => {
      this.regionList = response.data;
      this.timerList.push(window.setInterval(this.fillDataTable, 10 * 1000));
      _dashboardService.GetAllAreas().then(response => {
        this.allAreas = response.data;
        this.list_to_tree();
        this.timerList.push(
          window.setInterval(this.getAllAreaStates, 10 * 1000)
        );
        this.timerList.push(
          window.setInterval(this.getAllSensorValues, 10 * 1000)
        );
      });
    });
  },
  methods: {
    list_to_tree: function() {
      var myArealist = this.allAreas;
      this.treeData = [
        {
          label: "شیراز",
          children: [],
          expanded: true
        }
      ];
      this.regionList.forEach(region => {
        var currentReg = {
          label: "منطقه " + region.label,
          children: [],
          expanded: true
        };
        myArealist.forEach(area => {
          if (area.RegionId == region.value) {
            currentReg.children.push({
              areaId: area.Id,
              label: area.Name,
              img: "assets/img/" + area.CurrentState + ".png",
              currentState: area.CurrentState
            });
          }
        });
        this.treeData[0].children.push(currentReg);
      });
    },
    getAllAreaStates: function() {
      _dashboardService.GetAllAreaStates().then(response => {
        this.allAreasState = response.data;
        this.allAreasState.forEach(cAreaData => {
          this.treeData.forEach(element => {
            element.children.forEach(item => {
              item.children.forEach(area => {
                if (
                  area.areaId == cAreaData.AreaId &&
                  area.CurrentState != cAreaData.CurrentState
                ) {
                  area.img = "assets/img/" + cAreaData.CurrentState + ".png";
                  area.CurrentState = cAreaData.CurrentState;
                }
              });
            });
          });
        });
      });
    },
    getAllSensorValues: function() {
      // debugger;
      _dashboardService.GetAllSensorValues().then(response => {
        this.allSensorsValue = response.data;
      });
    },
    fillDataTable: function() {
      _dashboardService.getsensorwarningsDashboard().then(response => {
        this.mytableData = response.data;
        // if (response.data.length != 0) {
        //   var diffrence = parseInt(
        //     (new Date() - new Date(this.mytableData[0].DateTime)) / 1000
        //   );
        //   if (diffrence < 180)
        //     this.changeColor(
        //       this.mytableData[0].AreaId,
        //       this.mytableData[0].State
        //     );
        //   else this.changeColor(null, null);
        // } else this.changeColor(null, null);
      });
    },
    changeTreeColor(AreaId, State) {
      State = State || 3;
      this.treeData.forEach(element => {
        element.children.forEach(item => {
          item.children.forEach(area => {
            if (AreaId == null) {
              area.img = "assets/img/3.png";
            } else if (area.areaId == AreaId)
              area.img = "assets/img/" + State + ".png";
          });
        });
      });
    }
  },
  destroyed: function() {
    this.timerList.forEach(element => {
      clearInterval(element);
    });
  },
  // watch: {
  //   selected() {
  //     debugger;
  //     console.log(this.selected);
  //   }
  // },
  components: { myDashboardBoxes, googleMap }
};
</script>

<style>
.q-layout-page-container section {
  border: 1px solid;
  border-color: #c9dcec;
  border-radius: 4px;
}
@media only screen and (min-width: 576px) {
  #sideTree {
    height: 600px;
    overflow: auto;
  }
}
@media only screen and (max-width: 576px) {
  #sideTree {
    max-height: 350px;
    overflow: auto;
  }
}
#mapArea {
  min-height: 600px;
}
#boxArea {
  padding-left: 9px;
  padding-top: 3px;
  min-height: 145px;
}

#helpArea {
  margin-top: 10px;
  height: 163px;
}
.q-table-container {
  background: white;
}
.q-icon.q-tree-arrow {
  font-size: medium !important;
}
</style>

<style>
.q-tree-node-header img {
  max-width: 10px;
  max-height: 10px;
  margin: 2px;
}
.q-tree-node-header {
  padding: 0;
}
</style>


