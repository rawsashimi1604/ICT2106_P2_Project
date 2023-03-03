import HttpService from "requests/HttpService"

const GET_DEVICE_REPORT_API_ROUTE = `https://localhost:7140/api/analysis/device/download`

function getDeviceReport(){
    return HttpService.get(GET_DEVICE_REPORT_API_ROUTE);
}

export default{
    getDeviceReport,
}