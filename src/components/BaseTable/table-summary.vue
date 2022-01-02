<script>
export default {
  props: {
    totalRows: {
      type: Number,
      required: true
    },
    filterRows: {
      type: Number,
      required: true
    },
    rowTo: {
      type: Number,
      required: true
    },
    rowFrom: {
      type: Number,
      required: true
    }
  },
  render(h) {
    let self = this;

    let summaryText = '';
    if (self.filterRows == 0) {
      summaryText = `تهی (فیلتر شده از مجموع ${self.totalRows})`;
    } else if (self.filterRows != self.totalRows) {
      summaryText = `نمایش ${self.rowFrom} تا ${self.rowTo} از مجموع ${
        self.filterRows
      } (فیلتر شده از مجموع ${self.totalRows})`;
    } else {
      summaryText = `نمایش ${self.rowFrom} تا ${self.rowTo} از مجموع ${
        self.filterRows
      }`;
    }

    let summaryRender = h('label', {
      attrs: {
        style: 'float:left'
      },
      domProps: {
        innerHTML: summaryText
      }
    });

    return h(
      'section',
      {
        attrs: {
          class: 'col-sm-5'
        }
      },
      [summaryRender]
    );
  }
};
</script>
