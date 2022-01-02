import axios from "axios";

const apiKey = '7ef4a8b8370f25f7702f8cbce6825e7b';

const theMovieInstance = axios.create({
    baseURL: `https://api.themoviedb.org/3`
});

theMovieInstance.interceptors.request.use(config => {
    config.url += `&api_key=${apiKey}`;
    console.log(config.url);
    return config;
});

export default theMovieInstance;