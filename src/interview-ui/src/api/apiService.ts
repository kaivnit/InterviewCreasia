import axiosInstance from './axiosConfig';

export const getData = async () => {
    try {
        const response = await axiosInstance.get('/user/users');
        return response.data;
    } catch (error) {
        console.error('Error fetching data', error);
        throw error;
    }
};