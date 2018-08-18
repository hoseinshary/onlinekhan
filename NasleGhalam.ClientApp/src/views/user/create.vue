<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   size="xl"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.userObj.RoleId"
               :options="roleDdl"
               class="col-sm-6 col-md-4"
               clearable
               @change="setRoleName" />

    <my-select :model="$v.userObj.ProvinceId"
               :options="provinceDdl"
               class="col-sm-6 col-md-4"
               clearable
               @change="provinceChange" />

    <my-select :model="$v.userObj.CityId"
               :options="cityByProvinceDdl"
               class="col-sm-6 col-md-4"
               clearable
               @change="setCityName" />

    <my-hr/>

    <my-input :model="$v.userObj.Name"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.userObj.Family"
              class="col-sm-6 col-md-4" />

    <my-field class="col-sm-6 col-md-4"
              :model="$v.userObj.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="دختر" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="پسر" />
      </template>
    </my-field>

    <my-input :model="$v.userObj.NationalNo"
              align="right"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.userObj.Username"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.userObj.Password"
              type="password"
              class="col-sm-6 col-md-4" />

    <my-field class="col-sm-6 col-md-4"
              :model="$v.userObj.IsActive">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="فعال" />
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="غیر فعال" />
      </template>
    </my-field>

    <my-input :model="$v.userObj.Phone"
              align="right"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.userObj.Mobile"
              align="right"
              class="col-sm-6 col-md-4" />

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/user/userCreateViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('userStore', [
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore'
    ]),
    ...mapActions({
      fillRoleDdl: 'roleStore/fillDdlStore',
      fillProvinceDdl: 'provinceStore/fillDdlStore',
      fillCityByProvincIdDdl: 'cityStore/fillCityByProvinceIdDdlStore'
    }),
    setRoleName(item) {
      this.userObj.RoleName = item.label;
    },
    setCityName(item) {
      this.userObj.CityName = item.label;
    },
    modalOpen() {
      this.fillRoleDdl();
      this.fillProvinceDdl();
    },
    provinceChange(item) {
      if (item) {
        this.fillCityByProvincIdDdl(item.value);
      }
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState('userStore', {
      modelName: 'modelName',
      userObj: 'userObj',
      isOpenModalCreate: 'isOpenModalCreate'
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
    this.createVueStore(this);
  }
};
</script>

