import React, { useEffect, useState } from 'react';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import DataTable from './components/DataTable';
import './App.css';
import { ApiResponse, EmployeeWorkTime, WorkTime } from './api/models/ApiResponse';
import axios from 'axios';
import { getEmployeeAttendance } from './api/employeeApi';


// const dataEx: EmployeeWorkTime[] = [
//   { employeeCode: 'NV001', employeeName: 'Nhân viên 1', outletCode: 'CH0001', outletName: 'Cửa hàng 1', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV002', employeeName: 'Nhân viên 2', outletCode: 'CH0002', outletName: 'Cửa hàng 2', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV003', employeeName: 'Nhân viên 3', outletCode: 'CH0003', outletName: 'Cửa hàng 3', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV004', employeeName: 'Nhân viên 4', outletCode: 'CH0004', outletName: 'Cửa hàng 4', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV005', employeeName: 'Nhân viên 5', outletCode: 'CH0005', outletName: 'Cửa hàng 5', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV006', employeeName: 'Nhân viên 6', outletCode: 'CH0006', outletName: 'Cửa hàng 6', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV007', employeeName: 'Nhân viên 7', outletCode: 'CH0007', outletName: 'Cửa hàng 7', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV008', employeeName: 'Nhân viên 8', outletCode: 'CH0008', outletName: 'Cửa hàng 8', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] },
//   { employeeCode: 'NV009', employeeName: 'Nhân viên 9', outletCode: 'CH0009', outletName: 'Cửa hàng 9', workTime: [{ date: '2023-10-01', time: 8 }, { date: '2023-10-02', time: 8 }] }
// ];
const App: React.FC = () => {
  const [data, setData] = useState<EmployeeWorkTime[]>([]);
  const [error, setError] = useState<string | null>(null);
  
  useEffect(() => {
    const fetchData = async (): Promise<ApiResponse> => {
      try {
        const request = { year: 2023, month: 10 };
        // const response = await axios.post<EmployeeWorkTime[], any>('https://localhost:5001/api/Employee/attendance', request);
        const response = await getEmployeeAttendance(request);
        console.log('response', response.responseData);
        setData(response.responseData);
        return response;
      } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
      }
    };
    fetchData();
  }, []);
  const exportToExcel = () => {
    // Chuyển đổi dữ liệu thành cấu trúc phẳng
    const flatData = data.map(item => {
      const flatItem: any = {
        EmployeeCode: item.employeeCode,
        EmployeeName: item.employeeName,
        OutletCode: item.outletCode,
        OutletName: item.outletName,
      };
      item.workTime.forEach(workTime => {
        flatItem[workTime.date] = workTime.time;
      });
      return flatItem;
    });

    // Tạo worksheet và workbook
    const worksheet = XLSX.utils.json_to_sheet(flatData);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');

    // Xuất file Excel
    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const dataBlob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(dataBlob, 'data.xlsx');
  };

  return (
    <div>
      <h1>Employee Attendance</h1>
      <button onClick={exportToExcel}>Export to Excel</button>
      <DataTable data={data} />
    </div>
  );
};

export default App;
