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
            <q-btn v-if="accessControl.roleAccess" size="sm" round dense color="blue" icon="account_box" @click="loadModalAccessList(props.row.Id,' نقش '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>دسترسی</q-tooltip>
            </q-btn>
            <q-btn v-if="accessControl.canEdit" size="sm" round dense color="blue" icon="edit" @click="loadModalEdit(props.row.Id,' نقش '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>ویرایش</q-tooltip>
            </q-btn>
            <q-btn v-if="accessControl.canDelete" size="sm" round dense color="red" icon="delete_forever" @click="loadModalDelete(props.row.Id,' نقش '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>حذف</q-tooltip>
            </q-btn>
          </q-td>

        </q-table>
      </div>
    </div>
    <my-modal @hide="reset" v-bind:options="modalCreateOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-6" :validator="$v.instanceObj.Name">
            <span class="label">نام نقش</span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Name" @blur="$v.instanceObj.Name.$touch" :error="$v.instanceObj.Name.$error" />
          </form-group>

          <form-group class="col-xs-6" :validator="$v.instanceObj.SensorGroupId">
            <div class="selectContiner">
              <div>
                <span class="label">رتبه</span>
                <q-select v-model="instanceObj.Level" :options="LevelList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>

        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="green" @click="submitModalCreate" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @hide="reset" v-bind:options="modalEditOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-6" :validator="$v.instanceObj.Name">
            <span class="label">نام نقش</span>
            <q-input @keyup.enter="submitModalEdit" type="text" v-model="instanceObj.Name" @blur="$v.instanceObj.Name.$touch" :error="$v.instanceObj.Name.$error" />
          </form-group>

          <form-group class="col-xs-6" :validator="$v.instanceObj.SensorGroupId">
            <div class="selectContiner">
              <div>
                <span class="label">رتبه</span>
                <q-select v-model="instanceObj.Level" :options="LevelList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="blue" @click="submitModalEdit" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @hide="reset" v-bind:options="modalDeleteOptions">
      <template slot="modal-body">
        <div class="row">
          <div class="deleteAlert">
            آیا از حذف نقش
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
    <my-modal @show="loadControllerList" v-bind:options="modalAccessOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-12">
            <div class="selectContiner">
              <div>
                <span class="label">منو</span>
                <q-select v-model="accessObj.controllerId" :options="controllerList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
              <accessListTbl :allAccessObj="actionList">

              </accessListTbl>
            </div>
          </form-group>
        </div>
      </template>
    </my-modal>

  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";
import { roleValidation } from "utilities/validationRules";
import accessListTbl from "views/Role/accessListTable";

export default {
  validations: roleValidation,
  data: () => ({
    accessControl: {},
    LevelList: [
      { value: 1, label: "1" },
      { value: 2, label: "2" },
      { value: 3, label: "3" },
      { value: 4, label: "4" },
      { value: 5, label: "5" },
      { value: 6, label: "6" },
      { value: 7, label: "7" },
      { value: 8, label: "8" },
      { value: 9, label: "9" }
    ],
    modalCreateOptions: {
      title: "ایجاد نقش جدید",
      width: "80vw",
      height: "45vh",
      showModal: false,
      color: "green"
    },

    modalEditOptions: {
      title: "",
      width: "80vw",
      height: "45vh",
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
    modalAccessOptions: {
      title: "",
      width: "70vw",
      height: "80vh",
      showModal: false,
      color: "brown"
    },
    mycolumns: [
      {
        name: "Name",
        label: "نقش",
        field: "Name",
        align: "left",
        sortable: true
      },
      {
        name: "Level",
        label: "رتبه",
        field: "Level",
        align: "left",
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
    myvisibleColumns: ["Name", "Level", "Id"]
  }),
  computed: {
    ...mapState({
      controllerList: state => state.role.controllerList,
      actionList: state => state.role.actionList,
      allObj: state => state.role.allObj,
      instanceObj: state => state.role.instanceObj,
      accessObj: state => state.role.accessObj
    })
  },
  methods: {
    test(val) {
      console.log(val);
      debugger;
    },
    reset() {
      this.reset_store();
      this.$v.$reset();
    },
    ...mapActions("role", [
      "reset_store",
      "getAll_store",
      "getAllController_store",
      "getActionByControllerId_store",
      "submitModalCreate_store",
      "getById_store",
      "submitModalEdit_store",
      "submitModalDelete_store"
    ]),
    loadControllerList() {
      this.getAllController_store();
      this.accessObj.controllerId = 0;
      this.getActionByControllerId_store();
    },

    submitModalCreate() {
      this.$v.instanceObj.$touch();
      if (this.$v.instanceObj.$error)
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
      else this.submitModalCreate_store();
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
      else this.submitModalEdit_store();
    },
    loadModalDelete(Id, Title) {
      this.getById_store(Id);
      this.modalDeleteOptions.title = "حذف " + Title;
      this.modalDeleteOptions.showModal = true;
    },
    loadModalAccessList(Id, Title) {
      this.instanceObj.Id = Id;
      this.modalAccessOptions.title = "لیست دسترسی برای " + Title;
      this.modalAccessOptions.showModal = true;
    }
  },
  created() {
    // var myAccessControl = this.$q.localStorage.get
    //   .item("menuList")
    //   .filter(x => x.EnName.toLowerCase() == "/role")
    //   .map(x => x.UserAccess);
    this.accessControl.canDelete = true;// myAccessControl[0].includes("حذف");
    this.accessControl.canEdit = true;//myAccessControl[0].includes("ویرایش");
    this.accessControl.canWrite = true;//myAccessControl[0].includes("ایجاد");
    this.accessControl.roleAccess = true;//myAccessControl[0].includes("دسترسی");

    this.reset();
    this.getAll_store();
    window.$eventHub.on("closeModal", action => {
      if (action == "create") this.modalCreateOptions.showModal = false;
      if (action == "edit") this.modalEditOptions.showModal = false;
      if (action == "delete") this.modalDeleteOptions.showModal = false;
    });
  },
  components: { myModal, accessListTbl },
  watch: {
    "accessObj.controllerId": function(val, oldVal) {
      if (oldVal != undefined && val != 0) this.getActionByControllerId_store();
    }
  }
};
</script>

<style scoped>
.tblCreateBtn {
  margin-left: 150px;
}
</style>
