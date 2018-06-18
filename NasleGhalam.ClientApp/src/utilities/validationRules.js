import {
  required,
  minLength,
  maxLength,
  minValue
} from "vuelidate/lib/validators";

const regionValidation = {
  regionObj: {
    Name: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    }
  }
};
const areaValidation = {
  areaObj: {
    Name: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    }
  }
};
const sensorValidation = {
  instanceObj: {
    AreaId: {
      minValue: minValue(1)
    },
    Code: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    },
    RegionId: {
      minValue: minValue(1)
    },
    SensorGroupId: {
      minValue: minValue(1)
    }
  }
};
const roleValidation = {
  instanceObj: {
    Name: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    },
    Level: {
      minValue: minValue(1)
    }
  }
};
const userValidation = {
  instanceObj: {
    Name: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    },
    Family: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    },
    Username: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    },
    Password: {
      required,
      minLength: minLength(2),
      maxLength: maxLength(30)
    },
    RoleId: {
      minValue: minValue(1)
    }
  }
};
export {
  regionValidation,
  areaValidation,
  sensorValidation,
  roleValidation,
  userValidation
};
