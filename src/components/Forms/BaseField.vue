<template>
  <q-field :count="count" :helper="helper" :error-label="errorLabel()" :error="model.$error">
    <section class="s-q-field-border">
      <label class="s-q-field-label">{{displayName}}</label>
      <div class="gutter-sm s-q-field-body">
        <slot :obj="model"></slot>
      </div>
    </section>
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
    count: Boolean,
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

<style>
.s-q-field-border {
  border: 1px solid #cecece;
  border-radius: 2px;
  padding: 3px;
  margin: 0 1px;
  position: relative;
  height: 43px;
}

.s-q-field-border .s-q-field-label {
  color: #bdbdbd;
  font-size: 11px;
  position: absolute;
  top: 0;
}

.s-q-field-border .s-q-field-body {
  position: absolute;
  top: 18px;
  left: 16px;
  width: 100%;
}
</style>
