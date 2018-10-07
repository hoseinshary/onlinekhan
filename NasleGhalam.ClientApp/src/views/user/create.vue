<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   size="lg"
                   @confirm="submit"
                   @reset="resetCreateStore"
                   @open="modalOpen"
                   @close="toggleModalCreateStore(false)">

    <my-select :model="$v.userObj.RoleId"
               :options="roleDdl"
               class="col-sm-6 col-md-4"
               clearable
               ref="roleId" />

    <my-select :model="$v.userObj.ProvinceId"
               :options="provinceDdl"
               class="col-sm-6 col-md-4"
               clearable
               @change="provinceChange" />

    <my-select :model="$v.userObj.CityId"
               :options="cityByProvinceDdl"
               class="col-sm-6 col-md-4"
               clearable
               ref="cityId" />

    <!-- <my-hr /> -->

    <my-input :model="$v.userObj.Name"
              class="col-sm-6 col-md-4" />

    <my-input :model="$v.userObj.Family"
              class="col-sm-6 col-md-4" />

    <my-field class="col-sm-6 col-md-4"
              :model="$v.userObj.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 :val="false"
                 label="دختر" />
        <q-radio v-model="data.obj.$model"
                 :val="true"
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
        <q-toggle v-model="data.obj.$model" />
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
    modalOpen() {
      this.fillRoleDdl();
      this.fillProvinceDdl();
    },
    provinceChange(value) {
      this.fillCityByProvincIdDdl(value);
    },
    submit(closeModal) {
      this.userObj.CityName = this.$refs.cityId.getSelectedLabel();
      this.userObj.RoleName = this.$refs.roleId.getSelectedLabel();
      this.submitCreateStore(closeModal);
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

