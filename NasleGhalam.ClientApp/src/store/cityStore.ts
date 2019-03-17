import ICity from '../models/ICity';
import util from '../utilities/util';
import axios from '../utilities/axios';
import { CITY_URL as baseUrl } from '../utilities/site-config';

import {
  State as vState,
  Getter as vGetter,
  Mutation as vMutation,
  Action as vAction,
  namespace
} from 'vuex-class'

const store = {
  namespaced: true,
  state: {
    modelName: 'شهر',
    isOpenModalCreate: false,
    isOpenModalEdit: false,
    isOpenModalDelete: false,
    cityObj: {
      Id: 0,
      Name: '',
      ProvinceId: 0,
      ProvinceName: ''
    },
    cityGridData: [],
    // cityDdl: [],
    cityByProvinceDdl: [],
    selectedId: 0,
    ddlModelChanged: true,
    gridModelChanged: true,
    createVue: null,
    editVue: null
  }
}