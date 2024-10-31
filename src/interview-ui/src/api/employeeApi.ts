import axiosInstance from './axiosConfig';
import { ApiResponse } from './models/ApiResponse';

interface AttendanceRequest {
  year: number;
  month: number;
}

export const getEmployeeAttendance = async (request: AttendanceRequest): Promise<ApiResponse> => {
    try {
        const response = await axiosInstance.post('/api/Employee/attendance', request);
        return response.data;
    } catch (error) {
        console.error('Error fetching data', error);
        throw error;
    }
};