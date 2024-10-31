import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'https://localhost:5001', // Thay thế bằng URL API của bạn
    headers: {
        'Content-Type': 'application/json'
    },
});

export default axiosInstance;