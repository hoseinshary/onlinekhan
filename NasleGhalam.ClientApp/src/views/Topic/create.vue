<template>
  <base-modal-create
    :title="topicStore.modelName"
    :show="topicStore.openModal.create"
    @confirm="topicStore.submitCreate"
    @reset="topicStore.resetCreate"
    @close="topicStore.OPEN_MODAL_CREATE(false)"
    @open="open"
  >
    <base-input :model="$v.topic.Title" class="col-md-6"/>
    <base-input :model="$v.topic.ExamStock" class="col-md-6"/>
    <base-input :model="$v.topic.Importance" class="col-md-6"/>
    <base-field class="col-md-6" :model="$v.topic.IsExamSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-select
      :model="$v.topic.LookupId_HardnessType"
      :options="lookupTopicHardnessTypeDdl"
      class="col-md-6"
      clearable
    />
    <base-select
      :model="$v.topic.LookupId_AreaType"
      :options="lookupTopicAreaTypeDdl"
      class="col-md-6"
      clearable
    />
    <base-field class="col-md-6" :model="$v.topic.IsActive">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { topicValidations } from "src/validations/TopicValidation";

@Component({
  validations: topicValidations
})
export default class TopicCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  topicStore = vxm.topicStore;
  topic = vxm.topicStore.topic;
  //#endregion

  //#region ### hooks ###
  created() {
    this.topicStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
// import viewModel from "viewModels/topic/topicViewModel";
// import { mapState, mapActions } from "vuex";

// export default {
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("topicStore", [
//       "toggleModalCreateStore",
//       "createVueStore",
//       "submitCreateStore",
//       "resetCreateStore"
//     ]),
//     ...mapActions("lookupStore", [
//       "fillTopicHardnessTypeDdlStore",
//       "fillTopicAreaTypeDdlStore"
//     ]),
//     modalOpen: function() {
//       this.fillTopicHardnessTypeDdlStore();
//       this.fillTopicAreaTypeDdlStore();
//     }
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("topicStore", {
//       modelName: "modelName",
//       topic: "topic",
//       isOpenModalCreate: "isOpenModalCreate"
//     }),
//     ...mapState("lookupStore", [
//       "lookupTopicHardnessTypeDdl",
//       "lookupTopicAreaTypeDdl"
//     ])
//   },
//   /**
//    * validations
//    */
//   validations: viewModel,
//   /**
//    * created
//    */
//   created() {
//     this.createVueStore(this);
//   }
// };
</script>

