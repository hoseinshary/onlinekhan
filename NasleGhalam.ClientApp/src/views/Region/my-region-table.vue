<template>
  <div class="tabl-md" style="margin: 0 auto;">
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

        <q-td slot="body-cell-Id" slot-scope="props" :props="props" style="padding-right:5px">
          <q-btn size="sm" round dense color="green" icon="map" @click="loadModalRegionAreas(props.row.Id,props.row.Name)" class="q-mr-xs">
            <q-tooltip>محدوده ها</q-tooltip>
          </q-btn>
          <q-btn size="sm" round dense color="blue" icon="edit" @click="loadModalEditRegion(props.row.Id,props.row.Name)" class="q-mr-xs">
            <q-tooltip>ویرایش</q-tooltip>
          </q-btn>
          <q-btn size="sm" round dense color="red" icon="delete_forever" @click="loadModalDeleteRegion(props.row.Id,props.row.Name)" class="q-mr-xs">
            <q-tooltip>حذف</q-tooltip>
          </q-btn>
        </q-td>

      </q-table>
    </div>
    <my-modal @hide="regionObj.Name = '';$v.$reset();" v-bind:options="modalEditRegionOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-8 offset-xs-2" :validator="$v.regionObj.Name">
            <q-input @keyup.enter="submitModalEditRegion" float-label="نام منطقه" type="text" v-model="regionObj.Name" @blur="$v.regionObj.Name.$touch" :error="$v.regionObj.Name.$error" />
          </form-group>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="blue" @click="submitModalEditRegion" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @hide="regionObj.Name = '';$v.$reset();" v-bind:options="modalDeleteRegionOptions">
      <template slot="modal-body">
        <div class="row">
          <div class="deleteAlert">
            آیا از حذف منطقه
            <span>{{regionObj.Name}}</span> مطمئن هستید؟

          </div>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="red" @click="submitModalDeleteRegion" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal v-bind:options="modalRegionAreasOptions">
      <template slot="modal-body">
        <div class="row">
          <myregionAreastable :regionId="modalRegionAreasOptions.regionId" :regionName="modalRegionAreasOptions.regionName">
            <!-- </myregionAreastable> :tblDataAreas="allAreas"> -->

          </myregionAreastable>
        </div>
      </template>

    </my-modal>
  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import myregionAreastable from "views/Region/my-regionAreas-table.vue";
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";
import { regionValidation } from "utilities/validationRules";

export default {
  props: ["mytableData"],
  validations: regionValidation,
  data: () => ({
    modalRegionAreasOptions: {
      title: "",
      width: "80vw",
      height: "80vh",
      showModal: false,
      color: "secondary",
      regionId: 0,
      regionName: ""
    },
    modalEditRegionOptions: {
      title: "",
      width: "50vw",
      height: "60vh",
      showModal: false,
      color: "blue"
    },
    modalDeleteRegionOptions: {
      title: "",
      width: "50vw",
      height: "40vh",
      showModal: false,
      color: "red"
    },
    mycolumns: [
      {
        name: "Name",
        label: "منطقه",
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
    myvisibleColumns: ["Name", "Id"]
  }),
  computed: {
    ...mapState({
      regionObj: state => state.region.regionObj,
      allAreas: state => state.area.allAreas
    })
  },
  methods: {
    ...mapActions("area", ["getAllAreaByRegionId_store"]),
    ...mapActions("region", [
      "submitModalEditRegion_store",
      "submitModalDeleteRegion_store",
      "getRegionById_store"
    ]),
    loadModalRegionAreas(RegionId, RegionName) {
      this.modalRegionAreasOptions.title =
        "محدوده مربوط به منطقه  " + RegionName;
      this.modalRegionAreasOptions.showModal = true;
      this.modalRegionAreasOptions.regionId = RegionId;
      this.modalRegionAreasOptions.regionName = RegionName;
      this.getAllAreaByRegionId_store(RegionId);
    },
    loadModalEditRegion(RegionId, RegionName) {
      this.getRegionById_store(RegionId);
      this.modalEditRegionOptions.title = "ویرایش منطقه " + RegionName;
      this.modalEditRegionOptions.showModal = true;
    },
    submitModalEditRegion() {
      this.$v.regionObj.$touch();
      if (this.$v.regionObj.$error)
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
      else this.submitModalEditRegion_store();
    },
    loadModalDeleteRegion(RegionId, RegionName) {
      this.getRegionById_store(RegionId);
      this.modalDeleteRegionOptions.title = "حذف منطقه " + RegionName;
      this.modalDeleteRegionOptions.showModal = true;
    },
    submitModalDeleteRegion() {
      this.submitModalDeleteRegion_store();
    }
  },
  created() {
    window.$eventHub.on("closeModal", action => {
      if (action == "edit") this.modalEditRegionOptions.showModal = false;
      if (action == "delete") this.modalDeleteRegionOptions.showModal = false;
    });
  },
  components: { myModal, myregionAreastable }
};
</script>

<style scoped>
</style>
