import axios from "axios";

const instance = axios.create()

// Add a request interceptor
instance.interceptors.request.use(
  function (config) {
    config.baseURL = `https://localhost:7140/`;
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

export default {
  get: instance.get,
  post: instance.post,
  put: instance.put,
  delete: instance.delete,
  patch: instance.patch,
};