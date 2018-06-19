<template>
  <div style="margin: 0 auto;">
    <div class="col-12 tabl-md">
      <div class="tblCreateBtn">
        <div class="col-12">
          <span class="label">&nbsp;</span>
          <q-btn inverted-light color="secondary" icon="library_add" label="ایجاد" style="width:100%;" @click="loadModalCreateArea" />
        </div>
      </div>

      <q-table title="Table Title" :data="allAreas" :columns="mycolumns" row-key="name" :pagination.sync="paginationControl" :visible-columns="myvisibleColumns" :separator="separator" :filter="filter">
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

        <q-td slot="body-cell-Id" slot-scope="props" :props="props" style="padding-right:5px">

          <q-btn size="sm" round dense color="blue" icon="edit" @click="loadModalEditArea(props.row.Id,props.row.Name)" class="q-mr-xs">
            <q-tooltip>ویرایش</q-tooltip>
          </q-btn>
          <q-btn size="sm" round dense color="red" icon="delete_forever" @click="loadModalDeleteArea(props.row.Id,props.row.Name)" class="q-mr-xs">
            <q-tooltip>حذف</q-tooltip>
          </q-btn>
        </q-td>

      </q-table>
    </div>

    <my-modal @hide="$v.$reset();areaObj.Name = ''; newPoint=[]; newPoly=[]" v-bind:options="modalCreateAreaOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-8 offset-xs-2" :validator="$v.areaObj.Name">
            <q-input @keyup.enter="submitModalCreateArea" float-label="نام محدوده" type="text" v-model="areaObj.Name" @blur="$v.areaObj.Name.$touch" :error="$v.areaObj.Name.$error" />
          </form-group>
          <section class="mapArea">
            <google-map :polyAllData="allAreas" :editArea="null" :newPoly="newPoly" :newPoint="newPoint" :isEdit="isEdit" id="map" height=400 width=1000>
            </google-map>
          </section>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="green" @click="submitModalCreateArea" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>

    <my-modal @hide="$v.$reset();areaObj.Name = ''; newPoint=[]; newPoly=[]" v-bind:options="modalEditAreaOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-8 offset-xs-2" :validator="$v.areaObj.Name">
            <q-input @keyup.enter="submitModalEditArea" float-label="نام محدوده" type="text" v-model="areaObj.Name" @blur="$v.areaObj.Name.$touch" :error="$v.areaObj.Name.$error" />
          </form-group>
          <section class="mapArea">
            <google-map :editArea="areaObj" :newPoly="newPoly" :newPoint="newPoint" :isEdit="isEdit" id="mapEdit" height=400 width=1000 :polyAllData="allAreas">
            </google-map>
          </section>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="blue" @click="submitModalEditArea" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @hide="areaObj.Name = '';$v.$reset(); newPoint=[]; newPoly=[]" v-bind:options="modalDeleteAreaOptions">
      <template slot="modal-body">
        <div class="row">
          <div class="deleteAlert">
            آیا از حذف منطقه
            <span>{{areaObj.Name}}</span> مطمئن هستید؟

          </div>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="red" @click="submitModalDeleteArea" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import googleMap from "components/google-map-Area.vue";
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";
import { areaValidation } from "utilities/validationRules";

export default {
  props: ["regionId", "regionName"],
  validations: areaValidation,
  data: () => ({
    newPoly: [],
    newPoint: [],
    isEdit: false,
    modalCreateAreaOptions: {
      title: "",
      width: "90vw",
      height: "90vh",
      showModal: false,
      color: "green"
    },
    modalEditAreaOptions: {
      title: "",
      width: "90vw",
      height: "90vh",
      showModal: false,
      color: "blue"
    },
    modalDeleteAreaOptions: {
      title: "",
      width: "50vw",
      height: "40vh",
      showModal: false,
      color: "red"
    },
    mycolumns: [
      {
        name: "Name",
        label: "محدوده",
        field: "Name",
        align: "left",
        sortable: true,
        required: true
      },
      {
        name: "Id",
        label: "عملیات",
        field: "Id",
        align: "center",
        sortable: true
      }
    ],
    separator: "cell",
    paginationControl: { rowsPerPage: 10 },
    filter: "",
    myvisibleColumns: ["Area", "Id"]
  }),
  computed: {
    ...mapState({
      areaObj: state => state.area.areaObj,
      allAreas: state => state.area.allAreas
    })
  },
  methods: {
    ...mapActions("area", [
      "getAllAreaByRegionId_store",
      "submitModalCreateArea_store",
      "getAreaById_store",
      "submitModalEditArea_store",
      "submitModalDeleteArea_store"
    ]),
    loadModalCreateArea() {
      this.modalCreateAreaOptions.title =
        "ایجاد محدوده برای منطقه " + this.regionName;
      this.modalCreateAreaOptions.showModal = true;
      this.isEdit = false;
    },
    submitModalCreateArea() {
      this.$v.areaObj.Name.$touch();
      if (this.$v.areaObj.Name.$error) {
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
        return;
      }
      if (!this.newPoly[0] || this.newPoly[0].getPath().length <= 2) {
        this.$q.notify("لطفا روی نقشه یک چند ضلعی بسازید.");
        return;
      }
      if (this.newPoint.length != 1)
        this.$q.notify("لطفا درون چند ضلعی، یک نقطه بسازید.");
      else {
        this.submitModalCreateArea_store({
          poly: JSON.stringify(this.newPoly[0].getPath().getArray()),
          point: JSON.stringify(this.newPoint[0].getPosition()),
          regionId: this.regionId
        });
      }
    },
    loadModalEditArea(AreaId, AreaName) {
      this.getAreaById_store(AreaId);
      this.modalEditAreaOptions.title =
        " ویرایش محدوده " + AreaName + " مربوط به منطقه " + this.regionName;
      this.modalEditAreaOptions.showModal = true;
      this.isEdit = true;
    },
    submitModalEditArea() {
      this.$v.areaObj.Name.$touch();
      if (this.$v.areaObj.Name.$error) {
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
        return;
      }
      if (this.newPoly[0].getPath().length <= 2) {
        this.$q.notify("لطفا روی نقشه یک چند ضلعی بسازید.");
        return;
      }
      if (this.newPoint.length != 1)
        this.$q.notify("لطفا درون چند ضلعی، یک نقطه بسازید.");
      else {
        this.submitModalEditArea_store({
          poly: JSON.stringify(this.newPoly[0].getPath().getArray()),
          point: JSON.stringify(this.newPoint[0].getPosition()),
          regionId: this.regionId
        });
      }
    },
    loadModalDeleteArea(AreaId, AreaName) {
      this.getAreaById_store(AreaId);
      this.modalDeleteAreaOptions.title = "حذف محدوده " + AreaName;
      this.modalDeleteAreaOptions.showModal = true;
    },
    submitModalDeleteArea() {
      this.submitModalDeleteArea_store();
    }
  },
  created() {
    this.getAllAreaByRegionId_store(this.regionId);

    window.$eventHub.on("closeModal", action => {
      if (action == "create") this.modalCreateAreaOptions.showModal = false;
      if (action == "edit") this.modalEditAreaOptions.showModal = false;
      if (action == "delete") this.modalDeleteAreaOptions.showModal = false;
    });
  },
  components: { myModal, googleMap }
};
</script>

<style scoped>
.tabl-md .q-table-container {
  width: 60vw;
}
.tabl-md .layout-padding {
  padding-top: 0px;
}
.mapArea {
  margin: auto;
}
</style>
