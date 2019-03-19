
import Vue from 'Vue';
import ICity from '../models/ICity';
import util from '../utilities/util';
import { CrudType } from '../utilities/eumerations';
import axios from '../utilities/axios';
import { CITY_URL as baseUrl } from '../utilities/site-config';
import { VuexModule, mutation, action, getter, Module }
  from "vuex-class-component";


@Module({ namespacedPath: "city/" })
export class CityStore extends VuexModule {

  city: ICity;
  cityList: Array<ICity>;
  private openModalCreate: boolean = false;
  private openModalEdit: boolean = false;
  private openModalDelete: boolean = false;
  private selectedId: number = 0;
  private modelChanged: boolean = true;
  private createVue: Vue;
  private editVue: Vue;

  constructor() {
    super();

    this.city = {
      Id: 0,
      Name: '',
      ProvinceId: 0,
      ProvinceName: ''
    }
    this.cityList = [];
  }

  //### getters ###
  @getter modelName = "شهر";
  @getter recordName = this.city && this.city.Name || '';

  getIndexById(id: number) {
    return this.cityList.findIndex(x => x.Id == id);
  }

  findById(id: number) {
    return this.cityList.find(x => x.Id == id);
  }
  //--------------------------------------------------


  //### mutations ###
  @mutation
  private CREATE(city: ICity) {
    this.cityList.push(city);
  }

  @mutation
  UPDATE(city: ICity) {
    let index = this.getIndexById(city.Id);
    if (index < 0) return;

    this.cityList[index].Id = city.Id;
    this.cityList[index].Name = city.Name;
    this.cityList[index].ProvinceId = city.ProvinceId;
    this.cityList[index].ProvinceName = city.ProvinceName;
  }

  @mutation
  private DELETE(id: number) {
    let index = this.getIndexById(id);
    if (index < 0) return;

    this.cityList.splice(index, 1);
  }

  @mutation
  private RESET() {
    this.city.Id = 0;
    this.city.Name = '';
    this.city.ProvinceId = 0;
    this.city.ProvinceName = '';
  }
  //--------------------------------------------------


}



const store = {
  mutations: {
    [CrudType.Create](state) {

    },
    CREATE(state) {

    }
  },
  actions: {
    submitCreate({ state, commit, dispatch }) {
      axios.post(`${baseUrl}/Create`, state.cityObj).then(response => {
        let data = response.data;

        if (data.MessageType == 1) {
          commit(CrudType.Create);
        }

      });
    }
  }
}