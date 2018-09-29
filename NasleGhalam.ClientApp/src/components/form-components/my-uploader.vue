<template>
  <div class="col-sm-6">
    <div v-if="needValidation">
      <!-- <form-group :validator="model.Img"> -->
      <span v-if="hasSpan"
            class="label">{{label}}</span>
      <q-uploader url=""
                  :float-label='FlLabel'
                  :ref='myRef'
                  hide-upload-button
                  @add="emitAdd"
                  @remove:cancel="$emit('remove')"
                  :extensions='extensions' />
      <!-- </form-group> -->
    </div>
    <div v-else>
      <!-- <form-group> -->
      <span v-if="hasSpan"
            class="label">{{label}}</span>
      <q-uploader url=""
                  :float-label='FlLabel'
                  :ref='myRef'
                  hide-upload-button
                  @add="emitAdd"
                  @remove:cancel="$emit('remove')"
                  :extensions='extensions' />
      <!-- </form-group> -->
    </div>
  </div>
</template>
<script>
export default {
  props: {
    model: {},
    prop: { default: 'Img' },
    needValidation: { default: true },
    label: { default: 'تصویر' },
    myRef: '',
    hasSpan: { default: false },
    extensions: { default: '.gif,.jpg,.jpeg,.png,.bmp,.icn' }
  },
  methods: {
    emitAdd: function() {
      this.$emit('add', this);
    }
  },
  computed: {
    FlLabel() {
      return this.hasSpan ? '' : this.label;
    }
  }
};
</script>