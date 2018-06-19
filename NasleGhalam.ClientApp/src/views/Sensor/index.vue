<template>
  <div class="tabl-md">
    <div class="col-12">
      <div style="margin: auto">
        <div class="tblCreateBtn">
          <div v-if="accessControl.canWrite" class="col-12">
            <span class="label">&nbsp;</span>
            <q-btn inverted-light color="secondary" icon="library_add" label="ایجاد" style="width:100%;" @click="modalCreateOptions.showModal = true" />
          </div>
        </div>
        <q-table title="Table Title" :data="allObj" :columns="mycolumns" row-key="name" :pagination.sync="paginationControl" :visible-columns="myvisibleColumns" :separator="separator" :filter="filter">
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
            <q-btn v-if="accessControl.canEdit" size="sm" round dense color="blue" icon="edit" @click="loadModalEdit(props.row.Id,props.row.SensorGroupName +' محدوده '+props.row.AreaName)" class="q-mr-xs">
              <q-tooltip>ویرایش</q-tooltip>
            </q-btn>
            <q-btn v-if="accessControl.canDelete" size="sm" round dense color="red" icon="delete_forever" @click="loadModalDelete(props.row.Id,props.row.SensorGroupName +' محدوده '+props.row.AreaName)" class="q-mr-xs">
              <q-tooltip>حذف</q-tooltip>
            </q-btn>
          </q-td>

        </q-table>
      </div>
    </div>
    <my-modal @show.once="loadRegionAndSensorGroupList" @hide="reset" v-bind:options="modalCreateOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-6" :validator="$v.instanceObj.RegionId">
            <div class="selectContiner">
              <div>
                <span class="label">منطقه</span>
                <q-select v-model="instanceObj.RegionId" :options="regionList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.AreaId">
            <div class="selectContiner">
              <div>
                <span class="label">محدوده</span>
                <q-select v-model="instanceObj.AreaId" :display-value="instanceObj.AreaName" :options="areaList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.SensorGroupId">
            <div class="selectContiner">
              <div>
                <span class="label">گروه سنسور</span>
                <q-select v-model="instanceObj.SensorGroupId" :display-value="instanceObj.SensorGroupName" :options="sensorGroupList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>

          <form-group class="col-xs-6" :validator="$v.instanceObj.Code">
            <span class="label">کد سنسور</span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Code" @blur="$v.instanceObj.Code.$touch" :error="$v.instanceObj.Code.$error" />
          </form-group>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="green" @click="submitModalCreate" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @show.once="loadRegionAndSensorGroupList" @hide="reset" v-bind:options="modalEditOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-6" :validator="$v.instanceObj.RegionId">
            <div class="selectContiner">
              <div>
                <span class="label">منطقه</span>
                <q-select v-model="instanceObj.RegionId" :options="regionList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.AreaId">
            <div class="selectContiner">
              <div>
                <span class="label">محدوده</span>
                <q-select v-model="instanceObj.AreaId" :options="areaList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.SensorGroupId">
            <div class="selectContiner">
              <div>
                <span class="label">گروه سنسور</span>
                <q-select v-model="instanceObj.SensorGroupId" :options="sensorGroupList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>

          <form-group class="col-xs-6" :validator="$v.instanceObj.Code">
            <span class="label">کد سنسور</span>
            <q-input @keyup.enter="submitModalEdit" type="text" v-model="instanceObj.Code" @blur="$v.instanceObj.Code.$touch" :error="$v.instanceObj.Code.$error" />
          </form-group>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="blue" @click="submitModalEdit" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @show.once="loadRegionAndSensorGroupList" @hide="reset" v-bind:options="modalDeleteOptions">
      <template slot="modal-body">
        <div class="row">
          <div class="deleteAlert">
            آیا از حذف سنسور
            <span>{{instanceObj.Name}}</span> مطمئن هستید؟

          </div>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="red" @click="submitModalDelete_store" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>

  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";
import { sensorValidation } from "utilities/validationRules";

