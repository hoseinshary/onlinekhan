import axios from 'utilities/axios';
import { GRADE_URL } from 'utilities/site-config';

export default {
  getAll() {
    return axios.get(`${GRADE_URL}/GetAll`);
  },
  getById(id) {
    return axios.get(`${GRADE_URL}/GetById/` + id);
  },
  create(instanceObj) {
    return axios.post(`${GRADE_URL}/Create`, instanceObj);
  },
  update(instanceObj) {
    return axios.post(`${GRADE_URL}/Update`, instanceObj);
  },
  delete(id) {
    return axios.post(`${GRADE_URL}/Delete/` + id);
  }
};
