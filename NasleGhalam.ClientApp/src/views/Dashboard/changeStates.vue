<template>
  <div class="row">
    <div class="col-md-2 selectContiner">
      <div>
        <span class="label">منطقه</span>
        <q-select inverted-light color="secondary" v-model="changedRegion" :options="regionList" />
      </div>
    </div>
    <div class="col-md-2 selectContiner">
      <div>
        <span class="label">محدوده</span>
        <q-select inverted-light color="secondary" v-model="changedArea" :options="AreaList" />
      </div>
    </div>
    <div class="col-md-2 selectContiner">
      <div>
        <span class="label">تغییر</span>
        <q-select inverted-light color="secondary" v-model="changedState" :options="selectOptions" />
      </div>
    </div>
    <div class="col-md-2 selectContiner">
      <div class="col-12">
        <span class="label">&nbsp;</span>
        <q-btn inverted-light color="secondary" label="اعمال" style="width:100%;" @click="changeAreaState" />
      </div>
    </div>

  </div>
</template>

<script>
import { Notify } from "quasar";
import _dashboardService from "serviceLayers/DashboardService.js";
export default {
  data: () => ({
    select: null,
    changedRegion: 0,
    changedArea: 0,
    changedState: 0,
    regionList: [],
    AreaList: [],
    eastList: [],
    westList: null,
    centerList: null,
    southList: null,
    selectOptions: [
      {
        label: "بسیار کم فشار",
        value: "1"
      },
      {
        label: "کم فشار",
        value: "2"
      },
      {
        label: "معمولی",
        value: "3"
      },
      {
        label: "پر فشار",
        value: "4"
      },
      {
        label: "بسیار پر فشار",
        value: "5"
      },
      {
        label: "قطع ارتباط",
        value: "6"
      }
    ]
  }),
  methods: {
    changeAreaState() {
      if (
        this.changedRegion != 0 &&
        this.changedArea != 0 &&
        this.changedState != 0
      )
        _dashboardService
          .ChangeAreaState(this.changedArea, this.changedState)
          .then(response => {
            var flag = response.data == "ok";
            Notify.create({
              message: flag ? "باموفقیت انجام شد" : "مشکلی رخ داده است!",
              type: flag ? "positive" : "negative",
              position: "top"
            });
          });
      else
        Notify.create({
          message: "تمام موارد را انتخاب کنید",
          type: "negative",
          position: "top"
        });
    }
  },
  mounted: function() {
    _dashboardService.GetAllRegions().then(response => {
      this.regionList = response.data;
    });
  },
  watch: {
    changedRegion() {
      _dashboardService
        .GetAreasByRegionId(this.changedRegion)
        .then(response => {
          this.AreaList = response.data;
        });
    }
  }
};
</script>

<style>
.q-tabs-scroller.row {
  width: 100%;
}
.q-select.q-if-has-label {
  /* width: 150px; */
  margin: 5px;
}
.q-field-label-inner {
  color: black;
}
.selectContiner {
  background: #e5eaf0;
}
.selectContiner > div {
  margin: 15px;
}
</style>


