<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 size="lg"
                 @confirm="submit"
                 @reset="resetEditStore"
                 @open="modalOpen"
                 @close="toggleModalEditStore(false)">

    <my-select :model="$v.studentObj.User.RoleId"
               :options="roleDdl"
               class="col-sm-6 col-md-4"
               clearable
               ref="roleId" />

    <my-select :model="$v.studentObj.User.ProvinceId"
               :options="provinceDdl"
               class="col-sm-6 col-md-4"
               clearable
               @change="provinceChange" />

    <my-select :model="$v.studentObj.User.CityId"
               :options="cityByProvinceDdl"
               class="col-sm-6 col-md-4"
               clearable
               ref="cityId" />
    <!-- <my-hr/> -->

    <my-input :model="$v.studentObj.User.Name"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.studentObj.User.Family"
              class="col-sm-6 col-md-4" />

    <my-field class="col-sm-6 col-md-4"
              :model="$v.studentObj.User.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="دختر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
                 label="پسر" />
      </template>
    </my-field>

    <my-input :model="$v.studentObj.User.NationalNo"
              align="right"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.studentObj.User.Username"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.studentObj.User.Password"
              type="password"
              class="col-sm-6 col-md-4" />

    <my-field class="col-sm-6 col-md-4"
              :model="$v.studentObj.User.IsActive">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model" />
      </template>
    </my-field>

    <my-input :model="$v.studentObj.User.Phone"
              align="right"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.studentObj.User.Mobile"
              align="right"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.studentObj.FatherName"
              class="col-md-6" />

    <my-input :model="$v.studentObj.Address"
              class="col-md-6" />

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/student/studentEditViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('studentStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ]),
    ...mapActions({
      fillRoleDdl: 'roleStore/fillDdlStore',
      fillProvinceDdl: 'provinceStore/fillDdlStore',
      fillCityByProvincIdDdl: 'cityStore/fillCityByProvinceIdDdlStore'
    }),
    modalOpen() {
      this.fillRoleDdl();
      this.fillProvinceDdl();
    },
    provinceChange(value) {
      this.fillCityByProvincIdDdl(value);
    },
    submit() {
      this.studentObj.User.CityName = this.$refs.cityId.getSelectedLabel();
      this.studentObj.User.RoleName = this.$refs.roleId.getSelectedLabel();
      this.submitEditStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('studentStore', {
      modelName: 'modelName',
      studentObj: 'studentObj',
      isOpenModalEdit: 'isOpenModalEdit'
    }),
    ...mapState({
      roleDdl: s => s.roleStore.roleDdl,
      provinceDdl: s => s.provinceStore.provinceDdl,
      cityByProvinceDdl: s => s.cityStore.cityByProvinceDdl
    })
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.editVueStore(this);
  }
};
</script>

