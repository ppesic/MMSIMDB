import axios from "axios";

class BasicService {
    constructor(url) {
      this.baseURL = 'https://localhost:7192';
      const options = {
        baseURL: this.baseURL
      };
      
      this.fetcher = axios.create(options);
  
      this.fetcher.interceptors.request.use(
        config => {
          const { origin } = new URL(this.baseURL);
          const allowedOrigins = [this.baseURL];
          const token = localStorage.getItem('token');
          if (allowedOrigins.includes(origin)) {
            config.headers.authorization = `Bearer ${token}`;
          }
          return config;
        },
        error => {
          return Promise.reject(error);
        }
      );

      this.fetcher.interceptors.response.use(
        config => {
          return config;
        },
        error => {
          console.log(JSON.stringify(error));
          if(error.message.indexOf('status code 401') != -1){
            localStorage.clear();
          }
          return Promise.reject(error);
        }
      );
  }
}

const baseService = new BasicService();
export default baseService.fetcher;