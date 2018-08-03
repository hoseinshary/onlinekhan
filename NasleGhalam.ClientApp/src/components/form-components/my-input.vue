<template>
  <q-field count
           :helper="helper"
           :error-label="errorLabel()">
    <q-input ref="input"
             :type="type"
             v-model="model.$model"
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
             :maxlength="inputMaxLen"
             :rows="rows"
             :min="min"
             :max="max"
             :step="step"
             :autocomplete="autocomplete"
             @input="$emit('input', model)"
             @change="$emit('change', model)"
             @clear="$emit('clear', model)"
             @focus="$emit('focus')"
             @blur="$emit('blur')"
             @keydown="keydown"
             @keyup="$emit('keyup')"
             @click="$emit('click')" />
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
    type: {
      type: String,
      default: 'text'
    },
    helper: String,
    readonly: Boolean,
    clearable: Boolean,
    maxHeight: Number,
    prefix: String,
    suffix: String,
    align: String,
    disable: Boolean,
    before: Array,
    after: Array,
    maxlength: String,
    rows: String,
    min: String,
    max: String,
    step: String,
    autocomplete: String
  },
  /**
   * data
   */
  data() {
    return {
      inputMaxLen: Infinity
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
        return '';
      }

      if (this.isOnlyPersianChar) {
        return 'فقط از حروف فارسی استفاده شود';
      } else if (this.isNotPersianChar) {
        return 'کیبورد را انگلیسی نمایید';
      } else if (this.isNumeric) {
        return 'فقط عدد وارد شود';
      } else if (this.model.required !== undefined && !this.model.required) {
        return `(${this.displayName}) خالی میباشد`;
      } else if (this.model.minLength !== undefined && !this.model.minLength) {
        return `حداقل تعداد کاراکتر وارد شده برای (${this.displayName}) باید (${
          this.minLen
        }) باشد`;
      } else if (this.model.maxLength !== undefined && !this.model.maxLength) {
        return `تعداد کاراکتر وارد شده برای (${this.displayName}) بیش از (${
          this.maxLen
        }) می باشد`;
      }
      return '';
    },
    keydown(e) {
      this.$emit('keydown');

      if (this.isOnlyPersianChar || this.isNotPersianChar || this.isNumeric) {
        if (
          [46, 8, 9, 27, 13, 190, 32].includes(e.keyCode) || // {46:delete, 8:backspace, 9:tab, 27:escape, 13:enter, 110:decimal point, 190:period, 32:space}
          // Allow: Ctrl/cmd+A
          (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
          // Allow: Ctrl/cmd+C
          (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
          // Allow: Ctrl/cmd+X
          (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
          // Allow: home, end, left, right
          (e.keyCode >= 35 && e.keyCode <= 39)
        ) {
          // let it happen, don't do anything
          return;
        }

        e.preventDefault();
      }
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
      return '';
    },
    /**
     * get min value of model
     */
    minLen() {
      if (this.model && this.model.$params && this.model.$params.minLength) {
        return this.model.$params.minLength.min;
      }
      return 0;
    },
    /**
     * get jmax value of model
     */
    maxLen() {
      if (this.model && this.model.$params && this.model.$params.maxLength) {
        return this.model.$params.maxLength.max;
      }
      return 0;
    },
    /**
     * check if onlyPersianChar validator assign to model
     */
    isOnlyPersianChar() {
      return (
        this.model.onlyPersianChar !== undefined && !this.model.onlyPersianChar
      );
    },
    /**
     * check if notPersianChar validator assign to model
     */
    isNotPersianChar() {
      return (
        this.model.notPersianChar !== undefined && !this.model.notPersianChar
      );
    },
    /**
     * check if numeric validator assign to model
     */
    isNumeric() {
      return this.model.numeric !== undefined && !this.model.numeric;
    }
  },
  /**
   * created
   */
  created() {
    // set input maxlen
    if (this.maxlength === undefined && this.model.maxLength !== undefined) {
      this.inputMaxLen = this.maxLen;
    } else if (this.maxlength !== undefined) {
      this.inputMaxLen = this.maxlength;
    }
  }
};
</script>

<style>
</style>
