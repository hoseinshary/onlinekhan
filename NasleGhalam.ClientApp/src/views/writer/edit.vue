<template>
  <base-modal-edit
    :title="writerStore.modelName"
    :show="writerStore.openModal.edit"
    @confirm="writerStore.submitEdit"
    @reset="writerStore.resetEdit"
    @close="writerStore.OPEN_MODAL_EDIT(false)"
  >
    <base-input :model="$v.writer.Name" class="col-md-6"/>
    <base-field :model="$v.writer.UserId" class="col-md-6">
      <q-input v-model="searchTerm" placeholder="کدملی-نام خانوادگی-نام">
        <q-autocomplete
          @search="search"
          @selected="selected"
          :min-characters="2"
          :max-results="10"
        />
      </q-input>
    </base-field>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { writerValidations } from "src/validations/WriterValidation";

@Component({
  validations: writerValidations
})
export default class WriterEditVue extends Vue {
  $v: any;

  //#region ### data ###
  writerStore = vxm.writerStore;
  userStore = vxm.userStore;
  writer = vxm.writerStore.writer;
  searchTerm = "";
  //#endregion

  //#region ### methods ###
  search(terms, done) {
    var data = terms.split("-");
    var nationalNo = "";
    var family = "";
    var name = "";

    if (data.length == 3) {
      nationalNo = data[0];
      family = data[1];
      name = data[2];
    } else if (data.length == 2) {
      nationalNo = data[0];
      name = data[2];
    }
    if (data.length == 1) {
      nationalNo = data[0];
    }

    this.userStore.search({ nationalNo, family, name }).then(() => {
      done(this.userStore.searchDdl);
    });
  }

  selected(term) {
    this.searchTerm = term.label;
    this.writer.Name = term.label;
    this.writer.UserId = term.value;
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.writerStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>

