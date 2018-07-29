<template>
  <q-field count
           :helper="helper"
           :error-label="errorLabel()">
    <q-select ref="input"
              v-model="value"
              :options="options"
              :float-label="displayName"
              :multiple="multiple"
              :radio="radio"
              :toggle="toggle"
              :chips="chips"
              :filter="filter"
              :readonly="readonly"
              :clearable="clearable"
              autofocus-filter
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
              @blur="$emit('blur')" />
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
     * get model error
     */
    errorLabel() {
      if (!this.model.$dirty) {
        return '';
      }

      if (this.model.required !== undefined && !this.model.required) {
        return `(${this.displayName}) خالی میباشد`;
      } else if (
        this.model.requiredDdl !== undefined &&
        !this.model.requiredDdl
      ) {
        return `(${this.displayName}) انتخاب نشده است`;
      }
      return '';
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
      return '';
    },
    value: {
      get() {
        var value = this.model.$model;
        var item = this.options.find(o => o.value == value);
        if (item) this.$emit('change', item);
        return value;
      },
      set(newVal) {
        this.model.$model = newVal;
      }
    }
  }
};
</script>

<style>
</style>
