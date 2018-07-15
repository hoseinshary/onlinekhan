import axios from 'utilities/axios';
import { GRADE_URL } from 'utilities/site-config';

export default {
  getAll() {
    return axios.get(`${GRADE_URL}/GetAll`);
  },
  getAllRole() {
    return axios.get(`${GRADE_URL}/GetAllDdl`);
  },
  getById(Id) {
    return axios.get(`${GRADE_URL}/GetById/` + Id);
  },
  create(instanceObj) {
    return axios.post(`${GRADE_URL}/Create`, instanceObj);
  },
  update(instanceObj) {
    return axios.post(`${GRADE_URL}/Update`, instanceObj);
  },
  delete(Id) {
    return axios.post(`${GRADE_URL}/Delete/` + Id);
  }
};
