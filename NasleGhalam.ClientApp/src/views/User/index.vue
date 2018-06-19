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
          <q-td slot="body-cell-IsActive" slot-scope="props" :props="props" style="padding-right:5px">
            <q-icon v-if="props.row.IsActive == false" style="font-size: 25px" color="red" name="check_box_outline_blank" class="q-mr-xs">
              <q-tooltip>غیر فعال</q-tooltip>
            </q-icon>
            <q-icon v-if="props.row.IsActive == true" style="font-size: 25px" color="green" name="check_box" class="q-mr-xs">
              <q-tooltip>فعال</q-tooltip>
            </q-icon>
          </q-td>
          <q-td slot="body-cell-Id" slot-scope="props" :props="props" style="padding-right:5px">
            <q-btn v-if="accessControl.canEdit" size="sm" round dense color="blue" icon="edit" @click="loadModalEdit(props.row.Id,' کاربر '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>ویرایش</q-tooltip>
            </q-btn>
            <q-btn v-if="accessControl.canDelete" size="sm" round dense color="red" icon="delete_forever" @click="loadModalDelete(props.row.Id,' کاربر '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>حذف</q-tooltip>
            </q-btn>
          </q-td>

        </q-table>
      </div>
    </div>
    <my-modal @show.once="loadRoleList" @hide="reset" v-bind:options="modalCreateOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-6" :validator="$v.instanceObj.Name">
            <span class="label">نام</span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Name" @blur="$v.instanceObj.Name.$touch" :error="$v.instanceObj.Name.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.Family">
            <span class="label">نام خانوادگی</span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Family" @blur="$v.instanceObj.Family.$touch" :error="$v.instanceObj.Family.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.Username">
            <span class="label">نام کاربری </span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Username" @blur="$v.instanceObj.Username.$touch" :error="$v.instanceObj.Username.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.Password">
            <span class="label">رمز عبور </span>
            <q-input @keyup.enter="submitModalCreate" type="password" autocomplete="new-password" v-model="instanceObj.Password" @blur="$v.instanceObj.Password.$touch" :error="$v.instanceObj.Password.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.RoleId">
            <div class="selectContiner">
              <div>
                <span class="label">نقش</span>
                <q-select v-model="instanceObj.RoleId" :options="roleList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
          <form-group class="col-xs-6">
            <span class="label">فعال </span>
            <q-toggle v-model="instanceObj.IsActive" />
          </form-group>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="green" @click="submitModalCreate" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <my-modal @show.once="loadRoleList" @hide="reset" v-bind:options="modalEditOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-6" :validator="$v.instanceObj.Name">
            <span class="label">نام</span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Name" @blur="$v.instanceObj.Name.$touch" :error="$v.instanceObj.Name.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.Family">
            <span class="label">نام خانوادگی</span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Family" @blur="$v.instanceObj.Family.$touch" :error="$v.instanceObj.Family.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.Username">
            <span class="label">نام کاربری </span>
            <q-input @keyup.enter="submitModalCreate" type="text" v-model="instanceObj.Username" @blur="$v.instanceObj.Username.$touch" :error="$v.instanceObj.Username.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.Password">
            <span class="label">رمز عبور </span>
            <q-input @keyup.enter="submitModalCreate" type="password" autocomplete="new-password" v-model="instanceObj.Password" @blur="$v.instanceObj.Password.$touch" :error="$v.instanceObj.Password.$error" />
          </form-group>
          <form-group class="col-xs-6" :validator="$v.instanceObj.RoleId">
            <div class="selectContiner">
              <div>
                <span class="label">نقش</span>
                <q-select v-model="instanceObj.RoleId" :options="roleList" inverted-light style="background:#f3f3f3 !important;" />
              </div>
            </div>
          </form-group>
          <form-group class="col-xs-6">
            <span class="label">فعال </span>
            <q-toggle v-model="instanceObj.IsActive" />
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
            آیا از حذف کاربر
            <span>{{instanceObj.Name +" " + instanceObj.Family}}</span> مطمئن هستید؟
          </div>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="red" @click="submitModalDelete_store" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
    <!-- -->

  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";
import { userValidation } from "utilities/validationRules";

export default {
  validations: userValidation,
  data: () => ({
    accessControl: {},
    modalCreateOptions: {
      title: "ایجاد کاربر جدید",
      width: "80vw",
      height: "70vh",
      showModal: false,
      color: "green"
    },
    modalEditOptions: {
      title: "",
      width: "80vw",
      height: "70vh",
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
        name: "Name",
        label: "نام",
        field: "Name",
        align: "left",
        sortable: true
      },
      {
        name: "Family",
        label: "نام خانوادگی",
        field: "Family",
        align: "left",
        sortable: true
      },
      {
        name: "Username",
        label: "نام کاربری",
        field: "Username",
        align: "left",
        sortable: true
      },
      {
        name: "RoleName",
        label: "نقش",
        field: "RoleName",
        align: "left",
        sortable: true
      },
      {
        name: "IsActive",
        label: "فعال",
        field: "IsActive",
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
    filter2: "",
    myvisibleColumns: [
      "Name",
      "Family",
      "Username",
      "RoleName",
      "IsActive",
      "Id"
    ]
  }),
  computed: {
    ...mapState({
      roleList: state => state.user.roleList,
      allObj: state => state.user.allObj,
      instanceObj: state => state.user.instanceObj
    })
  },
  methods: {
    test() {
      debugger;
    },
    reset() {
      this.reset_store();
      this.$v.$reset();
    },
    setDdlLabel() {
      this.instanceObj.RoleName = this.roleList
        .filter(x => x.value == this.instanceObj.RoleId)
        .map(x => x.label)[0];
    },
    ...mapActions("user", [
      "reset_store",
      "getAll_store",
      "getAllRole_store",
      "submitModalCreate_store",
      "getById_store",
      "submitModalEdit_store",
      "submitModalDelete_store"
    ]),
    loadRoleList() {
      if (this.roleList.length == 0) this.getAllRole_store();
    },
    submitModalCreate() {
      this.$v.instanceObj.$touch();
      if (this.$v.instanceObj.$error)
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
      else {
        this.setDdlLabel();
        this.submitModalCreate_store();
      }
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
      else {
        this.setDdlLabel();
        this.submitModalEdit_store();
      }
    },
    loadModalDelete(Id, Title) {
      this.getById_store(Id);
      this.modalDeleteOptions.title = "حذف " + Title;
      this.modalDeleteOptions.showModal = true;
    }
  },
  created() {
    // var myAccessControl = this.$q.localStorage.get
    //   .item("menuList")
    //   .filter(x => x.EnName.toLowerCase() == "/user")
    //   .map(x => x.UserAccess);
    this.accessControl.canDelete = true;//myAccessControl[0].includes("حذف");
    this.accessControl.canEdit =true;// myAccessControl[0].includes("ویرایش");
    this.accessControl.canWrite = true;//myAccessControl[0].includes("ایجاد");

    this.reset();
    this.getAll_store();
    setTimeout(1000, function() {
      this.filter = "";
    });
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
