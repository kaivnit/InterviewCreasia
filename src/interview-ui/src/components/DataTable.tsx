import React from 'react';
import { EmployeeWorkTime } from '../api/models/ApiResponse';

interface DataItem {
  EmployeeCode: string;
  EmployeeName: string;
  OutletCode: string;
  OutletName: string;
  WorkTime: WorkTime[];
}

interface WorkTime {
  date: string;
  time: number;
}

const DataTable: React.FC<{ data: EmployeeWorkTime[] }> = ({ data }) => {
  console.log('table', data);
  // Kiểm tra dữ liệu đầu vào
  if (!data || data.length === 0) {
    return <div>No data available</div>;
  }
  // Lấy tất cả các ngày làm việc từ dữ liệu
  const allDates = Array.from(
    new Set(data.flatMap((employee) => employee.workTime.map((wt) => wt.date)))
  );

  return (
    <table>
      <thead>
        <tr>
          <th>Mã nhân viên</th>
          <th>Tên nhân viên</th>
          <th>Mã cửa hàng</th>
          <th>Tên cửa hàng</th>
          {allDates.map((date) => (
            <th key={date}>{date}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {data.map((employee) => (
          <tr key={employee.employeeCode}>
            <td>{employee.employeeCode}</td>
            <td>{employee.employeeName}</td>
            <td>{employee.outletCode}</td>
            <td>{employee.outletName}</td>
            {allDates.map((date) => {
              const workTime = employee.workTime.find((wt) => wt.date === date);
              return <td key={date}>{workTime ? workTime.time : '-'}</td>;
            })}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default DataTable;