<template>
  <q-field :count="count" :helper="helper" :error-label="errorLabel()">
    <q-input
      ref="input"
      class="q-input-border"
      :type="type"
      v-model="model.$model"
      :id="uid"
      :float-label="displayName"
      :error="model.$error"
      :max-height="maxHeight"
      :prefix="prefix"
      :suffix="suffix"
      :readonly="readonly"
      :clearable="clearable"
      :align="align"
      :disable="disable"
      :before="before"
      :after="after"
      :min="min"
      :max="max"
      :color="color"
      :inverted="inverted"
      :invertedLight="invertedLight"
    />

    <date-picker
      v-model="model.$model"
      :view="view"
      :format="format"
      :element="uid"
      :append-to="appendTo"
    />
  </q-field>
</template>

<script>
// https://talkhabi.github.io/vue-persian-datetime-picker
import { uid } from "quasar";

export default {
  /**
   * props
   */
  props: {
    model: {
      type: Object,
      required: true
    },
    type: String,
    helper: String,
    count: Boolean,
    appendTo: String,
    readonly: Boolean,
    clearable: Boolean,
    maxHeight: Number,
    prefix: String,
    suffix: String,
    align: String,
    disable: Boolean,
    before: Array,
    after: Array,
    min: String,
    max: String,
    color: String,
    inverted: Boolean,
    invertedLight: Boolean,
    ltr: Boolean,
    view: String,
    format: String
  },
  /**
   * data
   */
  data() {
    return {
      uid: "dateId1" + uid()
    };
  },
  methods: {
    /**
     *  Clear the model
     * */
    clear() {
      this.$refs.input.clear();
    },
    /**
     * showing password or not.
     */
    togglePass() {
      this.$refs.input.togglePass();
    },
    /**
     * Focused the textfield.
     */
    focus() {
      this.$refs.input.focus();
    },
    /**
     * Makes textfield lose focus.
     */
    blur() {
      this.$refs.input.blur();
    },
    /**
     * Selects all textfield text and focuses.
     */
    select() {
      this.$refs.input.select();
    },
    /**
     * get model error
     */
    errorLabel() {
      if (!this.model.$dirty) {
        return "";
      }
      if (this.model.required !== undefined && !this.model.required) {
        return `(${this.displayName}) خالی میباشد`;
      }
      return "";
    }
  },
  /**
   * computed
   */
  computed: {
    /**
     * get displayname of model
     */
    displayName() {
      if (this.model && this.model.$params && this.model.$params.displayName) {
        return this.model.$params.displayName.value;
      }
      return "";
    }
  }
};
</script>

<style>
.vpd-input-group input {
  width: 100%;
  border: none;
}
</style>

