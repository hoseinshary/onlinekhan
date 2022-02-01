<template>
  <q-field :count="count" :helper="helper" :error-label="errorLabel()">
    <!-- v-model="model.$model" -->
    <q-select
      ref="input"
      class="s-q-input-border"
      :value="model.$model"
      @change="val => { model.$model = val; $emit('change', val); }"
      :options="options"
      :float-label="displayName"
      :multiple="multiple"
      :radio="radio"
      :toggle="toggle"
      :chips="chips"
      :filter="filter"
      :readonly="readonly"
      :clearable="clearable"
      :autofocus-filter="true"
      filter-placeholder="جستجو"
      :error="model.$error"
      :max-height="maxHeight"
      :prefix="prefix"
      :suffix="suffix"
      :align="align"
      :disable="disable"
      :before="before"
      :after="after"
      @input="$emit('input', $event)"
      @clear="$emit('clear', $event)"
      @focus="$emit('focus')"
      @blur="$emit('blur')"
    />
  </q-field>
</template>

<script>
export default {
  /**
   * props
   */
  props: {
    model: {
      type: Object,
      required: true
    },
    options: {
      type: Array,
      required: true
    },
    count: Boolean,
    helper: String,
    multiple: Boolean,
    radio: Boolean,
    toggle: Boolean,
    chips: Boolean,
    filter: Boolean,
    readonly: Boolean,
    clearable: Boolean,
    maxHeight: Number,
    prefix: String,
    suffix: String,
    align: String,
    disable: Boolean,
    before: Array,
    after: Array
  },
  methods: {
    /**
     * Opens the Filter Popover
     */
    show() {
      this.$refs.input.show();
    },
    /**
     * Closes the Filter Popover
     */
    hide() {
      this.$refs.input.hide();
    },
    /**
     * Resets the model to an empty string.
     */
    clear() {
      this.$refs.input.clear();
    },
    /**
     * get selected item
     */
    getSelectedItem() {
      return this.getSelected;
    },
    /**
     * get selected label
     */
    getSelectedLabel() {
      var item = this.getSelected;
      return item ? item.label : "";
    },
    /**
     * get selected value
     */
    getSelectedValue() {
      return this.model.$model;
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
      } else if (
        this.model.requiredDdl !== undefined &&
        !this.model.requiredDdl
      ) {
        return `(${this.displayName}) انتخاب نشده است`;
      }
      return "";
    }
  },
  /**
   * computed
   */
  computed: {
    displayName() {
      if (this.model && this.model.$params && this.model.$params.displayName) {
        return this.model.$params.displayName.value;
      }
      return "";
    },
    getSelected() {
      return this.options.find(o => o.value == this.model.$model);
    }
  }
};
</script>

<style>
</style>
