// import something here
import Vuelidate from 'vuelidate'

import {
  required,
  maxLength,
  minLength,
  numeric,
  helpers
} from 'vuelidate/lib/validators'

// leave the export, even if you don't use it
export default ({ app, router, Vue }) => {
  // something to do
  Vue.use(Vuelidate)
}

// add displayName validator for only name
const displayName = param =>
  helpers.withParams(
    {
      type: 'displayName',
      value: param
    },
    value => true
  )

// add requried validator for ddl
const requiredDdl = invalidValue =>
  helpers.withParams(
    {
      type: 'requiredDdl',
      value: invalidValue
    },
    value => {
      if (value === undefined || value === null) return false

      return value != invalidValue
    }
  )

const onlyPersianChar = () =>
  helpers.withParams(
    {
      type: 'onlyPersianChar'
    },
    value => !helpers.req(value) || /^[\u0600-\u06FF\s]+$/.test(value)
  )

const notPersianChar = () =>
  helpers.withParams(
    {
      type: 'notPersianChar'
    },
    value => !helpers.req(value) || /[^=^[\u0600-\u06FF\s]+$]*/.test(value)
  )

// export
export {
  displayName,
  required,
  maxLength,
  minLength,
  numeric,
  minValue,
  maxValue,
  between,
  sameAs,
  requiredDdl,
  onlyPersianChar,
  notPersianChar
}