export default {
  validations: sensorValidation,
  data: () => ({
    accessControl: {},
    modalCreateOptions: {
      title: "ایجاد سنسور جدید",
      width: "80vw",
      height: "55vh",
      showModal: false,
      color: "green"
    },
    modalEditOptions: {
      title: "",
      width: "80vw",
      height: "55vh",
      showModal: false,
      color: "blue"
    },
    modalDeleteOptions: {
      title: "",
      width: "50vw",
      height: "40vh",
      showModal: false,
      color: "red"
    },
    mycolumns: [
      {
        name: "RegionName",
        label: "منطقه",
        field: "RegionName",
        align: "left",
        sortable: true
      },
      {
        name: "AreaName",
        label: "محدوده",
        field: "AreaName",
        align: "left",
        sortable: true
      },
      {
        name: "SensorGroupName",
        label: "گروه",
        field: "SensorGroupName",
        align: "left",
        sortable: true
      },
      {
        name: "Code",
        label: "کد",
        field: "Code",
        align: "center",
        sortable: true
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
    myvisibleColumns: [
      "RegionName",
      "AreaName",
      "SensorGroupName",
      "Code",
      "Id"
    ]
  }),
  computed: {
    ...mapState({
      regionList: state => state.sensor.regionList,
      areaList: state => state.sensor.areaList,
      sensorGroupList: state => state.sensor.sensorGroupList,
      allObj: state => state.sensor.allObj,
      instanceObj: state => state.sensor.instanceObj
    })
  },
  methods: {
    test() {
      debugger;
    },
    setDdlLabel(){
      this.instanceObj.RegionName = this.regionList.filter(x => x.value == this.instanceObj.RegionId).map(x => x.label)[0];
      this.instanceObj.AreaName = this.areaList.filter(x => x.value == this.instanceObj.AreaId).map(x => x.label)[0];
      this.instanceObj.SensorGroupName = this.sensorGroupList.filter(x => x.value == this.instanceObj.SensorGroupId).map(x => x.label)[0];
    },
    reset() {
      this.reset_store();
      this.$v.$reset();
    },
    ...mapActions("sensor", [
      "reset_store",
      "getAll_store",
      "getAreaByRegionId_store",
      "getAllRegion_store",
      "getAllSensorGroup_store",
      "submitModalCreate_store",
      "getById_store",
      "submitModalEdit_store",
      "submitModalDelete_store"
    ]),
    loadRegionAndSensorGroupList() {
      if (this.regionList.length == 0) this.getAllRegion_store();
      if (this.sensorGroupList.length == 0) this.getAllSensorGroup_store();
    },
    submitModalCreate() {
      this.$v.instanceObj.$touch();
      if (this.$v.instanceObj.$error)
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
      else {this.setDdlLabel();this.submitModalCreate_store();}
    },
    loadModalEdit(Id, Title) {
      this.getById_store(Id);
      this.modalEditOptions.title = "ویرایش " + Title;
      this.modalEditOptions.showModal = true;
    },
    submitModalEdit() {
      this.$v.instanceObj.$touch();
      if (this.$v.instanceObj.$error)
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
      else {this.setDdlLabel();this.submitModalEdit_store();}
    },
    loadModalDelete(Id, Title) {
      this.getById_store(Id);
      this.modalDeleteOptions.title = "حذف " + Title;
      this.modalDeleteOptions.showModal = true;
    }
  },
  created() {
    var myAccessControl = this.$q.localStorage.get
      .item("menuList")
      .filter(x => x.EnName.toLowerCase() == "/sensor")
      .map(x => x.UserAccess);
    this.accessControl.canDelete = myAccessControl[0].includes("حذف");
    this.accessControl.canEdit = myAccessControl[0].includes("ویرایش");
    this.accessControl.canWrite = myAccessControl[0].includes("ایجاد");

    this.reset();
    this.getAll_store();
    window.$eventHub.on("closeModal", action => {
      if (action == "create") this.modalCreateOptions.showModal = false;
      if (action == "edit") this.modalEditOptions.showModal = false;
      if (action == "delete") this.modalDeleteOptions.showModal = false;
    });
  },
  components: { myModal },
  watch: {
    "instanceObj.RegionId": function(val, oldVal) {
      if (oldVal != undefined && val != 0) this.getAreaByRegionId_store();
    }
  }
};
</script>

<style scoped>
.tblCreateBtn {
  margin-left: 150px;
}
</style>
