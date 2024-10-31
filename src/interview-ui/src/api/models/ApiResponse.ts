export interface ApiResponse {
    statusCode: string;
    message: string;
    responseData: EmployeeWorkTime[];
}

interface Data {
    employeeWorkTimes: EmployeeWorkTime[];
}

export interface WorkTime {
    date: string;
    time: number;
}

export interface EmployeeWorkTime {
    employeeCode: string;
    employeeName: string;
    outletCode: string;
    outletName: string;
    workTime: WorkTime[];
}