
<template>
  <div v-bind:id="id" v-bind:style="{height:height+'px'}">
  </div>
</template>

<script>
import _dashboardService from "serviceLayers/DashboardService.js";
export default {
  components: {},
  props: {
    id: {
      type: String,
      required: true
    },
    height: {
      type: [Number, String],
      default: 400
    },
    polyAllData: null,
    polyStateData: null,
    polySensorValue: null,
    selectedTreeNode: null
  },
  data() {
    return {
      mapsArray: [],
      map: null,
      content: "",
      colors: ["#FF594E", "#ffc711", "#00ff21", "#93CBFD", "#0056b5", "#404040"]
    };
  },
  methods: {
    changeRandomColors: function() {
      var letters = "0123456789ABCDEF";
      var color = "#";
      for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
      }
      this.mapsArray[0].setOptions({
        fillColor: color
      });
    },
    createInfoWinContent: function(areaName, sensor) {
      var tmp =
        '<div style="width:120px;">' +
        '<div style="color: red;padding-right: 25px;">' +
        areaName +
        "</div>";
      // sensors.forEach(sensor => {
      tmp =
        tmp +
        '<div style="padding-right: 20px;padding-top: 10px;">' +
        "<div>" +
        "فشار سنج" +
        "</div>" +
        "<div><span> کم فشار :</span><span style='float:left'>" +
        sensor.RuleLow +
        "</span></div>" +
        "<div><span>  پر فشار :</span><span style='float:left'>" +
        sensor.RuleHigh +
        "</span></div>" +
        "<div><span>  مقدار :</span><span style='float:left'>" +
        sensor.CurrentValue +
        "</span></div>" +
        "<div><span>  تعداد هشدار :</span><span style='float:left'>" +
        sensor.CountOfWarning +
        "</span></div></div>";
      // });

      tmp += "</div>";
      return tmp;
    }
  },
  mounted: function() {},
  watch: {
    selectedTreeNode() {
      debugger;
      this.mapsArray.forEach(poly => {
        debugger;
        if (poly.areaName == this.selectedTreeNode) {
          if (poly.infoWin.getZIndex() == 1) {
            poly.infoWin.close();
            poly.infoWin.setZIndex(0);
          } else {
            poly.infoWin.open(this.map, poly.marker);
            poly.infoWin.setZIndex(1);
            // this.map.setZoom(14);
            debugger;
            this.map.setCenter(poly.marker.getPosition());
          }
        }
      });
    },
    polyAllData() {
      var myMaps = [];
      const element = document.getElementById(this.id);
      const options = {
        zoom: 13,
        center: { lat: 29.62671253540981, lng: 52.49610900878906 }
      };

      this.map = new google.maps.Map(element, options);

      this.polyAllData.forEach(item => {
        myMaps.CurrentValue = item.CurrentValue;
        var mapObj = new google.maps.Polygon({
          paths: JSON.parse(item.PolygonPath),
          strokeColor: this.colors[item.CurrentState - 1],
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: this.colors[item.CurrentState - 1],
          fillOpacity: 0.3,
          map: this.map,
          areaId: item.Id,
          areaName: item.Name,
          currentState: item.CurrentState
        });
        mapObj.marker = new google.maps.Marker({
          areaId: item.Id,
          position: JSON.parse(item.PointLatLng),
          map: this.map,
          icon: {
            url: "../assets/img/spotlight-poi2.png",
            scaledSize: new google.maps.Size(10, 20)
          }
        });
        myMaps.push(mapObj);
      });
      _dashboardService.GetAllSensorDatas().then(response => {
        var sensorsData = response.data;
        myMaps.forEach(poly => {
          sensorsData.forEach(sensorDate => {
            if (poly.areaId == sensorDate.AreaId) {
              // debugger
              poly.infoWin = new google.maps.InfoWindow({
                areaId: poly.areaId,
                sensorId: sensorDate.SensorId,
                CurrentValue: sensorDate.CurrentValue,
                ruleLow: sensorDate.RuleLow,
                ruleHigh: sensorDate.RuleHigh,
                areaName: poly.areaName,
                content: this.createInfoWinContent(poly.areaName, sensorDate)
              });
              var list = [poly, poly.marker];
              list.forEach(item => {
                google.maps.event.addListener(item, "click", function() {
                  if (poly.infoWin.getZIndex() == 1) {
                    poly.infoWin.close();
                    poly.infoWin.setZIndex(0);
                  } else {
                    poly.infoWin.open(this.map, poly.marker);
                    poly.infoWin.setZIndex(1);
                  }
                });
              });
            }
          });
        });
      });
      this.mapsArray = myMaps;
      // window.setInterval(this.changeRandomColors, 2 * 1000);
    },
    polyStateData() {
      this.polyStateData.forEach(plySt => {
        this.mapsArray.forEach(item => {
          if (
            plySt.AreaId == item.areaId &&
            plySt.CurrentState != item.currentState
          ) {
            item.setOptions({
              strokeColor: this.colors[plySt.CurrentState - 1],
              fillColor: this.colors[plySt.CurrentState - 1]
            });
            item.currentState = plySt.CurrentState;
          }
        });
      });
    },
    polySensorValue() {
      this.polySensorValue.forEach(plyVal => {
        this.mapsArray.forEach(item => {
          var elem = item.infoWin;
          if (
            plyVal.SensorId == elem.sensorId &&
            plyVal.CurrentValue != elem.CurrentValue
          ) {
            item.infoWin.setOptions({
              content: this.createInfoWinContent(item.areaName, {
                RuleLow: elem.ruleLow,
                RuleHigh: elem.ruleHigh,
                CurrentValue: plyVal.CurrentValue,
                CountOfWarning: plyVal.CountOfWarning
              })
            });
            elem.CurrentValue = plyVal.CurrentValue;
          }
        });
      });
    }
  },
  computed: {}
};
</script>

<style>
.gm-style-iw {
  font-family: Vazir, Tahoma !important;
}
.gm-style-iw > div:nth-child(1) > div > div > div:nth-child(2) > div {
  border-bottom: solid;
  border-width: thin;
}
</style>
