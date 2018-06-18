<template>
  <div class="row">
    <!-- <router-link to="/Dashboard/Map">
      <q-item>
        <q-item-side icon="multiline chart" />
        <q-item-main label="داشبورد" sublabel="" />
      </q-item>
    </router-link> -->
    <div style="margin: 30px auto">
      <div class="tblCreateBtn">
        <div class="col-12">
          <span class="label">&nbsp;</span>
          <q-btn inverted-light color="secondary" icon="library_add" label="ایجاد" style="width:100%;" @click="modalCreateRegionOptions.showModal = true" />
        </div>
      </div>
      <myRegionTable :mytableData="allRegions">
      </myRegionTable>
    </div>

    <my-modal @hide="$v.$reset();regionObj.Name = '';" v-bind:options="modalCreateRegionOptions">
      <template slot="modal-body">
        <div class="row">
          <form-group class="col-xs-8 offset-xs-2" :validator="$v.regionObj.Name">
            <q-input @keyup.enter="submitModalCreateRegion" float-label="نام منطقه" type="text" v-model="regionObj.Name" @blur="$v.regionObj.Name.$touch" :error="$v.regionObj.Name.$error" />
          </form-group>
        </div>
      </template>
      <template slot="modal-btns">
        <q-btn color="green" @click="submitModalCreateRegion" label="تایید">
          <q-icon name="check" />
        </q-btn>
      </template>
    </my-modal>
  </div>
</template>

<script>
import myModal from "components/my-modal.vue";
import myRegionTable from "views/Region/my-region-table.vue";
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";
import { regionValidation } from "utilities/validationRules";

export default {
  data: () => ({
    modalCreateRegionOptions: {
      title: "ایجاد منطقه",
      width: "50vw",
      height: "50vh",
      showModal: false,
      color: "green"
    }
  }),
  validations: regionValidation,
  components: {
    myModal,
    myRegionTable
  },
  computed: {
    ...mapState({
      allRegions: state => state.region.allRegions,
      regionObj: state => state.region.regionObj
    })
  },
  methods: {
    ...mapActions("region", [
      "getAllRegions_store",
      "submitModalCreateRegion_store"
    ]),
    // ...mapMutations(["getAllRegions_store"]),
    asss: () => {
      debugger;
      this.regionObj.Name = "";
    },
    submitModalCreateRegion() {
      // this.$v.form.$touch();
      this.$v.regionObj.Name.$touch();
      if (this.$v.regionObj.Name.$error)
        this.$q.notify("تمام مقادیر را بصورت صحیح وارد نمایید.");
      else this.submitModalCreateRegion_store();
    }
  },
  created() {
    this.getAllRegions_store();
    window.$eventHub.on("closeModal", action => {
      if (action == "create") {
        this.modalCreateRegionOptions.showModal = false;
        console.log(this.$v);
      }
    });
  },
  destroyed() {},
  mounted: function() {},
  destroyed: function() {}
};
</script>

<style>
</style>
