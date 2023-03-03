import axios from "axios";

// Add a request interceptor
axios.interceptors.request.use(
  function (config) {
    config.baseURL = `https://localhost:7140/`;
    config.headers["Content-Type"] = "application/pdf";
    config.responseType = "blob";
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

export default {
  get: axios.get,
  post: axios.post,
  put: axios.put,
  delete: axios.delete,
  patch: axios.patch,
};