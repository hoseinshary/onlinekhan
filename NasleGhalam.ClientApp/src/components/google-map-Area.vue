
<template>
  <div>
    <!-- <button @click="printnewpoly">sss</button> -->
    <div v-bind:id="id" v-bind:style="{height:height+'px',width:width +'px'}">
    </div>
  </div>
</template>

<script>
import _dashboardService from "serviceLayers/DashboardService.js";
export default {
  components: {},
  props: {
    newPoint: null, //نقطه درون چند ضلعی جدید
    newPoly: null, //چند ضلعی جدید
    editArea: null, //شی چند ضلعی مورد ویرایش
    isEdit: false,
    id: {
      //HTML ID
      type: String,
      required: true
    },
    width: {
      type: [Number, String],
      default: 400
    },
    height: {
      type: [Number, String],
      default: 400
    },
    polyAllData: null
  },
  data() {
    return {
      myNewPoly: null, // شی چند ضلعی جدید
      map: null,
      colors: ["#64380beb", "#890c74eb", "#04ff00"]
    };
  },
  methods: {
    printnewpoly() {
      console.log("mypoly", this.mypoly.getPath());
    },
    addToParent() {
      this.newPoly.pop();
      this.newPoly.push(this.myNewPoly);
    },
    ininMap: function() {
      const element = document.getElementById(this.id);
      const options = {
        zoom: 13,
        center: { lat: 29.62671253540981, lng: 52.49610900878906 },
        disableDoubleClickZoom: true
      };

      this.map = new google.maps.Map(element, options);

      if (this.polyAllData.length != 0 && this.polyAllData[0].PolygonPath) {
        this.polyAllData.forEach(item => {
          if (this.isEdit && this.editArea && item.Id == this.editArea.Id)
            return;
          var poly = new google.maps.Polygon({
            paths: JSON.parse(item.PolygonPath),
            strokeColor: this.colors[0],
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: this.colors[0],
            fillOpacity: 0.3,
            map: this.map
          });
          poly.marker = new google.maps.Marker({
            position: JSON.parse(item.PointLatLng),
            map: this.map,
            icon: {
              url: "../assets/img/spotlight-poi2.png",
              scaledSize: new google.maps.Size(10, 20)
            }
          });
          var infoWin = new google.maps.InfoWindow({
            content: item.Name
          });

          google.maps.event.addListener(poly, "click", function() {
            if (infoWin.getZIndex() == 1) {
              infoWin.close();
              infoWin.setZIndex(0);
            } else {
              infoWin.open(this.map, poly.marker);
              infoWin.setZIndex(1);
            }
          });
        });
      }

      if (this.isEdit) {
        if (this.editArea.PolygonPath) {
          this.myNewPoly = new google.maps.Polygon({
            paths: JSON.parse(this.editArea.PolygonPath),
            strokeColor: this.colors[2],
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: this.colors[2],
            fillOpacity: 0.4,
            editable: true,
            map: this.map
          });
          this.newPoint.pop();
          this.newPoint.push(
            new google.maps.Marker({
              position: JSON.parse(this.editArea.PointLatLng),
              map: this.map
            })
          );
        }
        this.addToParent();
      } else {
        //if iscreate
        // کشیدن چندضلعی
        var polyOptions = {
          strokeColor: this.colors[1],
          strokeOpacity: 1.0,
          strokeWeight: 3,
          editable: true
        };
        this.myNewPoly = new google.maps.Polygon(polyOptions);
        this.myNewPoly.setMap(this.map);
        google.maps.event.addListener(this.map, "click", e => {
          this.myNewPoly.getPath().push(e.latLng);
          this.addToParent();
        });
      }
      google.maps.event.addListener(this.myNewPoly, "rightclick", e => {
        if (e.vertex == undefined) {
          if (this.newPoint.length != 0) this.newPoint[0].setMap(null);
          this.newPoint.pop();
        } else {
          this.myNewPoly.getPath().removeAt(e.vertex);
          this.addToParent();
        }
      });
      google.maps.event.addListener(this.myNewPoly, "click", e => {
        if (this.newPoint.length != 0) {
          this.newPoint[0].setMap(null);
          this.newPoint.pop();
        }
        this.newPoint.pop();
        this.newPoint.push(
          new google.maps.Marker({
            position: e.latLng,
            map: this.map
          })
        );
      });
    }
  },

  watch: {
    isEdit() {
      if (!this.isEdit) this.ininMap();
    },
    editArea() {
      this.ininMap();
    },
    polyAllData() {
      this.ininMap();
    }
  },
  computed: {},
  mounted() {
    this.ininMap();
  }
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
