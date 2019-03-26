<template>
  <q-field count :helper="helper" :error-label="errorLabel()" :error="model.$error">
    <label class="lbl">{{displayName}}</label>
    <br>
    <div class="gutter-sm">
      <slot :obj="model"></slot>
    </div>
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
    helper: String
  },
  /**
   * data
   */
  data() {
    return {};
  },
  methods: {
    errorLabel() {
      if (!this.model.$dirty) {
        return "";
      }
      if (this.model.required !== undefined && !this.model.required) {
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
    }
  }
};
</script>

<style scoped>
.lbl {
  color: #bdbdbd;
  position: relative;
  top: -7px;
  font-size: bold;
}
</style>
