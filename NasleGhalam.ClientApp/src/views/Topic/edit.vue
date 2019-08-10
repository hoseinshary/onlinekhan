<template>
  <base-modal-edit
    :title="topicStore.modelName"
    :show="topicStore.openModal.edit"
    size="lg"
    @confirm="topicStore.submitEdit"
    @reset="topicStore.resetEdit"
    @close="topicStore.OPEN_MODAL_EDIT(false)"
    @open="open"
  >
    <base-input :model="$v.topic.Title" class="col-md-3 col-sm-6"/>
    <base-input :model="$v.topic.ExamStock" class="col-md-3 col-sm-6"/>
    <base-input :model="$v.topic.Importance" class="col-md-3 col-sm-6"/>
    <base-input :model="$v.topic.DisplayPriority" class="col-md-3 col-sm-6" />
    <base-field class="col-md-3 col-sm-6" :model="$v.topic.IsExamSource">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
    <base-select
      :model="$v.topic.LookupId_HardnessType"
      :options="lookupStore.topicHardnessTypeDdl"
      class="col-md-3 col-sm-6"
      clearable
    />
    <base-select
      :model="$v.topic.LookupId_AreaType"
      :options="lookupStore.topicAreaTypeDdl"
      class="col-md-3 col-sm-6"
      clearable
    />
    <base-field class="col-md-3 col-sm-6" :model="$v.topic.IsActive">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
        <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
      </template>
    </base-field>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { topicValidations } from "src/validations/TopicValidation";

@Component({
  validations: topicValidations
})
export default class TopicEditVue extends Vue {
  $v: any;

  //#region ### data ###
  topicStore = vxm.topicStore;
  lookupStore = vxm.lookupStore;
  topic = vxm.topicStore.topic;
  //#endregion

  //#region ### methods ###
  open() {
    this.lookupStore.fillTopicHardnessType();
    this.lookupStore.fillTopicAreaType();
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.topicStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

